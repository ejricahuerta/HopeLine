//https://media.twiliocdn.com/sdk/js/video/releases/2.0.0-beta2/docs/

//FIXME : refactor have video on mentor page and instant chat

const Video = Twilio.Video;
var activeRoom;
var previewTracks;
var identity;
var roomName;
var room;
var localStream;
var option;
var localMediaConstraints


room = $("#roomId").val();
identity = $("#userId").val();
// if identity is "guest" then 
//set localMediaConstraints to video false
localMediaConstraints = {
    audio: true,
    video: true
}

//creating local media to pass to twilio
Video.createLocalVideoTrack(localMediaConstraints)
    .then(track => {
        option = {
            room: room,
            logLevel: 'debug',
            tracks: Array.from(track)
        }

        const local = $("#local-media");
        local.append(track.attach());
        return Video.connect($("#token").val(), option);
    })
    .then(room => {
        console.log('Connected to Room "%s"', room.name);

        room.participants.forEach(participantConnected);
        room.on('participantConnected', participantConnected);

        room.on('participantDisconnected', participantDisconnected);
        room.once('disconnected', error => room.participants.forEach(participantDisconnected));
    });


function participantConnected(participant) {
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
}

function trackSubscribed(div, track) {
    div.append(track.attach());
}

function trackUnsubscribed(track) {
    track.detach().forEach(element => element.remove());
}