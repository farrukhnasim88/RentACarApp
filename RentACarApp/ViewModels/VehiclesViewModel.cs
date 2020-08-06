using RentACarApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RentACarApp.ViewModels
{
    public class VehiclesViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter Vehicle Make")]
        public string Make { get; set; }

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
        public decimal RatePerDay { get; set; }

        public virtual Status VehicleStatus { get; set; }
        public int LocationId { get; set; }

        public int Days { get; set; }
       
        public decimal Price { get; set; }

    }
}

