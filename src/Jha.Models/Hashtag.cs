namespace Jha.Models
{
    public record Hashtag
    {
        public int Count { get; set; } = 0;
        public int Rank { get; set; } = 0;
        public string Tag { get; set; } = default!;
    }
}