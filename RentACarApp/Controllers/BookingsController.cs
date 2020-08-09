using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
           // HttpContext.Session.SetString("VehicelId", VehicleId);
            //HttpContext.Session.SetString("Price", price);
            int vehicleId = int.Parse(VehicleId);
            decimal bookingPrice = decimal.Parse(price);
            //ViewBag.Price = bookingPrice;
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
            
            //var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            //booking.HireDate = DateTime.Parse(HttpContext.Session.GetString("HireDate"));
            //booking.ReturnDate = DateTime.Parse(HttpContext.Session.GetString("ReturnDate"));
            //booking.LocationId = int.Parse(HttpContext.Session.GetString("Location"));
            return View (vm);
        }
             public async Task<IActionResult> Checkout()
        {

           
            return null;
        }
        public async Task<IActionResult> Confirm(int vehicleId, string hdate , string rdate, int location)
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            Booking booking = new Booking();
            booking.HireDate = DateTime.Parse(HttpContext.Session.GetString("HireDate"));
            booking.ReturnDate = DateTime.Parse(HttpContext.Session.GetString("ReturnDate"));
            booking.LocationId = int.Parse(HttpContext.Session.GetString("Location"));
            booking.VehicleId = 0;
            booking.CustomerId = user.Id;
            _context.Bookings.Add(booking);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
            
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext =  _service.GetAllBooking();
            return View( applicationDbContext);
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Location)
                .Include(b => b.Vehicle)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Name");
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "Id", "Id");
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id");

            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HireDate,ReturnDate,LocationId,VehicleId,CustomerId")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Id", booking.LocationId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "Id", "Id", booking.VehicleId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", booking.VehicleId);

            return View(booking);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Id", booking.LocationId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "Id", "Id", booking.VehicleId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", booking.VehicleId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HireDate,ReturnDate,LocationId,VehicleId,CustomerId")] Booking booking)
        {
            if (id != booking.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Id", booking.LocationId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "Id", "Id", booking.VehicleId);
            return View(booking);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", booking.VehicleId);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Location)
                .Include(b => b.Vehicle)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.Id == id);
        }
    }
}
