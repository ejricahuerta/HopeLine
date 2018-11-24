var userId = ($("#userId") != null) ? $("#userId").val() : null;
var accountType = $("#accountType") != null ? $("#accountType").val() : null;
var userId = $("#userId") != null ? $("#userId").val() : null;

var isconnected = false;
var onCall = false;
var currentuser = userId;
var isUser = currentuser.indexOf("Guest") != -1;
var connection;
var isconnected = false;
var requestingUser;
var timeout;
var room;

var url = "http://hopeline.azurewebsites.net/";
//comment out before pushing to master
//var url = "http://localhost:8000/";

connection = new signalR.HubConnectionBuilder()
    .withUrl("https://hopelineapi.azurewebsites.net/v2/chatHub")
    // .withUrl("http://localhost:5000/v2/chatHub")
    .build();

//ALL FUNCTIONS FOR THIS FILE
// put all functions after this line
function findTime() {
    timeout = setTimeout(function () {
        $("#loading").text("Unable to Find Mentor...");
        $("#loading").append('<a href="http://hopeline.azurewebsites.net/instantChat" class="btn btn-info">Retry</a>');
    }, 20000);
}

function registerhub() {

    //when a  call is connected
    connection.on("CallConnected", function () {
        $("#incomingCall").hide();
        window.open(url + "VideoChat?roomId=" + room, "HopeLine-Call",
            '_blank', 'toolbar=0,menubar=0');
    });

    //FIXME: redundant as load
    //when a user sent a message
    connection.on("ReceiveMessage", function (user, message) {
        console.log("Receive Message");
        addChatBubble(user, message);
        $("#message").animate({
            scrollTop: $('#message').prop("scrollHeight")
        }, "slow");
    });

    //FIXME: redundant as receivemessage
    //when a user refresh the page
    connection.on("Load", function (user, message) {
        console.log("Loading Message");
        addChatBubble(user, message);
        $("#message").animate({
            scrollTop: $('#message').prop("scrollHeight")
        }, "slow");
    //scrollToBottom();
});

    //when a room is created
    connection.on("Room", function (roomId) {
        room = roomId;
        console.log("Room: " + room);
        $("#sendArea").removeClass('d-none');
        connection.invoke("LoadMessage", room);
        $("#sendArea").removeClass("d-none");
        $("#loading").hide();
        timeout = null;
    });

    //register for users
    if (!isUser) {
        //notify mentors 
        notifyMentor();
        //notify mentor for incoming call
        connection.on("CallMentor", function () {
            console.log("Notifying");
            $("#incomingCall").toggle();
        });
    } else {
        //notify user
        notifyUser();
    }
}

// starts the connection
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
                //request to start connection
                connection.invoke("RequestToTalk", currentuser)
                    .catch(function (err) {
                        return console.error(err.toString());
                    });
            } else {
                //add to mentor list if is mentor
                connection.invoke("AddMentor", currentuser)
                    .catch(function (err) {
                        console.log("Unable to add mentor : " + err.toString());
                    });
            }
            // load message regardless 
            connection.invoke("LoadMessage", room).catch(function (err) {
                return console.error(err.toString());
            });
        });
}


//notifying user func
function notifyUser() {
    connection.on("NotifyUser", function (code) {
        //if positive then remove loading and pop the send area
        if (code == 1) {
            $("#sendArea").removeClass("d-none");
            console.log("code: " + code);
            $("#loading").hide();
            //if 0 then keep notify the mentor
        } else if (code == 0) {
            $("#sendArea").addClass('d-none');
            findTime();
            // else  chat is disconnected
        } else {
            $("#sendArea").addClass('d-none');
            $("#loading").show();
            findTime();
            // $("#message").remove();
            //FIXME: add a message or modal to notify the user's disconnection
            $("#modaltrigger").click();
        }
    });
}

//notifying mentors func
function notifyMentor() {
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
            });

        } else {
            //FIXME: refactor this
            $("#chatbox").append('<div class = "alert alert-primary" role = "alert" >' +
                'User has DISCONNECTED!' +
                '</div>'
            )
            connection.invoke("RemoveUser", room, isUser);
        }
    });
}


//adding each messages
function addChatBubble(user, message) {
    var classId = currentuser == user ? "border-primary" : "border-success";
    $("#chatbox").append(
        '<br/>' +
        '<div id="message" class="msg mb-3">' +
        '<small class="' +
        classId + '">' +
        user +
        '</small>' +
        '<div class="' +
        classId +
        ' text-justify border-left p-2" style="border-width:8px !important; min-height:50px;">' +
        message +
        "</div></div>"
    );
}

//!END OF FUNCTIONS



//ALL JQUERY USER INTERACTIONS (ACTIONS)
//Put your code here for all actions from html

$(function () {
    if (userId != null) {
        console.log("UserId = " + userId);
        console.log("pin = " + room);

        connection = new signalR.HubConnectionBuilder()
            .withUrl("https://hopelineapi.azurewebsites.net/v2/chatHub")
            // .withUrl("http://localhost:5000/v2/chatHub")
            .build();
        //register all methods
        registerhub();
        //start connection
        startConnection();
    }

});
//When user send a message
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
            }).then(function () {
                console.log("Message sent.")
            });

        event.preventDefault();
        $("#messageInput").val(" ");
    }
});

$("#logout").click(function () {
    connection.invoke("RemoveUser", userId, room, isUser);
});

$("#videoCallBtn").click(function () {
    if (!onCall) {
        console.log("video btn clicked");
        console.log(room);
        connection.invoke("RequestToVideoCall", room);
        console.log("after modal show");
    }
});

$("#acceptCall").click(function () {
    connection.invoke("ConnectCall", room);
});

 

//!END OF ALL ACTIONS


//FIXME : Refactor this and place it to its proper sections
// Automatically scroll down
// $("#message").scroll(function (m) {
//     if ($(this).is(':animated')) {
//         stopAutoScroll();
//     }
// // });
// const messages = document.getElementById('message');

// function getMessage() {
//     shouldScroll = messages.scrollTop + messages.clientHeight === messages.scrollHeight;
//     if (!shouldScroll) {
//         scrollToBottom();
//     }
// }

// function scrollToBottom() {
//     messages.scrollTop = messages.scrollHeight;
// }

// var i = setInterval(getMessage, 700);

// function stopAutoScroll() {
//     clearInterval(i);
//     console.log("CLEARED");
// }
    