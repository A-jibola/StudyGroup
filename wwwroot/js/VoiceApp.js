var OV;
var session;
window.elem;
window.sesh

function joinSession() {
    var groupId = document.getElementById("groupName").textContent;
    var sessionId = "VoiceSession" + groupId;
    window.sesh = sessionId;
    var UsersName = document.getElementById("usersname").textContent;

    //alert(sessionId);
    OV = new OpenVidu();
    session = OV.initSession();

    session.on("streamCreated", function (event) {
        var subscriber = session.subscribe(event.stream, "General");

        subscriber.on("videoElementCreated", event => {
            showUserData(event.element, subscriber.stream.connection);
            var elementId = event.element.id;
            var element = document.getElementById(elementId);
            if (subscriber.stream.audioActive == false) {
                MutedDisplay(element, false);
            };
        });

        subscriber.on("streamPropertyChanged", function (event) {
            var elementId = event.target.id;
            window.elem = elementId;
            var element = document.getElementById(elementId);
            if (event.changedProperty == "audioActive" && event.newValue== false && event.oldValue== true){
                MutedDisplay(element);
            }
            if (event.changedProperty == "audioActive" && event.newValue == true && event.oldValue == false) {
                UnMutedDisplay(element);
            }

        })
    });

    session.on("streamDestroyed", function (event) {
        removeUserData(event.stream.connection);
        if (window.elem) {
            var element = document.getElementById(window.elem)
            UnMutedDisplay(element);
        }
       
    })

    session.on("sessionDisconnected", event => {
        alert("The call has been Ended");
        var button = document.getElementById("Leave");
        button.click();
    })

    getToken(sessionId).then(token => {

        session.connect(token, { clientData: UsersName }).then(() => {

            window.publisher = OV.initPublisher("General", {
                audioSource: undefined,
                videoSource: undefined,
                publishAudio: true,
                publishVideo: false,
                resolution: "240x160",
                frameRate: 30,
                insertMode: "APPEND"
            });

            window.publisher.on('videoElementCreated', function (event) {
                event.element.muted = true
                showUserData(event.element, UsersName)
            })

            window.publisher.on("streamPropertyChanged", function (event) {
                console.log("SEE");
                console.log(event);
                var elementId = event.target.id;
                var element = document.getElementById(elementId);
                if (event.changedProperty == "audioActive" && event.newValue == false && event.oldValue == true) {
                    MutedDisplay(element, true);
                }
                
                if (event.changedProperty == "audioActive" && event.newValue == true && event.oldValue == false) {
                    UnMutedDisplay(element);
                }

            })


            session.publish(window.publisher);
        }).catch(error => {
            console.log("There was an error connecting to the video call, please try again");
        }
        )
    })
}

function showUserData(videoElement, connection) {
    var userData;
    var nodeId;
    if (typeof connection === "string") {
        userData = connection;
        nodeId = connection;
    }
    else {
        //alert(JSON.parse(connection.data).clientData);
        userData = JSON.parse(connection.data).clientData;
        nodeId = connection.connectionId;
    }
    var dataNode = document.createElement('div');
    dataNode.className = "movDiv";
    var sendNodeId = "data-" + nodeId;
    dataNode.id = sendNodeId;
    dataNode.innerHTML = "<h3 class='specialHead movTxt2' id=p" + nodeId + "> " + userData + "</h3> ";
    videoElement.parentNode.insertBefore(dataNode, videoElement.nextSibling);
}

function removeUserData(connection) {
    var datanode = document.getElementById("data-" + connection.connectionId);
    datanode.parentElement.removeChild(datanode);
}


window.addEventListener("DOMContentLoaded", function () {
    //when you join the page
    joinSession();
});


window.addEventListener("beforeunload", function () {
    //when you want to leave the page or cancel
    if (session) {
        session.disconnect();
    }
});


function MutedDisplay(element) {
    var vid = element
    var imgDiv = document.createElement("img");
    imgDiv.id = "img" + element.id;
    imgDiv.src = "/images/micOff.png"
    imgDiv.style.width = "240px";
    imgDiv.style.height = "160px";
    vid.parentNode.insertBefore(imgDiv, vid);
    vid.style.display = "none";
}

function UnMutedDisplay(element) {
    var vid = element
    var imgDiv = document.getElementById("img" + element.id);
    vid.parentNode.removeChild(imgDiv);
    vid.style.display = "";
}

function StopVoice() {
    var newbutton = document.createElement("button");
    newbutton.textContent = "StartVoice";
    newbutton.id = "VoiceButton";
    newbutton.onclick = StartVoice;
    newbutton.className += "col m-md-4 m-2 btn btn-outline-info";
    newbutton.innerHTML = '<i class="fas fa-phone-square"></i>';
    window.publisher.publishAudio(false);
    document.getElementById("VoiceButton").replaceWith(newbutton);
}

function StartVoice() {
    var newbutton = document.createElement("button");
    newbutton.textContent = "StopVoice";
    newbutton.id = "VoiceButton";
    newbutton.onclick = StopVoice;
    newbutton.className += "col m-md-4 m-2 btn btn-outline-info";
    newbutton.innerHTML = '<i class="fas fa-phone-slash"></i>';
    window.publisher.publishAudio(true);
    document.getElementById("VoiceButton").replaceWith(newbutton);
}


function EndCall() {
    stopCall(window.sesh);
}

//SERVER SIDE
var OPENVIDU_SERVER_URL = "https://" + location.hostname + ":4443";
var OPENVIDU_SERVER_SECRET = "MY_SECRET";

function getToken(mySessionId) {
    return createSession(mySessionId).then(sessionId => createToken(sessionId));
}

function createSession(sessionId) { // See https://docs.openvidu.io/en/stable/reference-docs/REST-API/#post-openviduapisessions
    return new Promise((resolve, reject) => {
        $.ajax({
            type: "POST",
            url: OPENVIDU_SERVER_URL + "/openvidu/api/sessions",
            data: JSON.stringify({ customSessionId: sessionId }),
            headers: {
                "Authorization": "Basic " + btoa("OPENVIDUAPP:" + OPENVIDU_SERVER_SECRET),
                "Content-Type": "application/json"
            },
            success: response => resolve(response.id),
            error: (error) => {
                if (error.status === 409) {
                    resolve(sessionId);
                } else {
                    console.warn('No connection to OpenVidu Server. This may be a certificate error at ' + OPENVIDU_SERVER_URL);
                    if (window.confirm('No connection to OpenVidu Server. This may be a certificate error at \"' + OPENVIDU_SERVER_URL + '\"\n\nClick OK to navigate and accept it. ' +
                        'If no certificate warning is shown, then check that your OpenVidu Server is up and running at "' + OPENVIDU_SERVER_URL + '"')) {
                        //location.assign(OPENVIDU_SERVER_URL + '/accept-certificate');
                    }
                }
            }
        });
    });
}

function createToken(sessionId) { // See https://docs.openvidu.io/en/stable/reference-docs/REST-API/#post-openviduapisessionsltsession_idgtconnection
    return new Promise((resolve, reject) => {
        $.ajax({
            type: 'POST',
            url: OPENVIDU_SERVER_URL + '/openvidu/api/sessions/' + sessionId + '/connection',
            data: JSON.stringify({}),
            headers: {
                'Authorization': 'Basic ' + btoa('OPENVIDUAPP:' + OPENVIDU_SERVER_SECRET),
                'Content-Type': 'application/json',
            },
            success: (response) => resolve(response.token),
            error: (error) => reject(error)
        });
    });
}



function stopCall(sessionId) {
    return new Promise((resolve, reject) => {
        $.ajax({
            type: "DELETE",
            url: OPENVIDU_SERVER_URL + "/openvidu/api/sessions/" + sessionId,
            //data: JSON.stringify({}),
            headers: {
                'Authorization': 'Basic ' + btoa('OPENVIDUAPP:' + OPENVIDU_SERVER_SECRET),
            },
            success: (response) => resolve(response),
            error: (error) => reject
        })
    })

}