using Microsoft.AspNetCore.Mvc.Rendering;
using RentACarApp.Models;
using System;
using System.Collections.Generic;

namespace RentACarApp.Service
{
    public interface IBookingsRepo
    {

        SelectList GetLocations();
        List<Booking> GetAllBooking();
        List<Booking> GetBookingsByCustomerId(string id);
        List<Booking> GetAllBookings();
        List<Vehicle> GetAllVehicles();
        Vehicle GetVehicleById(int id);
        void AddBooking(Booking booking);
        int NumberOfBookings();
        List<int> FindOverlapBookingsId(DateTime HireDate, DateTime ReturnDate, IEnumerable<Booking> existingBookings);
        List<Vehicle> FindAvailableVehicles(Booking booking, IEnumerable<Booking> bookings, int locationId);

    }
}
