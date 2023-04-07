using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlShortener.Business;
using UrlShortener.Domain.Interfaces;
using UrlShortener.Domain.Models;

namespace UrlShortener.Tests
{
    public class UserServiceTests
    {
        private Mock<IRepository<User>> _mockRepository;
        private UserService _userService;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new Mock<IRepository<User>>();
            _userService = new UserService(_mockRepository.Object);
        }


        [Test]
        public void GetAllReturnsAllUsers()
        {
            var users = new List<User>
            {
                new User { Id = 1, Email = "user1@example.com" },
                new User { Id = 2, Email = "user2@example.com" }
            };
            _mockRepository.Setup(r => r.GetAll()).Returns(users.AsQueryable());

            var result = _userService.GetAll();

            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Any(u => u.Email == "user1@example.com"));
            Assert.IsTrue(result.Any(u => u.Email == "user2@example.com"));
        }

        [Test]
        public async Task CreateAsyncCreatesNewUser()
        {
            var user = new User { Email = "user1@example.com" };

            await _userService.CreateAsync(user);

            _mockRepository.Verify(r => r.Create(user), Times.Once);
            _mockRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
    }
}
