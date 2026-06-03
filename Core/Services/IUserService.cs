using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IUserService
    {
        Task AddUser(User user);
        Task<User> GetUserById(long id);
        Task<List<User>> GetUsers();
        Task UpdateUser(User user);
    }
}
