using Microsoft.EntityFrameworkCore;
using RentACarApp.Areas.Identity.Data;
using RentACarApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace RentACarApp.Data
{
    public class RentACarAppDbContext : IdentityDbContext<RentACarAppUser>
    {
        public RentACarAppDbContext(DbContextOptions<RentACarAppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Booking> Bookings { get; set; }


    }
}
