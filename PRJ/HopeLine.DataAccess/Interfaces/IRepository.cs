using System.Collections.Generic;
using System.Threading.Tasks;

namespace HopeLine.DataAccess.Interfaces
{

    //TODO : add implementation
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll(string include = null);
        void Insert(T obj);
        void Update(T obj);

        T Get(object id);
        void Delete(T obj);

        void Remove(object id);

        void Save();

        Task SaveAsync();
    }
}
