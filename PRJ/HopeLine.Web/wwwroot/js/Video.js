
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
    name: $("#roomId").val(), // Getting the room name to connect
    audio: false, //Stop audio and video from connecting to the room automatically
    video: false
}

Video.connect($("#token").val(), option).then(room => { // Connect the to room
    
    console.log('Connected to Room "%s"', room.name);
    console.log('User name "%s"', userId);
    

    
    Video.createLocalVideoTrack().then(function (videoTrack) { //Creating Video media
        localVideoMedia = videoTrack; //Saving the video media to use later
        $("#local-media").append(videoTrack.attach()); //Adding video media to our webpage
        localVideoMedia.disable(); //Disable it
    });
    Video.createLocalAudioTrack().then(function(audioTrack) { //Creating audio media
        localAudioMedia = audioTrack; //Saving the audio media to use later
        $("#local-media").append(audioTrack.attach()); //Adding audio media to our webpage
        localAudioMedia.disable();//Disable it
    })

    $("#close-button").click(function () { //When the click on the close icon, disconnect from room and close window
        room.disconnect();
        alert("Video Disconnected");
        window.close();
    });

    $("#mute-button").click(function () { //When clicking the mute button
        if (localAudioShow) { //If not muted
            localAudioMedia.disable(); //Disable self audio from own screen
            localAudioShow = false;
            room.localParticipant.unpublishTrack(localAudioMedia); //Disable self audio from the room
        } else { //If muted
            localAudioMedia.enable(); //Enable self audio
            localAudioShow = true;
            room.localParticipant.publishTrack(localAudioMedia); //Enable self audio to room
        }
    });


    $("#video-button").click(function() { //When clicking the video button
        if (localVideoShow) { //If video enabled
            localVideoMedia.disable(); //Disable self video from own screen
            localVideoShow = false;
            room.localParticipant.unpublishTrack(localVideoMedia); //Disable self video from room
        } else { //If video showing
            localVideoMedia.enable(); //Enable self video to own screen
            room.localParticipant.publishTrack(localVideoMedia); //Enable self video to room
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


    room.participants.forEach(function (participant) { // When joining the room, checks whos there
        console.log("Already in Room: '" + participant.identity + "'");
    });

    
    
    room.participants.forEach(participantConnected);
    room.on('participantConnected', participantConnected); //When someone connects to room, run 'participantConnected function

    room.on('participantDisconnected', participantDisconnected); //When someone disconnects from room, run 'participantDisconnected
    room.once('disconnected', error => room.participants.forEach(participantDisconnected)); //If person randomally disconnects
    thisroom = room; //Save room to global variable to user later
});


function attachTracks(tracks, container) { //Attach media to own screen

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


function participantDisconnected(participant) { //When someone disconnects, close window
    console.log('Participant "%s" disconnected', participant.identity);
    alert("Other user disconnected video chat");
    window.close();
}

function trackSubscribed(div, track) {//Attach media to screen
    div.append(track.attach());
}

function trackUnsubscribed(track) {//Remove media from screen
    track.detach().forEach(element => element.remove());
}
