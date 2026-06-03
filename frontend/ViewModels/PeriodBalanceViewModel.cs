using Core.Models;
using Core.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Frontend.ViewModels
{
    public class PeriodBalanceViewModel : INotifyPropertyChanged
    {
        private readonly IPeriodBalanceService _service;

        public ObservableCollection<PeriodBalance> PeriodBalances { get; set; }
            = new ObservableCollection<PeriodBalance>();
        public long FarmerId { get; set; }

        public long CollectionPeriodId { get; set; }

        public decimal TotalLitres { get; set; }

        public ICommand UpdatePeriodBalanceCommand { get; }

        private PeriodBalance selectedPeriodBalance;

        public event PropertyChangedEventHandler? PropertyChanged;

        public PeriodBalance SelectedPeriodBalance
        {
            get => selectedPeriodBalance;
            set
            {
                selectedPeriodBalance = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(SelectedPeriodBalance)));
            }
        }

        public PeriodBalanceViewModel(IPeriodBalanceService service)
        {
            _service = service;
        }

        public async Task LoadPeriodBalances()
        {
            var PeriodBalances = await _service.GetPeriodBalances();

            PeriodBalances.Clear();
            foreach (var f in PeriodBalances)
                PeriodBalances.Add(f);
        }

        public async Task AddPeriodBalance()
        {
            await _service.AddPeriodBalance(new PeriodBalance
            {
                FarmerId = FarmerId,
                CollectionPeriodId = CollectionPeriodId,
                TotalLitres = TotalLitres,
            });

            await LoadPeriodBalances();
        }

        public async Task UpdatePeriodBalance()
        {
            await _service.UpdatePeriodBalance(SelectedPeriodBalance);
            await LoadPeriodBalances();
        }

        public async Task LoadPeriodBalance(long id)
        {
            SelectedPeriodBalance = await _service.GetPeriodBalanceById(id);
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}
