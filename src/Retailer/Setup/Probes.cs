namespace Retailer.Setup;

public class Liveness : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(HealthCheckResult.Healthy());
    }
}

public class Startup : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var init = DateTimeOffset.Now;

        Task.Delay(1, cancellationToken);

        var end = DateTimeOffset.Now;

        return Task.FromResult(init >= end
            ? HealthCheckResult.Unhealthy("Startup check failed")
            : HealthCheckResult.Healthy("Startup check succeeded"));
    }
}

public class SqlServerHealthCheck(IConfiguration configuration) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            var connectionString = configuration.GetSection("ALICUNDE_RETAILER_SQL_SERVICE").Value;

            using var connection = new SqlConnection(connectionString);

            await connection.OpenAsync(cancellationToken);

            return HealthCheckResult.Healthy("SQL Server is healthy.");
        }
        catch (SqlException ex)
        {
            return HealthCheckResult.Unhealthy("SQL Server is unhealthy.", ex);
        }
    }
}
