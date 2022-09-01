using Azure;
using Azure.Data.Tables;

namespace BoatRazorLibrary.Models;

public class BULoginModel : ITableEntity
{
    public string RowKey { get; set; } = default!;

    public string PartitionKey { get; set; } = default!;

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public bool IsLogedIn { get; set; }

    public ETag ETag { get; set; } = default!;

    public DateTimeOffset? Timestamp { get; set; } = default!;

}
