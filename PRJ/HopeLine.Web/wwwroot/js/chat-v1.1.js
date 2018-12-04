var userId = $("#userId").val();
var topicsSelected = null;
var onCall = false;
var hasRequestedCall = false;
var currentUser = userId;
var isUser = currentUser.indexOf("Guest") != -1;
var connection = null;
var isConnected = false;
var requestingUser;
var timeout;
var room;
var sendClick = 0;
var mentorTimeOut;
var isLoggedOut = false;
var room = null;
var topicIds = [];
//url = "http://hopeline.azurewebsites.net/";
var mentorMsgReceived = 0;
var isToggleOpen = false;

//comment out before pushing to master
var url = "http://localhost:8000/";

connection = new signalR.HubConnectionBuilder()
  //.withUrl("https://hopelineapi.azurewebsites.net/v2/chatHub")
  .withUrl("http://localhost:5000/v2/chatHub")
  .build();

//ALL FUNCTIONS FOR THIS FILE
// put all functions after this line
function findTime() {
  timeout = setTimeout(function () {
    $("#loadingheader").text("Unable to Find Mentor...");
    $("#imgLoading").hide();
    $("#retryLoading").show();
    $("#cancelLoading").hide();
  }, 20000);
}

function requestCallTime() {
  console.log("requesting call...");
  mentorTimeOut = setTimeout(function () {
    hasRequestedCall = false;
    connection.invoke(
      "SendMessage",
      "HopeLine",
      "Mentor has not answered the call.",
      room
    );
  }, 40000);
}

function alertTime() {
  $("#chatAlert").show();
  console.log("Alert Fired.");
  timeout = setTimeout(function () {
    $("#chatAlert > p").remove();
    $("#chatAlert").hide();
  }, 7000);
}

function registerHub() {
  //when a  call is connected
  connection.on("CallConnected", function () {
    mentorTimeOut = null;
    timeout = null;
    $("#requestedCall").hide();
    $("#callWindow").show();
    window.open(
      url + "VideoChat?roomId=" + room + "&userId=" + userId,
      "HopeLine-Call",
      "_blank",
      "toolbar=0,menubar=0"
    );
  });

  //when a user sent a message
  connection.on("ReceiveMessage", function (user, message) {
    console.log("Receive Message");
    addChatBubble(user, message);
    mentorMsgReceived++;

    $("#message").animate({
        scrollTop: $("#message").prop("scrollHeight")
      },
      0
    );
    $("#chatbox").animate({
        scrollTop: $("#chatbox").prop("scrollHeight")
      },
      0
    );
  });

  //when a room is created
  connection.on("Room", function (roomId) {
    room = roomId;
    connection.invoke("LoadMessage", room);
    $("#ChatWidget").show();
    $("#Logo").hide();
    $("#loading").modal("hide");
    $("#sendArea").removeClass("d-none");
    $("#sendArea").show();
    $("#chatbox").show();
    $("#mentorFound").click();
    $("#toggleChat").removeClass("disabled");
    timeout = null;
  });
  //When Topics are selected
  connection.on("Topics", function (topics) {
    var alltopics = "";
    $("#topics").show();
    if (topics != null) {
      topicsSelected = topics;
      topics.forEach(function (topic) {
        alltopics = alltopics + " " + topic;
      });
      $("#topics").append(alltopics);
      $("#topicsOnLeft").hide();
      $("#topicsOnTop").hide();
    } else {
      $("#topicsOnLeft").show();
      $("#topicsOnTop").show();
      $("#topics").append("No Topic Selected");

    }
    $("")
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
        connection.invoke("RequestToTalk", currentUser).catch(function (err) {
          return console.error(err.toString());
        });
      } else {
        //add to mentor list if is mentor
        connection.invoke("AddMentor", currentUser).catch(function (err) {
          console.log("Unable to add mentor : " + err.toString());
        });
      }

      if (room == null) {
        // load message regardless
        connection.invoke("LoadMessage", room).catch(function (err) {
          return console.error(err.toString());
        });
        connection.invoke("GetTopics", room, isUser).catch(function (err) {
          return console.error(err.toString());
        });
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
      $("#sendArea").show();
      $("#chatbox").show();
      $("#loading").modal("hide");
    } else if (code == 0) {
      $("#Logo").show();
      $("#ChatWidget").hide();
      $("#sendArea").hide();
      $("#openLoading").click();
      $("#retryLoading").hide();
      $("#cancelLoading").show();
      $("#imgLoading").show();
      $("chatbox").hide();
      findTime();
    } else {
      $("chatbox").hide();
      $("sendArea").hide();
      //$("#loading").show();
      findTime();
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
        console.log("Mentor Accepting");
        connection
          .invoke("AcceptUserRequest", userId, user, userConnectionId)
          .catch(function (err) {
            console.log(err.toString());
          });
        $(this)
          .parent()
          .remove();
        event.preventDefault();
      });
    } else {
      connection.invoke("RemoveUser", room, isUser);
      $("#chatAlert").append(
        '<p class="mt-3">User has been disconnected...</p>'
      );
      alertTime();
      setTimeout(function () {
        location.reload();
      }, 7000);
    }
  });
}

//adding each messages
function addChatBubble(user, message) {
  var classId;

  if (user.indexOf("Guest") != -1) {
    name = "Guest";
    classId = "border-success";
  } else if (user.indexOf("@") != -1) {
    name = "Mentor";
    classId = "border-primary";
  } else {
    name = user;
    classId = "border-warning";
  }

  $("#chatbox").append(
    '<div id="message" class="msg mb-2">' +
    '<small class="' +
    classId +
    '">' +
    name +
    "</small>" +
    '<div class="' +
    classId +
    ' text-justify border-left pl-2" style="border-width:8px !important; min-height:40px;  overflow-wrap:break-word;word-wrap: break-word;hyphens: auto;">' +
    message +
    "</div></div>"
  );
}
//!END OF FUNCTIONS

//ALL JQUERY USER INTERACTIONS (ACTIONS)
//Put your code here for all actions from html

//Received messages for mentor chat
setInterval(function () {
  if ($("#toggleChat > span").val() == null) {
    $("#toggleChat").append(
      '<span class="badge badge-light ml-1 mb-1" id="sp">' +
      mentorMsgReceived +
      "</span>"
    );
  } else {
    $("#sp").text(mentorMsgReceived);
    if (isToggleOpen) {
      $("#sp").hide();
    } else {
      $("#sp").show();
    }
  }
}, 100);

$(function () {
  if (userId != null) {
    console.log("UserId = " + userId);
    console.log("pin = " + room);

    connection = new signalR.HubConnectionBuilder()
      //.withUrl("https://hopelineapi.azurewebsites.net/v2/chatHub")
      .withUrl("http://localhost:5000/v2/chatHub")
      .build();
    //register all methods
    registerHub();
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
    //Prevent Spam
    sendClick++;
    console.log("sendClick: " + sendClick);
    if (sendClick >= 3) {
      $("#sendArea").addClass("d-none");
      $("#chatAlert").append(
        "<p>You entered messages too fast, please wait for 5 seconds</p>"
      );
      alertTime();
      setTimeout(function () {
        $("#sendArea").removeClass("d-none");
      }, 5000);
    }
    connection
      .invoke("SendMessage", currentUser, message, room)
      .catch(function (err) {
        return console.error(err.toString());
      })
      .then(function () {
        console.log("Message sent.");
      });

    event.preventDefault();
    $("#messageInput").val(" ");
  }
});

//Prevent Spam
setInterval(function () {
  sendClick = 0;
}, 1000);

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
});

$("#videoCallBtn").click(function () {
  if (!onCall && !hasRequestedCall) {
    connection.invoke(
      "SendMessage",
      "HopeLine",
      "Call has been requested...Waiting for Mentor.",
      room
    );
    connection.invoke("RequestToVideoCall", room);
    requestCallTime();

    $("#chatAlert").append(
      "<p id=msg>You have requested to Talk to Mentor via Call.</p>"
    );
    alertTime();
    hasRequestedCall = true;
  }
});

$("#acceptCall").click(function () {
  connection.invoke("ConnectCall", room);
  onCall = true;
  hasRequestedCall = true;
  $("#chatAlert").append("<p>Mentor has accepted the Call.</p>");
  alertTime();
  connection.invoke("SendMessage", "HopeLine", "Call Connected.", room);
});

$("#toggleChat").click(function () {
  if (isToggleOpen == false) {
    isToggleOpen = true;
  } else {
    isToggleOpen = false;
  }
  $("#message").animate({
      scrollTop: $("#message").prop("scrollHeight")
    },
    0
  );
  $("#chatbox").animate({
      scrollTop: $("#chatbox").prop("scrollHeight")
    },
    0
  );
  mentorMsgReceived = 0;
});

$("#messageInput").click(function () {
  $("#message").animate({
      scrollTop: $("#message").prop("scrollHeight")
    },
    0
  );
  $("#chatbox").animate({
      scrollTop: $("#chatbox").prop("scrollHeight")
    },
    0
  );
});

$("input[type=checkbox]").on("click", function () {
  if ($("input:checked").val() !== null) {
    console.log("Selected: " + $("input:checked").val());
    topicIds.push(parseInt($("input:checked").val()));
  }
});

$("#updateTopics").click(function () {
  if (topicIds.length > 0) {
    connection.invoke("AddTopics", room, topicIds);
    $("#topicsOnLeft").hide();
    $("#topicsOnTop").hide();
  }
});

$("#loading").on("hidden.bs.modal", function (e) {
  if (room == null) {
    $("#chatAlert").append("<p>Redirecting to Home Page..</p>");
    alertTime();
    setTimeout(function () {
      window.location.replace(url);
    }, 3000);
  }
});

//Rate Mnetor
// window.onbeforeunload = function (event) {
//   var rate = setInterval(function () {
//     $("#ratemodal").modal("toggle");
//   }, 100);
//   $("#happy").click(function () {
//     clearInterval(rate);
//   });
// };