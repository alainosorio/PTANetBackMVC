namespace Retailer.Routes;

public static class Post
{
    public static WebApplication MapPost(this WebApplication app, ApiVersionSet versionSet)
    {
        Retailer(app, versionSet);

        return app;
    }

    private static void Retailer(WebApplication app, ApiVersionSet versionSet)
    {
        app.MapPost("api/sync-retailers", async (IRetailerService service) =>
        {
            var response = await service.Sync();

            return Results.Ok(response);
        })
        .WithApiVersionSet(versionSet)
        .MapToApiVersion(1.0)
        .WithTags("retailer")
        .WithOpenApi();
    }
}
