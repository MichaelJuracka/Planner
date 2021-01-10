namespace Planner.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Collections.Generic;
    using Planner.Data.Models;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Planner.Data.ApplicationDbContext context)
        {
            /*
            var states = new List<State>
            {
                new State{ StateId = 1, Name = "Chorvatsko"},
                new State{ StateId = 2, Name = "Itálie"}
            };
            states.ForEach(x => context.States.Add(x));
            context.SaveChanges();

            var regions = new List<Region>
            {
                new Region{ RegionId = 1, Name = "Istrie", StateId = 1},
                new Region{ RegionId = 2, Name = "Kvarner", StateId = 1},
                new Region{ RegionId = 3, Name = "Dalmacie", StateId = 1},
                new Region{ RegionId = 4, Name = "Palmová Riviéra", StateId = 2}
            };
            regions.ForEach(x => context.Regions.Add(x));
            context.SaveChanges(); */
        }
    }
}
