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

        public CollectionPeriod CreatePeriod(DateTime start, DateTime end)
        {
            var period = new CollectionPeriod
            {
                StartDate = start,
                EndDate = end,
                IsClosed = false
            };

            _context.CollectionPeriods.Add(period);
            _context.SaveChangesAsync();

            return period;
        }

        public async Task<List<CollectionPeriod>> GetAll()
        {
            return _context.CollectionPeriods
                .OrderByDescending(p => p.StartDate)
                .ToList();
        }

        public CollectionPeriod GetCurrentPeriod(DateTime date)
        {
            var period = _context.CollectionPeriods
                .FirstOrDefault(p => date >= p.StartDate && date <= p.EndDate);

            if (period != null)
            {
                return period;
            }

            DateTime startDate;
            DateTime endDate;
            string periodName;

            if (date.Day <= 15)
            {
                startDate = new DateTime(date.Year, date.Month, 1);
                endDate = new DateTime(date.Year, date.Month, 15);
                periodName = date.ToString("yyyy-MM") + " (1st - 15th)";
            }
            else
            {
                startDate = new DateTime(date.Year, date.Month, 16);
                endDate = new DateTime(date.Year, date.Month, 1).AddMonths(1).AddDays(-1);
                periodName = date.ToString("yyyy-MM") + " (16th - End)";
            }

            var newPeriod = new CollectionPeriod
            {
                Name = periodName,
                StartDate = startDate,
                EndDate = endDate,
                IsClosed = false
            };

            _context.CollectionPeriods.Add(newPeriod);
            _context.SaveChangesAsync();

            return newPeriod;
        }

        public async Task ClosePeriod(long periodId)
        {
            var period = _context.CollectionPeriods
                .FirstOrDefault(p => p.Id == periodId);

            if (period == null)
                throw new Exception("Period not found");

            period.IsClosed = true;

            _context.SaveChangesAsync();
        }
        public async Task<OperationResult> EnsurePeriodIsOpen(long periodId)
        {
            var period = await _context.CollectionPeriods
                .FirstOrDefaultAsync(x => x.Id == periodId);

            if (period == null)
                return OperationResult.Fail("Period not found");

            if (period.IsClosed)
                return OperationResult.Fail("This period is closed.");

            return OperationResult.Ok();
        }
    }
}
