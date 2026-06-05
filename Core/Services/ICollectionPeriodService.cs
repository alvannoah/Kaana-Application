
using Core.Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface ICollectionPeriodService
    {
        CollectionPeriod GetCurrentPeriod(DateTime date);
        CollectionPeriod CreatePeriod(DateTime start, DateTime end);
        Task<List<CollectionPeriod>> GetAll();
        Task ClosePeriod(long periodId);
        Task<OperationResult> EnsurePeriodIsOpen(long periodId);
    }
}
