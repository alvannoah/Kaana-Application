using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IFarmerService
    {
        Task AddFarmer(Farmer farmer);
        Task<List<Farmer>> GetFarmers();
    }
}
