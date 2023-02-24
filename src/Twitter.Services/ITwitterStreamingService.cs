using Tweetinvi.Models.V2;
using Twitter.Models;
using Twitter.Services.Events;

namespace Twitter.Services
{
    public interface ITwitterStreamingApiService
    {
        /// <summary>
        /// Use to bubble tweets from service to subscriber.
        /// </summary>
        event EventHandler<TweetPublishedEventArgs> TweetPublished;

        /// <summary>
        /// Start streaming.
        /// </summary>
        /// <returns></returns>
        Task StartStreamAsync();

        /// <summary>
        /// Stop streaming.
        /// </summary>
        void StopStream();
    }
}