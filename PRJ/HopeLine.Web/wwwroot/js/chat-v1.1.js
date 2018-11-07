var userId = $("#userId") != null ? $("#userId").val() : null;
var isconnected = false;
var currentuser = userId;
var connection;
var isconnected = false;
var requestingUser;
var isUser = currentuser.indexOf("Guest") != -1;
var timeout;

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
            '<div id="message" class="col-11 mb-3">' +
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
            '<div id="message" class="col-11 mb-3">' +
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

    connection.on("NotifyMentor", function (user, userConnectionId) {
        console.log("User Request Id :" + user);
        if (user.indexOf("Guest") != -1) {
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
            })
        }
    });
    connection.on("NotifyUser", function (message) {
        if (message != "Finding Available Mentors...") {
            $("#sendArea").removeClass("d-none");
            $("#loading").hide();
            found();
        } else {
            findTime();
            $("#sendArea").addClass('d-none');
        }
    });

    connection.onclose(function () {
        connection.invoke("SendMessage", currentuser, "DISCONNECTED", room);
    });

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
});
//R
//End of Registrations
//Start the connection



$("#sendButton").click(function (event) {
    var message = $("#messageInput")
        .val()
        .trim();
    if (message != "") {
        console.log("Id :" + room);
        console.log("user: " + userId);
        console.log("message: " + message);
        if (message != null) {
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
    }
});