using Planner.Business.Interfaces;
using Planner.Data.Interfaces;
using Planner.Data.Models;
using System;
using System.Collections.Generic;

namespace Planner.Business.Managers
{
    public class ExportManager : IExportManager
    {
        private readonly IExportRepository exportRepository;
        public ExportManager(IExportRepository exportRepository)
        {
            this.exportRepository = exportRepository;
        }
        public void Add(byte[] file, string name, int routeId)
        {
            var export = new Export()
            {
                Name = name,
                Data = file,
                RouteId = routeId,
                Created = DateTime.Now
            };
            exportRepository.Insert(export);
        }
        public IEnumerable<Export> GetFilesByRoute(int routeId)
        {
            return exportRepository.FindByRouteId(routeId);
        }
    }
}
