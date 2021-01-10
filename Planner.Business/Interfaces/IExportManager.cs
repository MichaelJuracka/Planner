using Planner.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planner.Business.Interfaces
{
    public interface IExportManager
    {
        void Add(byte[] file, string name, int routeId);
        IEnumerable<Export> GetFilesByRoute(int routeId);
    }
}
