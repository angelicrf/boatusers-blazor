namespace BoatRazorLibrary.Models;

public class DeviceInfoWCode
{
    public bool IsMobile { get; set; }

    public bool IsDesktop { get; set; }

    public object LocationObj { get; set; }

    public string LocationLat { get; set; }

    public string LocationLon { get; set; }

    public string DeviceLoc { get; set; }

    public string DeviceBatteryLevel { get; set; }

    public string AppBrowserName { get; set; }

    public object DeviceNetworkInfoObj { get; set; }

    public string DNetworkEType { get; set; }

    public string DNetworkDLink { get; set; }

    public string DAppName { get; set; }

    public string DAppVersion { get; set; }

    public bool IsDNameVersion { get; set; }
    public DeviceInfoWCode() { }
    public async Task<Tuple<string, string>> FindDName()
    {
        var thisName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        var thisVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

        if (!string.IsNullOrEmpty(thisName) && !string.IsNullOrEmpty(thisVersion?.ToString()))
        {
            return await Task.FromResult(Tuple.Create(thisName, thisVersion.ToString()));
        }
        //string phrase = Path.GetFullPath("DeviceInfo.razor").ToString();
        //string[] words = phrase.Split('\\');

        //foreach (var word in words)
        //{
        //    if (word == "BoatBlazorServer")
        //    {
        //        return await Task.FromResult(word);
        //    }
        //}
        return await Task.FromResult(Tuple.Create("empty", "empty"));
    }
    public async Task<IEnumerable<object>> GetTupleValues(Tuple<string, string> tuple)
    {
        IEnumerable<object> values = new List<object>();
        if (tuple.Item1 != null && tuple.Item2 != null)
        {
            values = tuple.GetType().GetProperties().Select(p => p.GetValue(tuple));

            return await Task.FromResult(values);
        }
        return await Task.FromResult(values);
    }
    public async Task<bool> GetDeviceVersion(IEnumerable<object> thisList)
    {
        if (thisList.Any())
        {
            for (int i = 0; i < thisList.ToList().Count; i++)
            {
                if (i == 0)
                {
                    DAppName = thisList.ToList()[i].ToString();
                }
                else if (i == 1)
                {
                    DAppVersion = thisList.ToList()[i].ToString();
                }
            }
        }
        if (!string.IsNullOrEmpty(DAppVersion) && !string.IsNullOrEmpty(DAppName))
        {
            IsDNameVersion = true;
            return await Task.FromResult(IsDNameVersion);
        }
        IsDNameVersion = false;
        return await Task.FromResult(IsDNameVersion);
    }
}
