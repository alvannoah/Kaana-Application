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

        public ObservableCollection<CollectionPeriod> CollectionPeriods { get; set; }
            = new ObservableCollection<CollectionPeriod>();

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsClosed { get; set; }
        public ICommand UpdateCollectionPeriodCommand { get; }

        private CollectionPeriod selectedCollectionPeriod;

        public event PropertyChangedEventHandler? PropertyChanged;

        public CollectionPeriod SelectedCollectionPeriod
        {
            get => selectedCollectionPeriod;
            set
            {
                selectedCollectionPeriod = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(SelectedCollectionPeriod)));
            }
        }

        public CollectionPeriodViewModel(ICollectionPeriodService service)
        {
            _service = service;
        }

        public async Task LoadCollectionPeriods()
        {
            var CollectionPeriods = await _service.GetCollectionPeriods();

            CollectionPeriods.Clear();
            foreach (var f in CollectionPeriods)
                CollectionPeriods.Add(f);
        }

        public async Task AddCollectionPeriod()
        {
            await _service.AddCollectionPeriod(new CollectionPeriod
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                IsClosed = false
            });

            await LoadCollectionPeriods();
        }

        public async Task UpdateCollectionPeriod()
        {
            await _service.UpdateCollectionPeriod(SelectedCollectionPeriod);
            await LoadCollectionPeriods();
        }

        public async Task LoadCollectionPeriod(long id)
        {
            SelectedCollectionPeriod = await _service.GetCollectionPeriodById(id);
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}
