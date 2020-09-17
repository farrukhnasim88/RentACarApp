using AutoMapper;
using RentACarApp.Models;
using RentACarApp.ViewModels.Booking;

namespace RentACarApp.Profiles.Bookings
{
    public class BookingProfile: Profile
    {
        public BookingProfile()
        {
            // Source -> Target
            // mapping from Model to ViewModel
            CreateMap<Booking, BookingIndexModel>();
        }

    }
}
