using Amazon.DynamoDBv2.DataModel;

namespace Azure1.Dynamo;

public interface IDatabaseAccess
{

    Task<T?> CreateAsync<T>(T model);
    Task<T?> ReadAsync<T, V>(V key, string secondaryKey);
    Task<T?> UpdateAsync<T>(T model);
    Task<bool> DeleteAsync<T, V>(V key, string secondaryKey);
    Task<List<T>?> BatchGetAsync<T>(List<string> hashKeys);
    Task<List<T>?> BatchGetAsync<T>(string hashKey, List<string> secondaryKeys);
    Task<List<T>?> QueryAsync<T, V>(V hashKey, DynamoDBOperationConfig? config);
    Task<List<T>?> ScanAsync<T>(IEnumerable<ScanCondition> scanConditions);
}