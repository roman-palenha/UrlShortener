using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using UrlShortener.Business.Interfaces;
using UrlShortener.Domain.Models;

namespace UrlShortener.Controllers
{
    [Authorize]
    public class UrlController : Controller
    {
        private readonly IUrlService _urlService;

        public UrlController(IUrlService urlService)
        {
            _urlService = urlService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("/{path}")]
        public IActionResult Go(string path)
        {
            if (path == null || path == string.Empty || !Regex.IsMatch(path, "^[a-zA-Z0-9]*$"))
                return BadRequest();

            var id = _urlService.Decode(path);
            var origin = _urlService.GetById(id);
            if (origin == null)
                return NotFound();

            return Redirect(origin.FullUrl);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _urlService.GetAll();
            if (!User.IsInRole("Admin"))
                result = result.Where(x => x.CreatedBy.Equals(User.Identity.Name));

            return Ok(result);
        }

        [HttpGet]
        public IActionResult Info(int id)
        {
            var result = _urlService.GetById(id);
            if (!User.IsInRole("Admin") && !User.Identity.Name.Equals(result.CreatedBy))
            {
                return Unauthorized();
            }

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]UrlViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedBy = User.Identity.Name;
                var result = await _urlService.Create(model);
                return Ok(result);
            }
           
            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> Remove(int id)
        {
            await _urlService.Remove(id);
            return Ok();
        }
    }
}
