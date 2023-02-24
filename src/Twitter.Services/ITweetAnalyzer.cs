using Twitter.Models;
using Tweetinvi.Models.V2;

namespace Twitter.Services
{
    public interface ITweetAnalyzer
    {
        void Add(Tweet tweets);
        int GetCount();
        List<Hashtag> GetTopHashtags(int number);
    }
}
