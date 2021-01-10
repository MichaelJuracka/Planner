using Planner.Data.Models;
using System.Collections.ObjectModel;

namespace Planner.Business.Interfaces
{
    public interface IProviderManager
    {
        Provider Add(string name);
        ObservableCollection<Provider> GetAll();
        void Delete(int providerId);
    }
}
