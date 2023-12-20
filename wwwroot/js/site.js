// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/texts").build();

var groupname = document.getElementById("groupname").value;
var PresentUser = document.getElementById("username").value;



connection.on("SendMessage", function (message, username) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var p = document.createElement("p");
    if (PresentUser == username) {
        p.innerHTML = `<p class="p-3 border rounded-lg rounded-top bg-dark text-white offset-md-5 col-md-6 col-8 offset-4">
    ${msg}</p>`
    }
    else {
        p.innerHTML = `<p class="text-left p-3 border rounded-lg rounded-top col-md-6 col-8" style="background-color:#f6f5f5">
    ${username}: ${msg}</p>`
    }
    //p.innerHTML = username + " : " + msg;
    //p.className = "text-right";
    document.getElementById("messages").append(p);
    ScrollDown();
})

document.getElementById("sendMessage").addEventListener("click", function () {
    var message = document.getElementById("newMessage").value;
    var username = document.getElementById("username").value;
    connection.invoke("SendMessageToGroup", groupname, message, username).catch(err => console.log(err));
})

function ScrollDown() {
    var messages = document.getElementById("messages");
    messages.scrollTop = messages.scrollHeight;
}

window.addEventListener("DOMContentLoaded", function () {
    //when you join the page
    ScrollDown();
    connection.start().then(() => {
        connection.invoke("JoinGroup", groupname).catch(err => console.log(err));
        console.log("connected to group");
    })
});


window.addEventListener("beforeunload", function (e) {
    //when you want to leave the page or cancel
    connection.invoke("LeaveGroup", groupname).catch(err => console.log(err));
});
