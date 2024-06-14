using System.Text;
using UrlShortener.Business.Interfaces;
using UrlShortener.Domain.Interfaces;
using UrlShortener.Domain.Models;

namespace UrlShortener.Business
{
    public class UrlService : IUrlService
    {
        private readonly IRepository<ShortenUrl> _repository;
        
        public UrlService(IRepository<ShortenUrl> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<ShortenUrl> Create(UrlViewModel model)
        {
            var url = ShortUrl(model); 
            _repository.Create(url);
            await _repository.SaveChangesAsync();

            return url;
        }

        public IQueryable<ShortenUrl> GetAll()
        {
            return _repository.GetAll();
        }

        public ShortenUrl GetById(int id)
        {
            var result = _repository.Get(id);
            return result;
        }

        public ShortenUrl GetByPath(string path)
        {
            var result = _repository.GetAll().FirstOrDefault(x => x.Shorten.Equals(path));
            return result;
        }

        public async Task Remove(int id)
        {
            var url = _repository.Get(id);
            _repository.Remove(url);
            await _repository.SaveChangesAsync();
        }

        private ShortenUrl ShortUrl(UrlViewModel model)
        {
            var last = _repository.GetAll().AsEnumerable().Reverse().FirstOrDefault();
            int id = last == null ? 0 : last.Id + 1;

            return new ShortenUrl
            {
                CreatedBy = model.CreatedBy,
                CreatedOn = DateTime.Now,
                FullUrl = model.Url,
                Shorten = Encode(id)
            };
        }

        public int Decode(string encoded)
        {
            return encoded.Aggregate(0, (current, c) => (current * Constants.Alphabet.Length) + Constants.Alphabet.IndexOf(c));
        }

        private string Encode(int id)
        {
            if (id == 0) 
                return Constants.Alphabet[0].ToString();

            var sb = new StringBuilder();
            while (id > 0)
            {
                sb.Append(Constants.Alphabet[id % Constants.Alphabet.Length]);
                id /= Constants.Alphabet.Length;
            }

            return string.Join(string.Empty, sb.ToString().Reverse());
        }
    }
}
