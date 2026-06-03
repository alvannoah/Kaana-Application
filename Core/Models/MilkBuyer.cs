using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class MilkBuyer : BaseEntity
    {
        public string Name { get; set; }

        public string ContactPerson { get; set; }

        public ICollection<MilkLoading> Loadings { get; set; }
    }
}
