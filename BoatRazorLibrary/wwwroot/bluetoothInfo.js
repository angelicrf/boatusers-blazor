export const getBluetoothDevices = () => {

    try {
       
        const button = document.getElementById('btId')

        button.addEventListener('click', async function () {
           
            navigator.bluetooth.requestDevice({
                //'ShellyPlugUS-083AF200732C'
                filters: [{ name: 'ShellyPlugUS-083AF200732C' }],   
                optionalServices: ['0000180a-0000-1000-8000-00805f9b34fb']
                //or acceptAllDevices: true
            }).then(device => device.gatt.connect())
               .then(async(server) => {
                   let result = BluetoothUUID.getService('device_information');
                   let getResult = await server.getPrimaryService(result);
                   if (getResult != null) {
                       let getCharDevice = await getResult.getCharacteristic('00002a00-0000-1000-8000-00805f9b34fb')
                       if (getCharDevice != null) {
                           console.log(getCharDevice)
                           readDeviceValue(getCharDevice);
                       }
                   }
                })
            })
    } catch (e) {
        alert(e)
    }
}
const readDeviceValue = async (characteristic) => {
    let resDevice = await characteristic.readValue()
    if (resDevice != null) {
        console.log(resDevice.getUint8(0))
        startNotificationValue(characteristic)
        //83
    }
}
const startNotificationValue = async (characteristic) => {
    try {
        //var descriptId = await characteristic.getDescriptors()
        //var descriptor = await characteristic.getDescriptor("0000180a-0000-1000-8000-00805f9b34fb");
        //characteristic.setWriteType(BluetoothGattCharacteristic.WRIT‌​E_TYPE_DEFAULT);
        //await descriptor.setValue(BluetoothGattDescriptor.ENABLE_NOTIFICATION_VALUE);
        //await BluetoothGatt.writeDescriptor(descriptor);
        //console.log(descriptId)
        //characteristic.setCharacteristicNotification(characteristic, true);
        //let encoder = new TextEncoder('utf-8');
        //let sendMsg = encoder.encode("On");
        //await characteristic.writeValue(sendMsg)
        if (characteristic.properties.notify) {
            characteristic.addEventListener('characteristicvaluechanged', async (event) => {
                alert("clicked")
                console.log(`value: ${event.target.value}`);
            });

            await characteristic.startNotifications();
       }

    } catch (e) {
        alert(e)
    }
}

