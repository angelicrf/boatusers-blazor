import 'https://api.mapbox.com/mapbox-gl-js/v1.12.0/mapbox-gl.js';

export const callAlert = (thisMsg) => alert(thisMsg);

mapboxgl.accessToken = 'pk.eyJ1IjoiYW5nZWxyZWYiLCJhIjoiY2w0czNxMTA2MGkzcjNqbzB5cjlkM3BkaSJ9.gpg4wdvg4dobgzcw795VQw';

export const addMapToElement = element => {
    return new mapboxgl.Map({
        container: element,
        style: 'mapbox://styles/mapbox/streets-v11',
        center: [-74.5, 40],
        zoom: 9
    });
}

export const setMapCenter = (map, latitude, longitude) => {
    map.setCenter([longitude, latitude]);
}