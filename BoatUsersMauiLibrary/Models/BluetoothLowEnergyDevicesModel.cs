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
    public static string DeviceName { get; set; }
    public static string DeviceBLEAddress { get; set; }
    public static string DeviceIsConnected { get; set; }
    public static Guid DeviceGuid { get; set; }
    public static List<string> DeviceList { get; set; } = new List<string>();

    public static IReadOnlyDictionary<string, object> DevicesInfo = new Dictionary<string, object>();

    public static IReadOnlyDictionary<string, object> DeviceRemove = new Dictionary<string, object>();

    public static IReadOnlyDictionary<string, object> DeviceInfo = new Dictionary<string, object>();

    public static List<Guid> ServicesUUID = new List<Guid>();

    public static List<Guid> CharacteristicsUUID = new List<Guid>();

    public static Dictionary<string, List<string>> PairableDevices = new Dictionary<string, List<string>>();
    public static bool IsCharcReadable { get; set; } = false;
    public static bool IsCharcWritable { get; set; } = false;
    public static bool IsCharcWritableWithoutResponse { get; set; } = false;
    public static bool IsCharcNotify { get; set; } = false;
    public static Guid ThisGuidCharc { get; set; }


#if WINDOWS

    private BluetoothLowEnregyFuncs bluetoothLowenEnregyFuncs = new BluetoothLowEnregyFuncs();

#endif
    public async Task RunScanDevice()
    {
#if WINDOWS
      await bluetoothLowenEnregyFuncs.StartScan();
#endif

    }
    public async Task RunConnectDevice(string thisDeviceId)
    {
#if WINDOWS
       await bluetoothLowenEnregyFuncs.ConnectDevice(thisDeviceId);
#endif

    }
    public async Task ShowDeviceCharcteristics(Guid thisGuid)
    {
#if WINDOWS
       await bluetoothLowenEnregyFuncs.DisplayCharcteristics(thisGuid);
#endif

    }
    public void CharcteristicAbilities(Guid thisCharcGuid)
    {
#if WINDOWS
        bluetoothLowenEnregyFuncs.CharacteristicAbility(thisCharcGuid);
#endif

    }
    public async Task ReadDeviceCharcteristic(Guid thisCharcGuid)
    {
#if WINDOWS
       await bluetoothLowenEnregyFuncs.ReadFromChracteristic(thisCharcGuid);
#endif

    }
    public async Task NotifyDeviceCharcteristic(Guid thisCharcGuid)
    {
#if WINDOWS
       await bluetoothLowenEnregyFuncs.NotifyCharacteristic(thisCharcGuid);
#endif

    }
    public async Task WriteDeviceCharcteristic(Guid thisCharcGuid)
    {
#if WINDOWS
       await bluetoothLowenEnregyFuncs.WriteInCharacteristic(thisCharcGuid);
#endif

    }
    public async Task WriteWithoutResponseDeviceCharcteristic(Guid thisCharcGuid)
    {
#if WINDOWS
       await bluetoothLowenEnregyFuncs.WriteInCharacteristic(thisCharcGuid);
#endif

    }

}
