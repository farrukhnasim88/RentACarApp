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

        // Get Booking Date and Locations
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
                HttpContext.Session.SetString("HireDate", rdate.ToString());
                HttpContext.Session.SetString("Location", location.ToString());

                
                var allBookings = _context.Bookings.ToList();
               // ViewBag.HireDate = hdate.Ticks;
                //ViewBag.ReturnDate = rdate.Ticks;
                //ViewBag.LocationId = location;
                //string name = HttpContext.Session.GetString("FirstName");
               // string r = HttpContext.Session.GetString("HireDate");
                //DateTime t = DateTime.Parse(HttpContext.Session.GetString("HireDate"));
                //booking.ReturnDate = t;
                
                
                List<Vehicle> availableVehicles =  _service.FindAvailableVehicles(booking,allBookings, location);
                 
               return View("~/Views/Bookings/GetBookingPost.cshtml", availableVehicles.ToList());
                
            }

            return View(booking);

        }

        public async Task<IActionResult> MyConfirm(string vehicleId)
        {
            string abc = vehicleId;
            Booking booking = new Booking();
            booking.HireDate = DateTime.Parse(HttpContext.Session.GetString("HireDate"));
            booking.ReturnDate = DateTime.Parse(HttpContext.Session.GetString("ReturnDate"));
            booking.LocationId = int.Parse(HttpContext.Session.GetString("Location"));
            return null;
        }
        public async Task<IActionResult> Confirm(int vehicleId, string hdate , string rdate, int location)
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            Booking booking = new Booking();
            //   booking.HireDate = new DateTime(long.Parse(hdate));
            //  booking.ReturnDate = new DateTime(long.Parse(rdate));
            // booking.LocationId = location;
            //booking.VehicleId = vehicleId;
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
            var applicationDbContext = _context.Bookings.Include(b => b.Location).Include(b => b.Vehicle);
            return View(await applicationDbContext.ToListAsync());
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
