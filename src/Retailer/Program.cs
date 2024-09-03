var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddOpenApi()
    .ConfigureAppDependencies()
    .ConfigureAppServices()
    .ConfigureCors(builder.Configuration)
    .ConfigureDbContext()
    .ConfigureHealthChecks()
    .ConfigureHttpClient()
    .ConfigureRateLimit()
    .ConfigureRedis(builder.Configuration);


builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();

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

