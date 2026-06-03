using Core.Models;
using Core.Services;
using Database;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class CollectionCenterService : ICollectionCenterService
    {
        private readonly AppDbContext _context;

        public CollectionCenterService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddCollectionCenter(CollectionCenter collectionCenter)
        {
            _context.CollectionCenters.Add(collectionCenter);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCollectionCenter(CollectionCenter collectionCenter)
        {
            var existingRecord = await _context.CollectionCenters.FindAsync(collectionCenter.Id);
            if (existingRecord == null)
            {
                throw new Exception("Invalid collection center!");
            }

            existingRecord.Name = collectionCenter.Name;
            existingRecord.Location = collectionCenter.Location;
            existingRecord.PhoneNumber = collectionCenter.PhoneNumber;
            existingRecord.ManagerName = collectionCenter.ManagerName;
            existingRecord.IsActive = collectionCenter.IsActive;

            await _context.SaveChangesAsync();
        }

        public async Task<List<CollectionCenter>> GetCollectionCenters()
        {
            return await _context.CollectionCenters.ToListAsync();
        }

        public async Task<CollectionCenter> GetCollectionCenterById(long id)
        {
            if (id is 0)
            {
                throw new Exception("Invalid Id");
            }

            return await _context.CollectionCenters.FindAsync(id);
        }

    }
}
