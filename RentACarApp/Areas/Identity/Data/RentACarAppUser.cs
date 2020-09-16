using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace RentACarApp.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the RentACarAppUser class
    public class RentACarAppUser : IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; }
        [PersonalData]
        public string LastName { get; set; }

         
        public string Mobile { get; set; }
        public string LicenceNo { get; set; }
        public string Address { get; set; }



    }
}
