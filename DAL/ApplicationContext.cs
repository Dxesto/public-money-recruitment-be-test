using Models;
using System;
using System.Collections.Generic;

namespace DAL
{
    public class ApplicationContext
    {
        private Dictionary<Type, object> _context = new Dictionary<Type, object>()
        {
            { typeof(Rental),  new Dictionary<int, Rental>() },
            { typeof(Booking),  new Dictionary<int, Booking>() },
            { typeof(Calendar),  new Dictionary<int, Calendar>() }
        };

        public Dictionary<int, T> Set<T>() where T : class
        {
            var type = typeof(T);

            Dictionary<int, T> dataSource = (Dictionary<int, T>)_context[type];

            return dataSource;
        }
    }
}
