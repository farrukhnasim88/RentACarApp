using RentACarApp.Data;
using RentACarApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentACarApp.Service
{
    public class RentingService
    { 
        private readonly ApplicationDbContext _context;

        public RentingService(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public List<Booking> GetAllBookings()
        {
            return _context.Bookings.ToList();
        }

        public List<Vehicle> GetAllVehicles()
        {
            var vehicles = _context.Vehicles.ToList();
            return vehicles;

        }

        public Vehicle  GetVehicleById(int id)
        {
            return _context.Vehicles.FirstOrDefault(v => v.Id == id);

        }

        public List<int> FindOverlapBookingsId(DateTime HireDate, DateTime ReturnDate, IEnumerable<Booking> existingBookings)
        {
            List<int> existingId = new List<int>();

            foreach (var existingBooking in existingBookings)
            {
                
                if (HireDate <= existingBooking.ReturnDate && ReturnDate >= existingBooking.HireDate)
                {
                    existingId.Add(existingBooking.VehicleId);
                }
            }
            return existingId;
        }


        public List<Vehicle> FindAvailableVehicles(Booking booking, IEnumerable<Booking> bookings, int locationId)
        {
           List<int> ids= FindOverlapBookingsId(booking.HireDate, booking.ReturnDate, bookings);
            List<Vehicle> allVehicls = new List<Vehicle>();
            allVehicls = _context.Vehicles.Where(l => l.LocationId== locationId).ToList();

            for (int i = 0; i < ids.Count(); i++)
            {
                var vRemove = allVehicls.SingleOrDefault(v => v.Id == ids[i]);
                if (vRemove != null)
                {
                    allVehicls.Remove(vRemove);
                }
            }

            return allVehicls;

        }

    }
}
