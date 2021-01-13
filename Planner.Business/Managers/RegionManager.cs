using Planner.Business.Interfaces;
using Planner.Data.Interfaces;
using Planner.Data.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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
        public IEnumerable<Region> FilterRegion(IEnumerable<Region> regions, string id, string name, State state)
        {
            if (id.Length == 0 && name.Length == 0 && state.StateId == 0)
                return regions;

            if (id.Length != 0)
                regions = regions.Where(x => x.RegionId == int.Parse(id));
            if (name.Length != 0)
                regions = regions.Where(x => x.Name.ToLower().Contains(name.ToLower()));
            if (state.StateId != 0)
                regions = regions.Where(x => x.StateId == state.StateId);

            return regions;
        }
    }
}
