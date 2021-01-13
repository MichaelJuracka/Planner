using Planner.Data.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Planner.Business.Interfaces
{
    public interface IRegionManager
    {
        Region Add(string name, State state);
        ObservableCollection<Region> GetAll();
        void Delete(int regionId);
        IEnumerable<Region> FilterRegion(IEnumerable<Region> regions, string id, string name, State state);
    }
}
