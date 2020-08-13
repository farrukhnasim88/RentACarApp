using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RentACarApp.Areas.Identity.Data;
using RentACarApp.Data;
using RentACarApp.Models;
using RentACarApp.Service;
using RentACarApp.ViewModels;

namespace RentACarApp.Controllers
{
    public class BookingsController : Controller
    {
        private readonly RentACarAppDbContext _context;
        private readonly RentingService _service;
        private readonly UserManager<RentACarAppUser> _userManager;

        // dependies inj
        public BookingsController(RentACarAppDbContext context, RentingService service, UserManager<RentACarAppUser> userManager )
        {
            _context = context;
            _service = service;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index1()
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            string a = user.Id;
            var r = _service.GetBookingsByCustomerId(a);
            return View("Index", r);
        }

        // Get Booking Dates and Locations
        [HttpGet]
        public async Task<IActionResult> GetBooking()
        {
            // Get all locations from db
            ViewData["LocationId"] =  new SelectList(_context.Locations, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public async Task<IActionResult> GetBookingPost( Booking booking)
        {
            if (ModelState.IsValid)
            {
                var hdate = booking.HireDate;
                var rdate = booking.ReturnDate;
                var location = booking.LocationId;
                HttpContext.Session.SetString("ReturnDate", rdate.ToString());
                HttpContext.Session.SetString("HireDate", hdate.ToString());
                HttpContext.Session.SetString("Location", location.ToString());
                int days = (booking.ReturnDate.Date - booking.HireDate.Date).Days;

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

            return View(booking);

        }

        // After login or Register 
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
       
        public async Task<IActionResult> MyBookings(string customerId)
        {
            List<Booking> bookingByCustomer = _service.GetBookingsByCustomerId(customerId);
            return View(bookingByCustomer);
        }

       
    }
}
