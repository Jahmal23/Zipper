

function googleAddPushpins(googleMap, resultData) {

     var icon = {
      
        url: "http://www.google.com/mapfiles/marker.png"
    };


    var markerOptions = {
        icon: icon
    };

    //NOTE - Assumes resultData.latLons && resultData.data are the same length!!
    for (var i = 0; i < resultData.latLons.length; i++) {

        var marker = new google.maps.Marker(markerOptions);
        marker.set("position", resultData.latLons[i]);
        marker.set("title", resultData.data[i]);
        marker.setMap(googleMap);
    }

    fitmap(googleMap, resultData.latLons);
}

function fitmap(googleMap, latLonArray) {
    var bounds = new google.maps.LatLngBounds();

    for (var i = 0; i < latLonArray.length; i++) {
        bounds.extend(latLonArray[i]);
    }

    googleMap.fitBounds(bounds);
}
