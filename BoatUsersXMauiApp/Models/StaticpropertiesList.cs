using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BoatUsersXMauiApp;

public class StaticpropertiesList : INotifyPropertyChanged, INotifyCollectionChanged
{
    private Dictionary<int, StaticProperties> allProps;

    public event PropertyChangedEventHandler PropertyChanged;

    public event NotifyCollectionChangedEventHandler CollectionChanged;

    public Dictionary<int, StaticProperties> AllProps
    {

        get { return allProps; }
        set
        {
            allProps = value;
        }
    }
    public StaticpropertiesList()
    {
        allProps = new Dictionary<int, StaticProperties>
        {
            { 120, new StaticProperties() {ShowName = "TestBTN", IsVisible = true, DeviceId = 120 } }
        };
        AllProps = allProps;
    }
    public void AddItems(int thisId, StaticProperties thisProp)
    {
        allProps.Add(thisId, thisProp);

        allProps.TrimExcess();

        AllProps = allProps;

        OnCollectionChanged(AllProps);

        OnPropertyChanged("AllProps");
    }
    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    protected void OnCollectionChanged(Dictionary<int, StaticProperties> thisNewList)
    {
        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, thisNewList));
    }
}
