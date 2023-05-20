using Amazon.DynamoDBv2.DataModel;

namespace Azure1.Models;

[DynamoDBTable("Licenses")]
public class LicenseModel
{
    [DynamoDBHashKey]
    public string OwnerId { get; set; } = string.Empty;

    public string Id { get; set; } = string.Empty;

    public int MaxRepos { get; set; } = 0;


    public int MaxTeams { get; set; } = 0;

    public long MaxStorageBytes { get; set; } = 0;

    public int MaxTeamMembers { get; set; } = 0;

}
