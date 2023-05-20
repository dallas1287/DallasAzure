using Amazon.DynamoDBv2;
using Amazon.Runtime;
using Azure1.Data;
using Azure1.Dynamo;
using Azure1.Dynamo.DataAccess;
using Azure1.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

string? connString = builder.Environment.IsStaging() ? "Server=localhost,5433;User ID=sa;Password=Pwd12345!;Database=AzureTest;TrustServerCertificate=True;" : null;
var localConnString = "Data Source=localhost;Database=AzureTest;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true";

Console.WriteLine(connString);

builder.Services.AddDbContext<AzureDbContext>(options =>
    options.UseSqlServer(
        connString ?? localConnString,
        sqlServerOptions =>
        {
            sqlServerOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(5), null);
            sqlServerOptions.CommandTimeout(120);
        }
        ));

builder.Services.AddScoped<UnitOfWork>();
builder.Services.AddScoped<UserRepository>();

if (builder.Environment.IsStaging())
{
    builder.Services.AddSingleton(new AmazonDynamoDBClient(
                    new BasicAWSCredentials("AAAAAAAAAAAAAAAAAAAA", "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"),
                    new AmazonDynamoDBConfig { ServiceURL = "http://localhost:8000" }));
    builder.Services.AddSingleton<IDatabaseAccess, LocalMockDataAccess>();
}
else
{
    builder.Services.AddSingleton<AmazonDynamoDBClient>();
    builder.Services.AddSingleton<IDatabaseAccess, DynamoDataAccess>();
}

builder.Services.AddSingleton<LicenseData>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
scope.ServiceProvider.GetService<AzureDbContext>()?.Database.Migrate();

app.Run();
