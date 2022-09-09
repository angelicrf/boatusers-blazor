namespace BoatUsersMauiLibrary.Models;

public class DeviceSensors
{
    public int CountElement { get; set; }
    public Dictionary<string, double> MyCurrentLocation { get; set; }

    public string name = AppInfo.Current.Name;

    public string package = AppInfo.Current.PackageName;

    public string version = AppInfo.Current.VersionString;

    public string build = AppInfo.Current.BuildString;

    public Dictionary<string, double> result = new Dictionary<string, double>();

    public string Granted { get; set; }

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
    public Dictionary<string, double> ResetLocationSates(double thisLat, double thisLng)
    {
        if (result.Count > 0)
        {
            foreach (var item in result)
            {
                if (!result.ContainsKey(item.Key))
                {
                    result.Add("lat", thisLat);
                    result.Add("lon", thisLng);
                    return result;
                }
            }
        }
        else
        {
            result.Add("lat", thisLat);
            result.Add("lon", thisLng);
            return result;
        }
        return result;
    }
    public async void GetCurrentLocation()
    {

        try
        {
            //isDeviceLocation = !isDeviceLocation;
            PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

            if (status == PermissionStatus.Granted)
            {
                Granted = status.ToString();
            }
            var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

            var location = await Geolocation.GetLocationAsync(request);

            if (location.Latitude > 0)
            {
                ResetLocationSates(location.Latitude, location.Longitude);
            }
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


}