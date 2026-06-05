using Core.Models;
using Core.Services;
using Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AdvanceService : IAdvanceService
    {
        private readonly AppDbContext _context;

        public AdvanceService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAdvance(Advance advance)
        {
            _context.Advances.Add(advance);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAdvance(Advance advance)
        {
            var existingAdvance = await _context.Advances.FindAsync(advance.Id);
            if (existingAdvance == null)
            {
                throw new Exception("Invalid advance!");
            }

            existingAdvance.FarmerId = advance.FarmerId;
            existingAdvance.DateIssued = advance.DateIssued;
            existingAdvance.Amount = advance.Amount;
            existingAdvance.RepaidAmount = advance.RepaidAmount;
            existingAdvance.Notes = advance.Notes;

            await _context.SaveChangesAsync();
        }

        public async Task<List<Advance>> GetAdvances()
        {
            return await _context.Advances.ToListAsync();
        }

        public async Task<Advance> GetAdvanceById(long id)
        {
            if (id is 0)
            {
                throw new Exception("Invalid Id");
            }

            return await _context.Advances.FindAsync(id);
        }

        public async Task Delete(long id)
        {
            var advance = await _context.Advances.FindAsync(id);
            if (advance == null)
            {
                throw new Exception("Invalid Id");
            }
            _context.Advances.Remove(advance);
            await _context.SaveChangesAsync();

        }
    }
}
