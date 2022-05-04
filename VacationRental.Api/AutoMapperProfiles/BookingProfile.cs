using AutoMapper;
using DTO;
using Models;

namespace VacationRental.Api.AutoMapperProfiles
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<BookingDto, Booking>();
        }
    }
}
