using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Core.Models
{
    public class MilkCollection : BaseEntity, INotifyPropertyChanged
    {
        public long FarmerId { get; set; }
        public long CollectionCenterId { get; set; }
        public DateTime CollectionDate { get; set; }

        public virtual Farmer Farmer { get; set; }
        public virtual CollectionCenter CollectionCenter { get; set; }


        private decimal _litres;
        public decimal Litres
        {
            get => _litres;
            set
            {
                if (_litres != value)
                {
                    _litres = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Amount));
                }
            }
        }

        private decimal _buyingPricePerLitre;
        public decimal BuyingPricePerLitre
        {
            get => _buyingPricePerLitre;
            set
            {
                if (_buyingPricePerLitre != value)
                {
                    _buyingPricePerLitre = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Amount)); 
                }
            }
        }

        public decimal Amount => Litres * BuyingPricePerLitre;


        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public long CollectionPeriodId { get; set; }
        public CollectionPeriod CollectionPeriod { get; set; }
    }
}