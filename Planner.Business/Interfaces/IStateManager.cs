using Planner.Data.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Planner.Business.Interfaces
{
    public interface IStateManager
    {
        State Add(string name);
        ObservableCollection<State> GetAll();
        void Delete(int StateId);
        IEnumerable<State> FilterStates(IEnumerable<State> states, string id, string name);
    }
}
