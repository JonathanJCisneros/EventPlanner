namespace EventPlanner.Repository.Interfaces
{
    public interface IBaseInterface<T> where T : class
    {
        Task<T?> Get(Guid id);

        Task<List<T>> GetMany(List<Guid> ids);

        Task<List<T>> GetAll();

        Task<bool> Create(T entity);

        Task<bool> Update(T entity);

        Task<bool> Delete(Guid id);
    }
}
