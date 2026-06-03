using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class PeriodBalance : BaseEntity
    {
        public long FarmerId { get; set; }

        public long CollectionPeriodId { get; set; }

        public decimal TotalLitres { get; set; }
    }
}
