// wwwroot/js/notification.js
document.addEventListener("DOMContentLoaded", function () {
    var notification = document.getElementById("notification");
    if (notification) {
        notification.style.display = "block";
        setTimeout(function () {
            notification.style.display = "none";
        }, 3000); // Hide after 3 seconds
    }
});
