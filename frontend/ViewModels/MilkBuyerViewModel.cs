using Core.Models;
using Core.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Frontend.ViewModels
{
    public class MilkBuyerViewModel : INotifyPropertyChanged
    {
        private readonly IMilkBuyerService _service;

        public ObservableCollection<MilkBuyer> MilkBuyers { get; set; }
            = new ObservableCollection<MilkBuyer>();

        public string Name { get; set; }
        public string ContactPerson { get; set; }

        public ICommand UpdateMilkBuyerCommand { get; }

        private MilkBuyer selectedMilkBuyer;

        public event PropertyChangedEventHandler? PropertyChanged;

        public MilkBuyer SelectedMilkBuyer
        {
            get => selectedMilkBuyer;
            set
            {
                selectedMilkBuyer = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(SelectedMilkBuyer)));
            }
        }

        public MilkBuyerViewModel(IMilkBuyerService service)
        {
            _service = service;
        }

        public async Task LoadMilkBuyers()
        {
            var MilkBuyers = await _service.GetMilkBuyers();

            MilkBuyers.Clear();
            foreach (var f in MilkBuyers)
                MilkBuyers.Add(f);
        }

        public async Task AddMilkBuyer()
        {
            await _service.AddMilkBuyer(new MilkBuyer
            {
                Name = Name,
                ContactPerson = ContactPerson,
            });

            await LoadMilkBuyers();
        }

        public async Task UpdateMilkBuyer()
        {
            await _service.UpdateMilkBuyer(SelectedMilkBuyer);
            await LoadMilkBuyers();
        }

        public async Task LoadMilkBuyer(long id)
        {
            SelectedMilkBuyer = await _service.GetMilkBuyerById(id);
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}
