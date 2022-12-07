using Microsoft.AspNetCore.Components.Authorization;
using Blazor;
using Blazor.Data;
using Blazor.Data.Providers;
using Blazor.Data.Services;
using Blazor.Data.Services.Base;
using Blazor.Data.Services.Interfaces;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddTransient<WeatherForecastService>();
//builder.Services.AddTransient<IUsersService, UsersService>();
//builder.Services.AddTransient<IClient, Client>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7248/") });
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
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
