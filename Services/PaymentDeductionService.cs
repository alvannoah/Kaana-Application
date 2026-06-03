using Core.Models;
using Core.Services;
using Database;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class PaymentDeductionService : IPaymentDeductionService
    {
        private readonly AppDbContext _context;

        public PaymentDeductionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddPaymentDeduction(PaymentDeduction paymentDeduction)
        {
            _context.PaymentDeductions.Add(paymentDeduction);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePaymentDeduction(PaymentDeduction paymentDeduction)
        {
            var existingRecord = await _context.PaymentDeductions.FindAsync(paymentDeduction.Id);
            if (existingRecord == null)
            {
                throw new Exception("Invalid payment deduction!");
            }

            existingRecord.PaymentId = paymentDeduction.PaymentId;
            existingRecord.Description = paymentDeduction.Description;
            existingRecord.Amount = paymentDeduction.Amount;

            await _context.SaveChangesAsync();
        }

        public async Task<List<PaymentDeduction>> GetPaymentDeductions()
        {
            return await _context.PaymentDeductions.ToListAsync();
        }

        public async Task<PaymentDeduction> GetPaymentDeductionById(long id)
        {
            if (id is 0)
            {
                throw new Exception("Invalid Id");
            }

            return await _context.PaymentDeductions.FindAsync(id);
        }

    }
}
