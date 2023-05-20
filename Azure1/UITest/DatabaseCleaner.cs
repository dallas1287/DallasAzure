using Azure1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Xunit.Sdk;

namespace UITest;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class DatabaseCleaner : BeforeAfterTestAttribute
{
    public override void Before(MethodInfo methodUnderTest)
    {
        IServiceCollection serviceCollection = new ServiceCollection();
        var connectionString = MockDbContext.DefaultConnString;
        serviceCollection.AddDbContext<AzureDbContext>(
                                            options => options.UseSqlServer(connectionString
                                            , sqlOptions => sqlOptions.MigrationsAssembly("Azure1")));
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var context = serviceProvider.GetService<AzureDbContext>();
        Assert.NotNull(context);

        var exception = Record.Exception(() => context.ClearAllTables());
        Assert.Null(exception);
        exception = Record.Exception(() => context.Database.Migrate());
        Assert.Null(exception);
    }
}
