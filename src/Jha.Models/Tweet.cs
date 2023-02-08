namespace Jha.Models
{
    public record Tweet
    {
        public string AuthorId { get; set; } = default!;
        public List<Hashtag> Hashtags { get; set; } = new();
    }
}