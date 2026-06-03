using Core.Models;
using Core.Services;
using Database;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class AdvanceDeductionService : IAdvanceDeductionService
    {
        private readonly AppDbContext _context;

        public AdvanceDeductionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAdvanceDeduction(AdvanceDeduction advanceDeduction)
        {
            _context.AdvanceDeductions.Add(advanceDeduction);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAdvanceDeduction(AdvanceDeduction advanceDeductione)
        {
            var existingAdvance = await _context.AdvanceDeductions.FindAsync(advanceDeductione.Id);
            if (existingAdvance == null)
            {
                throw new Exception("Invalid advance deduction!");
            }

            existingAdvance.AmountDeducted = advanceDeductione.AmountDeducted;
            existingAdvance.Date = advanceDeductione.Date;
            existingAdvance.PaymentId = advanceDeductione.PaymentId;
            existingAdvance.AdvanceId = advanceDeductione.AdvanceId;

            await _context.SaveChangesAsync();
        }

        public async Task<List<AdvanceDeduction>> GetAdvanceDeductions()
        {
            return await _context.AdvanceDeductions.ToListAsync();
        }

        public async Task<AdvanceDeduction> GetAdvanceDeductionById(long id)
        {
            if (id is 0)
            {
                throw new Exception("Invalid Id");
            }

            return await _context.AdvanceDeductions.FindAsync(id);
        }

    }
}
