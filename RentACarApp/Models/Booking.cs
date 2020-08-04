using System;
using System.ComponentModel.DataAnnotations;

namespace RentACarApp.Models
{
    public class Booking
    {

        public int Id { get; set; }
        [DataType(DataType.Date)]
        [Required]
        [Display(Name ="Hire Date")]
        public DateTime HireDate { get; set; }
        [DataType(DataType.Date)]
        [Required]
        [Display(Name ="Return Date")]
        public DateTime ReturnDate { get; set; }
        [Required]
        public int LocationId { get; set; }
        public int VehicleId { get; set; }
        public string CustomerId { get; set; }

        public Location Location { get; set; }
        public Vehicle Vehicle { get; set; }
       



    }
}
