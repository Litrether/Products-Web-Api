using System.IO;
using Contracts;
using CurrencyConverter.ExchangeRatesAbstractAPI;
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
using Products.Managers;

namespace Products
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration($"{Directory.GetCurrentDirectory()}/Properties/nlog.config");
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureCors();
            services.ConfigureIISIntegration();
            services.ConfigureLoggerServices();
            services.ConfigureSqlContext(Configuration);
            services.ConfigureRepositoryManager();
            services.ConfigureSwagger();
            services.ConfigureVersioning();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.ConfigureValidationAttributes();
            services.ConfigureDataShaper();

            services.AddScoped<IAutenticationManager, AuthenticationManager>();
            services.AddScoped<ICurrencyConverterManager, CurrencyConverterManager>();

            services.AddScoped<ICurrencyConnection, CurrencyConnectionERA>();

            services.AddAuthentication();
            services.AddAuthorization();

            services.ConfigureIdentity();
            services.ConfigureJWT(Configuration);

            services.AddAutoMapper(typeof(Startup));

            services.AddControllers(config =>
            {
                config.RespectBrowserAcceptHeader = true;
                config.ReturnHttpNotAcceptable = true;
            }).AddNewtonsoftJson()
                .AddXmlDataContractSerializerFormatters();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            ILoggerManager logger)
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
