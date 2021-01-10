using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using Planner.Data.Models;

namespace Planner.Data
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("name=ApplicationDbContext")
        {
        }
        public DbSet<BusType> BusTypes { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Export> Exports { get; set; }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
