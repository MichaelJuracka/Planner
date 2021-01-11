using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Planner.Data.Models;

namespace Planner.Business.Interfaces
{
    public interface IRouteManager
    {
        ObservableCollection<Route> GetAll();
        Route Add(DateTime? departureDate, bool routeBack, bool boardingRoute, bool isRealRoute, State state, Region region, Provider provider, string licensePlate, BusType busType);
        Route Update(Route route, Provider provider, string licensePlate, BusType busType);
        void Delete(int routeId);
        IEnumerable<Route> FilterRoutes(IEnumerable<Route> routes, DateTime? fromDate, DateTime? toDate, string Id, State state, Region region, bool routeTo, bool routeFrom, bool? filterOrderCreated = null, bool? filterAgendaCreated = null, bool? filterBoardingRoute = null);
        void UpdateAgenda(Route route, bool agendaCreated);
        void UpdateOrder(Route route, bool routeCreated);
    }
}
