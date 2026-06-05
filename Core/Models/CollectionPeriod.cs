
namespace Core.Models
{
    public class CollectionPeriod : BaseEntity
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public bool IsClosed { get; set; }
        public bool IsProcessed { get; set; }

        public ICollection<Payment> Payments { get; set; }
        public ICollection<MilkCollection> MilkCollections { get; set; } = new List<MilkCollection>();
        public ICollection<Expense> Expenses { get; set; }
    }
}
