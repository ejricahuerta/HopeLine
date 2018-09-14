using HopeLine.DataAccess.DatabaseContexts;
using HopeLine.DataAccess.Entities;
using HopeLine.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HopeLine.DataAccess.Repositories
{

    /// <summary>
    /// 
    /// </summary>
    public class UserRepository : IRepository<HopeLineUser>
    {
        private readonly HopeLineDbContext _hopeLineDb;
        private readonly DbSet<HopeLineUser> _entities;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hopeLineDb"></param>
        public UserRepository(HopeLineDbContext hopeLineDb)
        {
            _hopeLineDb = hopeLineDb;
            _entities = _hopeLineDb.Set<HopeLineUser>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public void Delete(HopeLineUser obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HopeLineUser Get(object id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<HopeLineUser> GetAll()
        {

            return _entities.Include(p => p.Profile).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public void Insert(HopeLineUser obj)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void Remove(object id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public void Update(HopeLineUser obj)
        {
            throw new NotImplementedException();
        }
    }
}
