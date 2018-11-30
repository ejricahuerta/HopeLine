const Video = Twilio.Video;
var activeRoom;
var previewTracks;
var identity;
var roomName;
var room;
var userId = $("#userId").val();

var option = {
    name: $("#roomId").val()
    //include usermediastream here and present it in preview
    //tracks: stream
}

Video.connect($("#token").val(), option).then(room => {
    console.log('Connected to Room "%s"', room.name);
    console.log('User name "%s"', userId);

    var localTracksPromise = previewTracks ?
    Promise.resolve(previewTracks) :
        Video.createLocalTracks();

    if (!String(userId).includes("Guest")) {
        localTracksPromise.then(function (tracks) {
            window.previewTracks = previewTracks = tracks;
            var previewContainer = document.getElementById('local-media');
            if (!previewContainer.querySelector('video')) {
                attachTracks(tracks, previewContainer);
            }
        }, function (error) {
            console.error('Unable to access local media', error);
            console.log('Unable to access Camera and Microphone');
        });
    }
    room.participants.forEach(function (participant) {
        console.log("Already in Room: '" + participant.identity + "'");
        var previewContainer = document.getElementById('remote-media');
        attachTracks(participant.tracks.val(), previewContainer);
    });
    

    room.participants.forEach(participantConnected);
    room.on('participantConnected', participantConnected);

    room.on('participantDisconnected', participantDisconnected);
    room.once('disconnected', error => room.participants.forEach(participantDisconnected));
});

function attachTracks(tracks, container) {
    tracks.forEach(function (track) {
        container.appendChild(track.attach());
    });
}

function participantConnected(participant) {
    console.log('Participant "%s" connected', participant.identity);

    const div = $("#remote-media");
    div.innerText = participant.identity;

    participant.on('trackSubscribed', track => trackSubscribed(div, track));
    participant.on('trackUnsubscribed', trackUnsubscribed);

    if (!String(participant.identity).includes("Guest")){
        participant.tracks.forEach(publication => {
            if (publication.isSubscribed) {
                trackSubscribed(div, publication.track);
            }
        });
    }

}

function participantDisconnected(participant) {
    console.log('Participant "%s" disconnected', participant.identity);
    alert("Other user disconnected video chat");
    window.close();
}

function trackSubscribed(div, track) {
    div.append(track.attach());
}

function trackUnsubscribed(track) {
    track.detach().forEach(element => element.remove());
}

















////Twilio Connection API
//const Video = Twilio.Video;
//var token = $("#token").val();
//var activeRoom;
//var previewTracks;
//var identity;
//var roomName = $("#roomId").val();;
//console.log("In video.js");
//// Secret RARbIc5RQS63xThPOKxaQTX088PPd2Pl
////SID SKf1fd0ea9a3e3d530efd1c76f53c3db08
////var token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiIsImN0eSI6InR3aWxpby1mcGE7dj0xIn0.eyJqdGkiOiJTS2NiMzgzOWNhNmIyYjRlOTE2ZTg3Yjk1MzlhZjk5NTg2LTE1NDMxNzQ5ODgiLCJpc3MiOiJTS2NiMzgzOWNhNmIyYjRlOTE2ZTg3Yjk1MzlhZjk5NTg2Iiwic3ViIjoiQUM5YTE0M2UwOTdiYzg2ZDQ1OTI1MWRhODcxYzZmMWJjNSIsImV4cCI6MTU0MzE3ODU4OCwiZ3JhbnRzIjp7ImlkZW50aXR5IjoidGVzdCIsInZpZGVvIjp7InJvb20iOiJyb29tIn19fQ.lLg85aN8_mYHRXNcpzwLykj_uJc_SiMHf9_rm - 461V4";
//console.log("token is: " + token);


//// Attach the Tracks to the DOM.
//function attachTracks(tracks, container) {
//    tracks.forEach(function (track) {
//        container.appendChild(track.attach());
//    });
//}

//// Attach the Participant's Tracks to the DOM.
//function attachParticipantTracks(participant, container) {
//    var tracks = Array.from(participant.tracks.values());
//    attachTracks(tracks, container);
//}

//// Detach the Tracks from the DOM.
//function detachTracks(tracks) {
//    tracks.forEach(function (track) {
//        track.detach().forEach(function (detachedElement) {
//            detachedElement.remove();
//        });
//    });
//}

//// Detach the Participant's Tracks from the DOM.
//function detachParticipantTracks(participant) {
//    var tracks = Array.from(participant.tracks.values());
//    detachTracks(tracks);
//}

//// When we are about to transition away from this page, disconnect
//// from the room, if .
//window.addEventListener('beforeunload', leaveRoomIfJoined);

//// Obtain a token from the server in order to connect to the Room.
//$(document).ready(function () {

//    var connectOptions = {
//        name: roomName,
//        logLevel: 'debug',
//        video: true,
//        audio: true,
//    };

//    if (previewTracks) {
//        connectOptions.tracks = previewTracks;
//    }

//    // Join the Room with the token from the server and the
//    // LocalParticipant's Tracks.
//    Video.connect(token, connectOptions).then(roomJoined, function (error) {
//        console.log(error);
//    });

//    var localTracksPromise = previewTracks ?
//        Promise.resolve(previewTracks) :
//        Video.createLocalTracks();

//    localTracksPromise.then(function (tracks) {
//        window.previewTracks = previewTracks = tracks;
//        var previewContainer = document.getElementById('local-media');
//        if (!previewContainer.querySelector('video')) {
//            attachTracks(tracks, previewContainer);
//        }
//    }, function (error) {
//        console.error('Unable to access local media', error);
//        console.log('Unable to access Camera and Microphone');
//    });
//});



//// Successfully connected!
//function roomJoined(room) {
//    window.room = activeRoom = room;
//    console.log("Joined as '" + identity + "'");
//    // Attach LocalParticipant's Tracks, if not already attached.
//    var previewContainer = document.getElementById('local-media');
//    if (!previewContainer.querySelector('video')) {
//        attachParticipantTracks(room.localParticipant, previewContainer);
//    }

//    // Attach the Tracks of the Room's Participants.
//    room.participants.forEach(function (participant) {
//        console.log("Already in Room: '" + participant.identity + "'");
//        var previewContainer = document.getElementById('remote-media');
//        attachParticipantTracks(participant, previewContainer);
//    });

//    // When a Participant joins the Room, log the event.
//    room.on('participantConnected', function (participant) {
//        console.log("Joining: '" + participant.identity + "'");
//    });

//    // When a Participant adds a Track, attach it to the DOM.
//    room.on('trackAdded', function (track, participant) {
//        console.log(participant.identity + " added track: " + track.kind);
//        var previewContainer = document.getElementById('remote-media');
//        attachTracks([track], previewContainer);
//    });

//    // When a Participant removes a Track, detach it from the DOM.
//    room.on('trackRemoved', function (track, participant) {
//        console.log(participant.identity + " removed track: " + track.kind);
//        detachTracks([track]);
//    });

//    // When a Participant leaves the Room, detach its Tracks.
//    room.on('participantDisconnected', function (participant) {
//        console.log("Participant '" + participant.identity + "' left the room");
//        detachParticipantTracks(participant);
//    });

//    // Once the LocalParticipant leaves the room, detach the Tracks
//    // of all Participants, including that of the LocalParticipant.
//    room.on('disconnected', function () {
//        console.log('Left');
//        if (previewTracks) {
//            previewTracks.forEach(function (track) {
//                track.stop();
//            });
//            previewTracks = null;
//        }
//        detachParticipantTracks(room.localParticipant);
//        room.participants.forEach(detachParticipantTracks);
//        activeRoom = null;
//        document.getElementById('button-join').style.display = 'inline';
//        document.getElementById('button-leave').style.display = 'none';
//    });
//}


//// Leave Room.
//function leaveRoomIfJoined() {
//    if (activeRoom) {
//        activeRoom.disconnect();
//    }
//}