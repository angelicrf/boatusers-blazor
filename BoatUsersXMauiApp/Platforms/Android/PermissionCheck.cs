using Android.Widget;
using Plugin.BLE;
using Plugin.CurrentActivity;
using System.Diagnostics;

namespace BoatUsersXMauiApp;

public class PermissionCheck
{
    MainActivity mn = new MainActivity();

    public List<Plugin.BLE.Abstractions.Contracts.IDevice> deviceList = new List<Plugin.BLE.Abstractions.Contracts.IDevice>();

    public PermissionCheck() { }
    public void TestActivityChanges()
    {
        mn.ActivityStateChanged += AndroidActivityStateChanged;
    }
    private void AndroidActivityStateChanged(object sender, ActivityEventArgs e)
    {
        Toast.MakeText(mn.AppContext, $"Activity Changed: {e.Activity.LocalClassName} -  {e.Event}", ToastLength.Short).Show();
    }
    public async void MyMainThreadCode()
    {
        var ble = CrossBluetoothLE.Current;
        var adapter = CrossBluetoothLE.Current.Adapter;

        var state = ble.State;
        //var scanFilterOptions = new ScanFilterOptions();
        ble.StateChanged += (s, e) =>
        {
            Debug.WriteLine($"The bluetooth state changed to {e.NewState}");
        };
        adapter.DeviceDiscovered += (s, a) => deviceList.Add(a.Device);
        var guid1 = new Guid("0000180a-0000-1000-8000-00805f9b34fb");
        adapter.ScanTimeout = 1000;

        //scanFilterOptions.DeviceAddresses = new[] { "80:6F:B0:43:8D:3B", "80:6F:B0:25:C3:15" }; // android only filter

        try
        {
            //scanFilterOptions.ServiceUuids = new[] { guid1 };
            //00002a00-0000-1000-8000-00805f9b34fb
            //00002a29-0000-1000-8000-00805f9b34fb
            //Allterco Robotics
            await adapter.StartScanningForDevicesAsync();
            // await adapter.StartScanningForDevicesAsync(scanFilterOptions);

            if (deviceList.Count > 0)
            {
                foreach (var item in deviceList)
                {
                    StaticProperties.ShowName = item.ToString();
                }
            }
            //await adapter.ConnectToDeviceAsync(deviceList[0]);

            //var service = await deviceList[0].GetServiceAsync(Guid.Parse("ffe0ecd2-3d16-4f8d-90de-e89e7fc396a5"));
            //var characteristic = await service.GetCharacteristicAsync(Guid.Parse("d8de624e-140f-4a22-8594-e2216b84a5f2"));
            //var characteristics = await service.GetCharacteristicsAsync();
            //var bytes = await characteristic.ReadAsync();
            //var services = await deviceList[0].GetServicesAsync();
            //await characteristic.WriteAsync(bytes);
            //characteristic.ValueUpdated += (o, args) =>
            //{
            //    var bytes = args.Characteristic.Value;
            //};

            //await characteristic.StartUpdatesAsync();
            //var descriptors = await characteristic.GetDescriptorsAsync();
            //var bytes2 = await descriptors[0].ReadAsync();
            //await descriptors[0].WriteAsync(bytes);

            //var systemDevices = adapter.GetSystemConnectedOrPairedDevices();

            //foreach (var device in systemDevices)
            //{
            //    await adapter.ConnectToDeviceAsync(device);
            //}
        }
        catch (Exception en)
        {
            Console.WriteLine(en.Message);
        }
    }
}
