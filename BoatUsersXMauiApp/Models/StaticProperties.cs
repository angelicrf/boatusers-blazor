using System.ComponentModel;
using System.Windows.Input;

namespace BoatUsersXMauiApp;

public class StaticProperties : INotifyPropertyChanged
{
    public StaticProperties() { }

    public event PropertyChangedEventHandler PropertyChanged;
    public ICommand MultiplyBy2Command { get; set; }

    private int _DeviceId;
    public int DeviceId
    {
        get => _DeviceId;
        set
        {
            if (_DeviceId != value)
            {
                _DeviceId = value;
                OnPropertyChanged("DeviceId");

                //if (PropertyChanged != null)
                //{
                //    PropertyChanged(this, new PropertyChangedEventArgs("DeviceId"));
                //}
            }
        }
    }

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

                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ShowName"));
                }
            }
        }
    }
    private bool _IsVisible;
    public bool IsVisible
    {
        get => _IsVisible;
        set
        {
            if (_IsVisible != value)
            {
                _IsVisible = value;
                OnPropertyChanged("IsVisible");

                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("IsVisible"));
                }
            }
        }
    }
    protected void OnPropertyChanged(string propertyName)
    {
        var handler = PropertyChanged;
        if (handler != null)
            handler(this, new PropertyChangedEventArgs(propertyName));
    }

}
