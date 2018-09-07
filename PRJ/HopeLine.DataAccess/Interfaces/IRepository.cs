using System.Collections.Generic;

namespace HopeLine.DataAccess.Interfaces
{

    //TODO : add implementation
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        void Insert(T obj);
        void Update(T obj);

        T Get(object id);
        void Delete(T obj);

        void Remove(object id);
        void Save();
    }
}
