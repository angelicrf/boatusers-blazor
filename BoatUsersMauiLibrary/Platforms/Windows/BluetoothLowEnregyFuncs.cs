using BoatUsersMauiLibrary.Models;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Enumeration;
using Windows.Storage.Streams;

namespace BoatUsersMauiLibrary;

public class BluetoothLowEnregyFuncs
{
    string[] requestedProperties = { "System.Devices.Aep.DeviceAddress", "System.Devices.Aep.IsConnected" };
    public BluetoothLEDevice bluetoothLeDevice { get; set; }

    public List<GattDeviceService> bleServices = new List<GattDeviceService>();

    private IReadOnlyList<GattCharacteristic> bleCharacteristics = new List<GattCharacteristic>();

    public List<Guid> bleCharcteristicsUUID = new List<Guid>();

    private GattCharacteristicProperties properties;

    private bool isConnectedDv { get; set; } = false;
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
    public async Task ConnectDevice()
    {
        if (!string.IsNullOrEmpty(BluetoothLowEnergyDevicesModel.BluetoothDId))
        {
            if (!isConnectedDv)
            {
                var pairedBleDevices = await DeviceInformation.FindAllAsync(BluetoothLEDevice.GetDeviceSelectorFromPairingState(true));
                bluetoothLeDevice = await BluetoothLEDevice.FromIdAsync(pairedBleDevices[0].Id);
                //var newSession = GattSession.FromDeviceIdAsync(bluetoothLeDevice.BluetoothDeviceId);
                //newSession.MaintainConnection = true;
                bluetoothLeDevice.ConnectionStatusChanged += DeviceConnectionStat;
                isConnectedDv = true;
            }
            try
            {
                await GetAllServices();

            }
            catch (Exception en)
            {

                Console.WriteLine(en.Message);
            }
        }

    }
    public async Task DisplayCharcteristics()
    {

        await GetAllCharacteristics(BluetoothLowEnergyDevicesModel.DeviceGuid);
        //display characteristics below each service
        //properties = bleCharacteristics[0].CharacteristicProperties;
        //await ReadFromChracteristic();
        //await WriteInCharacteristic();
        //await NotifyCharacteristic();

    }
    private void DeviceConnectionStat(BluetoothLEDevice sender, object args)
    {
        if (args != null)
        {
            BluetoothLowEnergyDevicesModel.DeviceIsConnected = args.ToString();
        }

    }

    private async Task GetAllServices()
    {
        try
        {
            GattDeviceServicesResult result = await bluetoothLeDevice.GetGattServicesAsync(BluetoothCacheMode.Uncached);
            if (bluetoothLeDevice != null)
            {
                if (result.Status == GattCommunicationStatus.Success)
                {
                    var services = result.Services;
                    foreach (var service in services)
                    {
                        bleServices.Add(service);
                        BluetoothLowEnergyDevicesModel.ServicesUUID.Add(service.Uuid);
                    }
                }
            }
        }
        catch (Exception en)
        {

            Console.WriteLine(en.Message);
        }

    }
    private async Task GetAllCharacteristics(Guid thisUUID)
    {
        try
        {
            foreach (var item in bleServices)
            {
                if (item.Uuid == thisUUID)
                {
                    var accessStatus = await item.RequestAccessAsync();
                    if (accessStatus == DeviceAccessStatus.Allowed)
                    {
                        GattCharacteristicsResult result2 = await item.GetCharacteristicsAsync(BluetoothCacheMode.Uncached);
                        if (result2.Status == GattCommunicationStatus.Success)
                        {
                            bleCharacteristics = result2.Characteristics;
                            bleCharcteristicsUUID = (from e in bleCharacteristics select e.Uuid).ToList();
                            BluetoothLowEnergyDevicesModel.CharacteristicsUUID = (from e in bleCharacteristics select e.Uuid).ToList();
                        }
                    }
                }
            }

        }
        catch (Exception)
        {

            bleCharacteristics = new List<GattCharacteristic>();
        }
    }
    public async Task ReadFromChracteristic(Guid thisCharcGuid)
    {
        try
        {
            if (bleCharacteristics.Any())
            {
                foreach (var charc in bleCharacteristics)
                {
                    if (charc.Uuid == thisCharcGuid)
                    {
                        if (charc.CharacteristicProperties.HasFlag(GattCharacteristicProperties.Read))
                        {

                            GattReadResult resultRead = await charc.ReadValueAsync();
                            if (resultRead.Status == GattCommunicationStatus.Success)
                            {
                                var reader = DataReader.FromBuffer(resultRead.Value);
                                byte[] input = new byte[reader.UnconsumedBufferLength];
                                reader.ReadBytes(input);
                            }
                        }
                    }
                }
            }
        }
        catch (Exception en)
        {

            Console.WriteLine(en.Message);
        }

    }

    public void CharacteristicAbility(Guid thisCharcGuid)
    {
        try
        {
            if (bleCharacteristics.Any())
            {
                foreach (var charc in bleCharacteristics)
                {
                    if (charc.Uuid == thisCharcGuid)
                    {

                        if (charc.CharacteristicProperties.HasFlag(GattCharacteristicProperties.Write))
                        {
                            BluetoothLowEnergyDevicesModel.IsCharcWritable = true;
                        }
                        if (charc.CharacteristicProperties.HasFlag(GattCharacteristicProperties.Read))
                        {
                            BluetoothLowEnergyDevicesModel.IsCharcReadable = true;
                        }
                        if (charc.CharacteristicProperties.HasFlag(GattCharacteristicProperties.WriteWithoutResponse))
                        {
                            BluetoothLowEnergyDevicesModel.IsCharcWritableWithoutResponse = true;
                        }
                    }

                }
            }

        }
        catch (Exception en)
        {

            Console.WriteLine(en.Message);
        }
    }
    public async Task WriteInCharacteristic(Guid thisCharcGuid)
    {
        try
        {
            if (bleCharacteristics.Any())
            {
                foreach (var charc in bleCharacteristics)
                {
                    if (charc.Uuid == thisCharcGuid)
                    {
                        if (charc.CharacteristicProperties.HasFlag(GattCharacteristicProperties.Write))
                        {
                            var writer = new DataWriter();

                            writer.WriteByte(0x01);

                            GattCommunicationStatus resultWrite = await charc.WriteValueAsync(writer.DetachBuffer());
                            if (resultWrite == GattCommunicationStatus.Success)
                            {
                                // Successfully wrote to device
                            }
                        }
                    }
                }
            }
        }
        catch (Exception en)
        {

            Console.WriteLine(en.Message);
        }

    }
    private async Task NotifyCharacteristic(Guid thisCharcGuid)
    {
        try
        {
            if (bleCharacteristics.Any())
            {
                foreach (var charc in bleCharacteristics)
                {
                    if (charc.Uuid == thisCharcGuid)
                    {
                        if (charc.CharacteristicProperties.HasFlag(GattCharacteristicProperties.Notify))
                        {
                            GattCommunicationStatus status = await charc.WriteClientCharacteristicConfigurationDescriptorAsync(
                                    GattClientCharacteristicConfigurationDescriptorValue.Notify);
                            if (status == GattCommunicationStatus.Success)
                            {
                                charc.ValueChanged += Characteristic_ValueChanged;
                            }
                        }
                    }
                }
            }
        }
        catch (Exception en)
        {
            Console.WriteLine(en.Message);

        }

        // BluetoothLowEnergyDevicesModel.DeviceIsConnected = bluetoothLeDevice.ConnectionStatus.ToString();
    }

    private void Characteristic_ValueChanged(GattCharacteristic sender, GattValueChangedEventArgs args)
    {
        var reader = DataReader.FromBuffer(args.CharacteristicValue);
        //display this to user

    }

}
