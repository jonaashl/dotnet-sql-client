namespace Chinook.Repositories
{
    internal interface ICrudRepository <T, ID>
    {
        List<T> GetAll();
        T GetById(ID id);
        void Add(T obj);
        void Update(T obj);
    }
}
