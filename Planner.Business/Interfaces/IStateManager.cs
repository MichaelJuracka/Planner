using Planner.Data.Models;
using System.Collections.ObjectModel;

namespace Planner.Business.Interfaces
{
    public interface IStateManager
    {
        State Add(string name);
        ObservableCollection<State> GetAll();
        void Delete(int StateId);
    }
}
