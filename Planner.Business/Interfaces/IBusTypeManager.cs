using Planner.Data.Models;
using System.Collections.ObjectModel;

namespace Planner.Business.Interfaces
{
    public interface IBusTypeManager
    {
        ObservableCollection<BusType> GetAll();
    }
}
