using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RentACarApp.Areas.Identity.Data;
using RentACarApp.Data;
using RentACarApp.Models;
using RentACarApp.Service;
using RentACarApp.ViewModels;

namespace RentACarApp.Controllers
{
    public class BookingsController : Controller
    {
        private readonly RentingService _service;
        private readonly UserManager<RentACarAppUser> _userManager;
        private readonly SignInManager<RentACarAppUser> _signInManager;
        
        
        public BookingsController(RentACarAppDbContext context, RentingService service, UserManager<RentACarAppUser> userManager, SignInManager<RentACarAppUser> signInManager  )
        {
            _service = service;
            _userManager = userManager;
            _signInManager = signInManager;
         
        }

        // Takes dates and location for booking
        public async Task<IActionResult> GetBooking()
        {
            // Get all locations from db
            ViewData["LocationId"] =  _service.GetLocations();
            return View();
        }

        // Post the Booking
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetBookingPost([Bind("HireDate, ReturnDate,LocationId")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                var hdate = booking.HireDate;
                var rdate = booking.ReturnDate;
                var location = booking.LocationId;
                HttpContext.Session.SetString("ReturnDate", rdate.ToString());
                HttpContext.Session.SetString("HireDate", hdate.ToString());
                HttpContext.Session.SetString("Location", location.ToString());

                int days = (booking.ReturnDate.Date - booking.HireDate.Date).Days ;
                if (days == 0)
                {
                    days = days + 1;
                }

                var allBookings = _service.GetAllBookings();
                
                List<Vehicle> availableVehicles =  _service.FindAvailableVehicles(booking,allBookings, location);

                var Vehicles = availableVehicles
                            .Select(result => new VehiclesViewModel
                            {
                                Id = result.Id,
                                Make = result.Make,
                                Model = result.Model,
                                Year = result.Year,
                                ImageUrl = result.ImageUrl,
                                Color = result.Color,
                                Kilometer = result.Kilometer,
                                RatePerDay = result.RatePerDay,
                                Price = result.RatePerDay * days,
                              
                            });

                return View(Vehicles);
              
            }
            return RedirectToAction(nameof(GetBooking));
        }
        // After login or Register User
        public async Task<IActionResult> MyConfirm(string VehicleId, string price)
        {
            int vehicleId = int.Parse(VehicleId);
            decimal bookingPrice = decimal.Parse(price);
            var user =await _userManager.FindByEmailAsync(User.Identity.Name);
            Vehicle selectedVehicel = _service.GetVehicleById(vehicleId);
            Random random = new Random();
            var ran = random.Next();
            var vm = new VehicleModel
            {
                HireDate= DateTime.Parse(HttpContext.Session.GetString("HireDate")),
                ReturnDate = DateTime.Parse(HttpContext.Session.GetString("ReturnDate")),
                Make = selectedVehicel.Make,
                Model = selectedVehicel.Model,
                Year = selectedVehicel.Year,
                Kilometer = selectedVehicel.Kilometer,
                Color = selectedVehicel.Color,
                Fee = bookingPrice,
                ImageUrl = selectedVehicel.ImageUrl,
                VehicleId = vehicleId,
                LocationId = int.Parse(HttpContext.Session.GetString("Location")),
                CustomerId = user.Id,
                RefrenceNo = ran
            };
                        
            return View (vm);
        }

        // checkout after selecting car
        public async Task<IActionResult> Checkout(int locationId, int vehicleId, string customerId, int refrenceNo)
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            ViewBag.email = user.Email;
            var addBooking = new Booking
            {
                RefrenceNo= refrenceNo,
                HireDate = DateTime.Parse(HttpContext.Session.GetString("HireDate")),
                ReturnDate = DateTime.Parse(HttpContext.Session.GetString("ReturnDate")),
                LocationId = locationId,
                VehicleId = vehicleId,
                CustomerId = customerId,
            };
            _service.AddBooking(addBooking);
            return View(addBooking);
        }
       // list of bookings by customer id
        public async Task<IActionResult> MyBookings(string customerId)
        {
            List<Booking> bookingByCustomer = _service.GetBookingsByCustomerId(customerId);
            return View(bookingByCustomer);
        }

       
    }
}
