using Core.Models;
using Core.Services;
using Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace Frontend.ViewModels
{
    public class ExpenseViewModel : INotifyPropertyChanged
    {
        private readonly IExpenseService _service;
        private readonly ICollectionPeriodService _collectionPeriodService;

        public ExpenseViewModel(IExpenseService service, ICollectionPeriodService collectionPeriodService)
        {
            _service = service;
            _collectionPeriodService = collectionPeriodService;
            FormModel = new Expense();

            OpenAddCommand = new RelayCommand<object>(async m => OpenAdd());
            OpenEditCommand = new RelayCommand<Expense>(async m => OpenEdit(m));
            SaveCommand = new RelayCommand<object>(async _ => await Save());
            DeleteCommand = new RelayCommand<Expense>(async e => await Delete(e));
        }

        public ObservableCollection<Expense> Expenses { get; }
            = new ObservableCollection<Expense>();

        private Expense formModel;
        public Expense FormModel
        {
            get => formModel;
            set
            {
                formModel = value;
                OnPropertyChanged(nameof(FormModel));
            }
        }

        private bool isPaneOpen;
        public bool IsPaneOpen
        {
            get => isPaneOpen;
            set
            {
                isPaneOpen = value;
                OnPropertyChanged(nameof(IsPaneOpen));
            }
        }

        private bool isEditMode;
        public bool IsEditMode
        {
            get => isEditMode;
            set
            {
                isEditMode = value;
                OnPropertyChanged(nameof(IsEditMode));
            }
        }

        public ICommand OpenAddCommand { get; }
        public ICommand OpenEditCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }

        public async Task Initialize()
        {
            await LoadExpenses();
        }

        public async Task LoadExpenses()
        {
            var expenses = await _service.GetExpenses();

            Expenses.Clear();

            foreach (var expense in expenses)
                Expenses.Add(expense);
        }

        private void OpenAdd()
        {
            var period = _collectionPeriodService.GetCurrentPeriod(DateTime.Now);

            if (period == null)
            {
                throw new Exception("No active collection period found.");
            }

            FormModel = new Expense
            {
                Date = System.DateTime.Now,
                CollectionPeriodId = period.Id
            };

            IsEditMode = false;
            IsPaneOpen = true;
        }

        private void OpenEdit(Expense expense)
        {
            if (expense == null)
                return;

            FormModel = expense;

            IsEditMode = true;
            IsPaneOpen = true;
        }

        private async Task Save()
        {
            var result = await _collectionPeriodService.EnsurePeriodIsOpen(FormModel.CollectionPeriodId);

            if (!result.Success)
            {
                await ShowDialog(result.Message);
                return;
            }

            if (IsEditMode)
            {
                await _service.UpdateExpense(FormModel);
            }
            else
            {
                await _service.AddExpense(FormModel);
            }

            await LoadExpenses();

            IsPaneOpen = false;
        }

        private async Task Delete(Expense expense)
        {
            var result = await _collectionPeriodService.EnsurePeriodIsOpen(FormModel.CollectionPeriodId);

            if (!result.Success)
            {
                await ShowDialog(result.Message);
                return;
            }
            if (expense == null)
                return;

            await _service.Delete(expense.Id);

            Expenses.Remove(expense);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }

        private async Task ShowDialog(string message)
        {
            var dialog = new ContentDialog
            {
                Title = "Warning",
                Content = message,
                CloseButtonText = "OK"
            };

            await dialog.ShowAsync();
        }
    }
}