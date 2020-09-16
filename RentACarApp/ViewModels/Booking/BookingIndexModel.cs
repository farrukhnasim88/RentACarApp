using RentACarApp.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace RentACarApp.ViewModels.Booking
{
    public class BookingIndexModel
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Please Enter Hire Date")]
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }
        [DataType(DataType.Date)]
        [Required]
        [Display(Name = "Return Date")]
        public DateTime ReturnDate { get; set; }
        [Required(ErrorMessage = "Please Enter Return Date")]
        public int LocationId { get; set; }
        public int RefrenceNo { get; set; }
        public int VehicleId { get; set; }
        public string CustomerId { get; set; }
         


    }
}
