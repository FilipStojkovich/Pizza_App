using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PizzaApp.DataAccess.DbContext;
using PizzaApp.Domain.Entities;
using PizzaApp.Helpers.DependencyInjectionHelper;
using PizzaApp.Helpers.Extensions;
using PizzaApp.Mappers.MappersConfig;
using PizzaApp.Shared.Settings;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Get app settings and the connection string
var appSettings = builder.Configuration.GetSection("AppSettings");

builder.Configuration.AddEnvironmentVariables();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly)
	.AddPostgreSqlDbContext(appSettings)
	.AddAuthentication()
	.AddJWT(appSettings)
	.AddIdentity()
	.AddCors()
	.AddSwager();

DependencyInjectionHelper.InjectRepositories(builder.Services);
DependencyInjectionHelper.InjectServices(builder.Services);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.UseCors(builder =>
{
	builder.AllowAnyOrigin();
	builder.AllowAnyMethod();
	builder.AllowAnyHeader();
});

app.Run();