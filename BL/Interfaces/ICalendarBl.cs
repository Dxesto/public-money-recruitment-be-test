using Models;
using System;

namespace BL.Interfaces
{
    public interface ICalendarBl
    {
        Calendar Get(int rentalId, DateTime start, int nights);
    }
}
