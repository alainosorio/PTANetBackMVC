namespace Retailer.Setup;

public static class App
{
    public static WebApplication ConfigureApp(this WebApplication app, ApiVersionSet versionSet)
    {
        app.UseCors("DefaultCorsPolicy");

        app.UseExceptionMiddleware();

        app.MapRoutes(versionSet);

        app.MapHealthChecks("/liveness", new HealthCheckOptions
        {
            Predicate = _ => _.Tags.Contains("liveness")
        });

        app.MapHealthChecks("/readiness", new HealthCheckOptions
        {
            Predicate = _ => _.Tags.Contains("readiness")
        });

        app.MapHealthChecks("/startup", new HealthCheckOptions
        {
            Predicate = _ => _.Tags.Contains("startup")
        });

        app.UseRateLimiter();

        return app;
    }
}
