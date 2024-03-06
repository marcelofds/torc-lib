using System.Reflection;
using System.Text;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using TorcLib.Application.Services;
using TorcLib.Application.Utils;
using TorcLib.Data.Context;
using TorcLib.Data.Repositories;

namespace TorcLib.IoC.Extensions;

public static class ServiceCollectionExtension
{
    private static IServiceCollection _services = null!;

    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        _services = services;
        ConfigureContext(configuration);
        ConfigureTools(configuration);
        RegisterApplicationServices(configuration);
        RegisterRepositories(configuration);
        ConfigureAuthentication(services);
    }

    private static void ConfigureAuthentication(IServiceCollection services)
    {
        var secretKey = Encoding.ASCII.GetBytes(Settings.Secret);
        services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
    }

    private static void RegisterApplicationServices(IConfiguration configuration)
    {
        _services.Scan(scan => scan.FromAssemblyOf<BookService>()
            .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service")))
            .AsMatchingInterface()
            .WithScopedLifetime()
        );
    }

    private static void RegisterRepositories(IConfiguration configuration)
    {
        _services.Scan(scan => scan.FromAssemblyOf<BookRepository>()
            .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Repository")))
            .AsMatchingInterface()
            .WithScopedLifetime()
        );
    }

    private static void ConfigureTools(IConfiguration configuration)
    {
        _services.AddSingleton(TypeAdapterConfig.GlobalSettings);
        _services.AddScoped<IMapper, ServiceMapper>();
    }

    public static void AddSwaggerDocumentations(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo {Title = "APIContagem", Version = "v1"});
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description =
                    "JWT Authorization Header - using with Bearer Authentication.\r\n\r\n" +
                    "Type 'Bearer' [space] and then you token in the field bellow.\r\n\r\n" +
                    "Sample (without cotes): 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
    }

    private static void ConfigureContext(IConfiguration configuration)
    {
        
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .CreateLogger();
        
        var migrationsAssembly = typeof(LibraryContext).GetTypeInfo().Assembly.GetName().Name;
        var dbHost     = Environment.GetEnvironmentVariable("DB_HOST");
        var dbName     = Environment.GetEnvironmentVariable("DB_NAME");
        var dbUser     = Environment.GetEnvironmentVariable("DB_USER");
        var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
        
        string connectionString = $"Server={dbHost};Database={dbName};User={dbUser};Password={dbPassword};";
        Log.Debug($"The connections string: {connectionString}");
        //Connection injected to be used into the repositories.
        _services
            .AddDbContext<LibraryContext>(builder =>
            {
                builder.UseSqlServer(connectionString,
                    options =>
                    {
                        options.MigrationsAssembly(migrationsAssembly);
                        options.EnableRetryOnFailure(15, TimeSpan.FromSeconds(30),
                            null);
                    });
            });
    }
}