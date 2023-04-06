using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Domain.Models
{
    public class Base
    {
        [Key]
        public int Id { get; set; }
    }
}
