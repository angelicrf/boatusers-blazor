using BoatUsersMauiLibrary.Models;

namespace BoatRazorLibrary.Models;

public class BluetoothWDevicesPageCode
{
    public bool IsCheckedInfo { get; set; } = false;

    public bool IsServiceClicked { get; set; } = false;

    public bool IsCharcClicked { get; set; } = false;

    public bool IsReadCharac { get; set; } = false;

    public bool IsWriteCharac { get; set; } = false;

    public bool IsNotifyCharac { get; set; } = false;

    public bool IsWriteWithoutResponseCharac { get; set; } = false;

    public Guid ThisGuidService { get; set; }

    private BluetoothLowEnergyDevicesModel bluetoothLowEnergyDevicesModel = new BluetoothLowEnergyDevicesModel();

    public BluetoothWDevicesPageCode() { }

    public async void ScanBledevices()
    {
        IsCheckedInfo = false;

        await bluetoothLowEnergyDevicesModel.RunScanDevice();

        //StateHasChanged();
    }
    public async Task ConnectBledevice()
    {

        await bluetoothLowEnergyDevicesModel.RunConnectDevice();
        //StateHasChanged();
        //BluetoothLE#BluetoothLEf8:b5:4d:66:a3:4f-64:6a:9d:44:66:9b
        //0d54daa9-3114-528d-8dda-dc6934f39ed5

    }
    public async Task ServiceClicked(Guid thisGuid)
    {

        IsServiceClicked = true;

        BluetoothLowEnergyDevicesModel.CharacteristicsUUID = new List<Guid>();

        ThisGuidService = thisGuid;

        BluetoothLowEnergyDevicesModel.DeviceGuid = thisGuid;

        if (IsServiceClicked)
        {

            if (BluetoothLowEnergyDevicesModel.DeviceGuid != Guid.Empty)
            {
                await bluetoothLowEnergyDevicesModel.ShowDeviceCharcteristics();

                IsServiceClicked = false;
            }
        }
    }
    public void EvaluteCharac(Guid thisCharac)
    {
        IsCharcClicked = true;

        BluetoothLowEnergyDevicesModel.ThisGuidCharc = thisCharac;

        bluetoothLowEnergyDevicesModel.CharcteristicAbilities(thisCharac);
    }
    public async Task ReadCharac(Guid thisCharac)
    {
        IsReadCharac = true;

        BluetoothLowEnergyDevicesModel.ThisGuidCharc = thisCharac;

        await bluetoothLowEnergyDevicesModel.ReadDeviceCharcteristic(thisCharac);

    }
    public async Task NotifyCharac(Guid thisCharac)
    {
        IsNotifyCharac = true;

        BluetoothLowEnergyDevicesModel.ThisGuidCharc = thisCharac;

        await bluetoothLowEnergyDevicesModel.NotifyDeviceCharcteristic(thisCharac);

    }
    public async Task WriteCharac(Guid thisCharac)
    {
        IsWriteCharac = true;

        BluetoothLowEnergyDevicesModel.ThisGuidCharc = thisCharac;

        await bluetoothLowEnergyDevicesModel.ReadDeviceCharcteristic(thisCharac);

    }
    public async Task WriteWithoutResponseCharac(Guid thisCharac)
    {
        IsWriteWithoutResponseCharac = true;

        BluetoothLowEnergyDevicesModel.ThisGuidCharc = thisCharac;

        await bluetoothLowEnergyDevicesModel.WriteWithoutResponseDeviceCharcteristic(thisCharac);

    }
}
