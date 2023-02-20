using Twitter.Services;
using Twitter.Services.Hubs;
using TwitterStreamingWorker;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
builder.Services.AddSingleton<ITwitterStreamingService, TwitterStreamingService>();
builder.Services.AddHostedService<Worker>();

var app = builder.Build();

app.MapHub<TweetHub>("/tweethub");

app.Run();