
namespace BoatRazorLibrary.Shared;

public class SharedClass
{
    public int SharedInt { get; set; }

    public int DisplaySharedInt()
    {
        SharedInt = SharedInt + 1;
        return SharedInt;
    }

}
