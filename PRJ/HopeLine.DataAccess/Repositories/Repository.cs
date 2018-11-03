using HopeLine.DataAccess.DatabaseContexts;
using HopeLine.DataAccess.Entities.Base;
using HopeLine.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public void Delete(T obj)
        {
            _entities.Remove(obj);
            _hopeLineDb.Entry(obj).State = EntityState.Deleted;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Get(object id)
        {
            var obj = _entities.Find(id);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAll(string include = null)
        {
            if (include == null)
            {
                return _entities;
            }
            return _entities.Include(include);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public void Insert(T obj)
        {
            if (obj == null) {
                throw new System.ArgumentNullException("Object not found");
            }
            _entities.Add(obj);
            _hopeLineDb.Entry(obj).State = EntityState.Added;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void Remove(object id)
        {
            var obj = _entities.Find(id);
            if (obj != null)
            {
                _entities.Remove(obj);
                Delete(obj);
            }


        }

        public void Save()
        {
            _hopeLineDb.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _hopeLineDb.SaveChangesAsync();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public void Update(T obj)
        {
            try
            {
                _entities.Update(obj);
                _hopeLineDb.Entry(obj).State = EntityState.Modified;
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("Unable to Update: ", ex);
            }
        }
    }
}
