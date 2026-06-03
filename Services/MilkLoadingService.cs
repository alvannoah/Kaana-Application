using Core.Models;
using Core.Services;
using Database;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class MilkLoadingService : IMilkLoadingService
    {
        private readonly AppDbContext _context;

        public MilkLoadingService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddMilkLoading(MilkLoading milkLoading)
        {
            _context.MilkLoadings.Add(milkLoading);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMilkLoading(MilkLoading milkLoading)
        {
            var existingRecord = await _context.MilkLoadings.FindAsync(milkLoading.Id);
            if (existingRecord == null)
            {
                throw new Exception("Invalid Milk Loading!");
            }

            existingRecord.MilkBuyerId = milkLoading.MilkBuyerId;
            existingRecord.VehicleNumber = milkLoading.VehicleNumber;
            existingRecord.ReceiverName = milkLoading.ReceiverName;
            existingRecord.LitresLoaded = milkLoading.LitresLoaded;
            existingRecord.PricePerLitre = milkLoading.PricePerLitre;
            existingRecord.Amount = milkLoading.Amount;
            existingRecord.CollectedLitres = milkLoading.CollectedLitres;
            existingRecord.Variance = milkLoading.Variance;

            await _context.SaveChangesAsync();
        }

        public async Task<List<MilkLoading>> GetMilkLoadings()
        {
            return await _context.MilkLoadings.ToListAsync();
        }

        public async Task<MilkLoading> GetMilkLoadingById(long id)
        {
            if (id is 0)
            {
                throw new Exception("Invalid Id");
            }

            return await _context.MilkLoadings.FindAsync(id);
        }

    }
}
