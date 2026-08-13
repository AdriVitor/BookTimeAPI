using Microsoft.EntityFrameworkCore;
using UserService_Application.Services;
using UserService_Application.Services.Interfaces;
using UserService_Infraestructure.Context;
using UserService_Infraestructure.Repositories;
using UserService_Infraestructure.Repositories.Interfaces;
using UserRoles.Services;
using UserService_API.Extensions;
using MassTransit;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ContextDb>(db =>
{
    db.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly(typeof(ContextDb).Assembly.FullName));
});

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

builder.Services.AddVersionedApiExplorer(o =>
{
    o.GroupNameFormat = "'v'VVV";
    o.SubstituteApiVersionInUrl = true;
});

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRolesService, RolesService>();
builder.Services.AddScoped<IUserRolesService, UserRolesService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRolesRepository, RolesRepository>();
builder.Services.AddScoped<IUserRolesRepository, UserRolesRepository>();

builder.Services.AddConfigurationMessageBroker(builder.Configuration);
builder.Services.AddMassTransitHostedService();

builder.Services.ConfigureJwt(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();

var app = builder.Build();

app.UseRouting();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
