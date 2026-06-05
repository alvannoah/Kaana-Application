using Core.DTOs;
using Core.Services;
using Database;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class DashboardService : IDashBoardService
    {
        private readonly AppDbContext _context;
        private readonly ICollectionPeriodService _periodService;

        public DashboardService(AppDbContext context, ICollectionPeriodService periodService)
        {
            _context = context;
            _periodService = periodService;
        }

        public async Task<DashboardDto> GetDashboardAsync()
        {
            var dashboard = new DashboardDto();


            var farmers = await _context.Farmers.ToListAsync();
            var collections = await _context.MilkCollections.ToListAsync();
            var payments = await _context.Payments.ToListAsync();
            var expenses = await _context.Expenses.ToListAsync();
            var advances = await _context.Advances.ToListAsync();

            dashboard.TotalFarmers = farmers.Count;

            dashboard.TotalMilkCollected =
                collections.Sum(x => x.Litres);

            dashboard.TotalRevenue =
                payments.Sum(x => x.NetAmount);

            dashboard.TotalExpenses =
                expenses.Sum(x => x.Amount);

            dashboard.NetProfit =
                dashboard.TotalRevenue - dashboard.TotalExpenses;

            dashboard.OutstandingAdvances =
                advances.Sum(x =>
                    Math.Max(0, x.Amount - x.RepaidAmount));


            var currentPeriod =
                await _context.CollectionPeriods
                    .FirstOrDefaultAsync(x => !x.IsClosed);

            if (currentPeriod != null)
            {
                dashboard.CurrentPeriodName = currentPeriod.Name;
                dashboard.IsPeriodClosed = false;

                dashboard.ActiveFarmersThisPeriod =
                    collections
                        .Where(x => x.CollectionPeriodId == currentPeriod.Id)
                        .Select(x => x.FarmerId)
                        .Distinct()
                        .Count();
            }


            dashboard.TopFarmers =
                collections
                    .GroupBy(x => x.FarmerId)
                    .Select(g => new TopFarmerDto
                    {
                        FarmerName = farmers
                            .First(f => f.Id == g.Key)
                            .FullName,

                        TotalLitres = g.Sum(x => x.Litres)
                    })
                    .OrderByDescending(x => x.TotalLitres)
                    .Take(5)
                    .ToList();

            // Outstanding Advances

            dashboard.OutstandingAdvancesList =
                advances
                    .Where(x => x.Amount > x.RepaidAmount)
                    .Select(x => new OutstandingAdvanceDto
                    {
                        FarmerName = farmers
                            .First(f => f.Id == x.FarmerId)
                            .FullName,

                        OutstandingBalance =
                            x.Amount - x.RepaidAmount
                    })
                    .OrderByDescending(x => x.OutstandingBalance)
                    .Take(10)
                    .ToList();

            return dashboard;
        }
    }
}