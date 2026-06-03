using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IMilkBuyerService
    {
        Task AddMilkBuyer(MilkBuyer milkBuyer);
        Task<List<MilkBuyer>> GetMilkBuyers();
        Task<MilkBuyer> GetMilkBuyerById(long id);
        Task UpdateMilkBuyer(MilkBuyer milkBuyer);
    }
}
