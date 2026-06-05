using Core.Models;
using Core.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Frontend.ViewModels
{
    public class MilkBuyerViewModel : INotifyPropertyChanged
    {
        private readonly IMilkBuyerService _service;

        public MilkBuyerViewModel(IMilkBuyerService service)
        {
            _service = service;

            FormModel = new MilkBuyer();

            OpenAddCommand = new RelayCommand<object>(async m =>  OpenAdd());

            OpenEditCommand = new RelayCommand<MilkBuyer>(async m => OpenEdit(m));

            SaveCommand = new RelayCommand<object>(async _ => await Save());

            DeleteCommand = new RelayCommand<MilkBuyer>(async m => await Delete(m));
        }

        public ObservableCollection<MilkBuyer> MilkBuyers { get; }
            = new ObservableCollection<MilkBuyer>();

        private MilkBuyer formModel;
        public MilkBuyer FormModel
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
            await LoadMilkBuyers();
        }

        public async Task LoadMilkBuyers()
        {
            var buyers = await _service.GetMilkBuyers();

            MilkBuyers.Clear();

            foreach (var buyer in buyers)
                MilkBuyers.Add(buyer);
        }

        private void OpenAdd()
        {
            FormModel = new MilkBuyer();

            IsEditMode = false;
            IsPaneOpen = true;
        }

        private void OpenEdit(MilkBuyer model)
        {
            if (model == null)
                return;

            FormModel = model;

            IsEditMode = true;
            IsPaneOpen = true;
        }

        private async Task Save()
        {
            if (IsEditMode)
            {
                await _service.UpdateMilkBuyer(FormModel);
            }
            else
            {
                await _service.AddMilkBuyer(FormModel);
            }

            await LoadMilkBuyers();

            IsPaneOpen = false;
        }

        private async Task Delete(MilkBuyer model)
        {
            if (model == null)
                return;

            await _service.Delete(model.Id);

            MilkBuyers.Remove(model);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}
