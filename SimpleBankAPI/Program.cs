using Microsoft.EntityFrameworkCore;
//using SimpleBankAPI.Data;
using Microsoft.Extensions.Configuration;
//using SimpleBankAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using System.Security.Cryptography.Xml;
using Microsoft.Extensions.DependencyInjection;
using SimpleBankAPI.Models;
using SimpleBankAPI.Data;
using SimpleBankAPI.Business;
using SimpleBankAPI.Interfaces;
using System.Buffers.Text;
using SimpleBankAPI.JWT;
using Npgsql;
using System.Data;
using NuGet.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
//Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle//

builder.Services.AddDbContext<postgresContext>(option => option.UseNpgsql(builder.Configuration["ConnectionStrings:DbConnection"]));
builder.Services.AddScoped<IDbTransaction>(s =>
{
    NpgsqlConnection connection = (NpgsqlConnection)s.GetRequiredService<IDbConnection>();
    connection.Open();
    return connection.BeginTransaction();
});

builder.Services.AddTransient<IUsersDb, UsersDb>();
builder.Services.AddTransient<IUserBusiness, UserBusiness>();
builder.Services.AddTransient<IAccountsDb, AccountsDb>();
builder.Services.AddTransient<IAccountsBusiness, AccountsBusiness>();
builder.Services.AddTransient<ITransfersDb, TransferDb>();

builder.Services.AddTransient<ITokenDb, TokenDb>();

builder.Services.AddTransient<ITokenBusiness, TokenBusiness>();
builder.Services.AddTransient<ITransfersBusiness, TransfersBusiness>();
builder.Services.AddTransient<IJwtAuth, JwtAuth>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        RequireSignedTokens = true,
        RequireExpirationTime = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]))
    };
});


builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});


builder.Services.AddAuthorization();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseHttpsRedirection();
app.Run();

