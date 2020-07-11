using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentACarApp.Data;
using RentACarApp.Models;

namespace RentACarApp.Controllers
{
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> GetVehicles()
        {
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> GetVehicles([Bind("Id,HireDate,ReturnDate,LocationId")] Booking booking)
        {
            if (ModelState.IsValid)
            {

                var hdate = booking.HireDate;
                var rdate = booking.ReturnDate;
                var location = booking.LocationId;

                var existingBookings = _context.Bookings.Include(b => b.Vehicle).Include(b => b.Location);
                List<int> abc = _context.Bookings.Where(o => o.HireDate <= hdate && o.ReturnDate >= hdate).Select(o => o.Vehicle.Id).ToList();
                List<int> alredyBookedCars = new List<int>();
                if (existingBookings != null && existingBookings.Count() > 0)
                {
                    foreach (Booking b in existingBookings)
                    {
                        //if the hdate falls between an existing booking that means the booking's car is not available
                        if (hdate >= b.HireDate && hdate <= b.ReturnDate)
                        {
                            alredyBookedCars.Add(b.Vehicle.Id);
                        }
                    }
                }
                List<Vehicle> allCars = _context.Vehicles.ToList();
                List<Vehicle> availableCars = new List<Vehicle>();
                if (allCars != null && allCars.Count > 0)
                {
                    foreach (Vehicle car in allCars)
                    {
                        if (alredyBookedCars.Contains(car.Id))
                        { 
                        }
                        else
                        {
                            availableCars.Add(car);
                        }
                    }
                }
                //var existingBookings = _context.Bookings.Where(o => o.HireDate =)
                var g = 9;

              





                 
            }
        

            return View(booking);
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
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Id");
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
