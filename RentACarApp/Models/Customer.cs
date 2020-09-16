using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RentACarApp.Models
{
    public class Customer
    {

        public string Id { get; set; }
        [Display(Name ="First Name")]
        public string FirstName { get; set; }
        [Display(Name ="Last Name")]
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        [Display(Name="Driving Licence")]
        public string DrivingLicence { get; set; }

        public virtual List<Booking> Bookings { get; set; }


    }
}
