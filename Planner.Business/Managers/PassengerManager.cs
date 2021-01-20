using Planner.Business.Interfaces;
using Planner.Business.Model;
using Planner.Data.Interfaces;
using Planner.Data.Models;
using Planner.Data.Models.Email;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Planner.Business.Managers
{
    public class PassengerManager : IPassengerManager
    {
        private readonly IPassengerRepository passengerRepository;
        private readonly IEmailSender emailSender;
        private readonly IOfficeManager officeManager;
        private readonly IEmailManager emailManager;
        public PassengerManager(IPassengerRepository passengerRepository, IEmailSender emailSender, IOfficeManager officeManager, IEmailManager emailManager)
        {
            this.passengerRepository = passengerRepository;
            this.emailSender = emailSender;
            this.officeManager = officeManager;
            this.emailManager = emailManager;
        }
        public Passenger Add(string businessCase, string firstName, string secondName, string phone, string email, string additionalInformation, Owner owner, Route route, Route boardingRoute, Station boardingStation, Station exitStation)
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
            if (owner == null)
                throw new ArgumentException("Vyberte prodejce");
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
                OwnerId = owner.OwnerId,
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
        public IEnumerable<Passenger> FilterPassengers(IEnumerable<Passenger> passengers, string id, string businessCase, string firstName, string secondName, Owner owner, DateTime? dateFrom = null, DateTime? dateTo = null, Region region = null)
        {
            if (id.Length == 0 && businessCase.Length == 0 && firstName.Length == 0 && secondName.Length == 0 && dateFrom == null && dateTo == null && region == null && owner.OwnerId == 0)
                return passengers;

            if (id.Length != 0)
                passengers = passengers.Where(x => x.PassengerId == int.Parse(id));
            if (businessCase.Length != 0)
                passengers = passengers.Where(x => x.BusinessCase == int.Parse(businessCase));
            if (firstName.Length != 0)
                passengers = passengers.Where(x => x.FirstName.ToLower().Contains(firstName.ToLower()));
            if (secondName.Length != 0)
                passengers = passengers.Where(x => x.SecondName.ToLower().Contains(secondName.ToLower()));
            if (owner.OwnerId != 0)
                passengers = passengers.Where(x => x.OwnerId == owner.OwnerId);
            if (dateFrom != null)
                passengers = passengers.Where(x => x.Route.DepartureDate >= dateFrom);
            if (dateTo != null)
                passengers = passengers.Where(x => x.Route.DepartureDate <= dateTo);
            if (region != null)
                passengers = passengers.Where(x => x.BoardingStation.RegionId == region.RegionId);

            return passengers;
        }
        public Passenger Update(Passenger passenger, string firstName, string secondName, string phone, string email, string additionalInformation, Station boardingStation, Station exitStation, Owner owner)
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
            if (owner == null)
                throw new ArgumentException("Vyberte prodejce");

            passenger.FirstName = firstName;
            passenger.SecondName = secondName;
            passenger.Phone = phone;
            passenger.Email = email;
            passenger.AdditionalInformation = additionalInformation;
            passenger.BoardingStationId = boardingStation.StationId;
            passenger.ExitStationId = exitStation.StationId;
            passenger.Owner = owner;

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
        public void UpdateDepartureTime(Passenger passenger, string departureTime)
        {
            if (departureTime.Length == 0)
                departureTime = null;

            passenger.DepartureTime = departureTime;
            passengerRepository.Update(passenger);
        }
        public async Task ClearPassengers(IEnumerable<Passenger> passengers, EmailTemplate emailTemplate, EmailUser emailUser)
        {
            var emailContents = new List<EmailContent>();
            var businessCases = new List<int>();

            foreach (var p in passengers)
            {
                if (!businessCases.Contains(p.BusinessCase))
                    businessCases.Add(p.BusinessCase);
            }

            foreach (var b in businessCases)
            {
                var content = new EmailContent();
                var passenger = passengers.FirstOrDefault(x => x.BusinessCase == b);
                string body = emailTemplate.Body;
                string subject = emailTemplate.Subject;

                body = body.Replace("<Planner:Passenger=DepartureTime>", passenger.DepartureTime);
                body = body.Replace("<Planner:Passenger=BoardingStation>", passenger.BoardingStation.ToString());
                body = body.Replace("<Planner:Station=DeparturePlace>", passenger.BoardingStation.DeparturePlace);
                subject = subject.Replace("<Planner:Passenger=BusinessCase>", b.ToString());
                content.Body = body;
                content.Subject = subject;

                string receiverEmail;
                if (passenger.Owner.Email == null)
                {
                    if (passengerRepository.FindByBusinessCaseEmail(b) == null)
                        receiverEmail = emailUser.UserName;
                    else
                        receiverEmail = passengerRepository.FindByBusinessCaseEmail(b).Email;
                }
                else
                    receiverEmail = passenger.Owner.Email;
                content.ReceiverEmail = receiverEmail;

                var stations = new List<Station>();
                foreach (var p in passengers.Where(x => x.BusinessCase == b))
                {
                    if (!stations.Contains(p.BoardingStation))
                        stations.Add(p.BoardingStation);
                    p.IsCleared = true;
                }
                var stationDepartureTimes = new List<StationDepartureTimeClearance>();
                foreach (var s in stations)
                {
                    var departurePlaces = new StationDepartureTimeClearance()
                    {
                        Station = s,
                        DepartureTime = passengers.FirstOrDefault(x => x.BusinessCase == b && x.BoardingStationId == s.StationId).DepartureTime
                    };
                }
                var email = emailManager.AddEmail(DateTime.Now, content.Subject, content.Body, emailUser.EmailUserId, emailTemplate.EmailTemplateId);
                content.Attachment = officeManager.UpdateClearanceWord(b, $"{passenger.FirstName} {passenger.SecondName}", passenger.Route, stationDepartureTimes, email.EmailId);
                emailContents.Add(content);
            }

            await emailSender.SendEmails(emailContents, emailUser);
        }
        public void Delete(int passengerId)
        {
            passengerRepository.Delete(passengerId);
        }
    }
}
