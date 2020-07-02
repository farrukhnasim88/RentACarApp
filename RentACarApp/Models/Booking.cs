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
        
        [Required(ErrorMessage ="Please Enter First Name")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please Enter Last Name")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please Enter Driver Licence")]
        [Display(Name ="Driver Licence")]

        public int Licence { get; set; }

        [Required(ErrorMessage = "Please Enter Address")]
        [Display(Name = "Driver Licence")]
        public string Address {get;set;}

        [Required(ErrorMessage = "Please Enter Telephone Number")]
        [Display(Name = "Driver Licence")]
        public string Telephone { get; set; }

        public Vehicle BookingVehicle { get; set; }
    }
}
