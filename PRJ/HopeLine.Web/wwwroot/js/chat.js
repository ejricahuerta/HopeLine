"use strict";

var userId = $("#userId") != null ? $("#userId").val() : null;

if (userId != null) {
    console.log("UserId = " + userId);
    var currentuser = userId;
    var connection;
    var isconnected = false;

    var room = $('#pin').val();
    console.log("pin = " + room);
    connection = new signalR.HubConnectionBuilder()
        //.withUrl("https://hopelineapi.azurewebsites.net/chatHub")
        .withUrl("http://localhost:5000/chatHub")
        .build();

    //Register all Method here.
    connection.on("ReceiveMessage", function (user, message) {
        console.log("Receive Message");
        var classId = (userId == user) ? 'bg-secondary' : 'bg-info';
        var userClass = (userId == user)? 'text-right' : null;
        $('#chatbox').append('<div id="message" class="col-11 mb-5">' +
            '<h5 class= ' + userClass + '>' + userId + '</h5>' +
            '<p class="bg-primary ' + classId +' text-justify rounded p-2" style="min-height:50px;">' +
            message  +
            '</p></div>');
    });
    connection.on("Load", function (user, message) {
        currentuser = $('#userInput').val();
        var classId = (currentuser == user) ? 'bg-primary' : 'bg-light';

        $('#chatbox').append('<div id="message" class="col-11 mb-5">' +
            '<h5>' + userId + '</h5>' +
            '<p class="bg-primary ' + classId + ' text-justify rounded p-2" style="min-height:50px;">' +
            message +
            '</p></div>');
    });

    //End of Registrations
    //Start the connection
    connection.start({
        withCredentials: false
    })
        .catch(function (err) {
            return console.error(err.toString());
        });

    connection
        .invoke("LoadMessage", room)
        .catch(function (err) {
            return console.error(err.toString());
        });
}
 $('#sendButton').click(function (event) {
     if (!isconnected) {
         connection
             .invoke("AddUserToRoom", room)
             .catch(function (err) {
                 return console.error(err.toString());

             });

         console.log("Added");
         isconnected = true;

     }
     var message = $('#messageInput').val().trim();
     console.log('Id :' + room);

     console.log('user: ' + userId);
     console.log('message: ' + message);
      if (message != null) {
            console.log("Sending Message");
            console.log("room " + room);
            connection.invoke("SendMessage", userId, message, room)
                .catch(function (err) {
                    return console.error(err.toString());
                });

            event.preventDefault();
            $('#messageInput').val(' ');
        }
    });