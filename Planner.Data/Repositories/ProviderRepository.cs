using Planner.Data.Interfaces;
using Planner.Data.Models;

namespace Planner.Data.Repositories
{
    public class ProviderRepository: BaseRepository<Provider>, IProviderRepository
    {
        public ProviderRepository(ApplicationDbContext dbContext) : base(dbContext)
        { }
    }
}
