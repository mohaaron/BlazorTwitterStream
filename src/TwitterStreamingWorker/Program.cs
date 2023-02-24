using Twitter.Services;
using Twitter.Services.Hubs;
using TwitterStreamingWorker;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
builder.Services.AddSingleton<ITwitterStreamingApiService, TwitterStreamingApiService>();
builder.Services.AddHostedService<Worker>();

var app = builder.Build();

app.MapHub<TweetHub>("/tweethub");

app.Run();