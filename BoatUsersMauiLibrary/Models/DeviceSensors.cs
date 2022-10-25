namespace BoatUsersMauiLibrary.Models;

public class DeviceSensors
{
    public int CountElement { get; set; }

    public string name = AppInfo.Current.Name;

    public string package = AppInfo.Current.PackageName;

    public string version = AppInfo.Current.VersionString;

    public string build = AppInfo.Current.BuildString;
    public string BatteryLevel { get; set; }
    public string ConnectionMethod { get; set; }
    private bool IsBatteryWatched { get; set; }
    public string Granted { get; set; }
    public string DevicePressureLevel { get; set; }
    public bool IsBatteryEventFired { get; set; } = false;
    public bool IsCrashReported { get; set; } = false;
    public bool IsPermited { get; set; } = false;

    private CancellationTokenSource _cancelTokenSource;
    //public List<BluetoothDevice> DeviceList = new List<BluetoothDevice>();
    public DeviceSensors() { }
    public void DisplayCounterValue(int thisInt)
    {
        CountElement += thisInt;
    }
    public async Task DriveToNewLocation()
    {
        var location = new Location(26.609859, -80.058571);
        var options = new MapLaunchOptions
        {
            Name = "School",
            NavigationMode = NavigationMode.Driving
        };

        try
        {
            await Map.Default.OpenAsync(location, options);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    public async Task VerifyPermission()
    {
        PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
        if (status != PermissionStatus.Granted)
        {
            status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            if (status != PermissionStatus.Granted)
            {
                IsPermited = true;
                await Task.CompletedTask;
            }
        }
    }
    public async Task<object> GetCurrentLocation()
    {

        try
        {
            Location lct = new Location(0, 0);
            var request = new GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromMinutes(1));
            _cancelTokenSource = new CancellationTokenSource();
            Location thisLocation = await Geolocation.GetLocationAsync(request, _cancelTokenSource.Token);
            bool thisBoolLocation = thisLocation.IsFromMockProvider;
            return thisLocation;
        }
        catch (FeatureNotSupportedException fnsEx)
        {
            throw new Exception(fnsEx.Message);
        }
        catch (FeatureNotEnabledException fneEx)
        {
            throw new Exception(fneEx.Message);
        }
        catch (PermissionException pEx)
        {
            throw new Exception(pEx.Message);
        }
        catch (Exception en)
        {

            throw new Exception(en.Message);
        }

    }

    public async Task WatchBattery()
    {

        if (!IsBatteryWatched)
        {
            Microsoft.Maui.Devices.Battery.Default.BatteryInfoChanged += Battery_BatteryInfoChanged;
            await Task.CompletedTask;
        }
        else
        {
            Microsoft.Maui.Devices.Battery.Default.BatteryInfoChanged -= Battery_BatteryInfoChanged;
            await Task.CompletedTask;
        }

        IsBatteryWatched = !IsBatteryWatched;
    }

    public void Battery_BatteryInfoChanged(object sender, BatteryInfoChangedEventArgs e)
    {
        IsBatteryEventFired = true;
        BatteryLevel = e.State switch
        {
            BatteryState.Charging => "Battery is currently charging",
            BatteryState.Discharging => "Charger is not connected and the battery is discharging",
            BatteryState.Full => "Battery is full",
            BatteryState.NotCharging => "The battery isn't charging.",
            BatteryState.NotPresent => "Battery is not available.",
            BatteryState.Unknown => "Battery is unknown",
            _ => "Battery is unknown"
        };

        BatteryLevel = $"Battery is {e.ChargeLevel * 100}% charged.{e.PowerSource}";
    }

    public async Task ToggleBarometer()
    {
        if (Barometer.Default.IsSupported)
        {
            if (!Barometer.Default.IsMonitoring)
            {
                Barometer.Default.ReadingChanged += Barometer_ReadingChanged;
                Barometer.Default.Start(SensorSpeed.UI);
                await Task.CompletedTask;
            }
            else
            {
                Barometer.Default.Stop();
                Barometer.Default.ReadingChanged -= Barometer_ReadingChanged;
                await Task.CompletedTask;
            }
        }
    }

    private void Barometer_ReadingChanged(object sender, BarometerChangedEventArgs e)
    {
        string thisStr = e.Reading.ToString().Split(':')[1].Split(' ')[1];
        var thisValue = double.Parse(thisStr);

        DevicePressureLevel = thisValue switch
        {
            _ when thisValue > 900 && thisValue < 1050 => ((Func<string>)(() =>
            {
                //if (!IsCrashReported) { AlertUser(); IsCrashReported = true; }
                return $"Pressure is normal {thisValue}";
            }))(),
            <= 900 => $"Pressure is low {thisValue}",
            >= 1050 => ((Func<string>)(() =>
            {
                if (!IsCrashReported) { AlertUser(); IsCrashReported = true; }
                return $"Pressure is high {thisValue}";
            }))(),
            _ => "Presseure is unknown"
        };
    }
    private async void AlertUser()
    {
        if (Sms.Default.IsComposeSupported)
        {
            string[] recipients = new[] { "206-321-1169" };
            string text = "Hello, Reporting a Crash.";

            var message = new SmsMessage(text, recipients);

            if (PhoneDialer.Default.IsSupported)
                PhoneDialer.Default.Open("206-321-1169");

            await Sms.Default.ComposeAsync(message);
        }
    }
    public async Task<string> GetGeocodeReverseData(double latitude, double longitude)
    {
        try
        {
            IEnumerable<Placemark> placemarks = await Geocoding.Default.GetPlacemarksAsync(latitude, longitude);

            Placemark placemark = placemarks?.FirstOrDefault();

            if (placemark != null)
            {
                return
                   $"{placemark.FeatureName + ' ' + placemark.Locality + ", " + placemark.SubAdminArea + ",\n " + placemark.AdminArea + ", this" + placemark.PostalCode + ", " + placemark.CountryCode}";
            }
        }
        catch (Exception e)
        {

            Console.WriteLine(e.Message);
        }


        return "";
    }
    public async Task DeviceConnetNetwork()
    {
        var current = Connectivity.NetworkAccess;
        var profiles = Connectivity.ConnectionProfiles;

        if (current == NetworkAccess.Internet)
        {

            if (profiles.Contains(ConnectionProfile.WiFi))
            {
                ConnectionMethod = "Active Wi-Fi connection";
                await Task.CompletedTask;
            }
            if (profiles.Contains(ConnectionProfile.Ethernet))
            {
                ConnectionMethod = "Active Ethernet connection";
                await Task.CompletedTask;
            }
            if (profiles.Contains(ConnectionProfile.Cellular))
            {
                ConnectionMethod = "Active Cellular connection";
                await Task.CompletedTask;
            }
            if (profiles.Contains(ConnectionProfile.Bluetooth))
            {
                ConnectionMethod = "Active Bluetooth connection";
                await Task.CompletedTask;
            }
        }
    }

}