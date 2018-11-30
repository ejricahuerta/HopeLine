const Video = Twilio.Video;
var activeRoom;
var previewTracks;
var identity;
var roomName;
var room;

var option = {
    name: $("#roomId").val(),
    logLevel: 'debug'
    //include usermediastream here and present it in preview
    //tracks: stream
}

Video.connect($("#token").val(), option).then(room => {
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