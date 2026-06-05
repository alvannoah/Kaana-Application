using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class DashboardDto
    {
        public int TotalFarmers { get; set; }
        public decimal TotalMilkCollected { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal NetProfit { get; set; }
        public decimal OutstandingAdvances { get; set; }
        public string CurrentPeriodName { get; set; }
        public bool IsPeriodClosed { get; set; }
        public int ActiveFarmersThisPeriod { get; set; }

        public List<TopFarmerDto> TopFarmers { get; set; } = new();
        public List<OutstandingAdvanceDto> OutstandingAdvancesList { get; set; } = new();
    }
}
