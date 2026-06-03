using Core.Models;
using Core.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Frontend.ViewModels
{
    public class FarmerViewModel : INotifyPropertyChanged
    {
        private readonly IFarmerService _service;

        public ObservableCollection<Farmer> Farmers { get; set; }
            = new ObservableCollection<Farmer>();

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PrimaryPhone { get; set; }

        public string SecondaryPhone { get; set; }
        public ICommand UpdateFarmerCommand { get; }

        private Farmer selectedFarmer;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Farmer SelectedFarmer
        {
            get => selectedFarmer;
            set
            {
                selectedFarmer = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(SelectedFarmer)));
            }
        }

        public FarmerViewModel(IFarmerService service)
        {
            _service = service;
        }

        public async Task LoadFarmers()
        {
            var farmers = await _service.GetFarmers();

            Farmers.Clear();
            foreach (var f in farmers)
                Farmers.Add(f);
        }

        public async Task AddFarmer()
        {
            await _service.AddFarmer(new Farmer
            {
                FirstName = FirstName,
                LastName = LastName
            });

            await LoadFarmers();
        }

        public async Task UpdateFarmer()
        {
            await _service.UpdateFarmer(SelectedFarmer);
            await LoadFarmers();
        }

        public async Task LoadFarmer(long id)
        {
            SelectedFarmer = await _service.GetFarmerById(id);
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}
