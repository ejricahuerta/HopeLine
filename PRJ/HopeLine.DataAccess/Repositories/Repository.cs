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

        //public object Select => throw new System.NotImplementedException();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public void Delete(T obj)
        {
            try
            {
                _hopeLineDb.Remove(obj);
                _entities.Remove(obj);
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Error: " + e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Get(object id)
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAll(string include = null)
        {
            return _entities.Include(include);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public void Insert(T obj)
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void Remove(object id)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public void Update(T obj)
        {
            throw new System.NotImplementedException();
        }

        object IRepository<T>.Select(System.Func<object, HopeLine.Service.Models.CommunityModel> p)
        {
            throw new System.NotImplementedException();
        }
    }
}
