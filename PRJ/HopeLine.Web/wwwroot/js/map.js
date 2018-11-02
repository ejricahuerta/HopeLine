var searchBox, input, bounds, locationType, marker, placeLoc, places;
var results = [];
var markers = [];
var tempResults = [];

var pos = {
    lat: position.coords.latitude,
    lng: position.coords.longitude
}


/* When the page is loaded initAutocompelte would be called to generate the map*/
function initAutocomplete() {
    /*Default generated position is Seneca College@York */
    map = new google.maps.Map(document.getElementById('map'), {

        center: { lat: 43.77, lng: -79.49 },
        zoom: 13,
        mapTypeId: 'roadmap'

    });

    //load the places(Google api feature) service map
    var infoWindow = new google.maps.InfoWindow();
    // Create the search box and link it to the UI element.
    input = document.getElementById('pac-input');
    searchBox = new google.maps.places.SearchBox(input);
    map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);
    ////////////////////////////////////////////// 
    bounds = new google.maps.LatLngBounds();

    if (navigator.geolocation) {

        navigator.geolocation.getCurrentPosition(function (position) {

            pos = {
                lat: position.coords.latitude,
                lng: position.coords.longitude

            };

            infoWindow.setPosition(pos);
            infoWindow.setContent('You are here!');
            infoWindow.open(map);
            map.setCenter(pos);

        }, function () {
            handleLocationError(true, infoWindow, map.getCenter());
        });
    } else {
        // Browser doesn't support Geolocation
        handleLocationError(false, infoWindow, map.getCenter());
    }

    // Bias the SearchBox results towards current map's viewport.
    map.addListener('bounds_changed', function () {
        searchBox.setBounds(map.getBounds());
    });
    markers = [];
    //  var markers = [];
    // Listen for the event fired when the user selects a prediction and retrieve
    // more details for that place.
    searchBox.addListener('places_changed', function () {

        places = searchBox.getPlaces();

        if (places.length == 0) {
            return;
        }
        // Clear out the old markers.
        markers.forEach(function (marker) {
            marker.setMap(null);

        });

        markers = [];
        // For each place, get the icon, name and location.
        places.forEach(function (place) {

            if (!place.geometry) {
                console.log("Returned place contains no geometry");
                return;
            }

            var icon = {

                url: place.icon,
                size: new google.maps.Size(71, 71),
                origin: new google.maps.Point(0, 0),
                anchor: new google.maps.Point(17, 34),
                scaledSize: new google.maps.Size(25, 25)

            };

            // Create a marker for each place.
            markers.push(new google.maps.Marker({

                map: map,
                icon: icon,
                title: place.name,
                position: place.geometry.location

            }));

            if (place.geometry.viewport) {
                // Only geocodes have viewport.
                bounds.union(place.geometry.viewport);
            } else {
                bounds.extend(place.geometry.location);
            }

        });
        map.fitBounds(bounds);

    });
}

/* Everytime a checkbox is checked or unchecked search type would
be called and locationType data would be passed on. If
It's checked call searchLocation(LocationType) else delete the markers*/
function searchType(locationType, isChecked) {

    if (isChecked.checked) {

        searchLocation(locationType);
    } else {
        deleteMarkers(isChecked, locationType);

    }

}
/*Generate infoWindow and service for the generated google map.
Request variable would find the nearest locations (locatioNType) within it's
radius. Callback function would be called passing in the results,
status if callback is succesful and the location Type*/


function searchLocation(locationType) {

    infowindow = new google.maps.InfoWindow();
    service = new google.maps.places.PlacesService(map);
    var request = {
        location: pos,
        radius: 9000,
        type: [locationType]
    };
    service.nearbySearch(request, function (results, status) {
        callback(results, status, locationType);
    });
}

/*If geolocation fails send a message  */

function handleLocationError(browserHasGeolocation, infoWindow, pos) {
    infoWindow.setPosition(pos);
    infoWindow.setContent(browserHasGeolocation ?
        'Error: The Geolocation service failed.' :
        'Error: Your browser doesn\'t support geolocation.');
    infoWindow.open(map);
}
//////////////////////////////////////////////////////
/*Sets both filtered markers and google search markers to null */
function setMapOnAll(map) {
    for (var i = 0; i < results.length; i++) {
        results[i].setMap(map);

    }

    for (var j = 0; j < markers.length; j++) {
        markers[j].setMap(map);
    }
}

function clearMarkers() {
    var checkboxes = document.getElementsByName('locationBox');
    for (var i = 0; i < checkboxes.length; i++) {
        checkboxes[i].checked = false;
    }
    setMapOnAll(null);
}
/*Scans through temporary array(tempResults) and checks if it's locationType
property is equal to locationType of passed parameter.
If true will set the markers to null. */

function deleteMarkers(isChecked, locationType) {

    for (var i = 0; i < tempResults.length; i++) {
        // console.log(tempResults[i].locationType + "<-results[i].locationType INdforloop");
        if (tempResults[i].locationType == locationType) {
            // console.log(tempResults[i].locationType + "<-results[i].locationType IN DELETEMARKERS");
            results[i].setMap(null);

        }

    }

}
