
namespace BoatRazorLibrary.Models;

public interface IBoatsProducts
{
    List<BoatMenuModel> ServiceBoatsList { get; set; }
    List<BoatAccesssoriesModel> ServiceCartList { get; set; }

}
