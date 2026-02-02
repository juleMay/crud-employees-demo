using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using WebApi.Domain.Entities;

namespace WebApi.Infrastructure.Contexts;

public class ReadDbContext(DbContextOptions<ReadDbContext> options) : DbContext(options)
{
    public DbSet<Employee> Employees => Set<Employee>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReadDbContext).Assembly);
    }

    public MySqlConnection GetMySqlConnection()
    {
        DbConnection dbConnection = Database.GetDbConnection();

        if (dbConnection is MySqlConnection mySqlConnection)
        {
            return mySqlConnection;
        }
        else
        {
            throw new InvalidOperationException("The underlying database connection is not a MySqlConnection.");
        }
    }

}
