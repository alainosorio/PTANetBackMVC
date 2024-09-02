var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddOpenApi()
    .ConfigureAppDependencies()
    .ConfigureAppServices(builder.Configuration)
    .ConfigureAppSettings(builder.Configuration)
    .ConfigureCors(builder.Configuration)
    .ConfigureHealthChecks()
    .ConfigureHttpClient(builder.Configuration)
    .ConfigureRateLimit()
    .ConfigureRedis(builder.Configuration);

var app = builder.Build();

var versionSet = app.NewApiVersionSet()
    .HasApiVersion(1.0)
    .ReportApiVersions()
    .Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.MapOpenApi();
}
else
{
    app.UseHttpsRedirection();
}

app.ConfigureApp(versionSet);

app.Run();

