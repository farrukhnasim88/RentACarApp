using System;
using System.Collections.Generic;
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

                List<VehiclesViewModel> vehiclesViewModels = new List<VehiclesViewModel>();

                foreach (var vehicle in availableVehicles)
                {
                    VehiclesViewModel vvm = new VehiclesViewModel();
                    vvm.Id = vehicle.Id;
                    vvm.Make = vehicle.Make;
                    vvm.Model = vehicle.Model;
                    vvm.Year = vehicle.Year;
                    vvm.ImageUrl = vehicle.ImageUrl;
                    vvm.Color = vehicle.Color;
                    vvm.Kilometer = vehicle.Kilometer;
                    vvm.RatePerDay = vehicle.RatePerDay;
                    vvm.Days = days;
                    vvm.Price = vvm.RatePerDay * vvm.Days;
                    vehiclesViewModels.Add(vvm);
                }
                return View(vehiclesViewModels);
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
            VehicleModel vm = new VehicleModel();
            Random random = new Random();
            var ran = random.Next();
            vm.HireDate= DateTime.Parse(HttpContext.Session.GetString("HireDate"));
            vm.ReturnDate= DateTime.Parse(HttpContext.Session.GetString("ReturnDate"));
            vm.Make = selectedVehicel.Make;
            vm.Model = selectedVehicel.Model;
            vm.Year = selectedVehicel.Year;
            vm.Kilometer = selectedVehicel.Kilometer;
            vm.Color = selectedVehicel.Color;
            vm.Fee = bookingPrice;
            vm.ImageUrl = selectedVehicel.ImageUrl;
            vm.VehicleId = vehicleId;
            vm.LocationId= int.Parse(HttpContext.Session.GetString("Location"));
            vm.CustomerId= user.Id;
            vm.RefrenceNo = ran;
                        
            return View (vm);
        }

        // checkout after selecting car
        public async Task<IActionResult> Checkout(int locationId, int vehicleId, string customerId, int refrenceNo)
        {
             
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            ViewBag.email = user.Email;
            Booking addBooking = new Booking();
            addBooking.RefrenceNo = refrenceNo;
            addBooking.HireDate = DateTime.Parse(HttpContext.Session.GetString("HireDate"));
            addBooking.ReturnDate = DateTime.Parse(HttpContext.Session.GetString("ReturnDate"));
            addBooking.LocationId = locationId;
            addBooking.VehicleId = vehicleId;
            addBooking.CustomerId = customerId;
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
