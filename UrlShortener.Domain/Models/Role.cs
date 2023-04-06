using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Domain.Models
{
    public enum Role
    {
        [Display(Name = "User")]
        User,
        [Display(Name = "Admin")]
        Admin
    }
}
