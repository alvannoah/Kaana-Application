using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Core.Models
{
    public class MilkLoading : BaseEntity, INotifyPropertyChanged
    {
        public DateTime Date { get; set; }
        public long MilkBuyerId { get; set; }
        public MilkBuyer MilkBuyer { get; set; }
        public string VehicleNumber { get; set; }
        public string ReceiverName { get; set; }


        private decimal _litresLoaded;
        public decimal LitresLoaded
        {
            get => _litresLoaded;
            set
            {
                if (_litresLoaded != value)
                {
                    _litresLoaded = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Amount));
                    OnPropertyChanged(nameof(Variance));
                }
            }
        }

        private decimal _pricePerLitre;
        public decimal PricePerLitre
        {
            get => _pricePerLitre;
            set
            {
                if (_pricePerLitre != value)
                {
                    _pricePerLitre = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Amount));
                }
            }
        }

        private decimal _collectedLitres;
        public decimal CollectedLitres
        {
            get => _collectedLitres;
            set
            {
                if (_collectedLitres != value)
                {
                    _collectedLitres = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Variance));
                }
            }
        }

        public decimal Amount => LitresLoaded * PricePerLitre;

        public decimal Variance => LitresLoaded - CollectedLitres;


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}