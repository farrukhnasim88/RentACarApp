using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentACarApp.Service;
using RentACarApp.ViewModels.Booking;

namespace RentACarApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsApiController : ControllerBase
    {

        private readonly RentingService _service;
        private readonly IMapper _mapper;
        public BookingsApiController(RentingService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult <IEnumerable<BookingIndexModel>> GetAllBookings()
        {
            var bookings = _service.GetAllBookings();
            return Ok(_mapper.Map<IEnumerable<BookingIndexModel>>(bookings));
        }



    }
}
