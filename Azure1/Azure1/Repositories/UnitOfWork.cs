using Azure1.Data;

namespace Azure1.Repositories;


public class UnitOfWork
{
    readonly AzureDbContext _context;
    public UserRepository UserRepo { get; }

    public AzureDbContext Context { get => _context; }

    public UnitOfWork(AzureDbContext context, UserRepository userRepo)
    {
        _context = context;
        UserRepo = userRepo;
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}