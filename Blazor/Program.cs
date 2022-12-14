using Microsoft.AspNetCore.Components.Authorization;
using Blazor;
using Blazor.Data;
using Blazor.Data.Providers;
using Blazor.Data.Services;
using Blazor.Data.Services.Base;
using Blazor.Data.Services.Interfaces;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using System.Runtime.Versioning;
using System.Net.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddTransient<WeatherForecastService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IAccountsService, AccountsService>();
builder.Services.AddScoped<ITransfersService, TransfersService>();
builder.Services.AddTransient<IClient, Client>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7248/") });
// builder.Services.AddHttpClient<IClient, Client>(cl => cl.BaseAddress = new Uri("https://localhost:7248/"));
// builder.Services.AddTransient<IClient, Client>();
// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7248/") });
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

//builder.Services.AddServices();
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
