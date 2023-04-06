using UrlShortener.Domain.Interfaces;
using UrlShortener.Domain.Models;

namespace UrlShortener.Domain
{
    public class Repository<T> : IRepository<T> where T : Base
    {
        private readonly AppDbContext _dbContext;

        public Repository(AppDbContext db)
        {
            _dbContext = db;
        }

        public void Create(T model)
        {
            _dbContext.Set<T>().Add(model);
        }

        public IQueryable<T> GetAll()
        {
            return _dbContext.Set<T>().AsQueryable();
        }

        public T Get(int id)
        {
            return _dbContext.Set<T>().FirstOrDefault(x => x.Id == id);
        }

        public void Remove(T model)
        {
            _dbContext.Set<T>().Remove(model);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Update(T model)
        {
            _dbContext.Set<T>().Update(model);
        }
    }
}
