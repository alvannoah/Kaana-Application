using Core.Models;
using Core.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Frontend.ViewModels
{
    public class AdvanceDeductionViewModel : INotifyPropertyChanged
    {
        private readonly IAdvanceDeductionService _service;

        public ObservableCollection<AdvanceDeduction> AdvanceDeductions { get; set; }
            = new ObservableCollection<AdvanceDeduction>();

        public long AdvanceId { get; set; }
        public long PaymentId { get; set; }
        public decimal AmountDeducted { get; set; }
        public DateTime Date { get; set; }
        public ICommand UpdateAdvanceDeductionCommand { get; }

        private AdvanceDeduction selectedAdvanceDeduction;

        public event PropertyChangedEventHandler? PropertyChanged;

        public AdvanceDeduction SelectedAdvanceDeduction
        {
            get => selectedAdvanceDeduction;
            set
            {
                selectedAdvanceDeduction = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(SelectedAdvanceDeduction)));
            }
        }

        public AdvanceDeductionViewModel(IAdvanceDeductionService service)
        {
            _service = service;
        }

        public async Task LoadAdvanceDeductions()
        {
            var farmers = await _service.GetAdvanceDeductions();

            AdvanceDeductions.Clear();
            foreach (var f in farmers)
                AdvanceDeductions.Add(f);
        }

        public async Task AddAdvanceDeduction()
        {
            await _service.AddAdvanceDeduction(new AdvanceDeduction
            {
                AdvanceId = AdvanceId,
                PaymentId = PaymentId,
                AmountDeducted = AmountDeducted,
                Date = Date,
            });

            await LoadAdvanceDeductions();
        }

        public async Task UpdateFarmer()
        {
            await _service.UpdateAdvanceDeduction(SelectedAdvanceDeduction);
            await LoadAdvanceDeductions();
        }

        public async Task LoadAdvanceDeduction(long id)
        {
            SelectedAdvanceDeduction = await _service.GetAdvanceDeductionById(id);
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}
