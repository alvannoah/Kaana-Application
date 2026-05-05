using Core;
using Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class FarmerService : IFarmerService
    {
        private readonly AppDbContext _context;

        public FarmerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddFarmer(Farmer farmer)
        {
            _context.Farmers.Add(farmer);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Farmer>> GetFarmers()
        {
            return await _context.Farmers.ToListAsync();
        }
    }
}
