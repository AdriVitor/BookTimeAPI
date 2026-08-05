using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

builder.Services.ConfigureAuthentication(builder.Configuration);

builder.Services.AddOcelot();

var app = builder.Build();

app.UseAuthentication();

await app.UseOcelot();

app.Run();
