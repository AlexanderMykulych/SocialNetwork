$(function () {
    var chat = $.connection.chatHub;

    
    chat.client.addMessage = function (name, message) {
        
        console.log("addMessage"); 
        var html_name = htmlEncode(name);
        if (name.toLowerCase() == $("#user-name").val().toLowerCase())
            html_name = '<strong>' + htmlEncode(name) + '</strong>';
        var arr = new Array();
        arr[0] = "info";
        arr[1] = "success";
        arr[2] = "warning";
        arr[3] = "danger";
        var min = 0, max = 3;
        var rand = Math.round(min - 0.5 + Math.random() * (max - min + 1));
        console.log('<li class="list-group-item list-group-item-' + arr[rand] + '">');
        $('.list-group').append('<li class="list-group-item list-group-item-' + arr[rand] + '">' + html_name
            + ':' + htmlEncode(message) + '</li>');
       // $(".chat-message-dody").scrollTop = $(".chat-message-dody").scrollHeight;
        $(".chat-message-dody").scrollTop(999999);
    };

    
    chat.client.onConnected = function (id, userName, allUsers) {
        console.log("onConnected");
       
        $('.hidden-id').val(id);
        $('#user-name').val(userName);
        $('.panel-title').html('<h3>Hello, ' + userName + '</h3>');

        
        for (i = 0; i < allUsers.length; i++) {

            AddUser(allUsers[i].ConnectionId, allUsers[i].Name);
        }
    }

    
    chat.client.onNewUserConnected = function (id, name) {
        console.log("onNewUserConnected");
        AddUser(id, name);
    }

    
    chat.client.onUserDisconnected = function (id, userName) {
        console.log("onUserDisconnected");
        $('.' + id).remove();
    }

    // Open connection
    $.connection.hub.start().done(function () {
        console.log("done");
        $('.submit-btn').click(function () {
            console.log("click");
            chat.server.send($('#user-name').val(), $('.text-message').val());
            $('.text-message').val('');
        });

        
    });
});

$(document).ready(function () {
    var chat = $.connection.chatHub;
    console.log("connected...");
    var name = $("#user-name").val();

    chat.server.connect(name)

});

function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}


function AddUser(id, name) {

    var userId = $('.hidden-id').val();
    console.log(name);
    if (userId != id) {
        console.log(name);
        $("#chatures").append('<p><b>' + name + '</b></p>');
    }
}