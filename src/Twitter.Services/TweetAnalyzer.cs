using Twitter.Models;
using System.Collections.Concurrent;

namespace Twitter.Services
{
    public class TweetAnalyzer : ITweetAnalyzer
    {
        private int _total = 0;
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
            _total++;
            AddHashtags(tweet.Hashtags.ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetCount() => _total;

        private void AddHashtags(Hashtag[] hashtags)
        {
            for (int i = 0; i < hashtags.Length; i++)
            {
                string tag = hashtags[i].Tag;

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

        public List<Hashtag> GetTopHashtags(int number)
        {
            int rank = 1;
            return _distinctHashtags
                .Where(dict => dict.Value.Count > 1)
                .OrderByDescending(dict => dict.Value.Count)
                .Take(number)
                .Select(dict => new Hashtag(dict.Value.Tag)
                {
                    Rank = rank++,
                    Count = dict.Value.Count,
                })
                .ToList();
        }
    }
}
