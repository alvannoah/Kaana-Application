using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class MilkLoading : BaseEntity
    {
        public DateTime Date { get; set; }

        public long MilkBuyerId { get; set; }
        public MilkBuyer MilkBuyer { get; set; }

        public string VehicleNumber { get; set; }

        public string ReceiverName { get; set; }

        public decimal LitresLoaded { get; set; }

        public decimal PricePerLitre { get; set; }

        public decimal Amount { get; set; }

        public decimal CollectedLitres { get; set; }

        public decimal Variance { get; set; }
    }
}
