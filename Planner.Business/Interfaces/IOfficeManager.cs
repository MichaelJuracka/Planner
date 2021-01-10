using Planner.Data.Models;
using System.Collections.Generic;

namespace Planner.Business.Interfaces
{
    public interface IOfficeManager
    {
        List<Passenger> ImportPassengers(string filePath, Route route, IEnumerable<Station> boardingStations, IEnumerable<Station> exitStations);
        List<Station> ImportStations(string filePath, IEnumerable<Region> regions);
        void RouteList(IEnumerable<Passenger> passengers, string filePath, int routeId, string fileName, bool isRealRoute);
        void UpdateOrder(string filePath, string fileName, Route route, IEnumerable<Passenger> passengers, IEnumerable<Passenger> passengersBack);
        void UpdateOrderWord(string filePath, string fileName, Route route, IEnumerable<Passenger> passengers);
    }
}
