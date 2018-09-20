using HopeLine.DataAccess.DatabaseContexts;
using HopeLine.DataAccess.Interfaces;
using System.Threading.Tasks;

namespace HopeLine.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HopeLineDbContext _hopeLineDb;

        public UnitOfWork(HopeLineDbContext hopeLineDb)
        {
            _hopeLineDb = hopeLineDb;
        }

        public void Save()
        {
            System.Console.WriteLine("Saving...");
            _hopeLineDb.SaveChanges();
        }

        public async Task SaveAsync()
        {
            System.Console.WriteLine("Saving Async...");
            await _hopeLineDb.SaveChangesAsync();
        }
    }
}
