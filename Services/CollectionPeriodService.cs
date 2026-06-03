using Core.Models;
using Core.Services;
using Database;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class CollectionPeriodService : ICollectionPeriodService
    {
        private readonly AppDbContext _context;

        public CollectionPeriodService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddCollectionPeriod(CollectionPeriod collectionPeriod)
        {
            _context.CollectionPeriods.Add(collectionPeriod);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCollectionPeriod(CollectionPeriod collectionPeriod)
        {
            var existingRecord = await _context.CollectionPeriods.FindAsync(collectionPeriod.Id);
            if (existingRecord == null)
            {
                throw new Exception("Invalid collection period!");
            }

            existingRecord.StartDate = collectionPeriod.StartDate;
            existingRecord.EndDate = collectionPeriod.EndDate;
            existingRecord.IsClosed = collectionPeriod.IsClosed;

            await _context.SaveChangesAsync();
        }

        public async Task<List<CollectionPeriod>> GetCollectionPeriods()
        {
            return await _context.CollectionPeriods.ToListAsync();
        }

        public async Task<CollectionPeriod> GetCollectionPeriodById(long id)
        {
            if (id is 0)
            {
                throw new Exception("Invalid Id");
            }

            return await _context.CollectionPeriods.FindAsync(id);
        }

    }
}
