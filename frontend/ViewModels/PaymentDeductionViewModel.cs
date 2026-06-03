using Core.Models;
using Core.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Frontend.ViewModels
{
    public class PaymentDeductionViewModel : INotifyPropertyChanged
    {
        private readonly IPaymentDeductionService _service;

        public ObservableCollection<PaymentDeduction> PaymentDeductions { get; set; }
            = new ObservableCollection<PaymentDeduction>();
        public int PaymentId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }

        public ICommand UpdatePaymentDeductionCommand { get; }

        private PaymentDeduction selectedPaymentDeduction;

        public event PropertyChangedEventHandler? PropertyChanged;

        public PaymentDeduction SelectedPaymentDeduction
        {
            get => selectedPaymentDeduction;
            set
            {
                selectedPaymentDeduction = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(SelectedPaymentDeduction)));
            }
        }

        public PaymentDeductionViewModel(IPaymentDeductionService service)
        {
            _service = service;
        }

        public async Task LoadPaymentDeductions()
        {
            var PaymentDeductions = await _service.GetPaymentDeductions();

            PaymentDeductions.Clear();
            foreach (var f in PaymentDeductions)
                PaymentDeductions.Add(f);
        }

        public async Task AddPaymentDeduction()
        {
            await _service.AddPaymentDeduction(new PaymentDeduction
            {
                PaymentId = PaymentId,
                Description = Description,
                Amount = Amount,
            });

            await LoadPaymentDeductions();
        }

        public async Task UpdatePaymentDeduction()
        {
            await _service.UpdatePaymentDeduction(SelectedPaymentDeduction);
            await LoadPaymentDeductions();
        }

        public async Task LoadPaymentDeduction(long id)
        {
            SelectedPaymentDeduction = await _service.GetPaymentDeductionById(id);
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}
