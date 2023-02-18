using Twitter.Services;
using Twitter.Services.Events;
using Twitter.Services.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;

namespace TwitterStreamingWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ITwitterStreamingService _twitterService;
        private readonly IHubContext<TweetHub, ITweetHub> _hubContext;

        public Worker(ILogger<Worker> logger, ITwitterStreamingService twitterService, IHubContext<TweetHub, ITweetHub> hubContext)
        {
            _logger = logger;
            _twitterService = twitterService;
            _hubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _twitterService.TweetPublished += OnTweetPublished;
            await _twitterService.StartStreamAsync();

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }

        private void OnTweetPublished(object? sender, TweetPublishedEventArgs args)
        {
            _hubContext.Clients.All.PublishTweet(args.Tweet);
        }
    }
}