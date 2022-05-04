using AutoMapper;
using DTO;
using Models;

namespace VacationRental.Api.AutoMapperProfiles
{
    public class RentalProfile : Profile 
    {
        public RentalProfile()
        {
            CreateMap<RentalDto, Rental>();
        }
    }
}
