using Azure1.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UITest;

public class MockDbContext : AzureDbContext
{
    public static readonly string DefaultConnString = "Server=localhost,5433;User ID=sa;Password=Pwd12345!;Database=Vitruvian;TrustServerCertificate=True;";
    public string ConnString { get; set; } = DefaultConnString;
    public MockDbContext(DbContextOptions<AzureDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseSqlServer(
                   ConnString,
                   sqlServerOptions =>
                   {
                       sqlServerOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(5), null);
                       sqlServerOptions.CommandTimeout(120);
                   });
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}

public static class DbContextExtensions
{
    public static void ClearAllTables(this AzureDbContext context)
    {
        var tables = context.GetTableNames();
        foreach (var table in tables.Where(t => !string.IsNullOrEmpty(t)))
        {
            context.ClearTable(table!);
        }
    }

    public static void ClearTable(this AzureDbContext context, string tableName)
    {
        context.Database.ExecuteSqlRaw($"DELETE FROM {tableName}");
    }

    public static List<string?> GetTableNames(this AzureDbContext context)
    {
        return context.Model.GetEntityTypes().Select(t => t.GetTableName()).ToList();
    }
}