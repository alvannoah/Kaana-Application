using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Advance : BaseEntity
    {
        public Farmer Farmer { get; set; }
        public long FarmerId { get; set; }
        public DateTime DateIssued { get; set; }
        public decimal Amount { get; set; }
        public string Notes { get; set; }
        public bool IsSettled { get; set; }
    }
}
