using Azure1.Models;

namespace Azure1.Dynamo.DataAccess;

public class LicenseData : BaseData<LicenseModel>
{
    public LicenseData(IDatabaseAccess dataAccess) : base(dataAccess)
    {
    }

    public async Task<LicenseModel?> CreateAsync(LicenseModel model)
    {
        return await _dataAccess.CreateAsync(model);
    }

    public async Task<LicenseModel?> ReadAsync(string ownerId, string id)
    {
        return await _dataAccess.ReadAsync<LicenseModel, string>(ownerId, id);
    }

    public async Task<LicenseModel?> UpdateAsync(LicenseModel model)
    {
        return await _dataAccess.UpdateAsync(model);
    }

    public async Task<bool> DeleteAsync(string ownerId, string id)
    {
        return await _dataAccess.DeleteAsync<LicenseModel, string>(ownerId, id);
    }

    public async Task<List<LicenseModel>?> QueryAllUserLicensesAsync(string ownerId)
    {
        if (string.IsNullOrEmpty(ownerId))
            return null;

        return await _dataAccess.QueryAsync<LicenseModel, string>(ownerId, null);
    }
}
