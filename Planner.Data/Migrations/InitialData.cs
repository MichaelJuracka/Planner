using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Planner.Data.Models;

namespace Planner.Data.Migrations
{
    public class InitialData
    {
        //Done
        public static void States(ApplicationDbContext context)
        {
            var states = new List<State>
            {
                new State{ StateId = 1, Name = "Chorvatsko"},
                new State{ StateId = 2, Name = "Itálie"}
            };
            states.ForEach(x => context.States.Add(x));
            context.SaveChanges();
        }
        public static void Regions(ApplicationDbContext context)
        {
            var regions = new List<Region>
            {
                new Region() { RegionId = 1, Name = "Istrie", StateId = 1 },
                new Region() { RegionId = 2, Name = "Kvarner", StateId = 1},
                new Region() { RegionId = 3, Name = "Dalmacie", StateId = 1},
                new Region() { RegionId = 4, Name = "Palmová Riviéra", StateId = 2}
            };
            regions.ForEach(x => context.Regions.Add(x));
            context.SaveChanges();
        }
        public static void Providers(ApplicationDbContext context)
        {
            var providers = new List<Provider>
            {
                new Provider() { ProviderId = 1, Name = "ČSAD Tišnov, spol. s.r.o."}
            };
            providers.ForEach(x => context.Providers.Add(x));
            context.SaveChanges();
        }
        public static void Stations(ApplicationDbContext context)
        {
            //var stations = new List<Station>
            //{
            //    new Station() { StationId = 1, Name = "Ostrava", DepartureTime = new DateTime( 1900, 1, 1, 14, 0, 0), DeparturePlace = "Parkoviště vedle domu kultury, města Ostravy, ul. 28.října", Gps = "49.830973,18.274327", IsInCze = true, Description = ""}
            //};
            //stations.ForEach(x => context.Stations.Add(x));
            //context.SaveChanges();
        }

    }
}
