using Core.Enums;
using Core.Models;
using Core.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace Frontend.ViewModels
{
    public class PaymentViewModel : INotifyPropertyChanged
    {
        private readonly IPaymentService _service;
        private readonly IFarmerService _farmerService;
        private readonly ICollectionPeriodService _collectionPeriodService;

        public PaymentViewModel(
            IPaymentService service,
            IFarmerService farmerService,
            ICollectionPeriodService collectionPeriodService)
        {
            _service = service;
            _farmerService = farmerService;
            _collectionPeriodService = collectionPeriodService;

            FormModel = new Payment
            {
                PaymentDate = DateTime.Now,
                TotalDeductions = 0
            };

            OpenAddCommand = new RelayCommand<object>(async m => OpenAdd());
            OpenEditCommand = new RelayCommand<Payment>(async p => OpenEdit(p));
            SaveCommand = new RelayCommand<object>(async _ => await Save());
            DeleteCommand = new RelayCommand<Payment>(async p => await Delete(p));
        }

        public ObservableCollection<Payment> Payments { get; } = new();
        public ObservableCollection<Farmer> Farmers { get; } = new();
        public ObservableCollection<CollectionPeriod> CollectionPeriods { get; } = new();

        private Payment formModel;
        public Payment FormModel
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
            await LoadPayments();
            await LoadFarmers();
        }

        public async Task LoadPayments()
        {
            var data = await _service.GetPayments();

            Payments.Clear();
            foreach (var item in data)
                Payments.Add(item);
        }

        public async Task LoadFarmers()
        {
            var data = await _farmerService.GetFarmers();

            Farmers.Clear();
            foreach (var item in data)
                Farmers.Add(item);
        }

        private void OpenAdd()
        {
            var now = DateTime.Now;
            var period = _collectionPeriodService.GetCurrentPeriod(now);

            if (period == null)
                throw new Exception("No active collection period found.");

            FormModel = new Payment
            {
                PaymentDate = now,
                TotalDeductions = 0,
                CollectionPeriodId = period.Id,
                PaymentType = PaymentType.Partial
            };

            IsEditMode = false;
            IsPaneOpen = true;
        }

        private void OpenEdit(Payment payment)
        {
            if (payment == null)
                return;

            FormModel = new Payment
            {
                Id = payment.Id,
                FarmerId = payment.FarmerId,
                CollectionPeriodId = payment.CollectionPeriodId,
                TotalLitres = payment.TotalLitres,
                RatePerLitre = payment.RatePerLitre,
                TotalDeductions = payment.TotalDeductions,
                PaymentDate = payment.PaymentDate,
                PaymentType = payment.PaymentType,
                ReferenceNumber = payment.ReferenceNumber
            };

            IsEditMode = true;
            IsPaneOpen = true;
        }

        private async Task Save()
        {
            var result = await _collectionPeriodService.EnsurePeriodIsOpen(FormModel.CollectionPeriodId);

            if (!result.Success)
            {
                await ShowMessageDialogAsync("Validation Error", result.Message);
                return;
            }
            try
            {

                if (FormModel.FarmerId <= 0)
                    throw new Exception("Please select a farmer.");

                if (FormModel.TotalLitres <= 0)
                    throw new Exception("Litres must be greater than zero.");

                var period = _collectionPeriodService.GetCurrentPeriod(DateTime.Now);
                if (period == null)
                    throw new Exception("No active collection period found.");

                var actualDeliveredLitres = _service.GetTotalLitresDelivered(FormModel.FarmerId, period.Id);

                if (FormModel.TotalLitres > await actualDeliveredLitres)
                {
                    await ShowMessageDialogAsync("Validation Failed", $"You are trying to pay for {FormModel.TotalLitres} L, " +
                                        $"but this farmer only brought in {await actualDeliveredLitres} L during this 15-day period.");
                    return;
                }


                if (IsEditMode)
                    await _service.UpdatePayment(FormModel);
                else
                    await _service.AddPayment(FormModel);

                await LoadPayments();
                IsPaneOpen = false;
            }
            catch (Exception ex)
            {

                await ShowMessageDialogAsync("Validation / Saving Error", ex.Message);
            }
        }

        private async Task Delete(Payment payment)
        {
            var result = await _collectionPeriodService.EnsurePeriodIsOpen(FormModel.CollectionPeriodId);

            if (!result.Success)
            {
                await ShowMessageDialogAsync("Validation Error", result.Message);
                return;
            }
            if (payment == null)
                return;

            await _service.Delete(payment.Id);
            Payments.Remove(payment);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private async Task ShowMessageDialogAsync(string title, string content)
        {
            ContentDialog errorDialog = new ContentDialog
            {
                Title = title,
                Content = content,
                CloseButtonText = "OK",
                DefaultButton = ContentDialogButton.Close
            };

            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.Xaml.Controls.ContentDialog"))
            {
                await errorDialog.ShowAsync();
            }
        }
    }
}