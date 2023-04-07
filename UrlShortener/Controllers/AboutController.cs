using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Business.Interfaces;
using UrlShortener.Domain.Models;

namespace UrlShortener.Controllers
{
    [AllowAnonymous]
    public class AboutController : Controller
    {
        private readonly IAboutMessageService _aboutMessageService;

        public AboutController(IAboutMessageService aboutMessageService)
        {
            _aboutMessageService = aboutMessageService ?? throw new ArgumentNullException(nameof(aboutMessageService));
        }

        [HttpGet]
        public IActionResult Index()
        {
            var about = _aboutMessageService.GetAll().FirstOrDefault();
            return View(about);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit()
        {
            var about = _aboutMessageService.GetAll().FirstOrDefault();
            return View(about);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(AboutMessage about)
        {
            var newAbout = _aboutMessageService.GetAll().FirstOrDefault();
            if (newAbout != null)
            {
                newAbout.Message = about.Message;
                await _aboutMessageService.Update(newAbout);
            }
            return RedirectToAction("Index");
        }
    }
}
