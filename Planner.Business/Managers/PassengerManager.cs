using Planner.Business.Interfaces;
using Planner.Data.Interfaces;
using Planner.Data.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planner.Business.Managers
{
    public class PassengerManager : IPassengerManager
    {
        private readonly IPassengerRepository passengerRepository;
        private readonly IEmailSender emailSender;
        public PassengerManager(IPassengerRepository passengerRepository, IEmailSender emailSender)
        {
            this.passengerRepository = passengerRepository;
            this.emailSender = emailSender;
        }
        public Passenger Add(string businessCase, string firstName, string secondName, string phone, string email, string additionalInformation, string owner, Route route, Route boardingRoute, Station boardingStation, Station exitStation)
        {
            if (businessCase.Length == 0)
                throw new ArgumentException("Vyplňte obchodní případ");
            if (firstName.Length == 0)
                throw new ArgumentException("Vyplňte jméno");
            if (secondName.Length == 0)
                throw new ArgumentException("Vyplňte přijmení");
            if (phone.Length == 0)
                phone = null;
            if (email.Length == 0)
                email = null;
            if (additionalInformation.Length == 0)
                additionalInformation = null;
            if (owner.Length == 0)
                owner = null;
            if (route == null)
                throw new ArgumentException("Vyberte jízdu");
            if (boardingStation == null)
                throw new ArgumentException("Vyberte nástupní stanici");
            if (exitStation == null)
                throw new ArgumentException("Vyberte vástupní stanici");

            var passenger = new Passenger()
            {
                BusinessCase = int.Parse(businessCase),
                FirstName = firstName,
                SecondName = secondName,
                Phone = phone,
                Email = email,
                AdditionalInformation = additionalInformation,
                Owner = owner,
                RouteId = route.RouteId,
                BoardingStationId = boardingStation.StationId,
                ExitStationId = exitStation.StationId
            };
            if (!route.RouteBack)
                passenger.DepartureTime = boardingStation.DepartureTime;
            if (boardingRoute != null)
                passenger.BoardingRouteId = boardingRoute.RouteId;
            else
                passenger.BoardingRouteId = null;

            passengerRepository.Insert(passenger);

            return passenger;
        }
        public ObservableCollection<Passenger> GetAll()
        {
            return passengerRepository.GetAll();
        }
        public IEnumerable<Passenger> FilterPassengers(IEnumerable<Passenger> passengers, string id, string businessCase, string firstName, string secondName, DateTime? dateFrom = null, DateTime? dateTo = null, Region region = null)
        {
            int? filterId = null;
            int? filterBusinessCase = null;
            if (id.Length != 0)
                filterId = int.Parse(id);
            if (businessCase.Length != 0)
                filterBusinessCase = int.Parse(businessCase);

            IEnumerable<Passenger> filterPassengers = passengers;

            if (filterId == null && filterBusinessCase == null && firstName.Length == 0 && secondName.Length == 0 && dateFrom == null && dateTo == null && region.RegionId == 0)
            {
                return passengers.OrderBy(x => x.ExitStation.Order);
            }
            else
            {
                if (filterId != null)
                    filterPassengers = filterPassengers.Where(x => x.PassengerId == filterId);
                if (filterBusinessCase != null)
                    filterPassengers = filterPassengers.Where(x => x.BusinessCase == filterBusinessCase);
                if (firstName.Length != 0)
                    filterPassengers = filterPassengers.Where(x => x.FirstName.ToLower().Contains(firstName.ToLower()));
                if (secondName.Length != 0)
                    filterPassengers = filterPassengers.Where(x => x.SecondName.ToLower().Contains(secondName.ToLower()));
                if (dateFrom != null)
                    filterPassengers = filterPassengers.Where(x => x.Route.DepartureDate >= dateFrom);
                if (dateTo != null)
                    filterPassengers = filterPassengers.Where(x => x.Route.DepartureDate <= dateTo);
                if (region != null)
                    filterPassengers = filterPassengers.Where(x => x.BoardingStation.RegionId == region.RegionId);

                return filterPassengers.OrderBy(x => x.ExitStation.Order);
            }
        }
        public Passenger Update(Passenger passenger, string firstName, string secondName, string phone, string email, string additionalInformation, Station boardingStation, Station exitStation)
        {

            if (firstName.Length == 0)
                throw new ArgumentException("Jméno je povinné");
            if (secondName.Length == 0)
                throw new ArgumentException("Přijmení je povinné");
            if (phone.Length == 0)
                phone = null;
            if (email.Length == 0)
                email = null;
            if (additionalInformation.Length == 0)
                additionalInformation = null;
            if (boardingStation == null)
                throw new ArgumentException("Vyberte nástupní stanici");
            if (exitStation == null)
                throw new ArgumentException("Vyberte výstupní stanici");

            passenger.FirstName = firstName;
            passenger.SecondName = secondName;
            passenger.Phone = phone;
            passenger.Email = email;
            passenger.AdditionalInformation = additionalInformation;
            passenger.BoardingStationId = boardingStation.StationId;
            passenger.ExitStationId = exitStation.StationId;

            passengerRepository.Update(passenger);

            return passenger;
        }
        public void UpdateRoute(Passenger passenger, Route route, bool isRealRoute, bool boardingRoute)
        {
            if (route.IsRealRoute)
            {
                passenger.RealRouteId = route.RouteId;
                passengerRepository.Update(passenger);
            }
            if (route.BoardingRoute)
            {
                passenger.BoardingRouteId = route.RouteId;
                passengerRepository.Update(passenger);
            }
        }
        public void RemoveRoute(Passenger passenger, bool isRealRoute, bool boardingRoute)
        {
            if (isRealRoute)
            {
                passenger.RealRouteId = null;
                passengerRepository.Update(passenger);
            }
            if (boardingRoute)
            {
                passenger.BoardingRoute = null;
                passengerRepository.Update(passenger);
            }
        }
        public void ClearPassenger(IEnumerable<Passenger> passengers, string body, string receiverEmail = null)
        {
            List<int> businessCases = new List<int>();

            foreach (var p in passengers)
            {
                if (!businessCases.Contains(p.BusinessCase))
                    businessCases.Add(p.BusinessCase);
            }

            foreach (var b in businessCases)
            {
                var passenger = passengerRepository.FindByBusinessCaseEmail(b);

                string data = $"{passenger.DepartureTime}, {passenger.BoardingStation}, {passenger.BoardingStation.DeparturePlace}";
                string message = string.Format(body, data);

                emailSender.SendEmail("michael.juracka@email.cz", "Odbavení dopravy", body);
            }
        }
    }
}
