using HopeLine.DataAccess.DatabaseContexts;
using HopeLine.DataAccess.Entities.Base;
using HopeLine.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HopeLine.DataAccess.Repositories
{

    //TODO : add functionalities and implementation
    /// <summary>
    /// Generic Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly HopeLineDbContext _hopeLineDb;
        private readonly ResourcesDbContext _resourcesDb;
        private DbSet<T> _entities;

        /// <summary>
        /// Injecting db contexts
        /// </summary>
        /// <param name="hopeLineDb"></param>
        public Repository(HopeLineDbContext hopeLineDb)
        {
            _hopeLineDb = hopeLineDb;
            _entities = _hopeLineDb.Set<T>();

        }

        public void Delete(T obj)
        {
            throw new System.NotImplementedException();
        }

        public T Get(object id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public void Insert(T obj)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(object id)
        {
            throw new System.NotImplementedException();
        }

        public void Save()
        {
            throw new System.NotImplementedException();
        }

        public void Update(T obj)
        {
            throw new System.NotImplementedException();
        }
    }
}
