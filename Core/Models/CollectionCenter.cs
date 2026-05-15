using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class CollectionCenter : BaseEntity
    {

        public string Name { get; set; }

        public string Location { get; set; }

        public string PhoneNumber { get; set; }

        public string ManagerName { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<MilkCollection> MilkCollections { get; set; }

        public ICollection<Expense> Expenses { get; set; }

        public ICollection<FactorySale> FactorySales { get; set; }
    }
}
