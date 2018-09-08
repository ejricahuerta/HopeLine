using HopeLine.DataAccess.DatabaseContexts;
using HopeLine.DataAccess.Entities.Base;
using HopeLine.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace HopeLine.DataAccess.Repositories
{
    /// <summary>
    /// This is a seperated to main context
    /// </summary>
    public class ResourceRepository : IRepository<CommonEntity>
    {
        private readonly ResourcesDbContext _resourcesDb;
        private readonly DbSet<CommonEntity> _entities;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourcesDb"></param>
        public ResourceRepository(ResourcesDbContext resourcesDb)
        {
            _resourcesDb = resourcesDb;
            _entities = _resourcesDb.Set<CommonEntity>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public void Delete(CommonEntity obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CommonEntity Get(object id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CommonEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public void Insert(CommonEntity obj)
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
        public void Update(CommonEntity obj)
        {
            throw new NotImplementedException();
        }
    }
}
