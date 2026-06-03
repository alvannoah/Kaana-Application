
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface ICollectionPeriodService
    {
        Task AddCollectionPeriod(CollectionPeriod collectionPeriod);
        Task<CollectionPeriod> GetCollectionPeriodById(long id);
        Task<List<CollectionPeriod>> GetCollectionPeriods();
        Task UpdateCollectionPeriod(CollectionPeriod collectionPeriod);
    }
}
