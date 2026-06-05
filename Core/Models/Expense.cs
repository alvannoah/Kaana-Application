using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Expense : BaseEntity
    {
        public DateTime Date { get; set; }

        public string? Description { get; set; }

        public string? Quantity { get; set; }

        public decimal Amount { get; set; }

        public string? Notes { get; set; }
        public long CollectionPeriodId { get; set; }
        public CollectionPeriod CollectionPeriod { get; set; }
    }
}
