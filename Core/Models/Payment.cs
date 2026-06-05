using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Core.Enums;

namespace Core.Models
{
    public class Payment : BaseEntity, INotifyPropertyChanged
    {
        public Farmer Farmer { get; set; }
        public long FarmerId { get; set; }
        public long CollectionPeriodId { get; set; }
        public CollectionPeriod CollectionPeriod { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentType? PaymentType { get; set; }
        public string? ReferenceNumber { get; set; }


        private decimal _totalLitres;
        public decimal TotalLitres
        {
            get => _totalLitres;
            set
            {
                if (_totalLitres != value)
                {
                    _totalLitres = value;
                    OnPropertyChanged();

                    OnPropertyChanged(nameof(GrossAmount));
                    OnPropertyChanged(nameof(NetAmount));
                }
            }
        }

        private decimal _ratePerLitre;
        public decimal RatePerLitre
        {
            get => _ratePerLitre;
            set
            {
                if (_ratePerLitre != value)
                {
                    _ratePerLitre = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(GrossAmount));
                    OnPropertyChanged(nameof(NetAmount));
                }
            }
        }

        private decimal? _totalDeductions;
        public decimal? TotalDeductions
        {
            get => _totalDeductions;
            set
            {
                if (_totalDeductions != value)
                {
                    _totalDeductions = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(NetAmount));
                }
            }
        }

        public decimal GrossAmount => TotalLitres * RatePerLitre;

        public decimal NetAmount
        {
            get
            {
                decimal activeDeductions = TotalDeductions ?? 0;
                decimal calculatedNet = GrossAmount - activeDeductions;
                return calculatedNet < 0 ? 0 : calculatedNet;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}