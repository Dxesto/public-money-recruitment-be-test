using DAL.Repositories.Interfaces;
using Models;

namespace DAL.Repositories
{
    public class UnitOfWork
    {
        private static ApplicationContext _applicationContext;

        private IInMemoryRepository<Booking> _bookingRepository;
        private IInMemoryRepository<Calendar> _calendarRepository;
        private IInMemoryRepository<Rental> _rentalRepository;


        public UnitOfWork(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public IInMemoryRepository<Booking> BookingRepository => _bookingRepository = _bookingRepository == null ? new InMemoryRepository<Booking>(_applicationContext) : _bookingRepository;

        public IInMemoryRepository<Calendar> CalendarRepository => _calendarRepository = _calendarRepository == null ? new InMemoryRepository<Calendar>(_applicationContext) : _calendarRepository;

        public IInMemoryRepository<Rental> RentalRepository => _rentalRepository = _rentalRepository == null ? new InMemoryRepository<Rental>(_applicationContext) : _rentalRepository;
    }
}
