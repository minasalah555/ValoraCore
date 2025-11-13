namespace Valora.Repositories
{
    public interface IRepository<T>
    {
        
        IQueryable<T> GetAll();
        Task<T?> GetById(int Id);
        Task<T?> GetByIDWithTracking(int Id);
        Task Add(T entity);
        void Update(T entity);
        Task Delete(int id);
        Task SaveChanges();

    }
}
