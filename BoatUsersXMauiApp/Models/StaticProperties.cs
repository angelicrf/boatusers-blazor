using System.ComponentModel;

namespace BoatUsersXMauiApp;

public class StaticProperties : INotifyPropertyChanged
{
    public StaticProperties() { GetShoName(); SetShoName(_ShowName); }
    public event PropertyChangedEventHandler PropertyChanged;
    private string _ShowName;
    public string ShowName
    {
        get => _ShowName;
        set
        {
            if (_ShowName != value)
            {
                _ShowName = value;
                OnPropertyChanged("ShowName");

                //if (PropertyChanged != null)
                //{
                //    PropertyChanged(this, new PropertyChangedEventArgs("ShowName"));
                //}
            }
        }
    }
    private string GetShoName()
    {
        return ShowName;
    }
    private void SetShoName(string ThisName)
    {
        ShowName = ThisName;
    }
    protected void OnPropertyChanged(string propertyName)
    {
        var handler = PropertyChanged;
        if (handler != null)
            handler(this, new PropertyChangedEventArgs(propertyName));
    }

}
