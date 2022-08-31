import 'https://api.mapbox.com/mapbox-gl-js/v1.12.0/mapbox-gl.js';
import 'https://api.mapbox.com/mapbox-gl-js/plugins/mapbox-gl-geocoder/v2.3.0/mapbox-gl-geocoder.min.js';
//import 'https://api.mapbox.com/mapbox-gl-js/plugins/mapbox-gl-geocoder/v2.3.0/mapbox-gl-geocoder.css';

export const callAlert = (thisMsg) => alert(thisMsg);

mapboxgl.accessToken = 'pk.eyJ1IjoiYW5nZWxyZWYiLCJhIjoiY2w0czNxMTA2MGkzcjNqbzB5cjlkM3BkaSJ9.gpg4wdvg4dobgzcw795VQw';

export const addMapToElement = element => {
    return new mapboxgl.Map({
        container: element,
        style: 'mapbox://styles/mapbox/streets-v11',
        center: [-74.5, 40],
        zoom: 9
    });
    //var marker = new mapboxgl.Marker({color:"#ff3300",dragable:true,scale:0.8}).setLngLat([lng, lat]).addTo(map)

}

export const setMapCenter = (map, latitude, longitude) => {
    map.setCenter([longitude, latitude]);
}
export const convertToCoord = (map,thisLat,thisLng) => {
    let geocoder = new MapboxGeocoder({
        accessToken: mapboxgl.accessToken,
        mapboxgl: mapboxgl,
        reverseGeocode: true
    })
    map.addControl(geocoder); 
    if (thisLat != null && thisLng != null && thisLat != "" && thisLng != "") {
        geocoder.query(thisLat, thisLng)
    }
}