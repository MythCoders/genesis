var timeoutWarningCounter;
var timeoutCounter;

function Timeout() {
    window.location.href = "/Dealers/Public/SessionTimeout";
}

function ResetTimer() {
    $.ajax({
        url: "/Dealers/Public/StayActive",
        method: "POST",
        success: function (data) {

        }
    });
    clearTimeout(timeoutCounter);
    timeoutWarningCounter = setTimeout(TimeoutWarning, 1080000);
    timeoutCounter = setTimeout(Timeout, 1200000);
    $('#timeoutModal').modal('hide');
}

function TimeoutWarning() {
    $('#timeoutModal').modal('show');
}