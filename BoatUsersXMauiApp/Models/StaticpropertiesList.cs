

namespace BoatUsersXMauiApp;

public class StaticpropertiesList
{

    public Dictionary<int, StaticProperties> allProps = new Dictionary<int, StaticProperties>();
    public Dictionary<int, StaticProperties> AllProps { get { return allProps; } }
    public StaticpropertiesList() { }
    public void AddItems(int thisId, StaticProperties thisProp)
    {
        allProps.Add(thisId, thisProp);
    }
}
