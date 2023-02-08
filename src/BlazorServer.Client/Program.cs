using BlazorServer.Client.Data;
using Jha.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddScoped<ITweetAnalyzer, TweetAnalyzer>(); // Scoped keeps service for the life of the circuit
builder.Services.AddScoped<ITwitterService, TwitterService>(); // Scoped keeps service for the life of the circuit
//builder.Services.AddTransient<ITwitterService, TwitterService>(); // BUG: Transient is caused an error when navigating back to page

var app = builder.Build();

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
app.MapFallbackToPage("/_Host");

app.Run();
