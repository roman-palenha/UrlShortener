using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using UrlShortener.Business.Helpers;
using UrlShortener.Business.Interfaces;
using UrlShortener.Domain.Interfaces;
using UrlShortener.Domain.Models;

namespace UrlShortener.Business
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repository;

        public UserService(IRepository<User> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository);
        }

        public IQueryable<User> GetAll()
        {
            return _repository.GetAll();
        }

        public async Task<User> CreateAsync(User model)
        {
            _repository.Create(model);
            await _repository.SaveChangesAsync();

            return model;
        }

        public async Task<ClaimsIdentity> Register(RegisterViewModel model)
        {
            var user = await _repository.GetAll().FirstOrDefaultAsync(x => x.Email == model.Email);
            if (user != null)
            {
                throw new Exception("User with this email is already existed");
            }

            user = new User()
            {
                Email = model.Email,
                Password = Helper.HashPassword(model.Password),
                Role = Role.User
            };

            await CreateAsync(user);
            var result = Authenticate(user);
            return result;

        }

        public async Task<ClaimsIdentity> Login(LoginViewModel model)
        {
            var user = await _repository.GetAll().FirstOrDefaultAsync(x => x.Email == model.Email);
            if (user == null)
            {
                throw new Exception("User with this email is not exist");
            }

            var result = Authenticate(user);
            return result;
        }

        private ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
            };

            return new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}
