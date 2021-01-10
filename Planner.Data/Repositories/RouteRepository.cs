using Planner.Data.Interfaces;
using Planner.Data.Models;

namespace Planner.Data.Repositories
{
    public class RouteRepository: BaseRepository<Route>, IRouteRepository
    {
        public RouteRepository(ApplicationDbContext dbContext) : base(dbContext)
        { }
    }
}
