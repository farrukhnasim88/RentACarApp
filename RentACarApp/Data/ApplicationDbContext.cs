using Microsoft.EntityFrameworkCore;
using RentACarApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentACarApp.Data
{
    public class ApplicationDbContext : DbContext
    {
         

        public ApplicationDbContext( DbContextOptions<ApplicationDbContext> options ): base(options)
        {
                
        }


        public DbSet <Vehicle> Vehicles { get; set; }
        public DbSet <Status> Status { get; set; }





    }
}
