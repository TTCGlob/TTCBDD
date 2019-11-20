window.onload = function () {
    const navBar = document.getElementsByClassName("nav-wrapper")[0]
    document.getElementsByClassName("brand-logo blue darken-3")[0].remove()

    const imageNode = document.createElement("img")
    imageNode.setAttribute("src", "http://amphibianrescue.org/amphibianwordpress/wp-content/uploads/2010/11/20090330-031MM.jpg")
    imageNode.classList += " brand-logo"
    navBar.insertBefore(imageNode, navBar.childNodes[0])
}