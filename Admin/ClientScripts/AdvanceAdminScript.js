// JScript File
Sys.Application.add_init(AppInit);

function AppInit(sender) {
    Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
}

function sstchur_SmartScroller_GetCoords() {

}

function sstchur_SmartScroller_Scroll() {


}

function gotoContentTop() {
    document.getElementById('innerContentHolderArea').scrollTop = 0;
}

function resetScrollCordinate() {
    document.getElementById('scrollBarLeft').value = 0;
    document.getElementById('scrollBarTop').value = 0;
}

function webappPrepare() {
    history.go(+1);
    resetScrollCordinate();
    getscrollCordinate();
    alertSize();
}

function webappEndresponse() {
    alertSize();
    getscrollCordinate();
    resetScrollCordinate();
}

function setscrollCordinate() {
    var scrollX, scrollY;
    var scrollX1, scrollY1;
    scrollX = document.getElementById('innerContentHolderArea').scrollLeft;
    scrollY = document.getElementById('innerContentHolderArea').scrollTop;

    document.getElementById('scrollBarLeft').value = scrollX;
    document.getElementById('scrollBarTop').value = scrollY;
}

function getscrollCordinate() {
    var x = document.getElementById('scrollBarLeft').value;
    var y = document.getElementById('scrollBarTop').value;

    document.getElementById('innerContentHolderArea').scrollTop = y;
}

window.onload = webappPrepare;
window.onresize = webappPrepare;


function BeginRequestHandler(sender, args) { }
function EndRequestHandler(sender, args) {
    webappEndresponse();

}
function resizeMenu(lside, id) {
    var attr = lside + ',*';
    var frameSet = parent.document.getElementById(id);
    frameSet.setAttribute('cols', attr);
}

function resizeMenuMasterPage(lsize, id) {
    var leftMenu = parent.document.getElementById(id);
    leftMenu.style.width = lsize;
}

function ShowAccording(showImage, hideImage, subMenuid) {
    var showImageMenu = document.getElementById(showImage);
    var hideImageMenu = document.getElementById(hideImage);
    var subMenu = document.getElementById(subMenuid);

    showImageMenu.style.display = "";
    hideImageMenu.style.display = "none";
    subMenu.style.display = '';
}

function showHideImage(showImage, hideImage, subMenuid) {
    var showImageMenu = document.getElementById(showImage);
    var hideImageMenu = document.getElementById(hideImage);
    var subMenu = document.getElementById(subMenuid);
    if (showImageMenu.style.display == 'block' || showImageMenu.style.display == '') {
        showImageMenu.style.display = "none";
        hideImageMenu.style.display = "";
        subMenu.style.display = 'none';
    }
    else {
        showImageMenu.style.display = "";
        hideImageMenu.style.display = "none";
        subMenu.style.display = '';
    }
}

function showMenu() {
    document.getElementById('MenuShow').style.display = "";
    document.getElementById('MenuShowA').style.display = "";
    document.getElementById('MenuHide').style.display = "none";
    document.getElementById('MenuHideA').style.display = "none";
    document.getElementById('innerMenu').style.width = "265px"; document.getElementById('adminBodyMenu').style.width = "265px"; var contentWidth = parseInt(document.getElementById('adminBodyContent').style.width);
    document.getElementById('adminBodyContent').style.width = (contentWidth - 237) + "px";
}

function hideMenu() {
    document.getElementById('MenuShow').style.display = "none";
    document.getElementById('MenuShowA').style.display = "none";
    document.getElementById('MenuHide').style.display = "";
    document.getElementById('MenuHideA').style.display = "";
    document.getElementById('innerMenu').style.width = "28px";
    document.getElementById('adminBodyMenu').style.width = "28px";
    var contentWidth = parseInt(document.getElementById('adminBodyContent').style.width); // removes the "px" at the end
    document.getElementById('adminBodyContent').style.width = (contentWidth + 237) + "px";
}


function alertSize() {
    var myWidth = 0, myHeight = 0;
    if (typeof (window.innerWidth) == 'number') {
        //Non-IE
        myWidth = window.innerWidth;
        myHeight = window.innerHeight;
    } else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
        //IE 6+ in 'standards compliant mode'
        myWidth = document.documentElement.clientWidth;
        myHeight = document.documentElement.clientHeight;
    } else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
        //IE 4 compatible
        myWidth = document.body.clientWidth;
        myHeight = document.body.clientHeight;
    }

    var marginSide = 0;
    var paddingSide = 0;
    var headerHeight = 87;
    var footerHeight = 30;

    var innerContentHeaderHeight = 0;
    var innerContentHolderAreaHeight = 0;

    var menuMinWidth = 18; menuMaxWidth = 265;

    document.getElementById('AdminArea').style.width = (myWidth - marginSide * 2) + "px";
    document.getElementById('AdminArea').style.height = (myHeight - marginSide * 2) + "px";

    document.getElementById('adminMainBorder').style.width = (myWidth) + "px";
    document.getElementById('adminMainBorder').style.height = (myHeight ) + "px";

    //Content Area.   adminBody, adminBodyMenu, adminBodyContent
    document.getElementById('adminBody').style.width = (myWidth) + "px";
    document.getElementById('adminBody').style.height = (myHeight - footerHeight) + "px";

    innerContentHolderAreaHeight = (myHeight - headerHeight - footerHeight)
    document.getElementById('innerContentHolderArea').style.height = innerContentHolderAreaHeight + "px";

    document.getElementById('adminBodyMenu').style.height = innerContentHolderAreaHeight + "px";

    //alert( document.getElementById('innerContentHolderArea').style.height );
    if (document.getElementById('MenuShow').style.display == "none") {
        document.getElementById('adminBodyMenu').style.width = (menuMinWidth) + "px";
        document.getElementById('adminBodyContent').style.width = (myWidth - menuMinWidth) + "px";
    }
    else {
        document.getElementById('adminBodyMenu').style.width = (menuMaxWidth) + "px";
        document.getElementById('adminBodyContent').style.width = (myWidth - menuMaxWidth) + "px";
    }
}