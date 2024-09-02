namespace Retailer.Setup;

// TODO: SQL Server Readyness

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
