using HopeLine.DataAccess.DatabaseContexts;
using HopeLine.DataAccess.Entities.Base;
using HopeLine.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HopeLine.DataAccess.Repositories
{
    /// <summary>
    /// This is a seperated to main context
    /// This class gets the information from the database and is a layer before hitting the real database
    /// 
    /// </summary>
    public class ResourceRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ResourcesDbContext _resourcesDb; // 
        private readonly DbSet<T> _entities; // These are the objects from the mapped table of type CommonEntity


        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourcesDb"></param>
        public ResourceRepository(ResourcesDbContext resourcesDb)
        {
            _resourcesDb = resourcesDb;
            _entities = _resourcesDb.Set<T>();
        }

        public object Select => throw new System.NotImplementedException();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public void Delete(T obj)
        {
            //TODO : Do try/catches for error handling
            _resourcesDb.Remove(obj);
            _entities.Remove(obj);
            this.Remove(obj.Id);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Get(object id)
        {
            return _entities.Find(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAll(string include = null)
        {
            return _entities;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public void Insert(T obj)
        {
            //TODO : 
            _entities.Add(obj);
            _resourcesDb.Add(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void Remove(object id)
        {
            _entities.Remove(_entities.Find(id));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public void Update(T obj)
        {
            _entities.Update(obj);
            _resourcesDb.Update(obj);
        }
    }
}
