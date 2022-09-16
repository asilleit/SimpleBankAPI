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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle//
builder.Services.AddDbContext<postgresContext>(option => option.UseNpgsql(builder.Configuration["ConnectionStrings:DbConnection"]));

builder.Services.AddTransient<IUsersDb, UsersDb>();
builder.Services.AddTransient<IUserBusiness, UserBusiness>();
builder.Services.AddTransient<IAccountsDb, AccountsDb>();
builder.Services.AddTransient<IAccountsBusiness, AccountsBusiness>();

builder.Services.AddTransient<ITransfersDb, TransferDb>();
builder.Services.AddTransient<ITransfersBusiness, TransfersBusiness>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSwaggerGen();
   

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

