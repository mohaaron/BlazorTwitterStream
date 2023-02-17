using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.ResponseCompression;
using BlazorServer.Client.Data;
using Jha.Services;
using Jha.Services.Hubs;
using Microsoft.AspNetCore.SignalR.Client;
using BlazorServer.Client.Shared;

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
builder.Services.AddScoped<ITweetAnalyzer, TweetAnalyzer>(); // Scoped keeps service for the life of the circuit
builder.Services.AddSingleton<ITwitterService, TwitterService>(); // TODO: Move twitter service to external web server/api
//builder.Services.AddTransient<ITwitterService, TwitterService>(); // BUG: Transient is caused an error when navigating back to page

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
