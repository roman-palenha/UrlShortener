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
    public class AboutMessageServiceTests
    {
        private Mock<IRepository<AboutMessage>> _mockRepository;
        private AboutMessageService _aboutMessageService;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new Mock<IRepository<AboutMessage>>();
            _aboutMessageService = new AboutMessageService(_mockRepository.Object);
        }

        [Test]
        public void GetAllReturnsAllMessages()
        {
            var messages = new List<AboutMessage>
            {
                new AboutMessage { Id = 1, Message = "Message 1" },
                new AboutMessage { Id = 2, Message = "Message 2" },
                new AboutMessage { Id = 3, Message = "Message 3" }
            }.AsQueryable();
            _mockRepository.Setup(r => r.GetAll()).Returns(messages);

            var result = _aboutMessageService.GetAll();

            Assert.AreEqual(messages.Count(), result.Count());
            foreach (var message in messages)
            {
                Assert.IsTrue(result.Contains(message));
            }
        }

        [Test]
        public async Task UpdateMessageIsUpdated()
        {
            var message = new AboutMessage { Id = 1, Message = "Message 1" };
            _mockRepository.Setup(r => r.Update(message));

            await _aboutMessageService.Update(message);

            _mockRepository.Verify(r => r.Update(message), Times.Once);
            _mockRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
    }
}
