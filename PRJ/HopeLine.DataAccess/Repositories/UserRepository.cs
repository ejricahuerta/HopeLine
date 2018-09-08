using HopeLine.DataAccess.DatabaseContexts;
using HopeLine.DataAccess.Entities;
using HopeLine.DataAccess.Interfaces;
using System;
using System.Collections.Generic;

namespace HopeLine.DataAccess.Repositories
{

    /// <summary>
    /// 
    /// </summary>
    public class UserRepository : IRepository<HopeLineUser>
    {
        private readonly HopeLineDbContext _hopeLineDb;

        public UserRepository(HopeLineDbContext hopeLineDb)
        {
            _hopeLineDb = hopeLineDb;
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
            throw new NotImplementedException();
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
