using Core.Models;
using Core.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Frontend.ViewModels
{
    public class ExpenseViewModel : INotifyPropertyChanged
    {
        private readonly IExpenseService _service;

        public ObservableCollection<Expense> Expenses { get; set; }
            = new ObservableCollection<Expense>();

        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Quantity { get; set; }
        public decimal Amount { get; set; }
        public ICommand UpdateExpenseCommand { get; }

        private Expense selectedExpense;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Expense SelectedExpense
        {
            get => selectedExpense;
            set
            {
                selectedExpense = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(SelectedExpense)));
            }
        }

        public ExpenseViewModel(IExpenseService service)
        {
            _service = service;
        }

        public async Task LoadExpenses()
        {
            var Expenses = await _service.GetExpenses();

            Expenses.Clear();
            foreach (var f in Expenses)
                Expenses.Add(f);
        }

        public async Task AddExpense()
        {
            await _service.AddExpense(new Expense
            {
                Date = DateTime.Now,
                Description = Description,
                Quantity = Quantity,
                Amount = Amount
            });

            await LoadExpenses();
        }

        public async Task UpdateExpense()
        {
            await _service.UpdateExpense(SelectedExpense);
            await LoadExpenses();
        }

        public async Task LoadExpense(long id)
        {
            SelectedExpense = await _service.GetExpenseById(id);
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}
