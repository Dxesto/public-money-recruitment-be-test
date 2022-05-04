using DAL.Repositories.Interfaces;
using Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    public class InMemoryRepository<T> : IInMemoryRepository<T> where T : class, IEntity
    {
        private IDictionary<int, T> _source;

        public InMemoryRepository(ApplicationContext applicationContext)
        {
            _source = applicationContext.Set<T>();
        }

        public T GetById(int id)
        {
            return _source[id];
        }

        public int Add(T entity)
        {
            var id = _source.Count + 1;
            entity.Id = id;

            _source.Add(_source.Count + 1, entity);

            return id;
        }

        public bool Any(int id)
        {
            return _source.ContainsKey(id);
        }

        public IList<T> Get(Func<T, bool> condition)
        {
            return _source.Select(x => x.Value).Where(condition).ToList();
        }

        public int Count(Func<T, bool> condition)
        {
            return _source.Select(x => x.Value).Count(condition);
        }
    }
}
