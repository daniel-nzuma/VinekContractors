var map = new GMaps({
    el: '#map',
    lat: -1.286389,
    lng: 36.817223
});

map.addMarker({
    lat: -1.286389,
    lng: 36.817223,
    title: 'Nairobi'
});

map.setZoom(8);