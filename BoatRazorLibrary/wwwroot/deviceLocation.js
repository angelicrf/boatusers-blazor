export const deviceLocationInfo = async() => {
    return await new Promise((resolve, reject) => {
        try {
            navigator.geolocation.getCurrentPosition(async (position) => {
                let thisAddress = await deviceGeocodeInfo(position.coords.latitude, position.coords.longitude)
                return resolve({ "lat": position.coords.latitude, "lon": position.coords.longitude, "deviceLoc": thisAddress})
           })
           
        } catch (e) {
            console.log(e)
            reject(e)
        }  
    }) 
   
}

export const deviceGeocodeInfo = async (thisLat,thisLon) => {
    return await new Promise((resolve, reject) => {

        var requestOptions = {
            method: 'GET',
        }

        fetch(`https://api.geoapify.com/v1/geocode/reverse?lat=${thisLat}&lon=${thisLon}&apiKey=8d0be6cef0d54827b70410032ecb7df1`, requestOptions)
            .then(response => response.json())
            .then(result => {
                if (result.features.length) {
                    console.log(JSON.stringify(result.features[0].properties.formatted))
                    return resolve(result.features[0].properties.formatted)
                } else {
                    console.log("No address found");
                }
            })
            .catch(error => console.log('error', error));

    })
}
