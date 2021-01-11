using Planner.Data.Interfaces;
using Planner.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace Planner.Data.Repositories
{
    public class ExportRepository : BaseRepository<Export>, IExportRepository
    {
        public ExportRepository(ApplicationDbContext dbContext) : base(dbContext)
        { }
        public IEnumerable<Export> FindByRouteId(int routeId)
        {
            return dbSet.Where(x => x.RouteId == routeId);
        }
    }
}
