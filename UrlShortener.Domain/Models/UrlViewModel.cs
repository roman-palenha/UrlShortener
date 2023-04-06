using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Domain.Models
{
    public class UrlViewModel
    {
        [Required]
        [RegularExpression(@"^(?:https?://)?(?:www\.)?[\w\-\.]+\.\w{2,}(?:\.\w{2,})?(?:[\w\-._/?=#&%]*?)$")]
        public string Url { get; set; }
        public string CreatedBy { get; set; }
    }
}
