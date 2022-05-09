using AutoMapper;
using DTOs;
using Models;

namespace Common.AutoMapperProfiles
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<BookingDto, Booking>().ReverseMap();
        }
    }
}
