namespace BoatRazorLibrary.Models;

public class BoatMenuModel
{
    public List<BoatAccesssoriesModel> BoatAccessory { get; set; }
    public string? BoatUrl { get; set; }
    public string? BoatIdentity { get; set; }
    public string? BoatImgSrc { get; set; }
    public string? BoatImgAlt { get; set; }
    public string? BoatTitle { get; set; }
    public string? BoatDescription { get; set; }
}
