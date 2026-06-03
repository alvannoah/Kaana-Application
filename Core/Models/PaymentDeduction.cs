using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class PaymentDeduction : BaseEntity
    {

        public long PaymentId { get; set; }
        public Payment Payment { get; set; }

        public string Description { get; set; }

        public decimal Amount { get; set; }
    }
}
