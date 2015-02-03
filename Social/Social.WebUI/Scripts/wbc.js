var ws;
$(document).ready(function () {
    
        var panel = $(".panel-title");
        panel.text("connecting...");

        ws = new WebSocket("ws://" + window.location.hostname + ":" + window.location.port + "/api/WsChat");
        ws.onopen = function () {
            panel.text("connected!");
        };

    
        ws.onmessage = function (evt) {
            $(".submit-btn").text(evt.data);
        };


        ws.onerror = function (evt) {
            $(".submit-btn").text(evt.message);
        };


        ws.onclose = function () {
            panel.text("disconnected");
        };

    $(".submit-btn").click(function () {
        if (ws.readyState == WebSocket.OPEN) {
            ws.send($("#user-name").attr("name") + ":" + $(".text-message").val());
        }
        else {
            panel.text("Connection is closed!");
        }
    });
    
});