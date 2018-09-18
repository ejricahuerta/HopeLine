using System.Threading.Tasks;

namespace HopeLine.DataAccess.Interfaces
{
    public interface IUnitOfWork
    {
        void Save();
        Task SaveAsync();
    }
}
