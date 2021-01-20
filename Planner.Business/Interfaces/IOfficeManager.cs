using Planner.Business.Model;
using Planner.Data.Models;
using Planner.Data.Models.Email;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Planner.Business.Interfaces
{
    public interface IOfficeManager
    {
        List<Passenger> ImportPassengers(string filePath, Route route, IEnumerable<Station> boardingStations, IEnumerable<Station> exitStations, ObservableCollection<Owner> owners);
        List<Station> ImportStations(string filePath, IEnumerable<Region> regions);
        void RouteList(IEnumerable<Passenger> passengers, string filePath, int routeId, string fileName, bool isRealRoute);
        void UpdateOrder(string filePath, string fileName, Route route, IEnumerable<Passenger> passengers, IEnumerable<Passenger> passengersBack);
        void UpdateOrderWord(string filePath, string fileName, Route route, IEnumerable<Passenger> passengers);
        EmailAttachment UpdateClearanceWord(int businessCase, string name, Route route, IEnumerable<StationDepartureTimeClearance> departurePlaces, int emailId);
    }
}
