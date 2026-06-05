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
    public class FarmerViewModel : INotifyPropertyChanged
    {
        private readonly IFarmerService _farmerService;
        private readonly ICollectionCenterService _centerService;

        public FarmerViewModel(IFarmerService farmerService,
                                ICollectionCenterService centerService)
        {
            _farmerService = farmerService;
            _centerService = centerService;

            FormModel = new Farmer();

            OpenAddCommand = new RelayCommand<Farmer>(async f => OpenAdd());
            OpenEditCommand = new RelayCommand<Farmer>(async f => OpenEdit(f));
            SaveCommand = new RelayCommand<Farmer>(async f => await Save());
            DeleteCommand = new RelayCommand<Farmer>(async f => await Delete(f));
        }

        public ObservableCollection<Farmer> Farmers { get; set; }
            = new ObservableCollection<Farmer>();

        public ObservableCollection<CollectionCenter> CollectionCenters { get; set; }
            = new ObservableCollection<CollectionCenter>();

        private Farmer formModel;
        public Farmer FormModel
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
        
        public async Task LoadFarmers()
        {
            var farmers = await _farmerService.GetFarmers();

            Farmers.Clear();
            foreach (var f in farmers)
                Farmers.Add(f);
        }

        public async Task LoadCollectionCenters()
        {
            var centers = await _centerService.GetCollectionCenters();

            CollectionCenters.Clear();
            foreach (var c in centers)
                CollectionCenters.Add(c);
        }

        public async Task Initialize()
        {
            await LoadCollectionCenters();
            await LoadFarmers();
        }

        private void OpenAdd()
        {
            FormModel = new Farmer();
            IsEditMode = false;
            IsPaneOpen = true;
        }

        private void OpenEdit(Farmer farmer)
        {
            FormModel = farmer;
            IsEditMode = true;
            IsPaneOpen = true;
        }

        private async Task Save()
        {
            if(FormModel.PrimaryPhone.Length == 10)
            {
                await ShowMessageDialogAsync("Validation Error", "Primary phone number cannot exceed 10 digits.");
                return;
            } else if (FormModel.PrimaryPhone.Equals(""))
            {
                await ShowMessageDialogAsync("Validation Error", "Primary phone number cannot be empty!");
                return;
            }
            if (IsEditMode)
            {
                await _farmerService.UpdateFarmer(FormModel);
            }
            else
            {
                await _farmerService.AddFarmer(FormModel);
            }

            await LoadFarmers();
            IsPaneOpen = false;
        }

        private async Task Delete(Farmer farmer)
        {
            await _farmerService.Delete(farmer.Id);
            Farmers.Remove(farmer);
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
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