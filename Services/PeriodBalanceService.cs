using Core.Models;
using Core.Services;
using Database;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class PeriodBalanceService : IPeriodBalanceService
    {
        private readonly AppDbContext _context;

        public PeriodBalanceService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddPeriodBalance(PeriodBalance periodBalance)
        {
            _context.PeriodBalances.Add(periodBalance);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePeriodBalance(PeriodBalance periodBalance)
        {
            var existingRecord = await _context.PeriodBalances.FindAsync(periodBalance.Id);
            if (existingRecord == null)
            {
                throw new Exception("Invalid period balance!");
            }

            //existingRecord.PaymentId = paymentDeduction.PaymentId;
            //existingRecord.Description = paymentDeduction.Description;
            //existingRecord.Amount = paymentDeduction.Amount;

            await _context.SaveChangesAsync();
        }

        public async Task<List<PeriodBalance>> GetPeriodBalances()
        {
            return await _context.PeriodBalances.ToListAsync();
        }

        public async Task<PeriodBalance> GetPeriodBalanceById(long id)
        {
            if (id is 0)
            {
                throw new Exception("Invalid Id");
            }

            return await _context.PeriodBalances.FindAsync(id);
        }

    }
}
