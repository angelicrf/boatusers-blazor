
using Android.Bluetooth;
using Android.Bluetooth.LE;
using Android.Runtime;
using Android.Widget;
using Java.Util;

namespace BoatUsersXMauiApp;

public class CreateScanCallBack : ScanCallback
{
    public CreateScanCallBack() { }

    public override void OnScanResult([GeneratedEnum] ScanCallbackType callbackType, ScanResult result)
    {
        base.OnScanResult(callbackType, result);

        BluetoothDevice btDevice = result.Device;

        UUID uuidExtra2 = UUID.NameUUIDFromBytes(result.ScanRecord.GetBytes());

        if (!string.IsNullOrEmpty(uuidExtra2.ToString()))
        {
            Console.WriteLine(uuidExtra2);

            var _socket = btDevice.CreateRfcommSocketToServiceRecord(uuidExtra2);

            ConenctToDevice(_socket, btDevice);
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
            await Task.Delay(TimeSpan.FromSeconds(2));
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
            Toast.MakeText(PermissionCheck.mActivityRef, $"Connected to Device {_socket.ConnectionType}{_socket.RemoteDevice.Name}", ToastLength.Short).Show();
        }
        catch (Exception ex)
        {

            Console.WriteLine(ex.Message);
        }

    }
}
