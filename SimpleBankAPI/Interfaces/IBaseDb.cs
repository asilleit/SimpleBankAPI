namespace SimpleBankAPI.Interfaces
{
    public interface IBaseDb<T>
    {
        Task<T> Create(T entity);

    }
}
