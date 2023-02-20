using TwitterWasm.Client;
using Twitter.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<ITwitterStreamingService, TwitterStreamingService>(); // BUG: Tweetinvi.ISampleStreamV2.TweetReceived event never fires

await builder.Build().RunAsync();
