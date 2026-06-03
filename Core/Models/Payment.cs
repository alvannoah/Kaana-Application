using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Payment : BaseEntity
    {
        public Farmer Farmer { get; set; }
        public long FarmerId { get; set; }
        public long CollectionPeriodId { get; set; }
        public CollectionPeriod CollectionPeriod { get; set; }
        public decimal TotalLitres { get; set; }
        public decimal RatePerLitre { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal TotalDeductions { get; set; }
        public decimal NetAmount { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
