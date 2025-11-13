using Microsoft.EntityFrameworkCore;
using Valora.Data;
using Valora.Models;

namespace Valora.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        Context Context;
        DbSet<T> _dbSet { get; set; }
        public Repository(Context context  )
        {
            Context = context;
            _dbSet = Context.Set<T>();
        }

        public async Task Add(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task Delete(int id)
        {
            var res =await GetByIDWithTracking(id);
            if (res!=null)
            {
                res.IsDeleted = true;
            }
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.Where(s => !s.IsDeleted);
        }

        public  virtual async Task<T?> GetById(int Id)
        {
            var res = await _dbSet.FirstOrDefaultAsync(s => s.ID == Id && !s.IsDeleted);
            return res;
        }

        public async Task<T?> GetByIDWithTracking(int Id)
        {
            var res = await _dbSet.AsTracking().FirstOrDefaultAsync(s => s.ID == Id && !s.IsDeleted);
            return res;
        }

        protected IQueryable<T> Query()
        {
            return Context.Set<T>();
        }

        public void Update(T entity)
        {     
             _dbSet.Update(entity); 
        }

        public async Task SaveChanges()
        {
              await  Context.SaveChangesAsync();
        }
    }
}
