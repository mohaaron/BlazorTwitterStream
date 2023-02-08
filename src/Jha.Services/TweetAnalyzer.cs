using Jha.Models;
using Tweetinvi.Models.V2;

namespace Jha.Services
{
    public class TweetAnalyzer : ITweetAnalyzer
    {
        private List<Tweet> _tweets = new();
        private Dictionary<string, Hashtag> _distinctHashtags = new();

        public TweetAnalyzer()
        {
            //
        }

        /// <summary>
        /// Add a tweet to be analyzed.
        /// </summary>
        /// <param name="tweet"></param>
        public void Add(Tweet tweet)
        {
            _tweets.Add(tweet);
            AddHashtags(tweet.Hashtags.ToArray());
        }

        public List<Tweet> GetTweets() => _tweets;

        /// <summary>
        /// TEST: Tweet count should equal tweets added.
        /// </summary>
        /// <returns></returns>
        public int GetTweetCount() => _tweets.Count;

        public void AddHashtags(Hashtag[] hashtags)
        {
            for (int i = 0; i < hashtags.Length; i++)
            {
                string tag = hashtags[i].Tag;

                if (!_distinctHashtags.TryGetValue(tag, out var hashtag))
                {
                    _distinctHashtags.Add(tag, new Hashtag
                    {
                        Count = 1,
                        Tag = tag
                    });
                }
                else
                {
                    hashtag.Count += 1;
                }
            }
        }

        public List<Hashtag> GetTop10Hashtags()
        {
            // TEST: 10 results from 15 hashtags
            // TEST: Results are ordered correctly
            // TEST: Input hashtags with count of 1 returns 0 results.

            int rank = 1;

            return _distinctHashtags
                .Where(dict => dict.Value.Count > 1)
                .OrderByDescending(dict => dict.Value.Count)
                .Take(10)
                .Select(dict => new Hashtag
                {
                    Rank = rank++,
                    Count = dict.Value.Count,
                    Tag = dict.Value.Tag
                })
                .ToList();
        }
    }
}
