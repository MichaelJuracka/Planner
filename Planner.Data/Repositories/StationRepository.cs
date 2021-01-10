using Planner.Data.Interfaces;
using Planner.Data.Models;
using System.Linq;

namespace Planner.Data.Repositories
{
    public class StationRepository : BaseRepository<Station>, IStationRepository
    {
        public StationRepository(ApplicationDbContext context) : base(context) { }
        public Station FindByName(string name)
        {
            return dbSet.SingleOrDefault(x => x.Name == name);
        }
    }
}
