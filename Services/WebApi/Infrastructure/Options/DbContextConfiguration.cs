using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace WebApi.Infrastructure.Options;

public static class DbContextConfiguration
{
    public static void ReadOptions(DbContextOptionsBuilder options, string connectionString)
    {
        options
            .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: TimeSpan.FromSeconds(10),
                    errorNumbersToAdd: null);
                sqlOptions.CommandTimeout(30);
                sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            })
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            .EnableSensitiveDataLogging(false)
            .EnableDetailedErrors(false)
            .ConfigureWarnings(warnings =>
                warnings.Ignore(CoreEventId.MultipleNavigationProperties));
    }

    public static void WriteOptions(DbContextOptionsBuilder options, string connectionString)
    {
        options
            .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: TimeSpan.FromSeconds(10),
                    errorNumbersToAdd: null);
                sqlOptions.CommandTimeout(90);
                sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
            })
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
            .EnableSensitiveDataLogging(false)
            .EnableDetailedErrors(false);
    }
}
