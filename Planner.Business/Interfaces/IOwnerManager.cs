using Planner.Data.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Planner.Business.Interfaces
{
    public interface IOwnerManager
    {
        ObservableCollection<Owner> GetAll();
        Owner Add(string name, string email);
        IEnumerable<Owner> FilterOwners(IEnumerable<Owner> owners, string name, string id, string email);
        Owner UpdateOwner(Owner owner, string name, string email);
    }
}
