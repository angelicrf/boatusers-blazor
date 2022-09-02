using Azure;
using Azure.Data.Tables;
using System.ComponentModel.DataAnnotations;

namespace BoatRazorLibrary.Models;

public class BULoginModel : ITableEntity
{
    public string RowKey { get; set; } = default!;

    public string PartitionKey { get; set; } = default!;
    [Required]
    public string? UserName { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    public bool IsLogedIn { get; set; }

    public ETag ETag { get; set; } = default!;

    public DateTimeOffset? Timestamp { get; set; } = default!;

}
