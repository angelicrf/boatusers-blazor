
namespace BoatRazorLibrary.Models;

public interface IBULogin
{
    List<BULoginModel> ServiceLoginList { get; set; }

    Task<List<BULoginModel>> GetBULoginInfoAsync();
    Task<List<BULoginModel>> AddBULoginInfoAsync(BULoginModel modelData);
}
