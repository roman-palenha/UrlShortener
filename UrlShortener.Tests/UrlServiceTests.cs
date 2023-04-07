using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlShortener.Business;
using UrlShortener.Domain.Interfaces;
using UrlShortener.Domain.Models;

namespace UrlShortener.Tests
{
    public class UrlServiceTests
    {
        private Mock<IRepository<ShortenUrl>> _mockRepository;
        private UrlService _urlService;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new Mock<IRepository<ShortenUrl>>();
            _urlService = new UrlService(_mockRepository.Object);
        }

        [Test]
        public async Task CreateValidModelReturnsShortenUrl()
        {
            var model = new UrlViewModel { CreatedBy = "user1", Url = "http://example.com" };
            _mockRepository.Setup(r => r.Create(It.IsAny<ShortenUrl>()))
                .Callback<ShortenUrl>(u => u.Id = 1);

            var result = await _urlService.Create(model);

            Assert.AreEqual("user1", result.CreatedBy);
            Assert.AreEqual(DateTime.Today, result.CreatedOn.Date);
            Assert.AreEqual("http://example.com", result.FullUrl);
            Assert.IsNotNull(result.Shorten);
        }

        [Test]
        public void GetAllReturnsIQueryableOfShortenUrl()
        {
            var data = new List<ShortenUrl>
            {
                new ShortenUrl { Id = 1, FullUrl = "http://example1.com", Shorten = "abcde" },
                new ShortenUrl { Id = 2, FullUrl = "http://example2.com", Shorten = "fghij" },
                new ShortenUrl { Id = 3, FullUrl = "http://example3.com", Shorten = "klmno" },
            }.AsQueryable();
            _mockRepository.Setup(r => r.GetAll()).Returns(data);

            var result = _urlService.GetAll();

            Assert.IsInstanceOf<IQueryable<ShortenUrl>>(result);
            Assert.AreEqual(3, result.Count());
        }

        [Test]
        public void GetByIdValidIdReturnsShortenUrl()
        {
            var id = 1;
            var expected = new ShortenUrl { Id = id, FullUrl = "http://example.com", Shorten = "abcde" };
            _mockRepository.Setup(r => r.Get(id)).Returns(expected);

            var result = _urlService.GetById(id);

            Assert.IsNotNull(result);
            Assert.AreEqual(expected.Id, result.Id);
            Assert.AreEqual(expected.FullUrl, result.FullUrl);
            Assert.AreEqual(expected.Shorten, result.Shorten);
        }

        [Test]
        public async Task RemoveValidIdRemovesShortenUrl()
        {
            var id = 1;
            var url = new ShortenUrl { Id = id, FullUrl = "http://example.com", Shorten = "abcde" };
            _mockRepository.Setup(r => r.Get(id)).Returns(url);

            await _urlService.Remove(id);

            _mockRepository.Verify(r => r.Remove(url), Times.Once);
            _mockRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
    }
}
