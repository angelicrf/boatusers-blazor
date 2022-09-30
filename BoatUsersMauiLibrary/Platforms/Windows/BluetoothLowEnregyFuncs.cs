using BoatUsersMauiLibrary.Models;
using Windows.Devices.Bluetooth;
using Windows.Devices.Enumeration;

namespace BoatUsersMauiLibrary;

public class BluetoothLowEnregyFuncs
{
    string[] requestedProperties = { "System.Devices.Aep.DeviceAddress", "System.Devices.Aep.IsConnected" };
    public BluetoothLEDevice bluetoothLeDevice { get; set; }
    public BluetoothLowEnregyFuncs() { }
    public Task StartScan()
    {
        DeviceWatcher deviceWatcher =
              DeviceInformation.CreateWatcher(
                      BluetoothLEDevice.GetDeviceSelectorFromPairingState(false),
                      requestedProperties,
                      DeviceInformationKind.AssociationEndpoint);

        deviceWatcher.Added += DeviceWatcher_Added;
        deviceWatcher.Updated += DeviceWatcher_Updated;
        deviceWatcher.Removed += DeviceWatcher_Removed;

        deviceWatcher.EnumerationCompleted += DeviceWatcher_EnumerationCompleted;
        deviceWatcher.Stopped += DeviceWatcher_Stopped;


        deviceWatcher.Start();
        deviceWatcher.Status.ToString();
        return Task.CompletedTask;
    }

    private void DeviceWatcher_Stopped(DeviceWatcher sender, object args)
    {
        if (args != null)
        {
            BluetoothLowEnergyDevicesModel.DeviceWatchStop = args.ToString();
        }
        else
        {
            BluetoothLowEnergyDevicesModel.DeviceWatchStop = "Device Stopped Null";
        }

    }

    private void DeviceWatcher_EnumerationCompleted(DeviceWatcher sender, object args)
    {
        if (args != null)
        {
            BluetoothLowEnergyDevicesModel.DeviceEnumComplete = args.ToString();
        }
        else
        {
            BluetoothLowEnergyDevicesModel.DeviceEnumComplete = "Device Complete Null";
        }
    }

    private void DeviceWatcher_Removed(DeviceWatcher sender, DeviceInformationUpdate args)
    {
        if (args != null)
        {
            if (args.Properties.Count > 0)
            {
                BluetoothLowEnergyDevicesModel.DeviceRemove = args.Properties;
            }
        }
        else
        {
            BluetoothLowEnergyDevicesModel.DeviceRemoveMsg = "Device Remove Null";
        }
    }

    private void DeviceWatcher_Updated(DeviceWatcher sender, DeviceInformationUpdate args)
    {
        if (args != null)
        {
            if (args.Properties.Count > 0)
            {
                BluetoothLowEnergyDevicesModel.DevicesInfo = args.Properties;
            }
        }
        else
        {
            BluetoothLowEnergyDevicesModel.DeviceUpdateMsg = "Device Update Null";
        }
    }

    private void DeviceWatcher_Added(DeviceWatcher sender, DeviceInformation args)
    {
        if (args != null)
        {

            BluetoothLowEnergyDevicesModel.DeviceInfo = args.Properties;


            BluetoothLowEnergyDevicesModel.BluetoothDId = args.Id;
        }
        else
        {
            BluetoothLowEnergyDevicesModel.BluetoothDName = "Device Name Null";
            BluetoothLowEnergyDevicesModel.BluetoothDId = "Device ID Null";
        }
    }
    async public void ConnectDevice()
    {
        if (!string.IsNullOrEmpty(BluetoothLowEnergyDevicesModel.BluetoothDId))
        {
            bluetoothLeDevice = await BluetoothLEDevice.FromIdAsync(BluetoothLowEnergyDevicesModel.BluetoothDId);
            BluetoothLowEnergyDevicesModel.DeviceIsConnected = bluetoothLeDevice.ConnectionStatus.ToString();
        }

    }
}
