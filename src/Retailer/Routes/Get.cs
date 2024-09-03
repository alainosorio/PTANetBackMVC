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
        app.MapGet("api/retailers/{id}", async(IRetailerService service, string id) =>
        {
            var response = await service.GetRetailerById(id);

            return Results.Ok(response);
        })
        .AllowAnonymous()
        .WithApiVersionSet(versionSet)
        .MapToApiVersion(1.0)
        .WithTags("retailer")
        .WithOpenApi();
    }
}
