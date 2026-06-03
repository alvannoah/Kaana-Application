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
        private readonly IAdvanceService _service;

        public ObservableCollection<Advance> Advances { get; set; }
            = new ObservableCollection<Advance>();
        public long FarmerId { get; set; }
        public DateTime DateIssued { get; set; }
        public decimal Amount { get; set; }
        public string Notes { get; set; }
        public bool IsSettled { get; set; }
        public ICommand UpdateFarmerCommand { get; }

        private Advance selectedAdvance;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Advance SelectedAdvance
        {
            get => selectedAdvance;
            set
            {
                selectedAdvance = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(SelectedAdvance)));
            }
        }

        public AdvanceViewModel(IAdvanceService service)
        {
            _service = service;
        }

        public async Task LoadAdvances()
        {
            var farmers = await _service.GetAdvances();

            Advances.Clear();
            foreach (var f in farmers)
                Advances.Add(f);
        }

        public async Task AddFarmer()
        {
            await _service.AddAdvance(new Advance
            {
                FarmerId = FarmerId,
                DateIssued = DateIssued,
                Amount = Amount,
                Notes = Notes,
                IsSettled = IsSettled,
            });

            await LoadAdvances();
        }

        public async Task UpdateFarmer()
        {
            await _service.UpdateAdvance(SelectedAdvance);
            await LoadAdvances();
        }

        public async Task LoadFarmer(long id)
        {
            SelectedAdvance = await _service.GetAdvanceById(id);
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}
