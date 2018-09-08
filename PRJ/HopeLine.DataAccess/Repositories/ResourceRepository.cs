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

        public ResourceRepository(ResourcesDbContext resourcesDb)
        {
            _resourcesDb = resourcesDb;
            _entities = _resourcesDb.Set<CommonEntity>();
        }

        public void Delete(CommonEntity obj)
        {
            throw new NotImplementedException();
        }

        public CommonEntity Get(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CommonEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Insert(CommonEntity obj)
        {
            throw new NotImplementedException();
        }

        public void Remove(object id)
        {
            throw new NotImplementedException();
        }

        public void Update(CommonEntity obj)
        {
            throw new NotImplementedException();
        }
    }
}
