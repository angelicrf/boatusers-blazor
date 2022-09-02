
namespace BoatRazorLibrary.Models;

public class BoatsProductsServicescs : IBoatsProducts
{
    public List<BoatMenuModel> ServiceBoatsList { get; set; } = new List<BoatMenuModel>
    {
         new BoatMenuModel{BoatUrl = "https://localhost:7016/bumenu/BoatOneUrl",BoatIdentity = "BoatOneUrl",BoatImgSrc = "./_content/BoatRazorLibrary/image1.png", BoatImgAlt = "imageOne", BoatDescription = "BoatOneDesc", BoatTitle = "BoatOneTitle"},
         new BoatMenuModel{BoatUrl = "https://localhost:7016/bumenu/BoatTwoUrl",BoatIdentity = "BoatTwoUrl",BoatImgSrc = "./_content/BoatRazorLibrary/image2.png", BoatImgAlt = "imageTwo", BoatDescription = "BoatTwoDesc", BoatTitle = "BoatTwoTitle"},
         new BoatMenuModel{BoatUrl = "https://localhost:7016/bumenu/BoatThreeUrl",BoatIdentity = "BoatThreeUrl",BoatImgSrc = "./_content/BoatRazorLibrary/image3.png", BoatImgAlt = "imageThree", BoatDescription = "BoatThreeDesc", BoatTitle = "BoatThreeTitle"},
         new BoatMenuModel{BoatUrl = "https://localhost:7016/bumenu/BoatFourUrl",BoatIdentity = "BoatFourUrl" ,BoatImgSrc = "./_content/BoatRazorLibrary/image4.png", BoatImgAlt = "imageFour", BoatDescription = "BoatFourDesc", BoatTitle = "BoatFourTitle"}
    };
}
