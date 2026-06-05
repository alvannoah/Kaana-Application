using Core.Models;
using Core.Services;
using Database;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly AppDbContext _context;

        public ExpenseService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddExpense(Expense expense)
        {
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateExpense(Expense expense)
        {
            var existingRecord = await _context.Expenses.FindAsync(expense.Id);
            if (existingRecord == null)
            {
                throw new Exception("Invalid Expense!");
            }

            expense.Date = expense.Date;
            expense.Description = expense.Description;
            expense.Quantity = expense.Quantity;
            expense.Amount = expense.Amount;


            await _context.SaveChangesAsync();
        }

        public async Task<List<Expense>> GetExpenses()
        {
            return await _context.Expenses.ToListAsync();
        }

        public async Task<Expense> GetExpenseById(long id)
        {
            if (id is 0)
            {
                throw new Exception("Invalid Id");
            }

            return await _context.Expenses.FindAsync(id);
        }

        public async Task Delete(long id)
        {
            var record = await _context.Expenses.FindAsync(id);
            if (record == null)
            {
                throw new Exception("Invalid Id");
            }
            _context.Expenses.Remove(record);
            await _context.SaveChangesAsync();

        }
    }
}
