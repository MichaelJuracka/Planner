using Planner.Data.Models;
using System.Collections.Generic;

namespace Planner.Business.Interfaces
{
    public interface IExportManager
    {
        void Add(byte[] file, string name, int routeId);
        IEnumerable<Export> GetFilesByRoute(int routeId);
    }
}
