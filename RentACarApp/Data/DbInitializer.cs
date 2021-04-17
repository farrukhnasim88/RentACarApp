using RentACarApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentACarApp.Data
{
    public class DbInitializer
    {
        public static void VehicleInitializer(RentACarAppDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Vehicles.Any())
            {
                return;   // DB has been seeded
            }

            var status = new Status[]
            {
                new Status {   Name="Available"},
                new Status {  Name="Hired"},
                new Status {   Name="Under Service"},
                new Status {   Name="Accident Repair"}
            };

            foreach (Status s in status)
            {
                context.Status.Add(s);
            }
            context.SaveChanges();

            var locations = new Location[]
            {
                new Location {   Name="Parramatta" , Address="18 Smith Street"},
                new Location {  Name="Mascot" , Address="4 High Street"},
                new Location {   Name="Chatswood" , Address="23 Victoria Street"},
                new Location {   Name="Bondi Junction", Address="78 Oxford Street"}
            };

            foreach (Location l in locations)
            {
                context.Locations.Add(l);
            }
            context.SaveChanges();


            var vehicles = new Vehicle[]
           {
                new Vehicle { Make = "Toyota",   Model="Corolla",
                    Color ="Yellow", Kilometer=11192, Year=2015, RatePerDay=100, ImageUrl="/images/corolla.jpg", LocationId=1,StatusId=1  },
                 new Vehicle { Make = "Toyota",   Model="Corolla",
                    Color ="White", Kilometer=111262, Year=2016, RatePerDay=90, ImageUrl="/images/corolla1.jpg",LocationId=2,StatusId=1 },
                  new Vehicle { Make = "Toyota",   Model="Corolla",
                    Color ="Green", Kilometer=41220, Year=2019, RatePerDay=150,
                      ImageUrl="~/images/corolla3.jpg" ,LocationId=3,StatusId=1 },
                   new Vehicle { Make = "Toyota",   Model="Kluger",
                    Color ="Grey", Kilometer=91109, Year=2019, RatePerDay=150, ImageUrl="/images/kluger.jpg",LocationId=4,StatusId=1 },
                    new Vehicle { Make = "Toyota",   Model="Rav4",
                    Color ="White", Kilometer=23098, Year=2020, RatePerDay=130, ImageUrl="/images/rav4.jpg",LocationId=1,StatusId=1 },
                     new Vehicle { Make = "Honda",   Model="Accord",
                    Color ="Blue", Kilometer=91300, Year=2018, RatePerDay=100, ImageUrl="/images/accord1.jpg",LocationId=2,StatusId=1 },
                      new Vehicle { Make = "Honda",   Model="Accord",
                    Color ="Black", Kilometer=61821, Year=2019, RatePerDay=110, ImageUrl="/images/accord2.jpg",LocationId=3,StatusId=1 },
                       new Vehicle { Make = "Audi",   Model="X4",
                    Color ="White", Kilometer=41398, Year=2019, RatePerDay=190, ImageUrl="/images/audi.jpg",LocationId=4,StatusId=1 },
                        new Vehicle { Make = "Honda",   Model="City",
                    Color ="Blue", Kilometer=111387, Year=2018, RatePerDay=100, ImageUrl="/images/city.jpg",LocationId=1,StatusId=1 },
                         new Vehicle { Make = "Ford",   Model="Falcon",
                    Color ="Silver", Kilometer=61692, Year=2019, RatePerDay=130, ImageUrl="/images/falcon.jpg",LocationId=2,StatusId=1 },
                          new Vehicle { Make = "Ford",   Model="Falcon",
                    Color ="Black", Kilometer=211093, Year=2015, RatePerDay=100, ImageUrl="/images/falcon2.jpg",LocationId=3,StatusId=1 },
                           new Vehicle { Make = "Ford",   Model="Falcon",
                    Color ="Black", Kilometer=41504, Year=2015, RatePerDay=100, ImageUrl="/images/falcon3.jpg",LocationId=4,StatusId=1 },
                            new Vehicle { Make = "Ford",   Model="XR6",
                    Color ="White", Kilometer=21598, Year=2020, RatePerDay=200, ImageUrl="/images/fordxr6.jpg",LocationId=1,StatusId=1 },
                             new Vehicle { Make = "Holden",   Model="Commodore",
                    Color ="White", Kilometer=211205, Year=2015, RatePerDay=100, ImageUrl="/images/holden.jpg",LocationId=2,StatusId=1 },
                              new Vehicle { Make = "Holden",   Model="Caprice",
                    Color ="Silver", Kilometer=21298, Year=2019, RatePerDay=150, ImageUrl="/images/holden2.jpg",LocationId=3,StatusId=1 },
                               new Vehicle { Make = "Mercedes",   Model="Benz",
                    Color ="Grey", Kilometer=21245, Year=2019, RatePerDay=300, ImageUrl="/images/mercedes.jpg",LocationId=4,StatusId=1 },
                                new Vehicle { Make = "Mitsubishi",   Model="V5",
                    Color ="Grey", Kilometer=21453, Year=2019, RatePerDay=100, ImageUrl="/images/mitsubishi.jpg",LocationId=1,StatusId=1 },
                                 new Vehicle { Make = "Ford",   Model="Mustang",
                    Color ="Yellow", Kilometer=21786, Year=2019, RatePerDay=200, ImageUrl="/images/mustan2.jpg",LocationId=2,StatusId=1 },
                                  new Vehicle { Make = "Ford",   Model="Mustang",
                    Color ="Black", Kilometer=21873, Year=2019, RatePerDay=300, ImageUrl="/images/mustang.jpg",LocationId=3,StatusId=1 },
           };



            foreach (Vehicle v in vehicles)
            {
                context.Vehicles.Add(v);
            }
            context.SaveChanges();



        }
    }
}
