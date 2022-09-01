namespace BoatRazorLibrary.Models;

public class BULoginService : IBULogin
{

    public List<BULoginModel> ServiceLoginList { get; set; } = new List<BULoginModel>();

    public Task<List<BULoginModel>> GetBULoginInfoAsync()
    {
        return Task.FromResult(ServiceLoginList);
    }
    public Task<List<BULoginModel>> AddBULoginInfoAsync(BULoginModel thisNewData)
    {
        ServiceLoginList.Add(thisNewData);
        return Task.FromResult(ServiceLoginList);
    }
}
