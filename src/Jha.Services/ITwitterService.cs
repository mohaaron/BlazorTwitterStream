using Tweetinvi.Models.V2;
using Jha.Models;
using Jha.Services.Events;

namespace Jha.Services
{
    public interface ITwitterService
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