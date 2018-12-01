using System.Collections.Generic;
using System.Threading.Tasks;

namespace StandardLibrary.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        List<TEntity> Get();
        TEntity Get(int id);
        Task<List<TEntity>> GetAsync();
        Task<TEntity> GetAsync(int id);
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task CreateAsync(TEntity entity);
    }
}