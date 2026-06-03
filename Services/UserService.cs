using Core.Models;
using Core.Services;
using Database;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddUser(User User)
        {
            _context.Users.Add(User);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUser(User user)
        {
            var existingRecord = await _context.Users.FindAsync(user.Id);
            if (existingRecord == null)
            {
                throw new Exception("Invalid collection center!");
            }

            existingRecord.FirstName = user.FirstName;
            existingRecord.LastName = user.LastName;
            existingRecord.Email    = user.Email;
            existingRecord.Password = user.Password;

            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserById(long id)
        {
            if (id is 0)
            {
                throw new Exception("Invalid Id");
            }

            return await _context.Users.FindAsync(id);
        }

    }
}
