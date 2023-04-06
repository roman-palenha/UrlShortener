using System.Security.Claims;
using UrlShortener.Domain.Models;

namespace UrlShortener.Business.Interfaces
{
    public interface IUserService
    {
        public IQueryable<User> GetAll();
        public Task<User> CreateAsync(User model);
        public Task<ClaimsIdentity> Register(RegisterViewModel model);
        public Task<ClaimsIdentity> Login(LoginViewModel model);
    }
}
