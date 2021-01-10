using Planner.Data.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planner.Business.Interfaces
{
    public interface IPassengerManager
    {
        ObservableCollection<Passenger> GetAll();
        Passenger Add(string businessCase, string firstName, string secondName, string phone, string email, string additionalInformation, string owner, Route route, Route boardingRoute, Station boardingStation, Station exitStation);
        IEnumerable<Passenger> FilterPassengers(IEnumerable<Passenger> passengers, string id, string businessCase, string firstName, string secondName, DateTime? dateFrom = null, DateTime? dateTo = null, Region region = null);
        Passenger Update(Passenger passenger, string firstName, string secondName, string phone, string email, string additionalInformation, Station boardingStation, Station exitStation);
        void UpdateRoute(Passenger passenger, Route route, bool isRealRoute, bool boardingRoute);
        void RemoveRoute(Passenger passenger, bool isRealRoute, bool boardingRoute);
        void ClearPassenger(IEnumerable<Passenger> passengers, string body, string receiverEmail = null);
    }
}
