using Microsoft.EntityFrameworkCore;
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
        private readonly RentACarAppDbContext _context;

        public RentingService(RentACarAppDbContext context)
        {
            _context = context;
        }

        public List<Booking> GetAllBooking()
        {
            var applicationDbContext = _context.Bookings.Include(b => b.Location).Include(b => b.Vehicle).ToList();
            return applicationDbContext;
        }
      
        public List<Booking> GetBookingsByCustomerId(string id)
        {
            var bookings = _context.Bookings.Where (b => b.CustomerId.Equals( id)).Include(v => v.Vehicle).Include(l => l.Location);
            return bookings.ToList();
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
        // this method will find existing bookings Vehicle Ids
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

        //Find Vehicles that do not have booking by location 
        public List<Vehicle> FindAvailableVehicles(Booking booking, IEnumerable<Booking> bookings, int locationId)
        {
           List<int> ids= FindOverlapBookingsId(booking.HireDate, booking.ReturnDate, bookings);
            List<Vehicle> allVehicls = _context.Vehicles.Where(p => p.LocationId==locationId && !ids.Any(q => q == p.Id)).ToList();
            return allVehicls;

        }

        public void AddBooking(Booking booking)
        {
            _context.Add(booking);
            _context.SaveChanges();

        }
    }
}
