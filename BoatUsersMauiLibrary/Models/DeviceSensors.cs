namespace BoatUsersMauiLibrary.Models;

public class DeviceSensors
{
    public int CountElement { get; set; }

    public string name = AppInfo.Current.Name;

    public string package = AppInfo.Current.PackageName;

    public string version = AppInfo.Current.VersionString;

    public string build = AppInfo.Current.BuildString;
    public string BatteryLevel { get; set; }
    private bool IsBatteryWatched { get; set; }
    public string Granted { get; set; }
    public string DevicePressureLevel { get; set; }

    public DeviceSensors() { }
    public void DisplayCounterValue(int thisInt)
    {
        CountElement += thisInt;
    }
    public async Task DriveToNewLocation()
    {
        var location = new Location(47.645160, -122.1306032);
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
    public async Task<object> GetCurrentLocation()
    {

        try
        {
            //PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

            //if (status == PermissionStatus.Granted)
            //{
            //    Granted = status.ToString();
            //}
            var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

            return await Geolocation.GetLocationAsync(request);
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

    public void WatchBattery()
    {

        if (!IsBatteryWatched)
        {
            Battery.Default.BatteryInfoChanged += Battery_BatteryInfoChanged;
        }
        else
        {
            Battery.Default.BatteryInfoChanged -= Battery_BatteryInfoChanged;
        }

        IsBatteryWatched = !IsBatteryWatched;
    }

    private void Battery_BatteryInfoChanged(object sender, BatteryInfoChangedEventArgs e)
    {
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

        BatteryLevel = $"Battery is {e.ChargeLevel * 100}% charged.";
    }

    public void ToggleBarometer()
    {
        if (Barometer.Default.IsSupported)
        {
            if (!Barometer.Default.IsMonitoring)
            {
                Barometer.Default.ReadingChanged += Barometer_ReadingChanged;
                Barometer.Default.Start(SensorSpeed.UI);
            }
            else
            {
                Barometer.Default.Stop();
                Barometer.Default.ReadingChanged -= Barometer_ReadingChanged;
            }
        }
    }

    private void Barometer_ReadingChanged(object sender, BarometerChangedEventArgs e)
    {
        string thisStr = e.Reading.ToString().Split(':')[1].Split(' ')[1];
        var thisValue = double.Parse(thisStr);
        DevicePressureLevel = thisValue switch
        {
            _ when thisValue > 900 && thisValue < 1050 => $"Presseure is normal {thisValue}",
            <= 900 => $"Presseure is low {thisValue}",
            >= 1050 => $"Presseure is high {thisValue}",
            _ => "Presseure is unknown"
        };
    }
}