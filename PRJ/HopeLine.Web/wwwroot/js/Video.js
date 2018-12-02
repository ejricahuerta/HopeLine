
const Video = Twilio.Video;
var activeRoom;
var previewTracks;
var identity;
var roomName;
var thisRoom;
var userId = $("#userId").val();

var remoteTracks;
var localTracks;
var localAudioShow = false;
var localVideoShow = false;
var remoteVideoShow = false;
var localVideoMedia;
var localAudioMedia;




var option = {
    name: $("#roomId").val(),
    audio: false,
    video: false
}

Video.connect($("#token").val(), option).then(room => { // Connect the to room
    console.log("updated1");
    
    console.log('Connected to Room "%s"', room.name);
    console.log('User name "%s"', userId);
    


    console.log("Creating local media");
    Video.createLocalVideoTrack().then(function (videoTrack) {
        console.log("inside video media");
        localVideoMedia = videoTrack;
        $("#local-media").append(videoTrack.attach());
        localVideoMedia.disable();
    });
    Video.createLocalAudioTrack().then(function(audioTrack) {
        console.log("inside audio media");
        localAudioMedia = audioTrack;
        $("#local-media").append(audioTrack.attach());
        localAudioMedia.disable();
    })

    $("#close-button").click(function () {
        room.disconnect();
        alert("Video Disconnected");
        window.close();
    });

    $("#mute-button").click(function () {
        if (localAudioShow) {
            prompt("Audio Disabled");
            localAudioMedia.disable();
            localAudioShow = false;
            room.localParticipant.unpublishTrack(localAudioMedia);
        } else {
            prompt("Audio Enabled");
            localAudioMedia.enable();
            localAudioShow = true;
            room.localParticipant.publishTrack(localAudioMedia);
        }
    });


    $("#video-button").click(function() {
        if (localVideoShow) {
            prompt("Video Disabled");
            console.log("clicking button to disable");
            localVideoMedia.disable();
            localVideoShow = false;
            room.localParticipant.unpublishTrack(localVideoMedia);
        } else {
            prompt("Video Enabled");
            console.log("clicking button to enable");
            localVideoMedia.enable();
            room.localParticipant.publishTrack(localVideoMedia);
            localVideoShow = true;
        }
    });

    room.on('trackPublished', function () {
        console.log("track was published");
    });
    room.on('trackUnpublished', function () {
        console.log("track was unpublished");
    });

    room.on('trackDisabled', function () {
        console.log("track was disabled");
    });

    room.on('trackEnabled', function () {
        console.log("track was enabled");
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
