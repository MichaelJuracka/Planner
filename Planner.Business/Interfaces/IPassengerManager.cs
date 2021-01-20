using Planner.Data.Models;
using Planner.Data.Models.Email;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Planner.Business.Interfaces
{
    public interface IPassengerManager
    {
        ObservableCollection<Passenger> GetAll();
        Passenger Add(string businessCase, string firstName, string secondName, string phone, string email, string additionalInformation, Owner owner, Route route, Route boardingRoute, Station boardingStation, Station exitStation);
        IEnumerable<Passenger> FilterPassengers(IEnumerable<Passenger> passengers, string id, string businessCase, string firstName, string secondName, Owner owner, DateTime? dateFrom = null, DateTime? dateTo = null, Region region = null);
        Passenger Update(Passenger passenger, string firstName, string secondName, string phone, string email, string additionalInformation, Station boardingStation, Station exitStation, Owner owner);
        void UpdateRoute(Passenger passenger, Route route, bool isRealRoute, bool boardingRoute);
        void RemoveRoute(Passenger passenger, bool isRealRoute, bool boardingRoute);
        void UpdateDepartureTime(Passenger passenger, string departureTime);
        void Delete(int passengerId);
        Task ClearPassengers(IEnumerable<Passenger> passengers, EmailTemplate emailTemplate, EmailUser emailUser);
    }
}
