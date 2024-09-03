namespace Retailer.Setup;

public static class Services
{
    private const string apiEndpoint = "https://api.opendata.esett.com";

    public static IServiceCollection ConfigureAppServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IRetailerService, RetailerService>();
        services.AddScoped<IStorageService, StorageService>();

        return services;
    }

    public static IServiceCollection ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RetailerDbContext>(options =>
        {
            var env = Environment.GetEnvironmentVariable("DOCKER_ASPNETCORE_ENVIRONMENT");

            var connectionString = env == "Container"
                ? Environment.GetEnvironmentVariable("ALICUNDE_RETAILER_SQL_SERVICE")
                : Environment.GetEnvironmentVariable("ALICUNDE_RETAILER_SQL_MIGRATIONS");

            options.UseSqlServer(connectionString);
        });

        return services;
    }

    public static IServiceCollection ConfigureAppDependencies(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Program).Assembly);
        services.AddMediatR(_ => _.RegisterServicesFromAssembly(MediatorAssembly.Assembly));

        services.AddApiVersioning(_ =>
        {
            _.ApiVersionReader = new QueryStringApiVersionReader();
            _.ReportApiVersions = true;
        });

        return services;
    }

    public static IServiceCollection ConfigureHttpClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<IApiService, ApiService>(_ =>
        {
            _.BaseAddress = new Uri(apiEndpoint);
        })
        .AddStandardResilienceHandler();

        return services;
    }

    public static IServiceCollection ConfigureCors(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("DefaultCorsPolicy",
                builder =>
                {
                    var origin = configuration.GetSection("CORS:ORIGIN").Get<string>();

                    builder.WithOrigins(origin!)
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });

        return services;
    }

    public static IServiceCollection ConfigureRedis(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(_ =>
        {
            _.Configuration = configuration.GetSection("REDIS_CACHE").Value;
        });

        return services;
    }

    public static IServiceCollection ConfigureHealthChecks(this IServiceCollection services)
    {
        services.AddHealthChecks()
            .AddCheck<Liveness>("liveness", HealthStatus.Unhealthy, tags: ["liveness"])
            .AddCheck<SqlServerHealthCheck>("sqlserver_readyness", HealthStatus.Unhealthy, tags: ["readiness"])
            .AddCheck<Startup>("startup", HealthStatus.Unhealthy, tags: ["startup"]);

        return services;
    }

    public static IServiceCollection ConfigureRateLimit(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
            {
                return RateLimitPartition.GetFixedWindowLimiter("any", _ => new FixedWindowRateLimiterOptions
                {
                    PermitLimit = 6,
                    QueueLimit = 9,
                    QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                    Window = TimeSpan.FromSeconds(3),
                });
            });

            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
        });

        return services;
    }
}
