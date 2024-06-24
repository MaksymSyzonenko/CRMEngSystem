document.getElementById("menu-container").addEventListener("mouseenter", function () {
    this.style.width = "215px";
    var menuItems = this.querySelectorAll(".menu-item");
    menuItems.forEach(function (item) {
        var textElement = item.querySelector("span");
        if (textElement) {
            textElement.style.visibility = "visible";
        }
    });

    var logo = document.getElementById("logo");
    logo.style.width = "180px";
    logo.src = "/icons/Logo_Big.svg";
});

document.getElementById("menu-container").addEventListener("mouseleave", function () {
    this.style.width = "70px";
    var menuItems = this.querySelectorAll(".menu-item");
    menuItems.forEach(function (item) {
        var textElement = item.querySelector("span");
        if (textElement) {
            textElement.style.visibility = "hidden";
        }
    });

    var logo = document.getElementById("logo");
    logo.style.width = "25px";
    logo.src = "/icons/Logo_Small.svg";
});