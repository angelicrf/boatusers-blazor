﻿using Android.Content;
using Android.Widget;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.CurrentActivity;
using System.Collections.ObjectModel;

namespace BoatUsersXMauiApp;

public class PermissionCheck
{
    MainActivity mn = new MainActivity();

    public ObservableCollection<IDevice> deviceList = new ObservableCollection<IDevice>();

    public PermissionCheck() { }
    private IBluetoothLE ble = CrossBluetoothLE.Current;
    private BluetoothState state;
    private Plugin.BLE.Abstractions.Contracts.IAdapter adapter = CrossBluetoothLE.Current.Adapter;
    private static Context mActivityRef;
    private StaticProperties staticProperties = new StaticProperties();
    public void TestActivityChanges()
    {
        mn.ActivityStateChanged += AndroidActivityStateChanged;
    }
    private void AndroidActivityStateChanged(object sender, ActivityEventArgs e)
    {
        Toast.MakeText(mActivityRef, $"Activity Changed: {e.Activity.LocalClassName} -  {e.Event} from Event", ToastLength.Short).Show();
    }
    public static void updateActivity(Context context)
    {
        mActivityRef = context;
    }
    public void MyMainThreadCode()
    {
        state = ble.State;
        ble.StateChanged += (s, e) =>
        {
            Toast.MakeText(mActivityRef, $"The bluetooth state changed to {e.NewState}", ToastLength.Short).Show();
        };

    }
    public async Task ShowDevices()
    {
        try
        {
            adapter.DeviceDiscovered += (s, a) => deviceList.Add(a.Device);
            //scanFilterOptions.ServiceUuids = new[] { guid1 };
            //00002a00-0000-1000-8000-00805f9b34fb
            //00002a29-0000-1000-8000-00805f9b34fb
            //Allterco Robotics
            if (!ble.Adapter.IsScanning)
            {
                await adapter.StartScanningForDevicesAsync();
                // await adapter.StartScanningForDevicesAsync(scanFilterOptions);

                if (deviceList.Count > 0)
                {
                    foreach (var item in deviceList)
                    {
                        staticProperties.ShowName = "findItem";
                        //item.ToString();
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
        }
        catch (Exception en)
        {
            Console.WriteLine(en.Message);
        }

    }
}