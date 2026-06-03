using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IMilkLoadingService
    {
        Task AddMilkLoading(MilkLoading milkloading);
        Task<List<MilkLoading>> GetMilkLoadings();
        Task<MilkLoading> GetMilkLoadingById(long id);
        Task UpdateMilkLoading(MilkLoading milkLoading);
    }
}
