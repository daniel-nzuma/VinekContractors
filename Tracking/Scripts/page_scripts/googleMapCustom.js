var map;

function initializeMap() {
    var mapProperties = {
        center: new google.maps.LatLng(-1.328445,36.8931866666667),
        zoom: 10,
    };
    map = new google.maps.Map(document.getElementById('map'), mapProperties);
};

function initializePositionsTable()
{
    var routesTable = $('#routesTable').DataTable({
        "scrollY": "200px",
        "scrollCollapse": true,
        "paging": true,
        "searching": true,
        "ordering": false,
        "columns": [
            { "data": "id" },
            { "data": "servertime" },
            { "data": "devicetime" },
            { "data": "latitude" },
            { "data": "longitude" },
            { "data": "altitude" },
            { "data": "speed" },
            { "data": "course" },
            { "data": "accuracy" }],

    });

    $('#routesTable tbody').on('click', 'tr', function () {
        var data = routesTable.row(this).data();
        //alert('You clicked on ' + data[0] + '\'s row');

    });

    $('#accordion').on('shown.bs.collapse', function (e) {
        routesTable.columns.adjust();

        $.ajax({
            url: "/CarTracking/FetchGPSPositions",
            data: { deviceID: 2},
            type:"POST"
        }).done(function (result)
        {
            var resul = JSON.parse(result);
            routesTable.clear().draw();
            routesTable.rows.add(resul.data).draw();
            console.log(resul.data);
        }).fail(function (jqXHR, textStatus, errorThrown)
        {


        });

    });
}

$(document).ready(function () {

    initializeMap();
    initializePositionsTable();

    new google.maps.Marker({
        position:{ lat: -0.091702, lng: 34.767956 },
        map,
        title: "Hello World!",
    });

    new google.maps.Marker({
        position: { lat: -1.328445, lng: 36.893186666666 },
        map,
        title: "Hello World!",
    });
   

});


