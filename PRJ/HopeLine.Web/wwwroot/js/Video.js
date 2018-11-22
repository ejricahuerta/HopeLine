
//Twilio Connection API
const video = Twilio.Video;

let token = $("#token").val();

console.log("token is: " + token);
video.connect(token, { name: 'room-name' }).then(room => {
    console.log('Connected to Room ' + room.name + " token is: " + token);

    room.participants.forEach(participantConnected);
    room.on('participantConnected', participantConnected);

    room.on('participantDisconnected', participantDisconnected);
    room.once('disconnected', error => room.participants.forEach(participantDisconnected));
});
