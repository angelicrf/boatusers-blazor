import 'https://api.mapbox.com/mapbox-gl-js/v1.12.0/mapbox-gl.js'
import 'https://api.mapbox.com/mapbox-gl-js/plugins/mapbox-gl-geocoder/v2.3.0/mapbox-gl-geocoder.min.js'

export const callAlert = (thisMsg) => alert(thisMsg)

let thisArray = []
mapboxgl.accessToken = 'pk.eyJ1IjoiYW5nZWxyZWYiLCJhIjoiY2w0czNxMTA2MGkzcjNqbzB5cjlkM3BkaSJ9.gpg4wdvg4dobgzcw795VQw'

export const initialLocationAddMap = element => {

        return new mapboxgl.Map({
            container: element,
            style: 'mapbox://styles/mapbox/streets-v11',
            center: [-122.091937, 47.632651],
            zoom: 9
        });
    }  
const getCurrentLocation = () => {
    navigator.geolocation.getCurrentPosition(position => {
        storeData(position.coords.latitude, position.coords.longitude)
    });
} 
const sleepTime = thisTime => {
    return new Promise(resolve => setTimeout(resolve, thisTime))
}
const storeData = (lat, lng) => {
    thisArray.push(lat)
    thisArray.push(lng)
    console.log(thisArray)
    return thisArray
}
export const currentLocationAddMap = element => {

    if (thisArray.length > 0) {
        return new mapboxgl.Map({
            container: element,
            style: 'mapbox://styles/mapbox/streets-v11',
            center: [thisArray[1], thisArray[0]],
            zoom: 9
        });
    }
}
export const setMapMarker = (element) => {

    navigator.geolocation.getCurrentPosition(position => {
        var marker = new mapboxgl.Marker({ color: "#ff3300", dragable: true, scale: 0.8 }).setLngLat([position.coords.longitude, position.coords.latitude]).addTo(element)
    });
     
}
export const showStoredMarkers = (map) => {

    if (localStorage.getItem("storedmarkers") != null) {

        let getStMarkers = JSON.parse(localStorage.getItem("storedmarkers"))
        for (var i = 0; i < getStMarkers.length; i++) {
            for (var j = 0; j < getStMarkers[i].length; j++) {
                var searchMarker = new mapboxgl.Marker({ color: "#ff3300", dragable: true, scale: 0.8 }).setLngLat([getStMarkers[i][0], getStMarkers[i][1]]).addTo(map)
                map.setZoom(-10);
            }
        }
    }
}
export const findPlace = (map, thisPlace) => {

    let geocoder = new MapboxGeocoder({
        accessToken: mapboxgl.accessToken,
        mapboxgl: mapboxgl,
        reverseGeocode: true,
        getItemValue: e => {
            return e['place_name'];
            }
    })
    map.addControl(geocoder);
    if (thisPlace != null && thisPlace != "") {      
        geocoder.on('result', (e) => {
            let searchNewMarker = new mapboxgl.Marker({ color: "#ff3300", dragable: true, scale: 0.8 }).setLngLat([e.result.center[0], e.result.center[1]]).addTo(map)
            if (localStorage.getItem("storedmarkers") == null) {
                localStorage.setItem("storedmarkers", JSON.stringify(e.result.center))
            } else {
                let getStrMarkers = JSON.parse(localStorage.getItem("storedmarkers"))

                let storeData = []
                storeData.push(getStrMarkers)
                storeData.push(e.result.center)

                localStorage.setItem("storedmarkers", JSON.stringify(storeData))
            }
        })
        geocoder.query(thisPlace)
    }
}
getCurrentLocation()
