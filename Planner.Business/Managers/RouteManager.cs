using System;
using System.Collections.Generic;
using System.Linq;
using Planner.Data.Interfaces;
using Planner.Business.Interfaces;
using Planner.Data.Models;
using System.Collections.ObjectModel;

namespace Planner.Business.Managers
{
    public class RouteManager : IRouteManager
    {
        private readonly IRouteRepository routeRepository;
        public RouteManager(IRouteRepository routeRepository)
        {
            this.routeRepository = routeRepository;
        }
        public Route Add(DateTime? departureDate, bool routeBack, bool boardingRoute, bool isRealRoute, State state, Region region, Provider provider, string licensePlate, BusType busType)
        {
            if (departureDate == null)
                throw new ArgumentException("Vyberte datum odjezdu");
            if (state == null)
                throw new ArgumentException("Vyberte stát");
            if (region == null)
                throw new ArgumentException("Vyberte oblast");
            if (provider == null)
                throw new ArgumentException("Vyberte dopravce");

            if (isRealRoute)
            {
                if (licensePlate.Length == 0)
                    throw new ArgumentException("U reálné jízdy zadejte spz");
                if (busType.BusTypeId == 0)
                    throw new ArgumentException("U reálné jízdy vyberte typ autobusu");
            }

            var route = new Route()
            {
                DepartureDate = departureDate.Value,
                RouteBack = routeBack,
                BoardingRoute = boardingRoute,
                IsRealRoute = isRealRoute,
                StateId = state.StateId,
                RegionId = region.RegionId,
                ProviderId = provider.ProviderId,
                LicensePlate = licensePlate,
                OrderCreated = false,
                AgendaCreated = false
            };
            if (busType.BusTypeId == 0)
                route.BusTypeId = null;
            else
                route.BusTypeId = busType.BusTypeId;

            routeRepository.Insert(route);

            return route;
        }
        public void Delete(int routeId)
        {
            routeRepository.Delete(routeId);
        }
        public ObservableCollection<Route> GetAll()
        {
            return routeRepository.GetAll();
        }
        public Route Update(Route route, Provider provider, string licensePlate, BusType busType)
        {
            if (provider == null)
                throw new ArgumentException("Vyberte dopravce");
            if (route.IsRealRoute)
            {
                if (licensePlate.Length == 0)
                    throw new ArgumentException("U reálné jízdy je spz povinná");
                if (busType.BusTypeId == 0)
                    throw new ArgumentException("U reálné jízdy je typ autobusu povinný");
            }
            else
            {
                if (licensePlate.Length == 0)
                    licensePlate = null;
            }
            if (route.BoardingRoute)
            {
                if (licensePlate != null)
                    throw new ArgumentException("U svozové jízdy nesmí být spz");
                if (busType.BusTypeId != 0)
                    throw new ArgumentException("U svozové jízdy nesmí být typ autobusu");
            }

            route.LicensePlate = licensePlate;
            route.ProviderId = provider.ProviderId;
            if (busType.BusTypeId != 0)
                route.BusTypeId = busType.BusTypeId;
            else
                route.BusTypeId = null;
            
            routeRepository.Update(route);
            return route;
        }
        public void UpdateAgenda(Route route, bool agendaCreated)
        {
            route.AgendaCreated = agendaCreated;
            routeRepository.Update(route);
        }
        public void UpdateOrder(Route route, bool orderCreated)
        {
            route.OrderCreated = orderCreated;
            routeRepository.Update(route);
        }
        public IEnumerable<Route> FilterRoutes(IEnumerable<Route> routes, DateTime? fromDate, DateTime? toDate, string Id, State state, Region region,bool routeTo, bool routeFrom, bool? filterOrderCreated = null, bool? filterAgendaCreated = null, bool? filterBoardingRoute = null)
        {
            DateTime? filterDateFrom = fromDate;
            DateTime? filterDateTo = toDate;
            int? filterId = null;
            if (Id.Length != 0)
                filterId = int.Parse(Id);

            IEnumerable<Route> filterRoutes = routes;

            if (filterDateFrom == null && filterDateTo == null && filterId == null && state.StateId ==  0 && region.RegionId == 0 && filterOrderCreated == null && filterAgendaCreated == null && filterBoardingRoute == null && routeTo == true && routeFrom == true)
                return routes;
            else
            {
                if (filterDateFrom != null)
                    filterRoutes = filterRoutes.Where(x => x.DepartureDate >= filterDateFrom);
                if (filterDateTo != null)
                    filterRoutes = filterRoutes.Where(x => x.DepartureDate <= filterDateTo);
                if (filterId != null)
                    filterRoutes = filterRoutes.Where(x => x.RouteId == filterId);
                if (state.StateId != 0)
                    filterRoutes = filterRoutes.Where(x => x.StateId == state.StateId);
                if (region.RegionId != 0)
                    filterRoutes = filterRoutes.Where(x => x.RegionId == region.RegionId);
                if (filterOrderCreated != null)
                    filterRoutes = filterRoutes.Where(x => x.OrderCreated == filterOrderCreated);
                if (filterAgendaCreated != null)
                    filterRoutes = filterRoutes.Where(x => x.AgendaCreated == filterAgendaCreated);
                if (filterBoardingRoute != null)
                    filterRoutes = filterRoutes.Where(x => x.BoardingRoute == filterBoardingRoute);
                if (routeTo == false)
                    filterRoutes = filterRoutes.Where(x => x.RouteBack == true);
                if (routeFrom == false)
                    filterRoutes = filterRoutes.Where(x => x.RouteBack == false);

                return filterRoutes;
            }
        }
    }
}
