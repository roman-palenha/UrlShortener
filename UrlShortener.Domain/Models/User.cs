using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Domain.Models
{
    public class User : Base
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public Role Role { get; set; }
    }
}
