namespace BoatRazorLibrary.Models;

public interface IPlatformCheck
{
    public bool IsDesktopPlatform { get; set; }
    public bool IsMobilePlatform { get; set; }
    public bool IsPCheckStart { get; set; }
}
