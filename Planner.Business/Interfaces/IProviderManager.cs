using Planner.Data.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Planner.Business.Interfaces
{
    public interface IProviderManager
    {
        Provider Add(string name);
        ObservableCollection<Provider> GetAll();
        void Delete(int providerId);
        IEnumerable<Provider> FilterProviders(IEnumerable<Provider> providers, string id, string name);
    }
}
