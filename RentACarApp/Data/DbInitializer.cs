using RentACarApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentACarApp.Data
{
    public class DbInitializer
    {
        public static void VehicleInitializer(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Vehicles.Any())
            {
                return;   // DB has been seeded
            }

            var status = new Status[]
            {
                new Status {Name="Available"},
                new Status {Name="Hired"},
                new Status {Name="Under Service"},
                new Status {Name="Accident Repair"}
            };

            foreach (Status s in status)
            {
                context.Status.Add(s);
            }
            context.SaveChanges();



            var vehicles = new Vehicle[]
           {
                new Vehicle { Make = "Toyota",   Model="Corolla",
                    Color ="Yellow", Kilometer=111222, Year=2015, RatePerDay=100, ImageUrl="/images/corolla.jpg"},
                 new Vehicle { Make = "Toyota",   Model="Corolla",
                    Color ="White", Kilometer=111262, Year=2016, RatePerDay=90, ImageUrl="/images/corolla1.jpg" },
                  new Vehicle { Make = "Toyota",   Model="Corolla",
                    Color ="Green", Kilometer=41222, Year=2019, RatePerDay=150, ImageUrl="/images/corolla3.jpg" },
                   new Vehicle { Make = "Toyota",   Model="Kluger",
                    Color ="Grey", Kilometer=91222, Year=2019, RatePerDay=150, ImageUrl="/images/kluger.jpg" },
                    new Vehicle { Make = "Toyota",   Model="Rav4",
                    Color ="White", Kilometer=23222, Year=2020, RatePerDay=130, ImageUrl="/images/rav4.jpg" },
                     new Vehicle { Make = "Honda",   Model="Accord",
                    Color ="Blue", Kilometer=91222, Year=2018, RatePerDay=100, ImageUrl="/images/accord1.jpg" },
                      new Vehicle { Make = "Honda",   Model="Accord",
                    Color ="Black", Kilometer=61222, Year=2019, RatePerDay=110, ImageUrl="/images/accord2.jpg" },
                       new Vehicle { Make = "Audi",   Model="X4",
                    Color ="White", Kilometer=41222, Year=2019, RatePerDay=190, ImageUrl="/images/audi.jpg" },
                        new Vehicle { Make = "Honda",   Model="City",
                    Color ="Blue", Kilometer=111222, Year=2018, RatePerDay=100, ImageUrl="/images/city.jpg" },
                         new Vehicle { Make = "Ford",   Model="Falcon",
                    Color ="Silver", Kilometer=61222, Year=2019, RatePerDay=130, ImageUrl="/images/falcon.jpg" },
                          new Vehicle { Make = "Ford",   Model="Falcon",
                    Color ="Black", Kilometer=211222, Year=2015, RatePerDay=100, ImageUrl="/images/falcon2.jpg" },
                           new Vehicle { Make = "Ford",   Model="Falcon",
                    Color ="Black", Kilometer=41222, Year=2015, RatePerDay=100, ImageUrl="/images/falcon3.jpg" },
                            new Vehicle { Make = "Ford",   Model="XR6",
                    Color ="White", Kilometer=21222, Year=2020, RatePerDay=200, ImageUrl="/images/fordxr6.jpg" },
                             new Vehicle { Make = "Holden",   Model="Commodore",
                    Color ="White", Kilometer=211222, Year=2015, RatePerDay=100, ImageUrl="/images/holden.jpg" },
                              new Vehicle { Make = "Holden",   Model="Caprice",
                    Color ="Silver", Kilometer=21222, Year=2019, RatePerDay=150, ImageUrl="/images/holden2.jpg" },
                               new Vehicle { Make = "Mercedes",   Model="Benz",
                    Color ="Grey", Kilometer=21222, Year=2019, RatePerDay=300, ImageUrl="/images/mercedes.jpg" },
                                new Vehicle { Make = "Mitsubishi",   Model="V5",
                    Color ="Grey", Kilometer=21222, Year=2019, RatePerDay=300, ImageUrl="/images/mitsubishi.jpg" },
                                 new Vehicle { Make = "Ford",   Model="Mustang",
                    Color ="Yellow", Kilometer=21222, Year=2019, RatePerDay=300, ImageUrl="/images/mustan2.jpg" },
                                  new Vehicle { Make = "Ford",   Model="Mustang",
                    Color ="Black", Kilometer=21222, Year=2019, RatePerDay=300, ImageUrl="/images/mustang.jpg" }, 
           };



            foreach (Vehicle v in vehicles)
            {
                context.Vehicles.Add(v);
            }
            context.SaveChanges();



        }
    }
}
