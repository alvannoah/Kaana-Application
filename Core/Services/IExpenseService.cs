using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IExpenseService
    {
        Task AddExpense(Expense expense);
        Task<List<Expense>> GetExpenses();
        Task<Expense> GetExpenseById(long id);
        Task UpdateExpense(Expense expense);
    }
}
