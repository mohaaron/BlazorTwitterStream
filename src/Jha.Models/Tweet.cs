namespace Jha.Models
{
    public record struct Tweet(string AuthorId)
    {
        public List<Hashtag> Hashtags { get; set; } = new();
    }
}