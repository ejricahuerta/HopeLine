"use strict";


var userId = $("#userId") != null ? $("#userId").val() : null;

if (userId != null) {
    console.log("User Id = " + userId);
    var currentuser = "";
    var connection;
    var isconnected = false;


    var room = $('#pin').val();
    connection = new signalR.HubConnectionBuilder()
        .withUrl("https://hopelineapi.azurewebsites.net/chatHub")
        .build();

    //Register all Method here.
    connection.on("ReceiveMessage", function (user, message) {
        var classId = (currentuser == user) ? 'bg-secondary' : 'bg-info';

        $('#chatbox').append('<div id="message"><span class= "badge">' + user + '</span>' +
            ' <div class="' + classId + ' col-11 mb-1 rounded"> <p class="p-2">' +
            message +
            '</p></div></div>');
    });

    connection.on("Load", function (user, message) {
        currentuser = $('#userInput').val();
        var classId = (currentuser == user) ? 'bg-secondary' : 'bg-info';

        $('#chatbox').append('<div id="message"><span class= "badge">' + user + '</span>' +
            ' <div class="' + classId + ' col-11 mb-1 rounded"> <p class="p-2">' +
            message +
            '</p></div></div>');
    });

    connection.onclose("ReceiveMessage", function (user, message) {
        var classId = (currentuser == user) ? 'bg-secondary' : 'bg-info';

        $('#chatbox').append('<div id="message"><span class= "badge">' + user + '</span>' +
            ' <div class="' + classId + ' col-11 mb-1 rounded"> <p class="p-2">' +
            message +
            '</p></div></div>');

    });

    //End of Registrations


    //Start the connection
    connection.start({
            withCredentials: false
        })
        .catch(function (err) {
            return console.error(err.toString());
        })

    $('#connectButton').click(function () {
        if (!isconnected) {
            connection.invoke("AddUserToRoom", room)
                .catch(function (err) {
                    return console.error(err.toString());

                });
            isconnected = true;
            console.log('Id :' + room);
            connection.invoke("LoadMessage", room)
                .catch(function (err) {
                    return console.error(err.toString());

                });
        }

    });

    // $("#loginbutton").click(function () {
    //     connection.invoke("AddMentor")
    //         .catch(function (err) {
    //             console.log("Unable to add Mentor to list: "  + err.toString());
    //         });
    // });

    $('#sendButton').click(function (event) {

        currentuser = $('#userInput').val()
        var message = $('#messageInput').val().trim();

        console.log('user: ' + currentuser);
        console.log('message: ' + message);

        if (message && isconnected) {

            connection.invoke("SendMessage", currentuser, message, room)
                .catch(function (err) {
                    return console.error(err.toString());
                });

            event.preventDefault();
            $('#messageInput').val(' ');
        }
    });


}