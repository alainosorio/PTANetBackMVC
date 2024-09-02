namespace Retailer.Setup;

public static class ExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(builder =>
        {
            builder.Run(async context =>
            {
                context.Response.ContentType = Application.Json;

                var errorData = context.Features.Get<IExceptionHandlerPathFeature>()!;

                var statusCode = HttpStatusCode.BadRequest;

                var path = $"{errorData.Path}{context.Request.QueryString}";

                var response = new HttpExceptionResult
                {
                    Data = errorData.Error.Data,
                    Message = errorData.Error.Message,
                    Path = path,
                    StatusCode = (int)statusCode,
                };

                context.Response.StatusCode = (int)statusCode;

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            });
        });

        return app;
    }
}
