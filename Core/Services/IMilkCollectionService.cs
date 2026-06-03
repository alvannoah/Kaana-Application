using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IMilkCollectionService
    {
        Task AddMilkCollection(MilkCollection milkCollection);
        Task<List<MilkCollection>> GetMilkCollections();
        Task<MilkCollection> GetMilkCollectionById(long id);
        Task UpdateMilkCollection(MilkCollection farmer);
    }
}
