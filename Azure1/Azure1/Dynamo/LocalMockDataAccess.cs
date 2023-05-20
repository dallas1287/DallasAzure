using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2;

namespace Azure1.Dynamo;

public class LocalMockDataAccess : DynamoDataAccess
{
    private static readonly string Ip = "localhost";
    private static readonly int Port = 8000;
    private static readonly string EndpointUrl = "http://" + Ip + ":" + Port;
    ProvisionedThroughput provisioned { get; } = new(10000, 10000);

    public LocalMockDataAccess(AmazonDynamoDBClient dbClient) : base(dbClient)
    {
        Task.WhenAll(CreateUserTable(), CreateAssetTable(), CreateTeamTable(), CreateRepoTable()).Wait();
    }

    public async Task CreateUserTable()
    {
        if (await CheckingTableExistenceAsync("Users")) return;

        var response = await CreateTableAsync("Users",
            new List<AttributeDefinition>
            {
                new AttributeDefinition("Username", ScalarAttributeType.S)
            },
            new List<KeySchemaElement>
            {
                new("Username", KeyType.HASH)
            },
            provisioned);

        if (response?.HttpStatusCode != System.Net.HttpStatusCode.OK)
        {
            Console.WriteLine("Failed to create table");
        }
    }

    public async Task<bool> RemoveUserTable()
    {
        return await DeleteTableAsync("Users") != null;
    }

    public async Task CreateAssetTable()
    {
        if (await CheckingTableExistenceAsync("Assets")) return;

        var response = await CreateTableAsync("Assets",
            new List<AttributeDefinition>
            {
                new AttributeDefinition("Id", ScalarAttributeType.S),
                new AttributeDefinition("OwnerId", ScalarAttributeType.S)
            },
            new List<KeySchemaElement>
            {
                new("OwnerId", KeyType.HASH),
                new("Id", KeyType.RANGE)
            },
            provisioned);

        if (response?.HttpStatusCode != System.Net.HttpStatusCode.OK)
        {
            Console.WriteLine("Failed to create table");
        }
    }

    public async Task<bool> RemoveAssetTable()
    {
        return await DeleteTableAsync("Assets") != null;
    }

    public async Task CreateTeamTable()
    {
        if (await CheckingTableExistenceAsync("Teams")) return;

        var response = await CreateTableAsync("Teams",
           new List<AttributeDefinition>
           {
                new AttributeDefinition("Id", ScalarAttributeType.S),
                new AttributeDefinition("OwnerId", ScalarAttributeType.S)
           },
           new List<KeySchemaElement>
           {
                new("OwnerId", KeyType.HASH),
                new("Id", KeyType.RANGE)
           },
           provisioned);

        if (response?.HttpStatusCode != System.Net.HttpStatusCode.OK)
        {
            Console.WriteLine("Failed to create table");
        }
    }

    public async Task<bool> RemoveTeamTable()
    {
        return await DeleteTableAsync("Teams") != null;
    }

    public async Task CreateRepoTable()
    {
        if (await CheckingTableExistenceAsync("Repositories")) return;

        var response = await CreateTableAsync("Repositories",
           new List<AttributeDefinition>
           {
                new AttributeDefinition("Id", ScalarAttributeType.S),
                new AttributeDefinition("OwnerId", ScalarAttributeType.S)
           },
           new List<KeySchemaElement>
           {
                new("OwnerId", KeyType.HASH),
                new("Id", KeyType.RANGE)
           },
           provisioned);

        if (response?.HttpStatusCode != System.Net.HttpStatusCode.OK)
        {
            Console.WriteLine("Failed to create table");
        }
    }

    public async Task<bool> RemoveRepoTable()
    {
        return await DeleteTableAsync("Repositories") != null;
    }

    public async Task<CreateTableResponse?> CreateTableAsync(string tableName,
       List<AttributeDefinition> tableAttributes,
       List<KeySchemaElement> tableKeySchema,
       ProvisionedThroughput provisionedThroughput)
    {

        // Build the 'CreateTableRequest' structure for the new table
        var request = new CreateTableRequest
        {
            TableName = tableName,
            AttributeDefinitions = tableAttributes,
            KeySchema = tableKeySchema,
            // Provisioned-throughput settings are always required,
            // although the local test version of DynamoDB ignores them.
            ProvisionedThroughput = provisionedThroughput
        };

        try
        {
            if (Dynamo == null) return null;
            return await Dynamo.CreateTableAsync(request);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<DeleteTableResponse?> DeleteTableAsync(string tableName)
    {
        var request = new DeleteTableRequest
        {
            TableName = tableName
        };

        try
        {
            if (Dynamo == null) return null;
            return await Dynamo.DeleteTableAsync(request);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<bool> CheckingTableExistenceAsync(string tblNm)
    {
        if (Dynamo == null) return false;
        var response = await Dynamo.ListTablesAsync();
        return response.TableNames.Contains(tblNm);
    }

    public async Task<TableDescription?> GetTableDescriptionAsync(string tableName)
    {
        TableDescription? result = null;

        // If the table exists, get its description.
        try
        {
            if (Dynamo == null) return result;
            var response = await Dynamo.DescribeTableAsync(tableName);
            result = response.Table;
        }
        catch (Exception)
        { }

        return result;
    }
}
