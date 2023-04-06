using UrlShortener.Domain.Models;

namespace UrlShortener.Domain.Interfaces
{
    public interface IRepository<T> where T : Base
    {
        public Task<int> SaveChangesAsync();
        public IQueryable<T> GetAll();
        public T Get(int id);
        public void Create(T model);
        public void Remove(T model);
        public void Update(T model);
    }
}
