using Contracts;
using CrossCuttingLayer;
using CurrencyConverter;
using Entities;
using Entities.DataTransferObjects.Outcoming;
using Entities.Models;
using Marvin.Cache.Headers;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Products.ActionFilters;
using Products.Managers;
using Repository.DataShaping;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Products.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                    .WithExposedHeaders("pagination"));
            });

        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options =>
            {
                options.AutomaticAuthentication = true;
                options.AuthenticationDisplayName = null;
                options.ForwardClientCertificate = true;
            });

        public static void ConfigureLoggerServices(this IServiceCollection services) =>
            services.AddScoped<ILoggerManager, LoggerManager>();

        public static void ConfigureAuthenticationManager(this IServiceCollection services) =>
            services.AddScoped<IAutenticationManager, AuthenticationManager>();

        public static void ConfigureRoleManager(this IServiceCollection services) =>
            services.AddScoped<RoleManager<IdentityRole>>();

        public static void ConfigureCurrencyApiConnection(this IServiceCollection services) =>
            services.AddScoped<ICurrencyApiConnection, CurrencyApiConnection>();

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration, 
            IWebHostEnvironment currentEnviroment)
        {
            if(currentEnviroment.EnvironmentName == "Testing")
            {
                services.AddDbContext<RepositoryContext>(options =>
                    options.UseInMemoryDatabase("TestingDB"));
            }
            else
            {
                services.AddDbContext<RepositoryContext>(options =>
                  options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), builder =>
                  builder.MigrationsAssembly("Products")));
            }
        }

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<User>(o =>
            {
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
                o.User.RequireUniqueEmail = true;
            });

            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole),
                builder.Services);

            builder.AddEntityFrameworkStores<RepositoryContext>()
                .AddDefaultTokenProviders();
        }

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = configuration.GetSection("SECRET").Value;

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    //ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
                    //ValidAudience = jwtSettings.GetSection("validAudience").Value,
                    ValidIssuer = "30",
                    ValidAudience = "https://localhost:5001",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("CodeMazeSecretKey"))
                };
            });
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Products API v1",
                    Version = "v1",
                    Description = "Products Web API",
                    Contact = new OpenApiContact
                    {
                        Name = "Victor Naumov",
                        Url = new Uri("https://vk.com/id267204544"),
                    }
                });
                s.SwaggerDoc("v2", new OpenApiInfo
                {
                    Title = "Products API v2",
                    Version = "v2"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                s.IncludeXmlComments(xmlPath);

                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Place to add JWT with Bearer",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                s.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Name = "Bearer",
                        },
                        new List<string>()
                    }
                });
            });
        }

        public static void ConfigureVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });
        }

        public static void ConfigureValidationAttributes(this IServiceCollection services)
        {
            services.AddScoped<ValidateCartAttribute>();
            services.AddScoped<ValidateAccountAttribute>();
            services.AddScoped<ValidateCategoryAttribute>();
            services.AddScoped<ValidateProductAttribute>();
            services.AddScoped<ValidateProviderAttribute>();
        }

        public static void ConfigureDataShaper(this IServiceCollection services)
        {
            services.AddScoped<IDataShaper<CategoryOutgoingDto>, DataShaper<CategoryOutgoingDto>>();
            services.AddScoped<IDataShaper<ProductOutgoingDto>, DataShaper<ProductOutgoingDto>>();
            services.AddScoped<IDataShaper<ProviderOutgoingDto>, DataShaper<ProviderOutgoingDto>>();
        }

        public static void ConfigureAutoMapper(this IServiceCollection services) =>
            services.AddAutoMapper(typeof(Startup));

        public static void ConfigureResponseCaching(this IServiceCollection services) =>
            services.AddResponseCaching(options =>
            {
                options.MaximumBodySize = 5096;
                options.UseCaseSensitivePaths = true;
            });

        public static void ConfigureHttpCacheHeaders(this IServiceCollection services) =>
            services.AddHttpCacheHeaders(
                (expirationOpt) =>
                {
                    expirationOpt.MaxAge = 65;
                    expirationOpt.CacheLocation = CacheLocation.Private;
                },
                (validationOpt) =>
                {
                    validationOpt.MustRevalidate = true;
                });

        public static void ConfigureMassTransit(this IServiceCollection services) =>
            services.AddMassTransit(x =>
            {
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
                {
                    config.Host(new Uri(RabbitMqConsts.RabbitMqRootUri), h =>
                    {
                        h.Username(RabbitMqConsts.UserName);
                        h.Password(RabbitMqConsts.Password);
                    });
                }));
            })
            .AddMassTransitHostedService();
    }
}
