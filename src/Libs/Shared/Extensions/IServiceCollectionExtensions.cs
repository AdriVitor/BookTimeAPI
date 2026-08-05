using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Shared.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication("Bearer")
                    .AddJwtBearer("Bearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]))
                };
            });

            return services;
        }

        //public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        //{
        //    services.SwaggerDocument(o =>
        //    {
        //        o.DocumentSettings = s =>
        //        {
        //            s.Title = "BookTime API";
        //            s.Version = "v1";
        //            s.AddAuth("Bearer", new()
        //            {
        //                Name = "Authorization",
        //                Description = "Enter: your JWT token",
        //                In = OpenApiSecurityApiKeyLocation.Header,
        //                Type = OpenApiSecuritySchemeType.Http,
        //                Scheme = "Bearer",
        //                BearerFormat = "JWT"
        //            });
        //        };

        //        o.EnableJWTBearerAuth = false;
        //    });

        //    return services;
        //}

        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "BookTime API",
                    Version = "v1"
                });

                // 🔐 JWT Bearer
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Enter: Bearer {your JWT token}",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                     }
                });
            });

            return services;
        }
    }
}

