using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IAdvanceService
    {
        Task AddAdvance(Advance advance);
        Task<Advance> GetAdvanceById(long id);
        Task<List<Advance>> GetAdvances();
        Task UpdateAdvance(Advance advance);
    }
}
