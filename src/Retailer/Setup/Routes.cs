namespace Retailer.Setup;

public static class Routes
{
    public static WebApplication MapRoutes(this WebApplication app, ApiVersionSet versionSet)
    {
        app.MapPost(versionSet);
        app.MapGet(versionSet);

        return app;
    }
}
