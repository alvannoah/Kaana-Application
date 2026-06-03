using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class MilkCollection : BaseEntity
    {
        public double Quantity { get; set; }
        public Farmer Farmer { get; set; }

        public long FarmerId { get; set; }

        public CollectionCenter CollectionCenter { get; set; }
        public long CollectionCenterId { get; set; }
    }
}
