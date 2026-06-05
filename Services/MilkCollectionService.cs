using Core.Models;
using Core.Services;
using Database;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class MilkCollectionService : IMilkCollectionService
    {
        private readonly AppDbContext _context;

        public MilkCollectionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddMilkCollection(MilkCollection milkCollection)
        {
            _context.MilkCollections.Add(milkCollection);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMilkCollection(MilkCollection milkCollection)
        {
            var existingMilkCollection = await _context.MilkCollections.FindAsync(milkCollection.Id);
            if (existingMilkCollection == null)
            {
                throw new Exception("Invalid collection!");
            }

            existingMilkCollection.CollectionDate = milkCollection.CollectionDate;
            existingMilkCollection.Litres = milkCollection.Litres;
            existingMilkCollection.BuyingPricePerLitre = milkCollection.BuyingPricePerLitre;
            existingMilkCollection.FarmerId = existingMilkCollection.FarmerId;
            existingMilkCollection.CollectionCenterId = milkCollection.FarmerId;

            await _context.SaveChangesAsync();
        }

        public async Task<List<MilkCollection>> GetMilkCollections()
        {
            return await _context.MilkCollections.ToListAsync();
        }

        public async Task<MilkCollection> GetMilkCollectionById(long id)
        {
            if (id is 0)
            {
                throw new Exception("Invalid Id");
            }

            return await _context.MilkCollections.FindAsync(id);
        }
        public async Task Delete(long id)
        {
            var existingMilkCollection = await _context.MilkCollections.FindAsync(id);
            if (existingMilkCollection == null)
            {
                throw new Exception("Invalid collection!");
            }
            _context.MilkCollections.Remove(existingMilkCollection);
            await _context.SaveChangesAsync();

        }
    }
}

