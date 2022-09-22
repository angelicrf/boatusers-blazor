
using Android.Bluetooth;
using Android.Bluetooth.LE;
using Android.Runtime;
using Android.Widget;
using Java.Lang;
using Java.Lang.Reflect;
using Java.Util;

namespace BoatUsersXMauiApp;

public class CreateScanCallBack : ScanCallback
{

    // public static BluetoothAdapter thisAdapter { get; set; }

    public CreateScanCallBack() { }
    private BluetoothDevice btDevice { get; set; }
    public override void OnScanResult([GeneratedEnum] ScanCallbackType callbackType, ScanResult result)
    {
        base.OnScanResult(callbackType, result);

        btDevice = result.Device;

        UUID uuidExtra2 = UUID.NameUUIDFromBytes(result.ScanRecord.GetBytes());

        if (!string.IsNullOrEmpty(uuidExtra2.ToString()))
        {
            Console.WriteLine(uuidExtra2);

            var _socket = btDevice.CreateInsecureRfcommSocketToServiceRecord(uuidExtra2);
            try
            {
                //stop scan
                //PermissionCheck.bluetoothLeScanner.StartScan(this);
                //bluetoothLeScanner.FlushPendingScanResults(this);

                ConenctToDevice(_socket, btDevice);
            }
            catch (System.Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

        }
    }
    public override void OnBatchScanResults(IList<ScanResult> results)
    {
        base.OnBatchScanResults(results);

        foreach (var item in results)
        {
            Console.Write($"ScanResult - Results {item}", ToastLength.Short);
        }

    }
    public override void OnScanFailed(ScanFailure errorCode)
    {
        base.OnScanFailed(errorCode);

        Console.Write("Scan Failed", "Error Code: " + errorCode);
    }
    private async void ConenctToDevice(BluetoothSocket _socket, BluetoothDevice thisDevice)
    {
        try
        {
            // await Task.Delay(TimeSpan.FromSeconds(2));
            _socket.Connect();
            //var rfcommServices = await thisDevice.get();
            //if (rfcommServices.Services.Count > 0)
            //{
            //    service = rfcommServices.Services[0];
            //}
            //var connectionCts = new CancellationTokenSource();
            //await _socket.ConnectAsync(service.ConnectionHostName,
            //     service.ConnectionServiceName)
            //      .AsTask(connectionCts.Token);
        }
        catch (System.Exception ex)
        {

            Console.WriteLine(ex.Message);
            try
            {
                Method m = btDevice.Class.GetMethod("createRfcommSocket", new Class[] { Integer.Type });
                _socket = (BluetoothSocket)m.Invoke(btDevice, 1);
                _socket.Connect();
                Toast.MakeText(PermissionCheck.mActivityRef, $"Connected to Device {_socket.ConnectionType}{_socket.RemoteDevice.Name}", ToastLength.Short).Show();

            }
            catch (System.Exception en)
            {
                Console.WriteLine($"Couldn't establish Bluetooth connection! {en.Message}");
            }

        }
    }
}
