window.onload = function () {
    const navBar = document.getElementsByClassName("nav-wrapper")[0]
    document.getElementsByClassName("brand-logo blue darken-3")[0].remove()

    const imageNode = document.createElement("img")
    imageNode.setAttribute("src", "https://pbs.twimg.com/profile_images/973012399363014656/lUZD5ZKJ_400x400.jpg")
    imageNode.classList += " brand-logo";
    imageNode.style.height = "50px";
    imageNode.style.width = "50px";
    imageNode.style.padding = "0px";
    imageNode.style.borderRadius = "25px";
    navBar.insertBefore(imageNode, navBar.childNodes[0])
}