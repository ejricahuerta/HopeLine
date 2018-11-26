//Twilio Connection API
var videoTwilio = Twilio.Video;
console.log("In video.js");
$(document).ready(function () {

    // Secret RARbIc5RQS63xThPOKxaQTX088PPd2Pl
    //SID SKf1fd0ea9a3e3d530efd1c76f53c3db08
    var roomId = $("#roomId").val();
    //var token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiIsImN0eSI6InR3aWxpby1mcGE7dj0xIn0.eyJqdGkiOiJTS2NiMzgzOWNhNmIyYjRlOTE2ZTg3Yjk1MzlhZjk5NTg2LTE1NDMxNzQ5ODgiLCJpc3MiOiJTS2NiMzgzOWNhNmIyYjRlOTE2ZTg3Yjk1MzlhZjk5NTg2Iiwic3ViIjoiQUM5YTE0M2UwOTdiYzg2ZDQ1OTI1MWRhODcxYzZmMWJjNSIsImV4cCI6MTU0MzE3ODU4OCwiZ3JhbnRzIjp7ImlkZW50aXR5IjoidGVzdCIsInZpZGVvIjp7InJvb20iOiJyb29tIn19fQ.lLg85aN8_mYHRXNcpzwLykj_uJc_SiMHf9_rm - 461V4";
    var token = "c7c3b6b84441e3a656f88fb3a7ad80bb";
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


});