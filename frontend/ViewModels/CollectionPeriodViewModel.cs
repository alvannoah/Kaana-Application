using Core.Models;
using Core.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Frontend.ViewModels
{
    public class CollectionPeriodViewModel : INotifyPropertyChanged
    {
        private readonly ICollectionPeriodService _service;

        public ObservableCollection<CollectionPeriod> CollectionPeriods { get; }
            = new();

        private CollectionPeriod selectedCollectionPeriod;

        public CollectionPeriod SelectedCollectionPeriod
        {
            get => selectedCollectionPeriod;
            set
            {
                selectedCollectionPeriod = value;
                OnPropertyChanged(nameof(SelectedCollectionPeriod));
            }
        }

        public ICommand ClosePeriodCommand { get; }

        public CollectionPeriodViewModel(
            ICollectionPeriodService service)
        {
            _service = service;

            ClosePeriodCommand =
                new RelayCommand<object>(async _ => await ClosePeriod());
        }

        public async Task LoadCollectionPeriods()
        {
            var periods = await _service.GetAll();

            CollectionPeriods.Clear();

            foreach (var period in periods)
                CollectionPeriods.Add(period);
        }

        private async Task ClosePeriod()
        {
            if (SelectedCollectionPeriod == null)
                return;

            await _service.ClosePeriod(
                SelectedCollectionPeriod.Id);

            await LoadCollectionPeriods();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}
