"use strict";
var currentuser = "";
var connection;
var isconnected = false;

var room = $('#pin').val();
connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:5000/chatHub")
    .build();

connection.start().catch(function (err) {
    return console.error(err.toString());


});

connection.on("Receivemessage", function (user, message) {


     var classId = (currentuser == user) ? 'bg-secondary' : 'bg-info';

    $('#chatbox').append('<div id="message"><span class= "badge">'+ user+'</span>' +
        ' <div class="' + classId + ' col-11 mb-1 rounded"> <p class="p-2">' +
        message +
        '</p></div></div>');

});


$('#userInput').click(function () {

    if (!isconnected) {
        connection.invoke("AddUserToRoom", room)
            .catch(function (err) {
                return console.error(err.toString());

            });
        isconnected = true;
    }

});


$('#sendButton').click(function () {

    currentuser = $('#userInput').val()
    var message = $('#messageInput').val();
    $('#messageInput').val(' ');

    connection.invoke("SendMessage", currentuser, message, room)
        .catch(function (err) {
            return console.error(err.toString());
        });

    event.preventDefault();
});