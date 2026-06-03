using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class CollectionPeriod : BaseEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public bool IsClosed { get; set; }

        public ICollection<Payment> Payments { get; set; }
    }
}
