
namespace BoatRazorLibrary.Models;

public class BoatsProductsServices : IBoatsProducts
{
    public static List<BoatMenuModel> ServiceBoatsList { get; set; } = new List<BoatMenuModel>
    {
         new BoatMenuModel{BoatUrl = "https://localhost:7016/bumenu/BoatOneUrl",BoatIdentity = "BoatOneUrl",BoatImgSrc = "./_content/BoatRazorLibrary/image1.png", BoatImgAlt = "imageOne", BoatDescription = "BoatOneDesc", BoatTitle = "BoatOneTitle" , BoatAccessory = new List<BoatAccesssoriesModel>
         { new BoatAccesssoriesModel { AccessoryName = "AccessOneBoatOne", AccessoryDesc = "AccessOneDescBoatOne", AccessoryImgSrc = "./_content/BoatRazorLibrary/BoatAccess2.png" } } },
         new BoatMenuModel{BoatUrl = "https://localhost:7016/bumenu/BoatTwoUrl",BoatIdentity = "BoatTwoUrl",BoatImgSrc = "./_content/BoatRazorLibrary/image2.png", BoatImgAlt = "imageTwo", BoatDescription = "BoatTwoDesc", BoatTitle = "BoatTwoTitle", BoatAccessory = new List<BoatAccesssoriesModel>
         { new BoatAccesssoriesModel { AccessoryName = "AccessOneBoatTwo", AccessoryDesc = "AccessOneDescBoatTwo", AccessoryImgSrc = "./_content/BoatRazorLibrary/BoatAccess3.png" } }},
         new BoatMenuModel{BoatUrl = "https://localhost:7016/bumenu/BoatThreeUrl",BoatIdentity = "BoatThreeUrl",BoatImgSrc = "./_content/BoatRazorLibrary/image3.png", BoatImgAlt = "imageThree", BoatDescription = "BoatThreeDesc", BoatTitle = "BoatThreeTitle", BoatAccessory = new List < BoatAccesssoriesModel >
         { new BoatAccesssoriesModel { AccessoryName = "AccessOneBoatThree", AccessoryDesc = "AccessOneDescBoatThree", AccessoryImgSrc = "./_content/BoatRazorLibrary/BoatAccess4.png" } }},
         new BoatMenuModel{BoatUrl = "https://localhost:7016/bumenu/BoatFourUrl",BoatIdentity = "BoatFourUrl" ,BoatImgSrc = "./_content/BoatRazorLibrary/image4.png", BoatImgAlt = "imageFour", BoatDescription = "BoatFourDesc", BoatTitle = "BoatFourTitle", BoatAccessory = new List < BoatAccesssoriesModel >
         { new BoatAccesssoriesModel {AccessoryName = "AccessOneBoatFour", AccessoryDesc = "AccessOneDescBoatFour", AccessoryImgSrc = "./_content/BoatRazorLibrary/BoatAccess5.png"},
           new BoatAccesssoriesModel {AccessoryName = "AccessOneBoatFour", AccessoryDesc = "AccessOneDescBoatFour", AccessoryImgSrc = "./_content/BoatRazorLibrary/BoatAccess6.png"}
         }
         }
    };
    public static List<BoatAccesssoriesModel> ServiceCartList { get; set; } = new List<BoatAccesssoriesModel>();

    public async Task<List<BoatAccesssoriesModel>> GetBoatAccessoryCartListAsync()
    {
        Task.Delay(200);
        return ServiceCartList;
    }

    public async Task<List<BoatMenuModel>> GetBoatMenuListAsync()
    {
        Task.Delay(200);
        return ServiceBoatsList;
    }
}
