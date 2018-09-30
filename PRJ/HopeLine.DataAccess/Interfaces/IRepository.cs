using System.Collections.Generic;

namespace HopeLine.DataAccess.Interfaces
{

    //TODO : add implementation
    public interface IRepository<T>
    {
        object Select { get; }

        IEnumerable<T> GetAll(string include = null);
        void Insert(T obj);
        void Update(T obj);

        T Get(object id);
        void Delete(T obj);

        void Remove(object id);
        // @Edmel, what this? It gives me an error.
        //object Select(System.Func<object, HopeLine.Service.Models.CommunityModel> p);
    }
}
