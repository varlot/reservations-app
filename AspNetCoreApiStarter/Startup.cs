using AspNetCoreApiStarter.Bll;
using AspNetCoreApiStarter.Dal;
using AspNetCoreApiStarter.Dal.Contexte;
using AspNetCoreApiStarter.Middlewares;
using AspNetCoreApiStarter.Resources;
using AspNetCoreApiStarter.Shared;
using AspNetCoreApiStarter.ViewModels.Core;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Reflection;

namespace AspNetCoreApiStarter
{
    /// <summary>
    /// Application Startup.
    /// </summary>
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="env">Hosting environment.</param>
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            this.Configuration = builder.Build();

            // Configure the Serilog pipeline
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(this.Configuration)
                .CreateLogger();
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        public IConfigurationRoot Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">Collection of services descriptors.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Register App configuration on the DI container (db connection,...)
            services.Configure<AppConfig>(this.Configuration);

            // Register BLLs
            services.AddBllLibrary();

            // Register DALs
            services.AddDalLibrary();

            // Register localizer
            services.AddResourcesLibrary();

            // Cors
            services.AddCors(options =>
            {
                options.AddPolicy(
                    name: MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                            .WithMethods("DELETE", "GET");
                    });
            });

            // Register helpers library (logger,...)
            services.AddSharedLibrary(this.Configuration);

            // Add Mvc
            // FV = > find any public, non-abstract types that inherit from AbstractValidator and register them with the container
            services
                .AddMvc();

            services.AddControllersWithViews();
            services.AddRazorPages();

            // configure le nom des propriétés de validation
            FluentValidationConfig.Config();

            // Add Cors policy
            services.AddCors();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen();

            // acces au context http
            services.AddHttpContextAccessor();

            services.AddDbContext<ApiContexte>(opt => opt.UseInMemoryDatabase("ReservationDb"));
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">Application request pipeline.</param>
        /// <param name="env">Hosting environement.</param>
        /// <param name="loggerFactory">logger factory.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Add Serilog to the logging pipeline
            loggerFactory.AddSerilog();
			app.UseRouting();

            // Add Localization
            app.UseRequestLocalization();

            // Enable default files
            app.UseDefaultFiles();

            // Enable static files
            app.UseStaticFiles();

            // Enable authentication middleware
            app.UseAuthentication();

            // Enable middleware for handling errors.
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                var swaggConf = this.Configuration.GetSection(nameof(AppConfig.Swagger));
                string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
#if DEBUG
                c.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"{swaggConf[nameof(AppConfig.Swagger.Title)]} {version} (DEBUG)");
#else
                c.SwaggerEndpoint($"{swaggConf[nameof(AppConfig.Swagger.VirtualDirectory)]}/swagger/{version}/swagger.json", $"{swaggConf[nameof(AppConfig.Swagger.Title)]} {version} (RELEASE)");
#endif
            });

            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            app.UseRouting();

            // Enabling Cross-Origin Requests (CORS)
            app.UseCors(this.MyAllowSpecificOrigins);

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
