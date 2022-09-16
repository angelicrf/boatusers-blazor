let thisElement = ""
let isClicked = false
let getResult = {}
export const getBluetoothDevices = () => {

    try {

        const button = document.getElementById('btId')

        button.addEventListener('click', async function () {

            let device = await navigator.bluetooth.requestDevice({
                //00002a00-0000-1000-8000-00805f9b34fb
                //00002a29-0000-1000-8000-00805f9b34fb
                optionalServices: ["battery_service", "device_information"],
                acceptAllDevices: true,
            });

            let server = await device.gatt.connect()

            let result = await BluetoothUUID.getService('device_information')
            getResult = await server.getPrimaryService(result)
            let allChars = await getResult.getCharacteristics()
            await displayElemnts(allChars, "thisId")

            //setInterval(async () => {
            //    if (isClicked && thisElement != null) {
            //        let getCharDevice = await getResult.getCharacteristic(thisElement)                    
            //        readDeviceValue(getCharDevice)
            //        startNotificationValue(getCharDevice)
            //    }
            //}, 10000)

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
        const value = new Uint8Array([0x00, 0x00, 0x00, 0x00]);
        await characteristic.writeValueWithoutResponse(value);
        
        if (characteristic.properties.notify) {
            characteristic.addEventListener('characteristicvaluechanged', async (event) => {
                console.log(`value: ${event.target.value}`);
            });
        }
       
       //     await characteristic.startNotifications();
        isClicked = false

    } catch (e) {
        alert(e)
        isClicked = false
    }
}
const displayElemnts = (thisList, thisId) => {

    return new Promise(async (resolve, reject)  => {
        var getId = document.getElementById(thisId)
        getId.innerHTML = "Characteristics"
        for (var i in thisList) {

            let li = document.createElement("li")
            let a = document.createElement("a")
            a.setAttribute("href", "#")
            a.appendChild(document.createTextNode(thisList[i]['uuid']))
            li.appendChild(a)
            getId.appendChild(li)
            let getresolve = createBtn(a, thisList[i]['uuid'])
            resolve(getresolve)
        }
    })
}
const createBtn = (a, c) => {
    return new Promise(function (resolve, reject) {
        a.onclick = async() => {  
            alert(c)
            let getCharDevice = await getResult.getCharacteristic(c) 
            readDeviceValue(getCharDevice)
            startNotificationValue(getCharDevice)
            //loadElements(c)
            //isClicked = true       
        }
        resolve("done")
    })
}
const loadElements = (c) => thisElement = c;


