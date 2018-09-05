namespace HopeLine.DataAccess.Interfaces
{

    //TODO : add implementation
    public interface IRepository<T>
    {
        void Insert(T obj);
        void Update(T obj);

        T Get(int id);
    }
}
