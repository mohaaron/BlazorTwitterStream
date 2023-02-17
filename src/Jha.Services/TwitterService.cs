using Microsoft.Extensions.Logging;
using Tweetinvi;
using Tweetinvi.Events;
using Tweetinvi.Exceptions;
using Tweetinvi.Models;
using Tweetinvi.Models.V2;
using Tweetinvi.Streaming.V2;
using Jha.Models;
using Jha.Services.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.SignalR;
using Jha.Services.Hubs;

namespace Jha.Services
{
    // TODO: Rename TwitterService to TwitterStreamingApiService
    public partial class TwitterService : ITwitterService
    {
        private readonly ILogger<TwitterService> _logger = default!;
        private readonly IConfiguration _configuration = default!;
        private readonly ISampleStreamV2 _tweetStream = default!;
        private readonly IHubContext<TweetHub, ITweetHub> _hubContext = default!;

        public TwitterService(
            ILogger<TwitterService> logger, 
            IConfiguration configuration, 
            IHubContext<TweetHub, ITweetHub> hubContext)
        {
            _logger = logger;
            _configuration = configuration;
            _hubContext = hubContext;

            // TODO: Move credentials somewhere
            string apiKey = configuration.GetSection("TwitterStreamingApi:ApiKey").Value!;
            string apiKeySecret = configuration.GetSection("TwitterStreamingApi:ApiKeySecret").Value!;
            string bearerToken = configuration.GetSection("TwitterStreamingApi:BearerToken").Value!;

            var appCredentials = new ConsumerOnlyCredentials(apiKey, apiKeySecret)
            {
                BearerToken = bearerToken
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
                _logger.LogDebug(e.ToString());

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

            Tweet tweet = new(tweetV2.AuthorId)
            {
                Hashtags = GetTweetHashtags(tweetV2)
            };

            _logger.LogDebug("Tweet published"); // TODO: Logging to LogDebug is not working
            _hubContext.Clients.All.PublishTweet(tweet); // TODO: Move SignalR tweet publishing to external service/server
            OnTweetPublished(tweet);
        }

        private List<Hashtag> GetTweetHashtags(TweetV2 tweet)
        {
            if (tweet.Entities.Hashtags != null)
            {
                HashtagV2[] hastagsV2 = tweet.Entities.Hashtags;
                return hastagsV2
                    .Select(t => new Hashtag(t.Tag))
                    .ToList();
            }
            return new List<Hashtag>();
        }

        public async Task StartStreamAsync()
        {
            try
            {
                await _tweetStream.StartAsync();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public void StopStream()
        {
            _tweetStream.StopStream();
        }

        public event EventHandler<TweetPublishedEventArgs> TweetPublished = default!;
        protected virtual void OnTweetPublished(Tweet tweet)
        {
            if (TweetPublished != null)
            {
                TweetPublished?.Invoke(this, new TweetPublishedEventArgs { Tweet = tweet });
            }
        }
    }
}