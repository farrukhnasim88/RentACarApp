using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace RentACarApp.Models
{
    public class Booking
    {

        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; }

        public int LocationId { get; set; }
        public int VehicleId { get; set; }
        public int CustomerId { get; set; }

        public Location Location { get; set; }
        public Vehicle Vehicle { get; set; }
       



    }
}
