using Jha.Models;
using Tweetinvi.Models.V2;

namespace Jha.Services
{
    public interface ITweetAnalyzer
    {
        void Add(Tweet tweets);
        int GetTweetCount();
        List<Tweet> GetTweets();
        List<Hashtag> GetTop10Hashtags();
    }
}
