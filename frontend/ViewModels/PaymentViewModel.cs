using Core.Models;
using Core.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Frontend.ViewModels
{
    public class PaymentViewModel : INotifyPropertyChanged
    {
        private readonly IPaymentService _service;

        public ObservableCollection<Payment> Payments { get; set; }
            = new ObservableCollection<Payment>();

        public long FarmerId { get; set; }
        public long CollectionPeriodId { get; set; }
        public decimal TotalLitres { get; set; }
        public decimal RatePerLitre { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal TotalDeductions { get; set; }
        public decimal NetAmount { get; set; }
        public DateTime PaymentDate { get; set; }

        public ICommand UpdatePaymentCommand { get; }

        private Payment selectedPayment;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Payment SelectedPayment
        {
            get => selectedPayment;
            set
            {
                selectedPayment = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(SelectedPayment)));
            }
        }

        public PaymentViewModel(IPaymentService service)
        {
            _service = service;
        }

        public async Task LoadPayments()
        {
            var Payments = await _service.GetPayments();

            Payments.Clear();
            foreach (var f in Payments)
                Payments.Add(f);
        }

        public async Task AddPayment()
        {
            await _service.AddPayment(new Payment
            {
                FarmerId = FarmerId,
                CollectionPeriodId = CollectionPeriodId,
                TotalDeductions = TotalDeductions,
                TotalLitres = TotalLitres,
                RatePerLitre = RatePerLitre,
                GrossAmount = GrossAmount,
                NetAmount = NetAmount,
                PaymentDate = PaymentDate,
            });

            await LoadPayments();
        }

        public async Task UpdatePayment()
        {
            await _service.UpdatePayment(SelectedPayment);
            await LoadPayments();
        }

        public async Task LoadPayment(long id)
        {
            SelectedPayment = await _service.GetPaymentById(id);
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}
