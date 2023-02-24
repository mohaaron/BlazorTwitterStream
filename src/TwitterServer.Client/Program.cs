using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.ResponseCompression;
using TwitterServer.Client.Data;
using Twitter.Services;
using Twitter.Services.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();
builder.Services.AddSignalR();
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});

builder.Services.AddSingleton<WeatherForecastService>();

// Blazor Server: Scoped instantiates service per circuit lifetime
// Blazor WebAssembly: Scoped instantiates service per Browser/Tab  lifetime
builder.Services.AddSingleton<ITweetAnalyzer, TweetAnalyzer>();

// TODO: Move twitter service to external web server/api
// TODO: Singleton is not working as singleton
builder.Services.AddSingleton<ITwitterStreamingApiService, TwitterStreamingApiService>();

var app = builder.Build();

app.UseResponseCompression();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapHub<TweetHub>("/tweethub");
app.MapFallbackToPage("/_Host");

app.Run();
