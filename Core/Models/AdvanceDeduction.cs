using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class AdvanceDeduction : BaseEntity
    {

        public long AdvanceId { get; set; }
        public Advance Advance { get; set; }

        public long PaymentId { get; set; }
        public Payment Payment { get; set; }

        public decimal AmountDeducted { get; set; }

        public DateTime Date { get; set; }
    }
}
