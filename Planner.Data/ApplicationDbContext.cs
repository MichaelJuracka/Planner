using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Planner.Data.Models;
using Planner.Data.Models.Email;

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
        public DbSet<Owner> Owners { get; set; }

        public DbSet<Email> Emails { get; set; }
        public DbSet<EmailUser> EmailUsers { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        public DbSet<EmailAttachment> EmailAttachments { get; set; }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //builder.Entity<Passenger>()
            //    .HasOptional(r => r.Route)
            //    .WithRequired()
            //    .WillCascadeOnDelete(false);
        }
    }
}
