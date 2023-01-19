using IndentifyApp.DAL.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace IndentifyApp.DAL;

public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
        try
        {
            var databaseCreator = (Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator);
            databaseCreator.CreateTables();
        }
        catch (SqlException)
        {
        }
    }
}