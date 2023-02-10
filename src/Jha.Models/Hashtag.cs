namespace Jha.Models
{
    public record Hashtag(string Tag)
    {
        public int Count { get; set; } = 0;
        public int Rank { get; set; } = 0;
    }
}