using Planner.Data.Models;
using System.Collections.Generic;

namespace Planner.Data.Interfaces
{
    public interface IPassengerRepository : IRepository<Passenger>
    {
        void ImportPassengers(List<Passenger> list);
        Passenger FindByBusinessCaseEmail(int businessCase);
    }
}
