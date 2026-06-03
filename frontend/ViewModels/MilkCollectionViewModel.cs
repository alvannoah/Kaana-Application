using Core.Models;
using Core.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Frontend.ViewModels
{
    public class MilkCollectionViewModel : INotifyPropertyChanged
    {
        private readonly IMilkCollectionService _service;

        public ObservableCollection<MilkCollection> MilkCollections { get; set; }
            = new ObservableCollection<MilkCollection>();

        public double Quantity { get; set; }
        public Farmer Farmer { get; set; }
        public long FarmerId { get; set; }
        public long CollectionCenterId { get; set; }

        public ICommand UpdateMilkCollectionCommand { get; }

        private MilkCollection selectedMilkCollection;

        public event PropertyChangedEventHandler? PropertyChanged;

        public MilkCollection SelectedMilkCollection
        {
            get => selectedMilkCollection;
            set
            {
                selectedMilkCollection = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(SelectedMilkCollection)));
            }
        }

        public MilkCollectionViewModel(IMilkCollectionService service)
        {
            _service = service;
        }

        public async Task LoadMilkCollections()
        {
            var MilkCollections = await _service.GetMilkCollections();

            MilkCollections.Clear();
            foreach (var f in MilkCollections)
                MilkCollections.Add(f);
        }

        public async Task AddMilkCollection()
        {
            await _service.AddMilkCollection(new MilkCollection
            {
                Quantity = Quantity,
                FarmerId = FarmerId,
                CollectionCenterId = CollectionCenterId,
            });

            await LoadMilkCollections();
        }

        public async Task UpdateMilkCollection()
        {
            await _service.UpdateMilkCollection(SelectedMilkCollection);
            await LoadMilkCollections();
        }

        public async Task LoadMilkCollection(long id)
        {
            SelectedMilkCollection = await _service.GetMilkCollectionById(id);
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}
