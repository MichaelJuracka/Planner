using Planner.Business.Interfaces;
using Planner.Data.Models;
using Planner.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;

namespace Planner.Business.Managers
{
    public class StationManager : IStationManager
    {
        private readonly IStationRepository stationRepository;
        public StationManager(IStationRepository stationRepository)
        {
            this.stationRepository = stationRepository;
        }
        public Station Add(string name, string departureTime, string departurePlace, string description, string gps, bool boardingStation, Region region)
        {
            if (name.Length == 0)
                throw new ArgumentException("Zadejte název stanice");
            if (boardingStation)
            {
                if (departureTime.Length == 0)
                    throw new ArgumentException("U nástupní stanice zadejte odjezdový čas");
            }
            else
            {
                if (departureTime.Length == 0)
                    departureTime = null;
            }
            if (departurePlace.Length == 0)
                throw new ArgumentException("Zadejte místo odjezdu");
            if (description.Length == 0)
                description = null;
            if (gps.Length == 0)
                gps = null;
            if (region == null)
                throw new ArgumentException("Vyberte oblast");

            var station = new Station()
            {
                Name = name,
                DepartureTime = departureTime,
                DeparturePlace = departurePlace,
                Description = description,
                Gps = gps,
                BoardingStation = boardingStation,
                RegionId = region.RegionId,
                Order = null
            };
            if (stationRepository.FindByName(station.Name) != null)
                throw new Exception("Tato zastávka je již v databázi");

            stationRepository.Insert(station);

            return station;
        }
        public ObservableCollection<Station> GetAll()
        {
            return stationRepository.GetAll();
        }
        public Station Update(Station station, string name, string departureTime, string departurePlace, string description, string gps, bool boardingStation, Region region)
        {
            if (name.Length == 0)
                throw new ArgumentException("Jméno je povinné");
            if (boardingStation)
            {
                if (departureTime.Length == 0)
                    throw new ArgumentException("U nástupní stanice je čas odjezdu povinný");
            }
            else
            {
                if (departureTime.Length == 0)
                    departureTime = null;
            }
            if (departurePlace.Length == 0)
                throw new ArgumentException("Místo odjezdu je povinné");
            if (description.Length == 0)
                description = null;
            if (gps.Length == 0)
                gps = null;
            if (region == null)
                throw new ArgumentException("Oblast je povinná");

            station.Name = name;
            station.DepartureTime = departureTime;
            station.DeparturePlace = departurePlace;
            station.Description = description;
            station.Gps = gps;
            station.BoardingStation = boardingStation;
            station.RegionId = region.RegionId;

            stationRepository.Update(station);

            return station;
        }
        public void Delete(int stationId)
        {
            stationRepository.Delete(stationId);
        }
        public IEnumerable<Station> FilterStations(IEnumerable<Station> stations, string id, string name, Region region, bool? isBoardingStation = null)
        {
            if (id.Length == 0 && name.Length == 0 && region.RegionId == 0 && isBoardingStation == null)
                return stations;

            if (id.Length != 0)
                stations = stations.Where(x => x.StationId == int.Parse(id));
            if (name.Length != 0)
                stations = stations.Where(x => x.Name.ToLower().Contains(name.ToLower()));
            if (region.RegionId != 0)
                stations = stations.Where(x => x.RegionId == region.RegionId);
            if (isBoardingStation != null)
                stations = stations.Where(x => x.BoardingStation == isBoardingStation);

            return stations;
        }
        public void UpdateOrder(int stationId, int order)
        {
            var station = stationRepository.FindById(stationId);

            station.Order = order;

            stationRepository.Update(station);
        }
    }
}
