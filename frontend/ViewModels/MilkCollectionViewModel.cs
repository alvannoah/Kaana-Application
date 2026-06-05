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
    public class MilkCollectionViewModel : INotifyPropertyChanged
    {
        private readonly IMilkCollectionService _service;
        private readonly IFarmerService _farmerService;
        private readonly ICollectionCenterService _centerService;
        private readonly ICollectionPeriodService _collectionPeriodService;

        public MilkCollectionViewModel(
            IMilkCollectionService service,
            IFarmerService farmerService,
            ICollectionCenterService centerService,
            ICollectionPeriodService collectionPeriodService)
        {
            _service = service;
            _farmerService = farmerService;
            _centerService = centerService;
            _collectionPeriodService = collectionPeriodService;
            FormModel = new MilkCollection { CollectionDate = DateTime.Now };

            OpenAddCommand = new RelayCommand<MilkCollection>(async m => OpenAdd());
            OpenEditCommand = new RelayCommand<MilkCollection>(async m => OpenEdit(m));
            SaveCommand = new RelayCommand<MilkCollection>(async m => await Save());
            DeleteCommand = new RelayCommand<MilkCollection>(async m => await Delete(m));
        }

        public ObservableCollection<MilkCollection> MilkCollections { get; set; }
            = new ObservableCollection<MilkCollection>();

        public ObservableCollection<Farmer> Farmers { get; set; }
            = new ObservableCollection<Farmer>();

        public ObservableCollection<CollectionCenter> CollectionCenters { get; set; }
            = new ObservableCollection<CollectionCenter>();

        private MilkCollection formModel;
        public MilkCollection FormModel
        {
            get => formModel;
            set
            {

                if (formModel != null)
                    formModel.PropertyChanged -= FormModel_PropertyChanged;

                formModel = value;

                if (formModel != null)
                    formModel.PropertyChanged += FormModel_PropertyChanged;

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
            await LoadMilkCollections();
            await LoadFarmers();
            await LoadCollectionCenters();
        }

        public async Task LoadMilkCollections()
        {
            var data = await _service.GetMilkCollections();

            MilkCollections.Clear();
            foreach (var item in data)
                MilkCollections.Add(item);
        }

        public async Task LoadFarmers()
        {
            var data = await _farmerService.GetFarmers();

            Farmers.Clear();
            foreach (var item in data)
                Farmers.Add(item);
        }

        public async Task LoadCollectionCenters()
        {
            var data = await _centerService.GetCollectionCenters();

            CollectionCenters.Clear();
            foreach (var item in data)
                CollectionCenters.Add(item);
        }

        private void OpenAdd()
        {
            var period = _collectionPeriodService.GetCurrentPeriod(DateTime.Now);

            if (period == null)
            {
                throw new Exception("No active collection period found.");
            }

            FormModel = new MilkCollection { 
                CollectionDate = DateTime.Now,
                CollectionPeriodId = period.Id
            };
            IsEditMode = false;
            IsPaneOpen = true;
        }

        private void OpenEdit(MilkCollection model)
        {

            FormModel = model;
            IsEditMode = true;
            IsPaneOpen = true;
        }

        private async Task Save()
        {

            if(FormModel.FarmerId == 0)
            {
                await ShowDialog("Please select a farmer");
                return;
            } else if(FormModel.CollectionCenterId == 0)
            {
                await ShowDialog("Please select a collection center");
                return;
            } else if (FormModel.Litres <= 0)
            {
                await ShowDialog("Litres must be greater than 0");
                return;
            } else if (FormModel.BuyingPricePerLitre <= 0)
            {
                await ShowDialog("Buying price per litre must be greater than 0");
                return;
            }
            var result = await _collectionPeriodService.EnsurePeriodIsOpen(FormModel.CollectionPeriodId);

            if (!result.Success)
            {
                await ShowDialog(result.Message);
                return;
            }

            if (IsEditMode)
                await _service.UpdateMilkCollection(FormModel);
            else
                await _service.AddMilkCollection(FormModel);

            await LoadMilkCollections();
            IsPaneOpen = false;
        }

        private async Task Delete(MilkCollection model)
        {
            var result = await _collectionPeriodService.EnsurePeriodIsOpen(FormModel.CollectionPeriodId);

            if (!result.Success)
            {
                await ShowDialog(result.Message);
                return;
            }
            await _service.Delete(model.Id);
            MilkCollections.Remove(model);
        }

        private void FormModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {

            if (e.PropertyName == nameof(MilkCollection.Litres) || e.PropertyName == nameof(MilkCollection.BuyingPricePerLitre))
            {

                OnPropertyChanged($"{nameof(FormModel)}.{nameof(MilkCollection.Amount)}");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private async Task ShowDialog(string message)
        {
            var dialog = new ContentDialog
            {
                Title = "Warning",
                Content = message,
                CloseButtonText = "OK",
            };

            await dialog.ShowAsync();
        }
    }
}