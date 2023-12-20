
var OV;
var session;
window.elem;
window.sesh;
window.capishe;

function joinSession() {
    var groupId = document.getElementById("groupName").textContent;
    var sessionId = "Session" + groupId;
    window.sesh = sessionId;
    var UsersName = document.getElementById("usersname").textContent;

    //alert(sessionId);
    OV = new OpenVidu();
    session = OV.initSession();

    session.on("streamCreated", function (event) {
        var subscriber = session.subscribe(event.stream, "Others");

        subscriber.on("videoElementCreated", event => {
            showUserData(event.element, subscriber.stream.connection);
            var elementId = event.element.id;
            var element = document.getElementById(elementId);
            window.elem = elementId;
            if (subscriber.stream.audioActive == false) {
                MutedDisplay(element, false);
            };
            if (subscriber.stream.videoActive == false) {
                noVideo(element);
            };

            
        });

        subscriber.on("streamPropertyChanged", function (event) {
            var elementId = event.target.id;
            var element = document.getElementById(elementId);
            window.elem = elementId;
            if (event.changedProperty == "audioActive" && event.newValue == false && event.oldValue == true) {
                MutedDisplay(element, false);
            }
            if (event.changedProperty == "videoActive" && event.newValue == false && event.oldValue == true) {
                noVideo(element);
            }
            if (event.changedProperty == "videoActive" && event.newValue == true && event.oldValue == false) {
                yesVideo(element);
            }
            if (event.changedProperty == "audioActive" && event.newValue == true && event.oldValue == false) {
                UnMutedDisplay(element);
            }

        })

    });

    session.on('streamDestroyed', event => {
        removeUserData(event.stream.connection);
        if (window.elem != null) {
            var element = document.getElementById(window.elem);
            yesVideo(element);
            UnMutedDisplay(element);
        }
        
    })

    session.on("sessionDisconnected", event => {
        alert("The Call has been Ended");
        var button = document.getElementById("Leave");
        button.click();
    })

    getToken(sessionId).then(token => {

        session.connect(token, { clientData: UsersName }).then(() => {

            window.publisher = OV.initPublisher("Main", {
                audioSource: undefined,
                videoSource: undefined,
                publishAudio: true,
                publishVideo: true,
                resolution: "320X240",
                frameRate: 30,
                insertMode: "APPEND"
            });

            window.publisher.on('videoElementCreated', function (event) {
                event.element.muted = true
                showMainUserData(event.element, UsersName)
            })

            window.publisher.on("streamPropertyChanged", function (event) {
                var elementId = event.target.id;
                var element = document.getElementById(elementId);
                if (event.changedProperty == "audioActive" && event.newValue == false && event.oldValue == true) {
                    MutedDisplay(element, true);
                }
                if (event.changedProperty == "videoActive" && event.newValue == false && event.oldValue == true) {
                    noVideo(element);
                }
                if (event.changedProperty == "videoActive" && event.newValue == true && event.oldValue == false) {
                    yesVideo(element);
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


function MutedDisplay(element, bool) {
    var vid = element;
    var imAg = document.createElement("img");
    var imgDiv = document.createElement("div");
    imgDiv.id = "imgDiv" + element.id;

   
    if (bool == false) {
        imgDiv.className = "movDiv";
        imAg.className = "movImg";
        vid.parentNode.insertBefore(imgDiv, vid);
    }
    else {
        imgDiv.className = "movDivImg";
        imAg.className = "movImg2";
        vid.parentNode.insertBefore(imgDiv, vid.nextSibling.nextSibling);
    }

    imgDiv.appendChild(imAg);
    
    imAg.id = "imgM" + element.id;
    imAg.src = "/images/micOff.png"
   
}

function UnMutedDisplay(element) {
    var vid = element
    var imgDiv = document.getElementById("imgDiv" + element.id);
    if (imgDiv != null) {
        vid.parentNode.removeChild(imgDiv);
        vid.style.display = "";
    }
    
}

function noVideo(element) {
    var vid = element;
    if (vid.style.display != "none") {
        var imgDiv = document.createElement("img");
        imgDiv.id = "imgV" + element.id;
        imgDiv.src = "/images/camOff.png"
        imgDiv.style.width = "320px";
        imgDiv.style.height = "240px";
        vid.parentNode.insertBefore(imgDiv, vid);
        vid.style.display = "none";
    }
    
}

function yesVideo(element) {
    var vid = element
    if (vid.style.display == "none") {
        var imgDiv = document.getElementById("imgV" + element.id);
        vid.parentNode.removeChild(imgDiv);
        vid.style.display = "";
    }
    
}

function showMainUserData(videoElement, connection) {
    var userData;
    var nodeId;
    if (typeof connection === "string") {
        userData = connection;
        nodeId = connection;
    }
    else {
        userData = JSON.parse(connection.data).clientData;
        nodeId = connection.connectionId;
    }
    var dataNode = document.createElement('div');
    dataNode.className = "";
    var sendNodeId = "data-" + nodeId;
    dataNode.id = sendNodeId
    dataNode.innerHTML = "<h1 class='' id=p" + nodeId + "> " + userData + "</h1> ";
    videoElement.parentNode.insertBefore(dataNode, videoElement.nextSibling);
    addClickListener(videoElement, userData, nodeId);
}

function showUserData(videoElement, connection) {
    var userData;
    var nodeId;
    if (typeof connection === "string") {
        userData = connection;
        nodeId = connection;
    }
    else {
        userData = JSON.parse(connection.data).clientData;
        nodeId = connection.connectionId;
    }
    var dataNode = document.createElement('div');
    dataNode.className = "movDiv";
    var sendNodeId = "data-" + nodeId;
    dataNode.id = sendNodeId;
    dataNode.innerHTML = "<h1 class='movTxt' id=p" + nodeId+"> " + userData + "</h1> ";
    videoElement.parentNode.insertBefore(dataNode, videoElement.nextSibling);
    addClickListener(videoElement, userData, nodeId);
}

function removeUserData(connection) {
    var dataNode = document.getElementById("data-" + connection.connectionId);
    dataNode.parentNode.removeChild(dataNode);
}


function addClickListener(videoElement, userData, nodeId) {
    videoElement.addEventListener("click", function () {
        var mainVideo = $("#Main video").get(0);
        if (mainVideo.srcObject !== videoElement.srcObject ) {
            var holdVideo = document.createElement("video");
            holdVideo.srcObject = mainVideo.srcObject;
            var holdDiv = document.querySelector("#Main div").id;
            var holdData = document.querySelector("#" + holdDiv + " h1");
            $("#Main").fadeOut("fast", () => {
                mainVideo.srcObject = videoElement.srcObject

                if (userData == $("#usersname").get()) {
                    mainVideo.muted = true; 
                }
                document.getElementById("p" + nodeId).textContent = holdData.textContent;
                holdData.textContent = userData;
                $("#Main").fadeIn("fast");
                videoElement.srcObject = holdVideo.srcObject;
                
            })
        }
    })
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

function EndCall() {
    stopCall(window.sesh);    
}

function StopVideo() {
    var newbutton = document.createElement("button");
    newbutton.textContent = "StartVideo";
    newbutton.id = "VideoButton";
    newbutton.onclick = StartVideo;
    newbutton.className += "col m-md-4 m-2 btn btn-outline-info"
    newbutton.innerHTML = '<i class="fas fa-video"></i>';
    window.publisher.publishVideo(false);
    document.getElementById("VideoButton").replaceWith(newbutton);
}

function StartVideo() {
    var newbutton = document.createElement("button");
    newbutton.textContent = "StopVideo";
    newbutton.id = "VideoButton";
    newbutton.onclick = StopVideo;
    newbutton.className += "col m-md-4 m-2 btn btn-outline-info"
    newbutton.innerHTML = '<i class="fas fa-video-slash"></i>';
    window.publisher.publishVideo(true);
    document.getElementById("VideoButton").replaceWith(newbutton);
}

function StopVoice() {
    var newbutton = document.createElement("button");
    newbutton.textContent = "StartVoice";
    newbutton.id = "VoiceButton";
    newbutton.onclick = StartVoice;
    newbutton.className += "col m-md-4 m-2 btn btn-outline-info"
    newbutton.innerHTML = '<i class="fas fa-phone-square"></i>';
    window.publisher.publishAudio(false);
    document.getElementById("VoiceButton").replaceWith(newbutton);
}

function StartVoice() {
    var newbutton = document.createElement("button");
    newbutton.textContent = "StopVoice";
    newbutton.id = "VoiceButton";
    newbutton.onclick = StopVoice;
    newbutton.className += "col m-md-4 m-2 btn btn-outline-info"
    newbutton.innerHTML = '<i class="fas fa-phone-slash"></i>';
    window.publisher.publishAudio(true);
    document.getElementById("VoiceButton").replaceWith(newbutton);
}

function ShareScreen() {
    var newbutton = document.createElement("button");
    newbutton.textContent = "StopScreenShare";
    newbutton.id = "ScreenButtons";
    newbutton.onclick = stopScreenSchare;
    newbutton.className += "col m-md-4 m-2 btn btn-outline-info"
    newbutton.innerHTML = '<i class="material-icons">stop_screen_share</i>';

    window.newPublisher = OV.initPublisher("Main", {
        videoSource: "screen",
        resolution: "640x480"
    });


    window.newPublisher.once("accessAllowed", event => {

        try {
            window.newPublisher.stream.getMediaStream().getVideoTracks()[0].applyConstraints({
                width: 360,
                height: 240
            });
        }
        catch (error) {
            alert(error);
        }

        session.unpublish(window.publisher);
        session.publish(window.newPublisher);
        window.newPublisher.stream.getMediaStream().getVideoTracks()[0].addEventListener("ended", () => {
            stopScreenSchare();
        })
        document.getElementById("ScreenButton").replaceWith(newbutton);
    });
    window.newPublisher.once('accessDenied', (event) => {
        alert("ScreenSharing Blocked! If you want to Reactivate it, reload the Page");
    });
}

function stopScreenSchare() {
    var newbutton = document.createElement("button");
    newbutton.textContent = "ShareScreen";
    newbutton.id = "ScreenButton";
    newbutton.onclick = ShareScreen;
    newbutton.className += "col m-md-4 m-2 btn btn-outline-info"
    newbutton.innerHTML = '<i class="material-icons">screen_share</i>';


    session.unpublish(window.newPublisher);
    window.publisher = OV.initPublisher("Main", {
        audioSource: undefined,
        videoSource: undefined,
        publishAudio: true,
        publishVideo: true,
        resolution: "320X480",
        frameRate: 30,
        insertMode: "APPEND"
    });

    window.publisher.on("videoElementCreated", function (event) {
        if (event.element.previousSibling) {
            event.element.muted = true
            var sib = event.element.previousSibling;
            var pVal = document.querySelector("#" + sib.id + " h1").textContent;
            sib.remove();
            showMainUserData(event.element, pVal);
        }              
    })
    window.publisher.on("streamPropertyChanged", function (event) {
        var elementId = event.target.id;
        var element = document.getElementById(elementId);
        if (event.changedProperty == "audioActive" && event.newValue == false && event.oldValue == true) {
            MutedDisplay(element, true);
        }
        if (event.changedProperty == "videoActive" && event.newValue == false && event.oldValue == true) {
            noVideo(element);
        }
        if (event.changedProperty == "videoActive" && event.newValue == true && event.oldValue == false) {
            yesVideo(element);
        }
        if (event.changedProperty == "audioActive" && event.newValue == true && event.oldValue == false) {
            UnMutedDisplay(element);
        }

    })

    session.publish(window.publisher);
    document.getElementById("ScreenButtons").replaceWith(newbutton);
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
            success: response => {
                resolve(response.id)
            },
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
