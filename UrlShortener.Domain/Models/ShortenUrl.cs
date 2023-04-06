namespace UrlShortener.Domain.Models
{
    public class ShortenUrl : Base
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string FullUrl { get; set; }
        public string Shorten { get; set; }
    }
}
