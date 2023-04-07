using UrlShortener.Business.Interfaces;
using UrlShortener.Domain.Interfaces;
using UrlShortener.Domain.Models;

namespace UrlShortener.Business
{
    public class AboutMessageService : IAboutMessageService
    {
        private readonly IRepository<AboutMessage> _repository;

        public AboutMessageService(IRepository<AboutMessage> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public IQueryable<AboutMessage> GetAll()
        {
            return _repository.GetAll();
        }

        public async Task Update(AboutMessage message)
        {
            _repository.Update(message);
            await _repository.SaveChangesAsync();
        }
    }
}
