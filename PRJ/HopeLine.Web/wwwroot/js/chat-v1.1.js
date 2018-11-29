var userId = ($("#userId") != null) ? $("#userId").val() : null;
var accountType = $("#accountType") != null ? $("#accountType").val() : null;
var userId = $("#userId") != null ? $("#userId").val() : null;
var onCall = false;
var hasRequestedCall = false;
var currentUser = userId;
var isUser = currentUser.indexOf("Guest") != -1;
var connection = null;
var isConnected = false;
var requestingUser;
var timeout;
var mentorTimeOut;
var isLoggedOut = false;
var room = null;
var url = "http://hopeline.azurewebsites.net/";
//comment out before pushing to master
//var url = "http://localhost:8000/";

connection = new signalR.HubConnectionBuilder()
    .withUrl("https://hopelineapi.azurewebsites.net/v2/chatHub")
    //.withUrl("http://localhost:5000/v2/chatHub")
    .build();

//ALL FUNCTIONS FOR THIS FILE
// put all functions after this line
function findTime() {
    timeout = setTimeout(function () {
        $("#loading").text("Unable to Find Mentor...");
        $("#loading").append('<a href="' + url + '/instantChat" class="btn btn-info">Retry</a>');

    }, 20000);
}

function requestCallTime() {
    console.log("requesting call...");
    timeout = setTimeout(function () {
        hasRequestedCall = false;
        connection.invoke("SendMessage", "system", "Mentor has not answered the call.", room);
    }, 40000);
}

function registerHub() {
    //when a  call is connected
    connection.on("CallConnected", function () {
        $("#requestedCall").hide();
        $("#incomingCall").hide();
        window.open(url + "VideoChat?roomId=" + room + "&userId=" + userId, "HopeLine-Call",
            '_blank', 'toolbar=0,menubar=0');
        timeout = null;
    });

    //when a user sent a message
    connection.on("ReceiveMessage", function (user, message) {
        console.log("Receive Message");
        addChatBubble(user, message);
        $("#message").animate({
            scrollTop: $('#message').prop("scrollHeight")
        }, 0);
        $("#chatbox").animate({
            scrollTop: $('#chatbox').prop("scrollHeight")
        }, 0);
    });

    //when a room is created
    connection.on("Room", function (roomId) {
        room = roomId;
        connection.invoke("LoadMessage", room);
        $("#sendArea").removeClass('d-none');
        $("#requestChat").hide();
        $("#sendArea").show();
        $("#chatbox").show();
        $("#loading").hide();
        $("#mentorFound").click();
        $("#toggleChat").removeClass("disabled");
        timeout = null;
    });

    //register for users
    if (!isUser) {
        //notify mentors 
        notifyMentor();
        //notify mentor for incoming call
        connection.on("CallMentor", function () {
            console.log("Notifying");
            $("#incomingCall").show();
            $("#requestedCall").show();
            mentorTimeOut = setTimeout(function () {
                $("#incomingCall").hide();
            }, 40000);
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
                console.log("isUser " + isUser);
                //request to start connection
                connection.invoke("RequestToTalk", currentUser)
                    .catch(function (err) {
                        return console.error(err.toString());
                    });
            } else {
                //add to mentor list if is mentor
                connection.invoke("AddMentor", currentUser)
                    .catch(function (err) {
                        console.log("Unable to add mentor : " + err.toString());
                    });
            }
            // load message regardless 
            connection.invoke("LoadMessage", room).catch(function (err) {
                return console.error(err.toString());
            });
            if (room == null) {
                $("#chat").removeClass("show");
                $("#toggleChat").addClass("disabled");
            } else {
                $("#chat").addClass("show");
                $("#toggleChat").removeClass("disabled");
            }
        });
}

//notifying user func
function notifyUser() {
    connection.on("NotifyUser", function (code) {
        //if positive then remove loading and pop the send area
        console.log("code:  " + code);
        if (code == 1) {
            isLoggedOut = false;
            $("#loading").hide();
            $("#requestChat").hide();
            $("#sendArea").show();
            $("#chatbox").show();
            //if 0 then keep notify the mentor
        } else if (code == 0) {
            if (isLoggedOut) {
                connection.close();
            } else {
                $("#sendArea").hide();
                $("#openLoading").click();
                $("#requestChat").show();
                $("chatbox").hide();
                findTime();
            }
            // else  chat is disconnected
        } else {
            $("chatbox").hide();
            $("sendArea").hide();
            $("#requestChat").show();
            //$("#loading").show();
            findTime()
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
            $("#chatbox").append('<div class = "alert alert-info" role = "alert" >' +
                'User has DISCONNECTED!' +
                '</div>'
            )
            connection.invoke("RemoveUser", room, isUser);
        }
    });
}

//adding each messages
function addChatBubble(user, message) {
    var classId = currentUser == user ? "border-primary" : "border-success";
    classId = user == "system" ? "border-warning" : classId;
    if (isUser) {
        if (user.indexOf("Guest") != -1) {
            name = "Guest";
        } else if (user.indexOf("@") != -1) {
            name = "Mentor";
        } else {
            name = user;
        }
    } else {
        name = user;
    }

    $("#chatbox").append(
        '<div id="message" class="msg mb-2">' +
        '<small class="' +
        classId + '">' +
        name +
        '</small>' +
        '<div class="' +
        classId +
        ' text-justify border-left pl-2" style="border-width:8px !important; min-height:40px;">' +
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
        registerHub();
        //start connection
        startConnection();
    }
});

//When user send a message
$("#sendButton").click(function (event) {
    if (room != null) {

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
                .invoke("SendMessage", currentUser, message, room)
                .catch(function (err) {
                    return console.error(err.toString());
                }).then(function () {
                    console.log("Message sent.")
                });

            event.preventDefault();
            $("#messageInput").val(" ");
        }
    }
});

$("#logout").click(function () {
    isLoggedOut = true;
    if (room != null) {
        connection.invoke("RemoveUser", userId, room, isUser);
    } else {
        connection.close();
    }
});

$("#endConversation").click(function () {
    isLoggedOut = true;
    $("#logout").click();
    //Rate
})

$("#videoCallBtn").click(function () {
    if (!onCall && !hasRequestedCall) {
        connection.invoke("SendMessage", "system", "Call has been requested...Waiting for Mentor.", room);
        connection.invoke("RequestToVideoCall", room);
        requestCallTime();
        hasRequestedCall = true;
    }
});

$("#acceptCall").click(function () {
    connection.invoke("ConnectCall", room);
    onCall = true;
    hasRequestedCall = true;
    mentorTimeOut = null;
});

$("#toggleChat").click(function () {
    $("#message").animate({
        scrollTop: $('#message').prop("scrollHeight")
    }, 0);
    $("#chatbox").animate({
        scrollTop: $('#chatbox').prop("scrollHeight")
    }, 0);
});

$("#messageInput").click(function () {
    $("#message").animate({
        scrollTop: $('#message').prop("scrollHeight")
    }, 0);
    $("#chatbox").animate({
        scrollTop: $('#chatbox').prop("scrollHeight")
    }, 0);
});