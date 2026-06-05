using Core.Models;
using Core.Services;
using Database;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class MilkBuyerService : IMilkBuyerService
    {
        private readonly AppDbContext _context;

        public MilkBuyerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddMilkBuyer(MilkBuyer milkBuyer)
        {
            _context.MilkBuyers.Add(milkBuyer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMilkBuyer(MilkBuyer milkBuyer)
        {
            var existingRecord = await _context.MilkBuyers.FindAsync(milkBuyer.Id);
            if (existingRecord == null)
            {
                throw new Exception("Invalid Milk Buyer!");
            }

            existingRecord.Name = milkBuyer.Name;
            existingRecord.ContactPerson = milkBuyer.ContactPerson;
            existingRecord.PhoneNumber = milkBuyer.PhoneNumber;
            existingRecord.Address = milkBuyer.Address;

            await _context.SaveChangesAsync();
        }

        public async Task<List<MilkBuyer>> GetMilkBuyers()
        {
            return await _context.MilkBuyers.ToListAsync();
        }

        public async Task<MilkBuyer> GetMilkBuyerById(long id)
        {
            if (id is 0)
            {
                throw new Exception("Invalid Id");
            }

            return await _context.MilkBuyers.FindAsync(id);
        }

        public async Task Delete(long id)
        {
            var record = await _context.MilkBuyers.FindAsync(id);
            if (record == null)
            {
                throw new Exception("Invalid Milk Buyer!");
            }
            _context.MilkBuyers.Remove(record);
            await _context.SaveChangesAsync();
        }

    }
}
