$(function () {
    var chat = $.connection.chatHub;

    
    chat.client.addMessage = function (name, message) {
        
        console.log("addMessage");

        $('.list-group').append('<li class="list-group-item list-group-item-info">' + htmlEncode(name)
            + ':' + htmlEncode(message) + '</li>');
       // $(".chat-message-dody").scrollTop = $(".chat-message-dody").scrollHeight;
        $(".chat-message-dody").scrollTop(99999);
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

    // Открываем соединение
    $.connection.hub.start().done(function () {
        console.log("done");
        $('.submit-btn').click(function () {
            console.log("click");
            chat.server.send($('#user-name').val(), $('.text-message').val());
            $('.text-message').val('');
        });

        // обработка логина
        //$("#btnLogin").click(function () {

        //    var name = $("#txtUserName").val();
        //    if (name.length > 0) {
        //        chat.server.connect(name);
        //    }
        //    else {
        //        alert("Введите имя");
        //    }
        //});
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