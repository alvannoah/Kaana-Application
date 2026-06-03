using Core.Models;
using Core.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Frontend.ViewModels
{
    public class MilkLoadingViewModel : INotifyPropertyChanged
    {
        private readonly IMilkLoadingService _service;

        public ObservableCollection<MilkLoading> MilkLoadings { get; set; }
            = new ObservableCollection<MilkLoading>();


        public DateTime Date { get; set; }

        public long MilkBuyerId { get; set; }

        public string VehicleNumber { get; set; }

        public string ReceiverName { get; set; }

        public decimal LitresLoaded { get; set; }

        public decimal PricePerLitre { get; set; }

        public decimal Amount { get; set; }

        public decimal CollectedLitres { get; set; }

        public decimal Variance { get; set; }

        public ICommand UpdateMilkLoadingCommand { get; }

        private MilkLoading selectedMilkLoading;

        public event PropertyChangedEventHandler? PropertyChanged;

        public MilkLoading SelectedMilkLoading
        {
            get => selectedMilkLoading;
            set
            {
                selectedMilkLoading = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(SelectedMilkLoading)));
            }
        }

        public MilkLoadingViewModel(IMilkLoadingService service)
        {
            _service = service;
        }

        public async Task LoadMilkLoadings()
        {
            var MilkLoadings = await _service.GetMilkLoadings();

            MilkLoadings.Clear();
            foreach (var f in MilkLoadings)
                MilkLoadings.Add(f);
        }

        public async Task AddMilkLoading()
        {
            await _service.AddMilkLoading(new MilkLoading
            {
                Date = Date,
                MilkBuyerId = MilkBuyerId,
                VehicleNumber = VehicleNumber,
                ReceiverName = ReceiverName,
                LitresLoaded = LitresLoaded,
                PricePerLitre = PricePerLitre,
                Amount = Amount,
                CollectedLitres = CollectedLitres,
                Variance = Variance,
            });

            await LoadMilkLoadings();
        }

        public async Task UpdateMilkLoading()
        {
            await _service.UpdateMilkLoading(SelectedMilkLoading);
            await LoadMilkLoadings();
        }

        public async Task LoadMilkLoading(long id)
        {
            SelectedMilkLoading = await _service.GetMilkLoadingById(id);
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}
