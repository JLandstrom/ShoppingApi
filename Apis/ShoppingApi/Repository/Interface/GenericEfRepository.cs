using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace StandardLibrary.Repository
{
    public abstract class GenericEfRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected DbContext DbContext;

        public GenericEfRepository(DbContext dbContext)
        {
            DbContext = dbContext;
        }
        public void Create(TEntity entity)
        {
            DbContext.Set<TEntity>().Add(entity);
        }

        public async Task CreateAsync(TEntity entity)
        {
            await DbContext.Set<TEntity>().AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            DbContext.Set<TEntity>().Remove(entity);
        }

        public List<TEntity> Get()
        {
            return DbContext.Set<TEntity>().ToList();
        }

        public TEntity Get(int id)
        {
            return DbContext.Set<TEntity>().Find(id);
        }

        public async Task<List<TEntity>> GetAsync()
        {
            return await DbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await DbContext.Set<TEntity>().FindAsync(id);
        }

        public void Update(TEntity entity)
        {
            DbContext.Set<TEntity>().Update(entity);
        }
    }
}