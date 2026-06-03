using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Farmer : BaseEntity
    {
        
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PrimaryPhone { get; set; }

        public string? SecondaryPhone { get; set; }

        public CollectionCenter CollectionCenter { get; set; }

        public long CollectionCenterId { get; set; }

        public ICollection<MilkCollection> MilkCollections { get; set; }
        public ICollection<Advance> Advances { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}
