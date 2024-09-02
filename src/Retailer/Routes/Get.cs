namespace Retailer.Routes;

public static class Get
{
    public static WebApplication MapGet(this WebApplication app, ApiVersionSet versionSet)
    {
        Retailer(app, versionSet);

        return app;
    }

    private static void Retailer(WebApplication app, ApiVersionSet versionSet)
    {
        app.MapGet("api/retailers/{id}", (string id) =>
        {
            return Results.Ok(id);
        })
        .WithApiVersionSet(versionSet)
        .MapToApiVersion(1.0)
        .WithTags("retailer")
        .WithOpenApi();
    }
}
