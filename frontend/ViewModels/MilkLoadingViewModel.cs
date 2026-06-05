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
    public class MilkLoadingViewModel : INotifyPropertyChanged
    {
        private readonly IMilkLoadingService _service;
        private readonly IMilkBuyerService _milkBuyerService;

        public MilkLoadingViewModel(IMilkLoadingService service, IMilkBuyerService milkBuyerService)
        {
            _service = service;
            _milkBuyerService = milkBuyerService;
            FormModel = new MilkLoading
            {
                Date = DateTime.Now
            };

            OpenAddCommand = new RelayCommand<object>(async m => OpenAdd());
            OpenEditCommand = new RelayCommand<MilkLoading>(async m => OpenEdit(m));
            SaveCommand = new RelayCommand<object>(async _ => await Save());
            DeleteCommand = new RelayCommand<MilkLoading>(async m => await Delete(m));
        }

        public ObservableCollection<MilkLoading> MilkLoadings { get; }
            = new ObservableCollection<MilkLoading>();
        public ObservableCollection<MilkBuyer> MilkBuyers { get; }
            = new ObservableCollection<MilkBuyer>();

        private MilkLoading formModel;
        public MilkLoading FormModel
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
            await LoadMilkLoadings();
            await LoadMilkBuyers();
        }

        public async Task LoadMilkLoadings()
        {
            var data = await _service.GetMilkLoadings();

            MilkLoadings.Clear();
            foreach (var item in data)
                MilkLoadings.Add(item);
        }

        public async Task LoadMilkBuyers()
        {
            var data = await _milkBuyerService.GetMilkBuyers();
            MilkBuyers.Clear();
            foreach (var item in data)
                MilkBuyers.Add(item);
        }


        private void OpenAdd()
        {
            FormModel = new MilkLoading
            {
                Date = DateTime.Now
            };

            IsEditMode = false;
            IsPaneOpen = true;
        }

        private void OpenEdit(MilkLoading model)
        {
            if (model == null)
                return;

            FormModel = model;

            IsEditMode = true;
            IsPaneOpen = true;
        }

        private async Task Save()
        {
            if (string.IsNullOrWhiteSpace(FormModel.ReceiverName))
            {
                await ShowMessageDialogAsync("Validation Error!", "Receiver name cannot be empty.");
                return;
            } else if (string.IsNullOrWhiteSpace(FormModel.ReceiverName))
            {
                await ShowMessageDialogAsync("Validation Error!", "Receiver name cannot be empty.");
                return;
            }
            else if (string.IsNullOrWhiteSpace(FormModel.VehicleNumber))
            {
                await ShowMessageDialogAsync("Validation Error!", "Vehicle number cannot be empty.");
                return;
            }
            else if (FormModel.MilkBuyerId == 0)
            {
                await ShowMessageDialogAsync("Validation Error!", "Please select a milk buyer.");
                return;
            }
             else if (FormModel.LitresLoaded <= 0)
            {
                await ShowMessageDialogAsync("Validation Error!", "Litres loaded must be greater than zero.");
                return;
            }
             else if (FormModel.PricePerLitre <= 0)
            {
                await ShowMessageDialogAsync("Validation Error!", "Price per litre must be greater than zero.");
                return;
            }
            else if (FormModel.CollectedLitres <= 0)
            {
                await ShowMessageDialogAsync("Validation Error!", " Collected Litres must be greater than zero.");
                return;
            }

            if (IsEditMode)
            {
                await _service.UpdateMilkLoading(FormModel);
            }
            else
            {
                await _service.AddMilkLoading(FormModel);
            }

            await LoadMilkLoadings();
            IsPaneOpen = false;
        }

        private async Task Delete(MilkLoading model)
        {
            if (model == null)
                return;

            await _service.Delete(model.Id);
            MilkLoadings.Remove(model);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
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