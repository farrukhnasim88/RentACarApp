using RentACarApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RentACarApp.ViewModels
{
    public class VehicleModel
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter Vehicle Make")]
        public string Make { get; set; }

        public DateTime HireDate { get; set; }
        public DateTime ReturnDate { get; set; }

        [Required(ErrorMessage = "Please Enter Vehicle Model")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Please Enter Vehicle Year of Make")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Please Enter Vehicle Kilometer")]
        [Display(Name = "Kilometer")]
        public int Kilometer { get; set; }

        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Please Enter Vehicle Color")]
        public string Color { get; set; }

        [Required(ErrorMessage = "Please Enter Vehicle Rate Per Day")]
        [Display(Name = "Vehicle Rate")]
        public decimal Fee { get; set; }

        public virtual Status VehicleStatus { get; set; }
        public int LocationId { get; set; }
        public int VehicleId { get; set; }
        public string CustomerId { get; set; }
        public int RefrenceNo { get; set; }

    }
}
