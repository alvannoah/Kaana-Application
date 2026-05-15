using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface ICollectionCenterService
    {
        Task AddCollectionCenter(CollectionCenter collectionCenter);
        Task<List<CollectionCenter>> GetCollectionCenters();
    }
}
