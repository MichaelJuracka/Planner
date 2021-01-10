using Planner.Business.Interfaces;
using Planner.Data.Interfaces;
using Planner.Data.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planner.Business.Managers
{
    public class RegionManager : IRegionManager
    {
        private IRegionRepository regionRepository;
        public RegionManager(IRegionRepository regionRepository)
        {
            this.regionRepository = regionRepository;
        }
        public Region Add(string name, State state)
        {
            if (name.Length == 0)
                throw new ArgumentException("Vyplňte jméno");
            if (state == null)
                throw new ArgumentException("Vyberte stát");

            var region = new Region()
            {
                Name = name,
                StateId = state.StateId
            };

            regionRepository.Insert(region);

            return region;
        }

        public void Delete(int regionId)
        {
            regionRepository.Delete(regionId);
        }

        public ObservableCollection<Region> GetAll()
        {
            return regionRepository.GetAll();
        }
    }
}
