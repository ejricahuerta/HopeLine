
var userId = ($("#userId") != null) ? $("#userId").val() : null;
var accountType = $("#accountType") != null ? $("#accountType").val() : null;
var userId = $("#userId") != null ? $("#userId").val() : null;

var isconnected = false;
var currentuser = userId;
var connection;
var isconnected = false;
var requestingUser;
var isUser = currentuser.indexOf("Guest") != -1;
var timeout;
var room

function findTime() {
    timeout = setTimeout(function () {
        $("#loading").text("Unable to Find Mentor...");
        $("#chatbox").append('<a href="http://localhost:8000/instantChat" class="btn btn-info">Retry</a>');
    }, 20000);
}

function found() {
    clearTimeout(timeout);
}


console.log("UserId = " + userId);
var room = $("#pin").val();
console.log("pin = " + room);

$(function () {
    connection = new signalR.HubConnectionBuilder()
        //.withUrl("https://hopelineapi.azurewebsites.net/chatHub")
        .withUrl("http://localhost:5000/v2/chatHub")
        .build();

    connection.on("ReceiveMessage", function (user, message) {
        console.log("Receive Message");
        var classId = currentuser == user ? "bg-secondary " : "bg-warning";
        var userClass = currentuser == user ? "float-right" : "float-left";
        $("#chatbox").append(
            '<div id="message" class=" msg col-11 mb-3 bg-light">' +
            // '<h5 class="' +
            // userClass +
            // '"><small>' +
            // user +
            // "</small></h5>" +
            '<div class="col-8 ' +
            userClass +
            " " +
            classId +
            ' text-justify rounded p-2" style="min-height:50px;">' +
            message +
            "</div></div>"
        );
    });

    connection.on("Load", function (user, message) {
        var classId = currentuser == user ? "bg-secondary " : "bg-warning";
        var userClass = currentuser == user ? " float-right" : "float-left";
        $("#chatbox").append(
            '<div id="message" class=" msg col-11 mb-3">' +
            // '<h5 class="' +
            // userClass +
            // '"><small>' +
            // user +
            // "</small></h5>" +
            '<div class="col-8 ' +
            userClass +
            " " +
            classId +
            ' text-justify rounded p-2" style="min-height:50px;">' +
            message +
            "</div></div>"
        );
    });
    connection.on("Room", function (roomId) {
        room = roomId;
        $("#sendArea").removeClass('d-none');
        connection.invoke("LoadMessage", room);
    });

    connection.onclose(function (e) {
        connection.invoke("DeleteFromRoom", room);
    });
    if (!isUser) {
        connection.on("NotifyMentor", function (user, userConnectionId, code) {
            if (code == null) {

                console.log("User Request Id :" + user);
                $("#incominguser").append(
                    '<div class="alert alert-info " role=" alert ">' +
                    user +
                    ' is looking for company!<input id="mentorAccept" type="button" class="btn btn-link" value="Accept?"/></div>'
                );
                $("#mentorAccept").on("click", function (event) {
                    console.log("Mentor Accepting")
                    connection.invoke("AcceptUserRequest", userId, user, userConnectionId)
                        .catch(function (err) {
                            console.log(err.toString());
                        });
                    $(this).parent().remove();
                    event.preventDefault();
                    window.location.replace("https://hopeline.azurewebsites.net/chat");
                });
            } else {
                $("#chatbox").append('<div class = "alert alert-primary" role = "alert" >' +
                    'User has DISCONNECTED!' +
                    '</div>'
                )
                connection.invoke("RemoveUser", room, isUser);
            }
        });
    } else {
        connection.on("NotifyUser", function (code) {
            if (code != 1) {
                $("#sendArea").removeClass("d-none");
                $("#loading").hide();
                found();
            } else if (code == 0) {
                findTime();
                $("#sendArea").addClass('d-none');
            } else {
                $("msg").remove();
            }
        });
    }
}

function startConnection() {
    connection
        .start({
            withCredentials: false
        })
        .catch(function (err) {
            return console.error(err.toString());
        })
        .then(function () {
            if (isUser) {
                console.log("isuser " + isUser);
                connection
                    .invoke("RequestToTalk", currentuser)
                    .catch(function (err) {
                        return console.error(err.toString());
                    });
            } else {

                connection.invoke("AddMentor", currentuser)
                    .catch(function (err) {
                        console.log("Unable to add mentor : " + err.toString());
                    });
            }

            connection.invoke("LoadMessage", room).catch(function (err) {
                return console.error(err.toString());
            });
        });
}

console.log("UserId = " + userId);

console.log("pin = " + room);

$(function () {
    if (userId != null) {

        connection = new signalR.HubConnectionBuilder()
            .withUrl("https://hopelineapi.azurewebsites.net/v2/chatHub")
            //.withUrl("http://localhost:5000/v2/chatHub")
            .build();

        registerhub();
        startConnection();
    }

});

$("#sendButton").click(function (event) {
    var message = $("#messageInput")
        .val()
        .trim();
    if (message != "") {
        console.log("Id :" + room);
        console.log("user: " + userId);
        console.log("message: " + message);

        console.log("Sending Message");
        console.log("room " + room);
        connection
            .invoke("SendMessage", currentuser, message, room)
            .catch(function (err) {
                return console.error(err.toString());
            });

        event.preventDefault();
        $("#messageInput").val(" ");
    }
});

$("#logout").click(function () {
    connection.invoke("RemoveUser", userId, room, isUser);
});
