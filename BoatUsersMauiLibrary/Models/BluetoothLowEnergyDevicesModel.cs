
namespace BoatUsersMauiLibrary.Models;

public class BluetoothLowEnergyDevicesModel
{

    public static string BluetoothDName { get; set; }
    public static string BluetoothDId { get; set; }
    public static string DeviceEnumComplete { get; set; }
    public static string DeviceWatchStop { get; set; }
    public static string DeviceUpdateMsg { get; set; }
    public static string DeviceRemoveMsg { get; set; }
    public static string DeviceCanPair { get; set; }
    public static string DeviceIsPair { get; set; }
    public static string DeviceBLEAddress { get; set; }
    public static string DeviceIsConnected { get; set; }
    public static Guid DeviceGuid { get; set; }

    public static IReadOnlyDictionary<string, object> DevicesInfo = new Dictionary<string, object>();

    public static IReadOnlyDictionary<string, object> DeviceRemove = new Dictionary<string, object>();

    public static IReadOnlyDictionary<string, object> DeviceInfo = new Dictionary<string, object>();

    public static List<Guid> ServicesUUID = new List<Guid>();

    public static List<Guid> CharacteristicsUUID = new List<Guid>();
#if WINDOWS

    private BluetoothLowEnregyFuncs bluetoothLowenEnregyFuncs = new BluetoothLowEnregyFuncs();

#endif
    public async Task RunScanDevice()
    {
#if WINDOWS
      await bluetoothLowenEnregyFuncs.StartScan();
#endif

    }
    public void RunConnectDevice()
    {
#if WINDOWS
        bluetoothLowenEnregyFuncs.ConnectDevice();
#endif

    }
    public async Task ShowDeviceCharcteristics()
    {
#if WINDOWS
       await bluetoothLowenEnregyFuncs.DisplayCharcteristics();
#endif

    }
}
