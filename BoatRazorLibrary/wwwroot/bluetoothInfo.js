let thisElement = ""
let isClicked = false
let getResult = {}
let outboundData = {}
let inboundData = {}
let specificCharacteristic = {}
let descriptorData = {}
var bluetoothDevice
let blueToothConnectData = {}

export const getBluetoothDevices = () => {

    try {

        const btnOn = document.getElementById('btId')
        const btnOff = document.getElementById('btOff')
        const btnScan = document.getElementById('btScan')
        const btnDisconnect = document.getElementById('btDist')

        btnOn.addEventListener('click', () => {

            return (bluetoothDevice ? connectDeviceAndCacheCharacteristics() : Promise.resolve(alert('No Device Found To Turn On. Scan to Pair a Device!')))
                .catch(e => console.log(`Error To Connect ${e}`))
            //await displayElemnts(allChars, "thisId")

            //setInterval(async () => {
            //    if (isClicked && thisElement != null) {
            //        let getCharDevice = await getResult.getCharacteristic(thisElement)                    
            //        readDeviceValue(getCharDevice)
            //        startNotificationValue(getCharDevice)
            //    }
            //}, 10000)

        })
        btnScan.addEventListener('click', async () => {
            return (bluetoothDevice ? Promise.resolve() : requestDevice())
                .catch(e => console.log(`Error To Scan ${e}`))
        })
        btnOff.addEventListener('click', async () => {
            
            return (bluetoothDevice ? turnOffDevice() : Promise.resolve(alert('No Device Found To Turn Off. Scan to Pair a Device!')))
                .catch(e => console.log("Error Connect Device " + e))
        })
        btnDisconnect.addEventListener('click', () => {  
            disconnectDevice()
        })
    } catch (e) {
        console.log(e)
    }
}
const turnOffDevice = async () => {

    if (blueToothConnectData != 'undefined') {
       
        if (blueToothConnectData.connected) {
           
            await getServicesAndCharacteristics() 

            if (inboundData.properties != undefined) {
                inboundData.properties.writeValueWithoutResponse = true
                try {

                    await inboundData.writeValueWithoutResponse(new Uint8Array([0x00000001])) // writeValue is active but depreciated // writeValueWithoutResponse - writeValueWithResponse are not active
                    await createDelay()
                    await outboundData.writeValue(new Uint8Array([0x00]))
                    await createDelay()
                    await outboundData.writeValue(new Uint8Array([0x0000]))
                    
                } catch (e) {
                    console.log(`Error Turn Off ${e}`)
                }
            }
        }
    } 
}
const requestDevice = () => {
    console.log('Requesting any Bluetooth Device...')
    return navigator.bluetooth.requestDevice({
        acceptAllDevices: true,
        optionalServices: ["battery_service", "device_information", "5f6d4f53-5f52-5043-5f53-56435f49445f"]
    })
        .then(async device => {
            bluetoothDevice = device;
           // bluetoothDevice.addEventListener('gattserverdisconnected', onDisconnected)
            blueToothConnectData = await bluetoothDevice.gatt.connect()
            return blueToothConnectData
        });
}
const connectDeviceAndCacheCharacteristics = async () => {

    if (blueToothConnectData != 'undefined' ) {
        try {
            if (blueToothConnectData.connected) {
                console.log("inTurnOn")
                await getServicesAndCharacteristics()
         
                if (inboundData.properties != undefined) {
                    await outboundData.writeValue(new Uint8Array([]))
                    await createDelay()
                    await outboundData.readValue()
                    await createDelay()
                    await outboundData.readValue()
                }
            }
        } catch (e) {
            console.log(e)
        }
    }
     //specificCharacteristic = await service.getCharacteristic("5f6d4f53-5f52-5043-5f72-785f63746c5f")

    //descriptorData = await specificCharacteristic.getDescriptor("00002902-0000-1000-8000-00805f9b34fb")

        //let receivedValue = await specificCharacteristic.readValue()

        //specificCharacteristic.characteristicvaluechanged = handleEventSpecific

        //if (specificCharacteristic.properties.notify) {

        //    specificCharacteristic.addEventListener('characteristicvaluechanged',
        //        event => {
        //            console.log(event.target.value)
        //            let value = event.target.value
        //            console.log("HELLO bis" + value.getUint8(0))

        //            let a = []
        //            for (let i = 0; i < value.byteLength; i++) {
        //                a.push('0x' + ('00' + value.getUint8(i).toString(16)).slice(-2))
        //            }
        //            console.log(`Reading New Value` + a.join(' '))
        //        })
        //    await specificCharacteristic.startNotifications()
        //}
    //})
}
const createDelay = () => {
    return new Promise(resolve =>
        setTimeout(resolve, 10000)
    )
}
const getServicesAndCharacteristics = () => {
    return new Promise(async (resolve,reject) => {
        let allServices = await blueToothConnectData.getPrimaryServices()
        let service = await blueToothConnectData.getPrimaryService(allServices[0].uuid)
        let allCharacteristics = await service.getCharacteristics()
        allCharacteristics.filter(x => {
            if (x.properties.write == true && x.properties.read == true) {
                outboundData = x
            } else if (x.properties.write == true && x.properties.read == false) {
                inboundData = x
                return resolve(x)
            }
        })
    })
}
const writeDataToDevide = () => {
    try {
        return new Promise((resolve, reject) => {
            return descriptorData.readValue().then(val => {

                console.log('Decoded Value: ' + new TextDecoder('utf-8').decode(val))
                return resolve (new TextDecoder('utf-8').decode(val))
            })
        })
    } catch (e) {
        alert(e)
    }
}
const disconnectDevice = () => {
  
    try {
        if (bluetoothDevice != 'undefined') {
            if (bluetoothDevice.gatt.connected) {
                return Promise.resolve(bluetoothDevice.gatt.disconnect());
            }
        }
    } catch (e) {
        alert(`Have To Connect Before Disconnect ${e}`)
    }

}
const readDeviceValueFromDescriptor = async (characteristic) => {
    let resDevice = await characteristic.readValue()
       //writeDataToDevide()
            //    .then(thisValue => {
            //        console.log(`Unit8Array of 1 : ${new Uint8Array([0x01, 0x00, 0x00, 0x00]) }`)
            //        let encodedValue = new TextEncoder('utf-8').encode(thisValue)
            //        //new Uint8Array([0x00, 0x00, 0x00, 0x00])
            //        //Uint8Array.of(1)
            //        let thisCmd = 'ATA1'
            //        let sendThisCommand = thisCmd + "\0"

            //        try {

            //            specificCharacteristic.readValue()
            //                .then(val => {
            //                    countRead++
            //                    val.setUint8(0, 255)
            //                    console.log(`${countRead} Time To Read ${val.getUint8(0)}`)
            //                })
            //                .catch(error => alert(error))

            //            specificCharacteristic.writeValueWithoutResponse(Uint8Array.from('turn off'))
            //                .then(_ => {
            //                    console.log(`Characteristic User Description changed to: ${encodedValue}`)
            //                })
            //                .catch(error => {
            //                    alert('Error From Write ' + error)
            //                });

            //        } catch (e) {
            //            alert(e)
            //        }
            //    })
}
const handleEventSpecific = (event) => console.log(event)
const startNotificationValue = async (characteristic) => {
    try {
        //var descriptId = await characteristic.getDescriptors()
        var descriptor = await characteristic.getDescriptor("00002902-0000-1000-8000-00805f9b34fb");
        descriptor.readValue()
            .then(value => console.log("descriptorValue " + value))
            .catch(e => console.log("Error From Descriptor " + e))
        if (characteristic.properties.notify) {
            characteristic.addEventListener('characteristicvaluechanged', async (event) => {
               
                outboundData.readValue();
                console.log(`value: ${event.target.value.getUint8(0)}`)
            });
        }      
          //await characteristic.startNotifications();
        isClicked = false

    } catch (e) {
        alert("This Is an Error " + e)
        isClicked = false
    }
}
const hexToBytes = (thisHex) => {
    for (var bytes = [], i = 0; i < thisHex.length; i += 2) 
        bytes.push(parseInt(thisHex.substr(i, 2), 16))
    return bytes
}
const displayElemnts = (thisList, thisId) => {

    return new Promise(async (resolve, reject)  => {
        var getId = document.getElementById(thisId)
        getId.innerHTML = "Characteristics"
        for (var i in thisList) {
            //thisList[i].setValue(data_to_write)
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
            //readDeviceValue(getCharDevice)
            startNotificationValue(getCharDevice)
            setTimeout(() => {
                SendCommand()
            },2500)
            //loadElements(c)
            //isClicked = true       
        }
        resolve("done")
    })
}
const SendCommand = async () => {

    let thisCmd = 'ATA1'
    let sendThisCommand = thisCmd + "\0"
    try {

        let thisEncodeMsg = new TextEncoder('utf-8')

        await inboundData.writeValueWithResponse(thisEncodeMsg.encode(sendThisCommand))
            //new Uint8Array([0x01, 0x02]))
            //hexToBytes("0x0123456789")
            //new Uint8Array([0x01, 0x20, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00]))

    } catch (e) {

        console.log("Error From SendCommand " + e)
    }

}
const loadElements = (c) => thisElement = c


