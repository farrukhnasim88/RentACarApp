using AutoMapper;
using RentACarApp.Models;
using RentACarApp.ViewModels;

namespace RentACarApp.Profiles
  
{
    public class VehiclesProfile : Profile

    {
        public VehiclesProfile()
        {
            // Source -> Target
            // mapping from Model to ViewModel
            CreateMap<Vehicle, VehiclesViewModel>();
        }
       



    }
}
