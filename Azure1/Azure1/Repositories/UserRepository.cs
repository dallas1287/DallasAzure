using Azure1.Data;
using Azure1.Models;

namespace Azure1.Repositories;

public class UserRepository : Repository<UserModel>
{
    public UserRepository(AzureDbContext inContext) : base(inContext)
    {
    }
}
