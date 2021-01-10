using Planner.Data.Models;

namespace Planner.Data.Interfaces
{
    public interface IStationRepository : IRepository<Station>
    {
        Station FindByName(string name);
    }
}
