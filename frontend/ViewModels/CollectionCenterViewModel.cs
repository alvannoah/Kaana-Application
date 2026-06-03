using Core.Models;
using Core.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Frontend.ViewModels
{
    public class CollectionCenterViewModel : INotifyPropertyChanged
    {
        private readonly ICollectionCenterService _service;

        public ObservableCollection<CollectionCenter> CollectionCenters { get; set; }
            = new ObservableCollection<CollectionCenter>();

        public string Name { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
        public string ManagerName { get; set; }
        public bool IsActive { get; set; } = true;
        public ICommand UpdateCollectionCenterCommand { get; }

        private CollectionCenter selectedCollectionCenter;

        public event PropertyChangedEventHandler? PropertyChanged;

        public CollectionCenter SelectedCollectionCenter
        {
            get => selectedCollectionCenter;
            set
            {
                selectedCollectionCenter = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(SelectedCollectionCenter)));
            }
        }

        public CollectionCenterViewModel(ICollectionCenterService service)
        {
            _service = service;
        }

        public async Task LoadCollectionCenters()
        {
            var CollectionCenters = await _service.GetCollectionCenters();

            CollectionCenters.Clear();
            foreach (var f in CollectionCenters)
                CollectionCenters.Add(f);
        }

        public async Task AddCollectionCenter()
        {
            await _service.AddCollectionCenter(new CollectionCenter
            {
                Name = Name,
                Location = Location,
                PhoneNumber = PhoneNumber,
                ManagerName = ManagerName,
                IsActive = IsActive
            });

            await LoadCollectionCenters();
        }

        public async Task UpdateCollectionCenter()
        {
            await _service.UpdateCollectionCenter(SelectedCollectionCenter);
            await LoadCollectionCenters();
        }

        public async Task LoadCollectionCenter(long id)
        {
            SelectedCollectionCenter = await _service.GetCollectionCenterById(id);
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}
