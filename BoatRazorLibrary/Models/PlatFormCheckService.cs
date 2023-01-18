namespace BoatRazorLibrary.Models;

public class PlatFormCheckService : IPlatformCheck
{
    public bool IsDesktopPlatform { get; set; }
    public bool IsMobilePlatform { get; set; }
    public bool IsPCheckStart { get; set; }
}
