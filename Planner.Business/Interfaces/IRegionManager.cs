using Planner.Data.Models;
using System.Collections.ObjectModel;

namespace Planner.Business.Interfaces
{
    public interface IRegionManager
    {
        Region Add(string name, State state);
        ObservableCollection<Region> GetAll();
        void Delete(int regionId);
    }
}
