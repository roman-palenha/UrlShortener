using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Domain.Models;

namespace UrlShortener.Business.Interfaces
{
    public interface IAboutMessageService
    {
        public IQueryable<AboutMessage> GetAll();
        public Task Update(AboutMessage message);
    }
}
