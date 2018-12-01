const Video = Twilio.Video;
var activeRoom;
var previewTracks;
var identity;
var roomName;
var thisRoom;
var userId = $("#userId").val();

var remoteTracks;
var localTracks;
var localVideoShow = true;
var remoteVideoShow = false;




var option = {
    name: $("#roomId").val()
    //include usermediastream here and present it in preview
    //tracks: stream
}

Video.connect($("#token").val(), option).then(room => { // Connect the to room
    
    
    console.log('Connected to Room "%s"', room.name);
    console.log('User name "%s"', userId);
    var localTracksPromise = previewTracks ? // Get local video
        Promise.resolve(previewTracks) :
        Video.createLocalTracks();

    var localMedia;
    console.log("Creating local media");
    Video.createLocalVideoTrack().then(function (videoTrack) {
        console.log("inside videoooo media");
        localMedia = videoTrack;
        $("#local-media").append(videoTrack.attach());
    });
    //Video.createLocalVideoTrackPublication().then(function () {
        
    //});

    $("#video-button").click(function myfunction() {
        console.log("clicking button");
        if (!localVideoShow) {
            localMedia.disable();
            localVideoShow = true;
        } else {
            localMedia.enable();
            localVideoShow = false;
        }
    });

    room.participants.forEach(function (participant) { // When joining the room, gets the participants and adds their media to your screen. 
        console.log("Already in Room: '" + participant.identity + "'");
        var previewContainer = document.getElementById('remote-media');
        attachTracks(participant.tracks.val(), previewContainer);
    });
    
    room.participants.forEach(participantConnected);
    room.on('participantConnected', participantConnected);

    room.on('participantDisconnected', participantDisconnected);
    room.once('disconnected', error => room.participants.forEach(participantDisconnected));
    thisroom = room;
});


function attachTracks(tracks, container) {

    if (!localVideoShow) {
        tracks.forEach(function (track) {
            container.append(track.attach());
        });
        localVideoShow = true;
    }
}

function participantConnected(participant) { // When a new person connects to the room that you are in already, get their media.
    console.log('Participant "%s" connected', participant.identity);

    const div = $("#remote-media");
    div.innerText = participant.identity;

    participant.on('trackSubscribed', track => trackSubscribed(div, track));
    participant.on('trackUnsubscribed', trackUnsubscribed);
    
    participant.tracks.forEach(publication => {
        if (publication.isSubscribed) {
            trackSubscribed(div, publication.track);
        }
    });

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
