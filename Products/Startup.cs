using Contracts;
using Messenger.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using Products.Extensions;
using System.IO;

namespace Products
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment _currentEnviroment;

        public Startup(IConfiguration configuration, IWebHostEnvironment currentEnviroment)
        {
            LogManager.LoadConfiguration($"{Directory.GetCurrentDirectory()}/nlog.config");
            Configuration = configuration;
            _currentEnviroment = currentEnviroment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication();
            services.AddAuthorization();
            services.AddControllers(config =>
            {
                config.RespectBrowserAcceptHeader = true;
                config.ReturnHttpNotAcceptable = true;
            }).AddNewtonsoftJson();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            services.ConfigureAuthenticationManager();
            services.ConfigureAutoMapper();
            services.ConfigureCors();
            services.ConfigureCurrencyApiConnection();
            services.ConfigureDataShaper();
            services.ConfigureHttpCacheHeaders();
            services.ConfigureIdentity();
            services.ConfigureIISIntegration();
            services.ConfigureJWT(Configuration);
            services.ConfigureLoggerServices();
            services.ConfigureMassTransit();
            services.ConfigureRepositoryManager();
            services.ConfigureResponseCaching();
            services.ConfigureRoleManager();
            services.ConfigureSqlContext(Configuration, _currentEnviroment);
            services.ConfigureSwagger();
            services.ConfigureValidationAttributes();
            services.ConfigureVersioning();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerManager logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.ConfigureExceptionHandler(logger);
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors("CorsPolicy");
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Products API v1");
                s.SwaggerEndpoint("/swagger/v2/swagger.json", "Products API v2");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}