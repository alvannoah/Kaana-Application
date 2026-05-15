using Core.Models;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.ViewModels
{
    public class FarmerViewModel
    {
        private readonly IFarmerService _service;

        public ObservableCollection<Farmer> Farmers { get; set; }
            = new ObservableCollection<Farmer>();

        public string NewFarmerName { get; set; }

        public FarmerViewModel(IFarmerService service)
        {
            _service = service;
        }

        public async Task LoadFarmers()
        {
            var farmers = await _service.GetFarmers();

            Farmers.Clear();
            foreach (var f in farmers)
                Farmers.Add(f);
        }

        public async Task AddFarmer()
        {
            await _service.AddFarmer(new Farmer
            {
                Name = NewFarmerName
            });

            await LoadFarmers();
        }
    }
}
