using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;

namespace Azure1.Dynamo;


public class DynamoDataAccess : IDatabaseAccess
{
    public virtual AmazonDynamoDBClient Dynamo { get; }
    protected DynamoDBContext context;

    public DynamoDataAccess(AmazonDynamoDBClient dbClient)
    {
        Dynamo = dbClient;
        context = new DynamoDBContext(Dynamo);
    }

    public async Task<T?> CreateAsync<T>(T model)
    {
        try
        {
            await context.SaveAsync(model);
            return model;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"CreateAsync failed. {ex.Message}");
            return default;
        }
    }

    public async Task<T?> ReadAsync<T, V>(V hashKey, string secondaryKey)
    {
        try
        {
            return await context.LoadAsync<T>(hashKey, secondaryKey);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"ReadAsync failed: {ex.Message}");
            return default;
        }
    }

    //only works for tables with a simple primary key made of only a hash key
    //if table includes a sort key, must use the other function
    public async Task<List<T>?> BatchGetAsync<T>(List<string> hashKeys)
    {
        var batch = context.CreateBatchGet<T>();
        foreach (var hash in hashKeys)
            batch.AddKey(hash);

        try
        {
            await context.ExecuteBatchGetAsync(batch);
            var found = batch.Results;
            return found;
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"BatchGetAsync hashkeys failed. {e.Message}");
            return null;
        }
    }

    public async Task<List<T>?> BatchGetAsync<T>(string hashKey, List<string> secondaryKeys)
    {
        var batch = context.CreateBatchGet<T>();
        foreach (var sk in secondaryKeys)
            batch.AddKey(hashKey, sk);

        try
        {
            await context.ExecuteBatchGetAsync(batch);
            var found = batch.Results;
            return found;
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"BatchGetAsync hash and secondaryKeys failed. {e.Message}");
            return null;
        }

    }

    public async Task<T?> UpdateAsync<T>(T model)
    {
        try
        {
            await context.SaveAsync(model);
            return model;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"UpdateAsync failed. {ex.Message}");
            return default;
        }
    }

    public async Task<bool> DeleteAsync<T, V>(V hashKey, string secondaryKey)
    {
        try
        {
            await context.DeleteAsync<T>(hashKey, secondaryKey);
            return true;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"DeleteAsync failed. {ex.Message}");
            return false;
        }
    }

    public async Task<List<T>?> QueryAsync<T, V>(V hashKey, DynamoDBOperationConfig? config)
    {
        try
        {
            //DynamoDBOperationConfig dbop = new()
            //{
            //    ConditionalOperator = Amazon.DynamoDBv2.DocumentModel.ConditionalOperatorValues.Or,
            //    QueryFilter = new List<ScanCondition>() 
            //    {
            //        new ScanCondition("Name", Amazon.DynamoDBv2.DocumentModel.ScanOperator.Equal, "Repo3"),
            //        new ScanCondition("Name", Amazon.DynamoDBv2.DocumentModel.ScanOperator.Equal, "Repo2")
            //    }
            //};
            //QueryOperationConfig qOp = new()
            //{
            //    KeyExpression = new()
            //    {

            //    },
            //    FilterExpression = new()
            //    {

            //    }
            //};
            var queryResult = context.QueryAsync<T>(hashKey, config ?? new DynamoDBOperationConfig());

            var items = await queryResult.GetRemainingAsync();
            return items;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"QueryAsync failed. {ex.Message}");
            return null;
        }

    }

    public async Task<List<T>?> ScanAsync<T>(IEnumerable<ScanCondition> scanConditions)
    {
        try
        {
            var scanResult = context.ScanAsync<T>(scanConditions);
            var items = await scanResult.GetRemainingAsync();
            return items;
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"ScanAsync failed. {e.Message}");
            return null;
        }

    }
}
