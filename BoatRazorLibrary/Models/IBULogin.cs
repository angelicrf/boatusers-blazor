
namespace BoatRazorLibrary.Models;

public interface IBULogin
{
    Task<List<BULoginModel>> GetBULoginInfoAsync();
    Task<List<BULoginModel>> AddBULoginInfoAsync(BULoginModel modelData);
}
