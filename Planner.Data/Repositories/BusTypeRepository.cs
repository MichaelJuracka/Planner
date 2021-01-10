using Planner.Data.Interfaces;
using Planner.Data.Models;

namespace Planner.Data.Repositories
{
    public class BusTypeRepository : BaseRepository<BusType>, IBusTypeRepository
    {
        public BusTypeRepository(ApplicationDbContext dbContext) : base(dbContext)
        { }
    }
}
