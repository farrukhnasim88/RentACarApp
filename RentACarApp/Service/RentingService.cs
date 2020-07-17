﻿using RentACarApp.Data;
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

        public List<int> GetBookdedVehiclesById(DateTime hirdDate , DateTime returnDate)
        {
            List<int> BookedVehiclesId = new List<int>();
            if(GetAllBookings() != null && GetAllBookings().Count() > 0)
            {
                foreach (Booking b in GetAllBookings())
                {
                    //if the hdate falls between an existing booking that means the booking's car is not available
                 //   dateToCheck >= startDate && dateToCheck < endDate;
                    if ((hirdDate >= b.HireDate && hirdDate < b.ReturnDate) && (returnDate >= b.HireDate && returnDate < b.ReturnDate))
                    {
                        BookedVehiclesId.Add(b.Vehicle.Id);
                    }
                }

            }
            return BookedVehiclesId;

        }

        public List<Vehicle> GetAvailableVehicles(Booking booking)
        {
                      
            
            List<Vehicle> availableCars = new List<Vehicle>();
            List<Vehicle> BookedCars = new List<Vehicle>();
            if (GetAllVehicles() != null && GetAllVehicles().Count > 0)
            {
                foreach (Vehicle car in GetAllVehicles())
                {
                    if (GetBookdedVehiclesById(booking.HireDate , booking.ReturnDate).Contains(car.Id))
                    {
                        BookedCars.Add(car);
                    }
                    else
                    {
                        availableCars.Add(car);
                    }
                }
            }

            return availableCars;
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
            var existingBookedVehicles = 0;

            foreach (var existingBooking in existingBookings)
            {
                // This is only if existingTimeBlocks are ordered. It is only
                // a speedup
                if (HireDate > existingBooking.ReturnDate)
                {

                   // do nothing
                    
                }

                // This is the real check. The ordering of the existingTimeBlocks
                // is irrelevant
                if (HireDate <= existingBooking.ReturnDate && ReturnDate >= existingBooking.HireDate)
                {
                    existingId.Add(existingBooking.VehicleId);
                }
            }
            return existingId;
        }


        public List<Vehicle> FindAvailableVehicles(Booking booking, IEnumerable<Booking> bookings)
        {
           List<int> ids= FindOverlapBookingsId(booking.HireDate, booking.ReturnDate, bookings);
            List<Vehicle> allVehicls = new List<Vehicle>();
            allVehicls = _context.Vehicles.ToList();

            List<Vehicle> availableVehicles = new List<Vehicle>();

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