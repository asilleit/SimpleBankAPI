namespace SimpleBankAPI.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<T> Create(T entity);
        Task<T> Update(T entityToUpdate);
    }
}
