using Microsoft.Extensions.Logging;
using Tweetinvi;
using Tweetinvi.Core.Models;
using Tweetinvi.Events;
using Tweetinvi.Exceptions;
using Tweetinvi.Models;
using Tweetinvi.Models.V2;
using Tweetinvi.Streaming.V2;
using Tweetinvi.Streams;
using Jha.Models;
using Jha.Services.Events;

namespace Jha.Services
{
    public partial class TwitterService : ITwitterService
    {
        private readonly ILogger<TwitterService> _logger = default!;
        private readonly ISampleStreamV2 _tweetStream = default!;
        private readonly ITweetAnalyzer _tweetAnalyzer = default!;
        private Dictionary<string, Hashtag> _distinctHashtags = new(StringComparer.OrdinalIgnoreCase);

        public TwitterService(ILogger<TwitterService> logger, ITweetAnalyzer tweetAnalyzer)
        {
            _logger = logger;
            _tweetAnalyzer = tweetAnalyzer;

            // TODO: Move credentials somewhere
            var appCredentials = new ConsumerOnlyCredentials("V825lD3tHNpAu8bNxBgdKjadf", "P2SdiJwYULWMKSCejW9k8KStThIrLNgHrj4AjzqOZmi5dj1ytU")
            {
                BearerToken = "AAAAAAAAAAAAAAAAAAAAALN%2BlgEAAAAA9ooB6zKDXMMoy514X0aPkVMX0d0%3DEJ8ug132nKUAuKjYdwCRNd5FhZD6w4PFTeibFOoqgs4Gbq3BW1" // bearer token is optional in some cases
            };

            var twitterClient = new TwitterClient(appCredentials);
            twitterClient.Config.RateLimitTrackerMode = RateLimitTrackerMode.TrackAndAwait;

            try
            {
                _tweetStream = twitterClient.StreamsV2.CreateSampleStream();
                _tweetStream.TweetReceived += (sender, args) =>
                {
                    ProcessTweet(args.Tweet);
                };
            }
            catch (TwitterException e)
            {
                if (e.StatusCode == 429)
                {
                    // TODO: This is never being hit
                    // Rate limits allowance have been exhausted - do your custom handling
                }
            }
        }

        private void ProcessTweet(TweetV2 tweetV2)
        {
            if (tweetV2 == null)
            {
                _logger.LogDebug("Tweet is null.");
                return;
            }

            Models.Tweet tweet = new()
            {
                AuthorId = tweetV2.AuthorId,
                Hashtags = GetTweetHashtags(tweetV2)
            };

            OnTweetPublished(tweet);
        }

        private List<Hashtag> GetTweetHashtags(TweetV2 tweet)
        {
            if (tweet.Entities.Hashtags != null)
            {
                HashtagV2[] hastagsV2 = tweet.Entities.Hashtags;
                return hastagsV2
                    .Select(t => new Hashtag
                    {
                        Tag = t.Tag,
                    })
                    .ToList();
            }
            return new List<Hashtag>();
        }

        public async Task StartStreamAsync()
        {
            await _tweetStream.StartAsync();
        }

        public void StopStream()
        {
            _tweetStream.StopStream();
        }

        public event EventHandler<TweetPublishedEventArgs> TweetPublished = default!;
        protected virtual void OnTweetPublished(Models.Tweet tweet)
        {
            if (TweetPublished != null)
            {
                TweetPublished?.Invoke(this, new TweetPublishedEventArgs { Tweet = tweet });
            }
        }
    }
}