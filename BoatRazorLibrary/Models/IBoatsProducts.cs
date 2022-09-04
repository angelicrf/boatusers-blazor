
namespace BoatRazorLibrary.Models;

public interface IBoatsProducts
{
    Task<List<BoatMenuModel>> GetBoatMauiMenuListAsync();

    Task<List<BoatMenuModel>> GetBoatMenuListAsync();

    Task<List<BoatAccesssoriesModel>> GetBoatAccessoryCartListAsync();

}
