using Core.Models;

namespace Core.Models
{
    public class Advance : BaseEntity
    {
        public Farmer Farmer { get; set; }
        public long FarmerId { get; set; }

        public DateTime DateIssued { get; set; }

        public decimal Amount { get; set; }

        public decimal RepaidAmount { get; set; }

        public decimal Balance => Amount - RepaidAmount;

        public string Notes { get; set; }
    }
}