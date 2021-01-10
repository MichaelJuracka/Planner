using Planner.Data.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Planner.Business.Interfaces
{
    public interface IStationManager
    {
        Station Add(string name, string departureTime, string departurePlace, string description, string gps, bool boardingStation, Region region);
        Station Update(Station station, string name, string departureTime, string departurePlace, string description, string gps, bool boardingStation, Region region);
        ObservableCollection<Station> GetAll();
        void Delete(int stationId);
        IEnumerable<Station> FilterStations(IEnumerable<Station> stations, string id, string name, Region region, bool? isBoardingStation = null);
        void UpdateOrder(int stationId, int order);
    }
}
