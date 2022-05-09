using System;
using System.Collections.Generic;

namespace DAL.Repositories.Interfaces
{
    public interface IInMemoryRepository<T> where T : class
    {
        T GetById(int id);

        int Add(T entity);

        bool Any(int id);

        IList<T> Get(Func<T, bool> condition);

        int Count(Func<T, bool> condition);
    }
}
