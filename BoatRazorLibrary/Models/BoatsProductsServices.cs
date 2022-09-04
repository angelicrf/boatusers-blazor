
namespace BoatRazorLibrary.Models;

public class BoatsProductsServices : IBoatsProducts
{
    public static List<BoatMenuModel> ServiceBoatsList { get; set; } = new List<BoatMenuModel>
    {
         new BoatMenuModel{BoatUrl = "https://localhost:7016/bumenu/BoatOneUrl",BoatIdentity = "BoatOneUrl",BoatImgSrc = "./_content/BoatRazorLibrary/image1.png", BoatImgAlt = "imageOne", BoatDescription = "BoatOneDesc", BoatTitle = "BoatOneTitle" , BoatAccessory = new List<BoatAccesssoriesModel>
         { new BoatAccesssoriesModel {AccessoryId = "100", AccessoryName = "AccessOneBoatOne", AccessoryDesc = "AccessOneDescBoatOne", AccessoryImgSrc = "./_content/BoatRazorLibrary/BoatAccess2.png", AccessoryPrice = 23.52} } },
         new BoatMenuModel{BoatUrl = "https://localhost:7016/bumenu/BoatTwoUrl",BoatIdentity = "BoatTwoUrl",BoatImgSrc = "./_content/BoatRazorLibrary/image2.png", BoatImgAlt = "imageTwo", BoatDescription = "BoatTwoDesc", BoatTitle = "BoatTwoTitle", BoatAccessory = new List<BoatAccesssoriesModel>
         { new BoatAccesssoriesModel {AccessoryId = "200", AccessoryName = "AccessOneBoatTwo", AccessoryDesc = "AccessOneDescBoatTwo", AccessoryImgSrc = "./_content/BoatRazorLibrary/BoatAccess3.png" , AccessoryPrice = 32.41} }},
         new BoatMenuModel{BoatUrl = "https://localhost:7016/bumenu/BoatThreeUrl",BoatIdentity = "BoatThreeUrl",BoatImgSrc = "./_content/BoatRazorLibrary/image3.png", BoatImgAlt = "imageThree", BoatDescription = "BoatThreeDesc", BoatTitle = "BoatThreeTitle", BoatAccessory = new List < BoatAccesssoriesModel >
         { new BoatAccesssoriesModel {AccessoryId = "300", AccessoryName = "AccessOneBoatThree", AccessoryDesc = "AccessOneDescBoatThree", AccessoryImgSrc = "./_content/BoatRazorLibrary/BoatAccess4.png" , AccessoryPrice = 57.32} }},
         new BoatMenuModel{BoatUrl = "https://localhost:7016/bumenu/BoatFourUrl",BoatIdentity = "BoatFourUrl" ,BoatImgSrc = "./_content/BoatRazorLibrary/image4.png", BoatImgAlt = "imageFour", BoatDescription = "BoatFourDesc", BoatTitle = "BoatFourTitle", BoatAccessory = new List < BoatAccesssoriesModel >
         { new BoatAccesssoriesModel {AccessoryId = "400",AccessoryName = "AccessOneBoatFour", AccessoryDesc = "AccessOneDescBoatFour", AccessoryImgSrc = "./_content/BoatRazorLibrary/BoatAccess5.png", AccessoryPrice = 61.53},
           new BoatAccesssoriesModel {AccessoryId = "500",AccessoryName = "AccessOneBoatFour", AccessoryDesc = "AccessTwoDescBoatFour", AccessoryImgSrc = "./_content/BoatRazorLibrary/BoatAccess6.png", AccessoryPrice = 84.65}
         }
         }
    };
    public static List<BoatMenuModel> ServiceMauiBoatsList { get; set; } = new List<BoatMenuModel>
    {
         new BoatMenuModel{BoatUrl = "/bumenu/BoatOneUrl",BoatIdentity = "BoatOneUrl",BoatImgSrc = "./_content/BoatRazorLibrary/image1.png", BoatImgAlt = "imageOne", BoatDescription = "BoatOneDesc", BoatTitle = "BoatOneTitle" , BoatAccessory = new List<BoatAccesssoriesModel>
         { new BoatAccesssoriesModel {AccessoryId = "100", AccessoryName = "AccessOneBoatOne", AccessoryDesc = "AccessOneDescBoatOne", AccessoryImgSrc = "./_content/BoatRazorLibrary/BoatAccess2.png", AccessoryPrice = 23.52} } },
         new BoatMenuModel{BoatUrl = "/bumenu/BoatTwoUrl",BoatIdentity = "BoatTwoUrl",BoatImgSrc = "./_content/BoatRazorLibrary/image2.png", BoatImgAlt = "imageTwo", BoatDescription = "BoatTwoDesc", BoatTitle = "BoatTwoTitle", BoatAccessory = new List<BoatAccesssoriesModel>
         { new BoatAccesssoriesModel {AccessoryId = "200", AccessoryName = "AccessOneBoatTwo", AccessoryDesc = "AccessOneDescBoatTwo", AccessoryImgSrc = "./_content/BoatRazorLibrary/BoatAccess3.png" , AccessoryPrice = 32.41} }},
         new BoatMenuModel{BoatUrl = "/bumenu/BoatThreeUrl",BoatIdentity = "BoatThreeUrl",BoatImgSrc = "./_content/BoatRazorLibrary/image3.png", BoatImgAlt = "imageThree", BoatDescription = "BoatThreeDesc", BoatTitle = "BoatThreeTitle", BoatAccessory = new List < BoatAccesssoriesModel >
         { new BoatAccesssoriesModel {AccessoryId = "300", AccessoryName = "AccessOneBoatThree", AccessoryDesc = "AccessOneDescBoatThree", AccessoryImgSrc = "./_content/BoatRazorLibrary/BoatAccess4.png" , AccessoryPrice = 57.32} }},
         new BoatMenuModel{BoatUrl = "/bumenu/BoatFourUrl",BoatIdentity = "BoatFourUrl" ,BoatImgSrc = "./_content/BoatRazorLibrary/image4.png", BoatImgAlt = "imageFour", BoatDescription = "BoatFourDesc", BoatTitle = "BoatFourTitle", BoatAccessory = new List < BoatAccesssoriesModel >
         { new BoatAccesssoriesModel {AccessoryId = "400",AccessoryName = "AccessOneBoatFour", AccessoryDesc = "AccessOneDescBoatFour", AccessoryImgSrc = "./_content/BoatRazorLibrary/BoatAccess5.png", AccessoryPrice = 61.53},
           new BoatAccesssoriesModel {AccessoryId = "500",AccessoryName = "AccessOneBoatFour", AccessoryDesc = "AccessTwoDescBoatFour", AccessoryImgSrc = "./_content/BoatRazorLibrary/BoatAccess6.png", AccessoryPrice = 84.65}
         }
         }
    };
    public static List<BoatAccesssoriesModel> ServiceCartList { get; set; } = new List<BoatAccesssoriesModel>();

    public async Task<List<BoatAccesssoriesModel>> GetBoatAccessoryCartListAsync()
    {
        Task.Delay(200);
        return ServiceCartList;
    }

    public async Task<List<BoatMenuModel>> GetBoatMauiMenuListAsync()
    {
        Task.Delay(200);
        return ServiceMauiBoatsList;
    }

    public async Task<List<BoatMenuModel>> GetBoatMenuListAsync()
    {
        Task.Delay(200);
        return ServiceBoatsList;
    }
}
