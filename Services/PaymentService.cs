using Core.Models;
using Core.Services;
using Database;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class PaymentService : IPaymentService
    {
        private readonly AppDbContext _context;

        public PaymentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddPayment(Payment payment)
        {
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePayment(Payment payment)
        {
            var existingRecord = await _context.Payments.FindAsync(payment.Id);
            if (existingRecord == null)
            {
                throw new Exception("Invalid Payment!");
            }

            existingRecord.FarmerId = payment.FarmerId;
            existingRecord.CollectionPeriodId = payment.CollectionPeriodId;
            existingRecord.TotalLitres = payment.TotalLitres;
            existingRecord.RatePerLitre = payment.TotalLitres;
            existingRecord.GrossAmount = payment.GrossAmount;
            existingRecord.NetAmount = payment.NetAmount;
            existingRecord.PaymentDate = payment.PaymentDate;
            existingRecord.TotalDeductions = payment.TotalDeductions;

            await _context.SaveChangesAsync();
        }

        public async Task<List<Payment>> GetPayments()
        {
            return await _context.Payments.ToListAsync();
        }

        public async Task<Payment> GetPaymentById(long id)
        {
            if (id is 0)
            {
                throw new Exception("Invalid Id");
            }

            return await _context.Payments.FindAsync(id);
        }

    }
}
