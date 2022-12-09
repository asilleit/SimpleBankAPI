
// using Blazor.Data.Services.Interfaces;
// using Blazor.Data.Services;
// using Blazor.Data.Services.Base;
// using Blazor.Data.Providers;
// using Blazor.Data.Models;
// using System.Diagnostics.CodeAnalysis;
// using System.Collections;
// using Microsoft.Extensions.DependencyInjection;

// namespace Blazor.Data
// {
//     public static class BusinessRegistration
//     {
//         public static IServiceCollection AddServices(this IServiceCollection services)
//         {
//             builder.Services.AddRazorPages();
// builder.Services.AddServerSideBlazor();
// builder.Services.AddTransient<WeatherForecastService>();
// builder.Services.AddTransient<IUsersService, UsersService>();
//             //builder.Services.AddTransient<IClient, Client>();
// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7248/") });
// builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

//             return services;
//         }
//     }
// }