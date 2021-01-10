using Planner.Data.Interfaces;
using Planner.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace Planner.Data.Repositories
{
    public class PassengerRepository : BaseRepository<Passenger>, IPassengerRepository
    {
        public PassengerRepository(ApplicationDbContext dbContext) : base(dbContext)
        { }
        public void ImportPassengers(List<Passenger> list)
        {
            dbSet.AddRange(list);
        }
        public Passenger FindByBusinessCaseEmail(int businessCase)
        {
            return dbSet.FirstOrDefault(x => x.BusinessCase == businessCase && x.Email != null);
        }
    }
}
