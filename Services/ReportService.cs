using Core.DTOs;
using Core.Services;
using Database;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class ReportService : IReportService
    {
        private readonly AppDbContext _context;
        public ReportService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<FarmerStatementDto>> GetFarmerStatement(long periodId)
        {
            var collections = await _context.MilkCollections
                .Where(x => x.CollectionPeriodId == periodId)
                .ToListAsync();

            var payments = await _context.Payments
                .Where(x => x.CollectionPeriodId == periodId)
                .ToListAsync();

            var farmers = await _context.Farmers.ToListAsync();

            var advances = await _context.Advances.ToListAsync();

            var result = new List<FarmerStatementDto>();

            foreach (var farmer in farmers)
            {
                var farmerCollections = collections
                    .Where(x => x.FarmerId == farmer.Id)
                    .ToList();

                var farmerPayments = payments
                    .Where(x => x.FarmerId == farmer.Id)
                    .ToList();

                var farmerAdvances = advances
                    .Where(x => x.FarmerId == farmer.Id)
                    .ToList();

                var totalLitres = farmerCollections.Sum(x => x.Litres);

                decimal milkRatePerLitre = 3400.00m;

                decimal gross = totalLitres * milkRatePerLitre;

                var deductions = farmerPayments.Sum(x => (decimal?)x.TotalDeductions) ?? 0;
                var paid = farmerPayments.Sum(x => (decimal?)x.NetAmount) ?? 0;

                var totalAdvanceIssued = farmerAdvances.Sum(x => x.Amount);
                var totalAdvanceRepaid = farmerAdvances.Sum(x => x.RepaidAmount);
                var advanceBalance = totalAdvanceIssued - totalAdvanceRepaid;

                var balance = gross - paid;

                result.Add(new FarmerStatementDto
                {
                    FarmerId = farmer.Id,
                    FarmerName = $"{farmer.FirstName} {farmer.LastName}".Trim(),

                    TotalLitres = totalLitres,
                    GrossAmount = gross,

                    TotalDeductions = deductions,
                    NetAmount = gross - deductions,

                    PaidAmount = paid,
                    Balance = balance,

                    AdvanceIssued = totalAdvanceIssued,
                    AdvanceRepaid = totalAdvanceRepaid,
                    AdvanceBalance = advanceBalance
                });
            }

            return result;
        }
    }
}
