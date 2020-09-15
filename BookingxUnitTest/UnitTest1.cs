using Microsoft.EntityFrameworkCore;
using RentACarApp.Data;
using RentACarApp.Models;
using RentACarApp.Service;
using System;
using Xunit;

namespace BookingxUnitTest
{
    public class UnitTest1
    {
        private readonly RentingService _service;
        public UnitTest1( RentingService service)
        {
            var dbOption = new DbContextOptionsBuilder<RentACarAppDbContext>()
            .UseSqlServer("RentACarAppDbContextConnection")
            .Options;
            var context = new RentACarAppDbContext(dbOption);
            _service = new RentingService(context);

        }
        
        [Fact]
        public void Test1()
        {
            int actual;

            actual = _service.NumberOfBookings();
            Assert.Equal(44, actual);

        }
    }
}
