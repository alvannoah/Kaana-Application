using Core.Models;
using Microsoft.EntityFrameworkCore;
namespace Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Farmer> Farmers { get; set; }
        public DbSet<MilkCollection> MilkCollections { get; set; }
        public DbSet<CollectionCenter> CollectionCenters { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentDeduction> PaymentDeductions { get; set; }
        public DbSet<MilkLoading> MilkLoadings { get; set; }
        public DbSet<MilkBuyer> MilkBuyers{ get; set; }
        public DbSet<CollectionPeriod> CollectionPeriods { get; set; }
        public DbSet<AdvanceDeduction> AdvanceDeductions { get; set; }
        public DbSet<Advance> Advances { get; set; }
        public DbSet<PeriodBalance> PeriodBalances { get; set; }
        public DbSet<User> Users { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseEntity>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.RecordStatus = Core.Enums.RecordStatus.ACTIVE;
                    entry.Entity.DateCreated = DateTime.UtcNow;

                    entry.Entity.CreatedBy = "Test Wantoo";
                    entry.Entity.UpdatedBy = "Test Wantoo";
                    entry.Entity.DateUpdated = DateTime.UtcNow;

                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.DateUpdated = DateTime.UtcNow;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MilkCollection>()
                .HasOne(mc => mc.Farmer)
                .WithMany(f => f.MilkCollections)
                .HasForeignKey(mc => mc.FarmerId);

            modelBuilder.Entity<MilkCollection>()
                .HasOne(mc => mc.CollectionCenter)
                .WithMany(cc => cc.MilkCollections)
                .HasForeignKey(mc => mc.CollectionCenterId);

            modelBuilder.Entity<Farmer>()
                .HasOne(f => f.CollectionCenter)
                .WithMany(cc => cc.Farmers)
                .HasForeignKey(f => f.CollectionCenterId);
        }
    }
}
