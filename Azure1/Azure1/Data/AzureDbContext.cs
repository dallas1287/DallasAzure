using Azure1.Models;
using Microsoft.EntityFrameworkCore;

namespace Azure1.Data;

public class AzureDbContext : DbContext
{
    public AzureDbContext(DbContextOptions<AzureDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<UserModel> Users { get; set; } = null!;
}