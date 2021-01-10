using Planner.Data.Interfaces;
using Planner.Data.Models;

namespace Planner.Data.Repositories
{
    public class StateRepository: BaseRepository<State>, IStateRepository
    {
        public StateRepository(ApplicationDbContext dbContext) : base(dbContext)
        { }
    }
}
