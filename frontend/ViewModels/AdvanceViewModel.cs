using Core.Models;
using Core.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Frontend.ViewModels
{
    public class AdvanceViewModel : INotifyPropertyChanged
    {
        private readonly IAdvanceService _advanceService;
        private readonly IFarmerService _farmerService;

        public AdvanceViewModel(IAdvanceService advanceService, IFarmerService farmerService)
        {
            _advanceService = advanceService;
            _farmerService = farmerService;
            FormModel = new Advance
            {
                DateIssued = DateTime.Now
            };

            OpenAddCommand = new RelayCommand<object>(async m => OpenAdd());
            OpenEditCommand = new RelayCommand<Advance>(async a => OpenEdit(a));
            SaveCommand = new RelayCommand<object>(async _ => await Save());
            DeleteCommand = new RelayCommand<Advance>(async a => await Delete(a));
        }

        public ObservableCollection<Advance> Advances { get; }
            = new ObservableCollection<Advance>();
        public ObservableCollection<Farmer> Farmers { get; }
            = new ObservableCollection<Farmer>();

        private Advance formModel;
        public Advance FormModel
        {
            get => formModel;
            set
            {
                formModel = value;
                OnPropertyChanged(nameof(FormModel));
            }
        }

        private Advance selectedAdvance;
        public Advance SelectedAdvance
        {
            get => selectedAdvance;
            set
            {
                selectedAdvance = value;
                OnPropertyChanged(nameof(SelectedAdvance));
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
            await LoadFarmers();
            await LoadAdvances();
        }

        public async Task LoadAdvances()
        {
            var data = await _advanceService.GetAdvances();

            Advances.Clear();
            foreach (var item in data)
                Advances.Add(item);
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
            FormModel = new Advance
            {
                DateIssued = DateTime.Now,
                RepaidAmount = 0
            };

            IsEditMode = false;
            IsPaneOpen = true;
        }

        private void OpenEdit(Advance advance)
        {
            if (advance == null)
                return;

            FormModel = new Advance
            {
                Id = advance.Id,
                FarmerId = advance.FarmerId,
                DateIssued = advance.DateIssued,
                Amount = advance.Amount,
                RepaidAmount = advance.RepaidAmount,
                Notes = advance.Notes
            };

            IsEditMode = true;
            IsPaneOpen = true;
        }

        private async Task Save()
        {
            if (FormModel.FarmerId <= 0)
                throw new Exception("Select a farmer");

            if (FormModel.Amount <= 0)
                throw new Exception("Amount must be greater than zero");

            if (IsEditMode)
                await _advanceService.UpdateAdvance(FormModel);
            else
                await _advanceService.AddAdvance(FormModel);

            await LoadAdvances();
            IsPaneOpen = false;
        }

        private async Task Delete(Advance advance)
        {
            if (advance == null)
                return;

            await _advanceService.Delete(advance.Id);
            Advances.Remove(advance);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}