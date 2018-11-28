//Twilio Connection API
const videoTwilio = Twilio.Video;
console.log("In video.js");
$(document).ready(function () {

    // Secret RARbIc5RQS63xThPOKxaQTX088PPd2Pl
    //SID SKf1fd0ea9a3e3d530efd1c76f53c3db08
    var roomId = $("#roomId").val();
    //var token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiIsImN0eSI6InR3aWxpby1mcGE7dj0xIn0.eyJqdGkiOiJTS2NiMzgzOWNhNmIyYjRlOTE2ZTg3Yjk1MzlhZjk5NTg2LTE1NDMxNzQ5ODgiLCJpc3MiOiJTS2NiMzgzOWNhNmIyYjRlOTE2ZTg3Yjk1MzlhZjk5NTg2Iiwic3ViIjoiQUM5YTE0M2UwOTdiYzg2ZDQ1OTI1MWRhODcxYzZmMWJjNSIsImV4cCI6MTU0MzE3ODU4OCwiZ3JhbnRzIjp7ImlkZW50aXR5IjoidGVzdCIsInZpZGVvIjp7InJvb20iOiJyb29tIn19fQ.lLg85aN8_mYHRXNcpzwLykj_uJc_SiMHf9_rm - 461V4";
    var token = $("#token").val();
    console.log("token is: " + token);

    // Sending data
    //createLocalTracks({
    //    audio: true,
    //    video: true
    //}).then(localTrack => {
    //        return connect(token, {
    //            name: roomId,
    //            tracks: localTrack
    //        });
    //    }).then(room => {
    //        console.log('Connected to Room: ' + roomId);
    //    });
    

    //const { connect } = videoTwilio.require('twilio-video');

    //videoTwilio.connect(token, { name: roomId }).then(room => {
    //    console.log(`Successfully joined a Room: ${room}`);
    //    room.on('participantConnected', participant => {
    //        console.log(`A remote Participant connected: ${participant}`);
    //    });
    //}, error => {
    //    console.error(`Unable to connect to Room: ${error.message}`);
    //});

    videoTwilio.connect(roomId, { loglevel: 'debug' });
    videoTwilio.connect(token, {
        name: roomId
    }).then(room => {
        console.log('Connected to Room ' + roomId + " token is: " + token);
        
        room.participants.forEach(participantConnected);
        room.on('participantConnected', participantConnected => {
            console.log("Remote participant is: " + participantConnected);
        });

        room.on('participantDisconnected', participantDisconnected => {
            console.log("participant disconnected");
        });
        room.once('disconnected', error => room.participants.forEach(participantDisconnected));
        });

    //// Log your Client's LocalParticipant in the Room
    //const localParticipant = room.localParticipant;
    //console.log(`Connected to the Room as LocalParticipant "${localParticipant.identity}"`);

    //// Log any Participants already connected to the Room
    //room.participants.forEach(participant => {
    //    console.log(`Participant "${participant.identity}" is connected to the Room`);
    //});

    //// Log new Participants as they connect to the Room
    //room.once('participantConnected', participant => {
    //    console.log(`Participant "${participant.identity}" has connected to the Room`);
    //});

    //// Log Participants as they disconnect from the Room
    //room.once('participantDisconnected', participant => {
    //    console.log(`Participant "${participant.identity}" has disconnected from the Room`);
    //});

    function participantConnected(participant) {
        console.log('Participant "%s" connected', participant.identity);
        
        //const div = $("#participant-video");

        const div = document.createElement('div');
        div.id = participant.sid;
        div.innerText = participant.identity + "TESTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT";
        console.log("In participant function");
        participant.on('trackSubscribed', track => trackSubscribed(div, track));
        participant.on('trackUnsubscribed', trackUnsubscribed);

        participant.tracks.forEach(publication => {
            if (publication.isSubscribed) {
                trackSubscribed(div, publication.track);
            }
        });

        document.body.appendChild(div);
    }

    function participantDisconnected(participant) {
        console.log('Participant "%s" disconnected', participant.identity);
        document.getElementById(participant.sid).remove();
    }

    function trackSubscribed(div, track) {
        div.appendChild(track.attach());
    }

    function trackUnsubscribed(track) {
        track.detach().forEach(element => element.remove());
    }
    
    videoTwilio.connect(token, {
        audio: true,
        name: roomId,
        video: { width: 640 }
    }).then(room => {
        console.log(`Connected to Room: ` + roomId);
        });
    
    //room.on('participantConnected', participant => {
    //    console.log(`Participant "${participant.identity}" connected`);

    //    participant.tracks.forEach(publication => {
    //        if (publication.isSubscribed) {
    //            const track = publication.track;
    //            document.getElementById('remote-media-div').appendChild(track.attach());
    //        }
    //    });

    //    participant.on('trackSubscribed', track => {
    //        document.getElementById('remote-media-div').appendChild(track.attach());
    //    });
    //});
});
