"use strict";
var currentuser = "";

var connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:5000/chatHub")
        .build();

connection.on("ReceiveMessage", function (user, message) {

    var classId = (currentuser != user) ? 'bg-success' : 'bg-secondary';
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    $('#chatbox').append('<div id="message"><label>' + user + '</label>' +
        ' <div class="' + classId + ' col-11 mb-1 rounded"> <p class="p-2">' +
        msg + 
      '</p></div></div>');
    
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton")
        .addEventListener("click", function (event) {
            currentuser = document.getElementById("userInput").value;
            var message = document.getElementById("messageInput").value;
            document.getElementById("messageInput").value = "";
            connection.invoke("SendMessage", currentuser, message).catch(function (err) {
                return console.error(err.toString());
    });
    event.preventDefault();
});