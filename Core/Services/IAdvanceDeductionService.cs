using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IAdvanceDeductionService
    {
        Task AddAdvanceDeduction(AdvanceDeduction advanceDeduction);
        Task<AdvanceDeduction> GetAdvanceDeductionById(long id);
        Task<List<AdvanceDeduction>> GetAdvanceDeductions();
        Task UpdateAdvanceDeduction(AdvanceDeduction advanceDeduction);
    }
}
