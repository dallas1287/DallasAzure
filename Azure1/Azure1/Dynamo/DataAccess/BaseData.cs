namespace Azure1.Dynamo.DataAccess;

public class BaseData<T>
{
    protected readonly IDatabaseAccess _dataAccess;

    public BaseData(IDatabaseAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
}
