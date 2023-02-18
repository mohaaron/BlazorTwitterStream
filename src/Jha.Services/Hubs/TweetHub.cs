using Twitter.Models;
using Microsoft.AspNetCore.SignalR;

namespace Twitter.Services.Hubs
{
    public interface ITweetHub
    {
        Task PublishTweet(Tweet tweet);
    }

    public class TweetHub : Hub<ITweetHub>
    {
        public async Task PublishToAll(Tweet tweet)
        {
            await Clients.All.PublishTweet(tweet);
        }
    }
}
