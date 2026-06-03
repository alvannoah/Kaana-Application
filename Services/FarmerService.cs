using Core.Models;
using Core.Services;
using Database;
using Microsoft.EntityFrameworkCore;

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

        public async Task UpdateFarmer(Farmer farmer)
        {
            var existingFarmer = await _context.Farmers.FindAsync(farmer.Id);
            if (existingFarmer == null)
            {
                throw new Exception("Invalid person");
            }
          
            existingFarmer.FirstName = farmer.FirstName;
            existingFarmer.LastName = farmer.LastName;
            existingFarmer.Email = farmer.Email;
            existingFarmer.PrimaryPhone = farmer.PrimaryPhone;
            existingFarmer.SecondaryPhone = farmer.SecondaryPhone;            

            await _context.SaveChangesAsync();
        }

        public async Task<List<Farmer>> GetFarmers()
        {
            return await _context.Farmers.ToListAsync();
        }

        public async Task<Farmer> GetFarmerById(long id)
        {
            if (id is 0 )
            {
                throw new Exception("Invalid Id");
            }

            return await _context.Farmers.FindAsync(id);
        }

    }
}
