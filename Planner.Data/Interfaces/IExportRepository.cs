using Planner.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planner.Data.Interfaces
{
    public interface IExportRepository : IRepository<Export>
    {
        IEnumerable<Export> FindByRouteId(int routeId);
    }
}
