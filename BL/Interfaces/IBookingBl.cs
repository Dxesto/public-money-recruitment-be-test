using Models;

namespace BL.Interfaces
{
    public interface IBookingBl : IBaseBl<Booking>
    {
        int Create(Booking booking);
    }
}
