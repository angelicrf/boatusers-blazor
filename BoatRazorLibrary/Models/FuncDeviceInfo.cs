
namespace BoatRazorLibrary.Models;

public class FuncDeviceInfo
{
    public int CountElement { get; set; } = 10;

    public void CountCall()
    {
        CountElement += 20;
    }
}
