using Twitter.Models;
using System.Collections.Concurrent;

namespace Twitter.Services
{
    public class TweetAnalyzer : ITweetAnalyzer
    {
        private List<Tweet> _tweets = new();
        private ConcurrentDictionary<string, Hashtag> _distinctHashtags = new();

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

                // TODO: Why does this if stop working when this is a singleton in DI?
                if (!_distinctHashtags.TryGetValue(tag, out var hashtag))
                {
                    _distinctHashtags.TryAdd(tag, new Hashtag(tag)
                    {
                        Count = 1,
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
                .Select(dict => new Hashtag(dict.Value.Tag)
                {
                    Rank = rank++,
                    Count = dict.Value.Count,
                })
                .ToList();
        }
    }
}
