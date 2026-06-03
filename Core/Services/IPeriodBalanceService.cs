using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IPeriodBalanceService
    {
        Task AddPeriodBalance(PeriodBalance periodBalance);
        Task<PeriodBalance> GetPeriodBalanceById(long id);
        Task<List<PeriodBalance>> GetPeriodBalances();
        Task UpdatePeriodBalance(PeriodBalance periodBalance);
    }
}
