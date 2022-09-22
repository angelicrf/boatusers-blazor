namespace BoatUsersXMauiApp;

public partial class BoatUsersData : ContentPage
{
    public bool IsClicked { get; set; } = false;
    public static StaticpropertiesList _properties { get; set; } = new StaticpropertiesList();

#if ANDROID
    private PermissionCheck pr = new PermissionCheck();

#endif
    public BoatUsersData()
    {

        Resources["Allprops"] = _properties.AllProps;
        InitializeComponent();
    }
    //private void OnTextChanged(object sender, TextChangedEventArgs e)
    //{

    //}
    private void CallGetData(object sender, EventArgs e)
    {
#if ANDROID
       GetData();
       pr.TestActivityChanges();
#endif
    }
    private async void ShowDevicedata(object sender, EventArgs e)
    {
        if (MainThread.IsMainThread)
        {
#if ANDROID

      await pr.ShowDevices();

      Resources["Allprops"] = null;
      Resources["Allprops"] = _properties.AllProps;
#endif

        }
        else
        {
#if ANDROID
      MainThread.InvokeOnMainThreadAsync(async() => {

      await pr.ShowDevices();  

      Resources["Allprops"] = null;
      Resources["Allprops"] = _properties.AllProps;
      
      });
#endif

        }
    }

    private void GetData()
    {
        IsClicked = !IsClicked;
        if (IsClicked)
        {
            if (MainThread.IsMainThread)
            {
#if ANDROID
      pr.MyMainThreadCode();
#endif

            }
            else
            {
#if ANDROID
      MainThread.InvokeOnMainThreadAsync(() => pr.MyMainThreadCode());
#endif

            }

        }

    }

    //private List<string> GetAdapters()
    //{
    //    var appContext = Android.App.Application.Context;

    //    var tf = (BluetoothManager)appContext.GetSystemService("bluetooth");

    //    var pairedDevices = tf.Adapter.BondedDevices;

    //    if (tf.Adapter.IsEnabled)
    //    {
    //        if (pairedDevices.Count > 0)
    //        {
    //            return tf.Adapter.BondedDevices.Select(d => d.Name).ToList();
    //        }
    //    }
    //    else
    //    {
    //        Console.Write("Bluetooth is not enabled on device");
    //    }

    //    return new List<string>();
    //}
    //public void Connect(string deviceName)
    //{
    //    var appContext = Android.App.Application.Context;

    //    var tf = (BluetoothManager)appContext.GetSystemService("bluetooth");
    //    var device = tf.Adapter.BondedDevices.FirstOrDefault(d => d.Name == deviceName);
    //    var _socket = device.CreateRfcommSocketToServiceRecord(UUID.FromString("00001101-0000-1000-8000-00805f9b34fb"));
    //    _socket.Connect();
    //    var buffer = new byte[] { 1 };
    //    // Write data to the device to trigger LED
    //    _socket.OutputStream.WriteAsync(buffer, 0, buffer.Length);

    //}
    private void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
    {
        StaticProperties item = args.SelectedItem as StaticProperties;
    }
    private void OnTextCompleted(object sender, EventArgs e)
    {
        //lastName.Text = ((Entry)sender).Text;
        //showValue.Text = "newtext";
    }
    private async void ConnectDevice(object sender, EventArgs e)
    {
        if (MainThread.IsMainThread)
        {

#if ANDROID
    await pr.ConnectDevicesInfo();
#endif

        }
        else
        {
#if ANDROID
      MainThread.BeginInvokeOnMainThread(new Action (async() => await pr.ConnectDevicesInfo()));
#endif

        }
    }

}