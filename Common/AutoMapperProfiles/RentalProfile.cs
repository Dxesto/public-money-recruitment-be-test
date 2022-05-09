using AutoMapper;
using DTOs;
using Models;

namespace Common.AutoMapperProfiles
{
    public class RentalProfile : Profile
    {
        public RentalProfile()
        {
            CreateMap<RentalDto, Rental>().ReverseMap();
        }
    }
}
