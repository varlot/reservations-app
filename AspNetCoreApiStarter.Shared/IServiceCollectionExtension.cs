using AspNetCoreApiStarter.Shared.Logger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace AspNetCoreApiStarter.Shared
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddSharedLibrary(this IServiceCollection services, IConfigurationRoot Configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            // Register Application Logger
            // services.AddSingleton<ILoggerHelper, LoggerHelper>();
            services.AddSingleton(typeof(ILoggerHelper<>), typeof(LoggerHelper<>));

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                var swaggConf = Configuration.GetSection(nameof(AppConfig.Swagger));
                string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                c.SwaggerDoc(version, new OpenApiInfo
                {
                    Title = swaggConf[nameof(AppConfig.Swagger.Title)],
                    Version = $"v{version}",
                    Contact = new OpenApiContact {
                        Name = swaggConf[nameof(AppConfig.Swagger.ContactName)],
                        Email = swaggConf[nameof(AppConfig.Swagger.ContactEmail)],
                        Url = new Uri(swaggConf[nameof(AppConfig.Swagger.ContactUrl)]),
                    },
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer schemea",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                        },
                        new string[] { }
                    },
                });

                // Set the comments path for the Swagger JSON and UI.
                var basePath = System.AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, $"{Configuration[nameof(AppConfig.AppName)]}.xml");
                c.IncludeXmlComments(xmlPath);
            });

            return services;
        }
    }
}
