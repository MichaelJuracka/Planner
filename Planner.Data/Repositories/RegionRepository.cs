using Planner.Data.Models;
using Planner.Data.Interfaces;

namespace Planner.Data.Repositories
{
    public class RegionRepository : BaseRepository<Region>, IRegionRepository
    {
        public RegionRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
