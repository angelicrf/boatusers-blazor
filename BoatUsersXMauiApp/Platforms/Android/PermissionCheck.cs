using Android.Bluetooth;
using Android.Bluetooth.LE;
using Android.Content;
using Android.OS;
using Android.Widget;
using Java.Lang;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;
using Plugin.CurrentActivity;
using System.Collections.ObjectModel;
using Intent = Android.Content.Intent;

namespace BoatUsersXMauiApp;

[BroadcastReceiver(Enabled = true, Exported = true)]
public class PermissionCheck : BroadcastReceiver
{
    MainActivity mn = new MainActivity();

    public ObservableCollection<BluetoothDevice> deviceList = new ObservableCollection<BluetoothDevice>();
    public PermissionCheck() { }

    private IBluetoothLE ble = CrossBluetoothLE.Current;

    private BluetoothState state;

    private Plugin.BLE.Abstractions.Contracts.IAdapter adapter = CrossBluetoothLE.Current.Adapter;

    public static Context mActivityRef;

    public static BluetoothManager bluetoothManager { get; set; }

    private ScanCallback mScanCallback = new CreateScanCallBack();

    private ObservableCollection<IDevice> connectedDevices;

    private BluetoothAdapter thisAdapter = bluetoothManager.Adapter;
    //BluetoothAdapter.DefaultAdapter;
    public static BluetoothLeScanner bluetoothLeScanner { get; set; }

    private byte[] buffer;

    private int CountDevice = 0;
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

    public override async void OnReceive(Context context, Intent intent)
    {

        string action = intent.Action;

        if (BluetoothDevice.ActionFound.Equals(action))
        {
            BluetoothDevice device = (BluetoothDevice)intent.GetParcelableExtra(BluetoothDevice.ExtraDevice);

            string deviceName = device.Name;

            string deviceMCAddress = device.Address;

            int rssi = intent.GetShortExtra(BluetoothDevice.ExtraBondState, Short.MinValue);


            Toast.MakeText(mActivityRef, " ActionFound RSSI" + rssi.ToString() + " Bond STATE " + device.BondState + " GETUUID " + device.GetUuids(), ToastLength.Short).Show();

            deviceList.Add(device);

            if (deviceList.Count > 0)
            {
                CountDevice++;

                for (int i = CountDevice; i < deviceList.Count + 1; i++)
                {
                    // if (!deviceList.Contains(deviceList[i - 1]))
                    // {
                    BoatUsersData._properties.AddItems(i, new StaticProperties { ShowName = deviceList[i - 1].Name.ToString(), IsVisible = true, DeviceId = i });
                    // }
                }
                // await ConnectDevicesInfo();
                thisAdapter.NotifyAll();
            }
        }
        else if (BluetoothAdapter.ActionDiscoveryFinished.Equals(action))
        {
            if (deviceList.Any())
            {

                //Toast.MakeText(mActivityRef, $"Action Discovery Finished {deviceList[0].Address}", ToastLength.Short).Show();

                BluetoothDevice foundDevice = thisAdapter.GetRemoteDevice(deviceList[0].Address);

                bool result = foundDevice.FetchUuidsWithSdp();

                if (result)
                {
                    //ParcelUuid[] uuidExtra2 = UUID.NameUUIDFromBytes(result2.getScanRecord().getBytes()).toString();
                    Toast.MakeText(mActivityRef, $"After Action Discovery Finished {result}", ToastLength.Short).Show();
                }
                thisAdapter.NotifyAll();
            }
        }
        else if (BluetoothDevice.ActionUuid.Equals(action))
        {

            BluetoothDevice deviceExtra = (BluetoothDevice)intent.GetParcelableExtra(BluetoothDevice.ExtraDevice);

            BluetoothDevice device = thisAdapter.GetRemoteDevice(deviceList[0].Address);

            ParcelUuid[] uuidExtra2 = device.GetUuids();
            //(Parcelable[])intent.GetParcelableArrayExtra(BluetoothDevice.ExtraUuid);

            Toast.MakeText(mActivityRef, $"Action UUID {uuidExtra2}", ToastLength.Short).Show();

            if (uuidExtra2 != null)
            {
                foreach (var item in uuidExtra2)
                {
                    Console.WriteLine(item);
                }
            }
            else
            {
                Console.WriteLine("uuidExtra is still null");
            }
            if (deviceList.Any())
            {
                bool result2 = deviceList[0].FetchUuidsWithSdp();
            }
        }
    }
    public async Task ShowDevices()
    {
        try
        {
            using (BluetoothAdapter thisAdapter2 = bluetoothManager.Adapter)
            {
                var pairedDevices = thisAdapter2?.BondedDevices;

                if (thisAdapter2 == null)
                {
                    Toast.MakeText(mActivityRef, "No Bluetooth adapter found.", ToastLength.Short).Show();
                    throw new System.Exception("No Bluetooth adapter found.");
                }

                if (!thisAdapter2.IsEnabled)
                {
                    Intent enableAdapter = new Intent(BluetoothAdapter.ActionRequestEnable);

                    Toast.MakeText(mActivityRef, "Bluetooth adapter is not enabled.", ToastLength.Short).Show();
                    throw new System.Exception("Bluetooth adapter is not enabled.");
                }

                if (pairedDevices.Count > 0)
                {
                    foreach (var item in thisAdapter2?.BondedDevices)
                    {
                        Toast.MakeText(mActivityRef, "Have Paired Devices" + item.ToString(), ToastLength.Short).Show();
                    }
                }
                else
                {
                    //var foundD = thisAdapter.StartDiscovery();
                    bluetoothLeScanner = thisAdapter.BluetoothLeScanner;
                    bluetoothLeScanner.StartScan(mScanCallback);

                }

                //adapter.DeviceDiscovered += (s, a) => deviceList.Add(a.Device);

                //if (!ble.Adapter.IsScanning)
                //{
                //    await adapter.StartScanningForDevicesAsync();
                //    await adapter.StartScanningForDevicesAsync(scanFilterOptions);

                //    if (deviceList.Count > 0)
                //    {

                //        BoatUsersData.AllTestViewModel.MultiplyBy2Command = new Command(() => BoatUsersData.AllTestViewModel.DeviceId = BoatUsersData.AllTestViewModel.DeviceId);

                //        for (int i = 0; i < deviceList.Count; i++)
                //        {

                //            BoatUsersData._properties.AddItems(i, new StaticProperties { ShowName = deviceList[i].ToString(), IsVisible = true, DeviceId = i });
                //        }
                //    }
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
        catch (System.Exception en)
        {
            Console.WriteLine(en.Message);
        }

    }

    public async Task ConnectDevicesInfo()
    {
        try
        {
            //BluetoothDevice device = (from bd in thisAdapter.BondedDevices
            //                          where bd.Name == "gDevice-beacon"
            //                          select bd).FirstOrDefault();
            //if (device != null)
            //{
            ParcelUuid[] uuids = null;

            //Toast.MakeText(mActivityRef, $"Connected to Device {deviceList.Count}", ToastLength.Long).Show();

            if (deviceList[0].FetchUuidsWithSdp())
            {
                uuids = deviceList[0].GetUuids();
            }
            if ((uuids != null) && (uuids.Length > 0))
            {
                foreach (var uuid in uuids)
                {
                    try
                    {
                        var _socket = deviceList[0].CreateRfcommSocketToServiceRecord(uuid.Uuid);
                        //UUID.FromString("00001101-0000-1000-8000-00805f9b34fb"));

                        await _socket.ConnectAsync();
                        Toast.MakeText(mActivityRef, $"Connected to Device {deviceList[0].Name}", ToastLength.Short).Show();
                        //await _socket.InputStream.ReadAsync(buffer, 0, buffer.Length);

                        //await _socket.OutputStream.WriteAsync(buffer, 0, buffer.Length);
                        break;
                    }
                    catch (System.Exception ex)
                    {
                        Console.WriteLine("ex: " + ex.Message);
                    }
                }
            }
            // }
            //await adapter.StopScanningForDevicesAsync();
            //CrossBluetoothLE.Current.Adapter.ScanTimeoutElapsed += Adapter_ScanTimeoutElapsed;
            //adapter.DeviceConnected += Adapter_DeviceConnected;
            //ConnectParameters para = new ConnectParameters(true, true);
            //await adapter.ConnectToDeviceAsync(deviceList[0], para, new CancellationTokenSource().Token);
            //Toast.MakeText(mActivityRef, deviceList[0].State.ToString(), ToastLength.Short).Show();

            //var services = await deviceList[0].GetServicesAsync();
            //AC:10:00:00:01
            //UUID=ad27158f-fdba-4fd1-bc54-af16d5398220
            //Console.WriteLine(services);
            //var characteristics = await services[0].GetCharacteristicsAsync();
        }

        catch (System.Exception ex)
        {
            Console.Write(ex.Message);
        }
    }

    private void Adapter_ScanTimeoutElapsed(object sender, EventArgs e)
    {
        Toast.MakeText(mActivityRef, "TimeElapsed", ToastLength.Short).Show();
    }

    private void Adapter_DeviceConnected(object sender, DeviceEventArgs e)
    {
        Toast.MakeText(mActivityRef, e.Device.Name.ToString(), ToastLength.Short).Show();
        connectedDevices.Add(e.Device);

        //if (e.Device.Name == "PC-100")
        //{
        //    //spotServHandler.discoverServices(e.Device);
        //}
    }
}
