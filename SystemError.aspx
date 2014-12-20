<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SystemError.aspx.cs" Inherits="SystemError" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>System Error</title>
    <link rel="StyleSheet" href="App_Themes/Specialized/SystemError.css" type="text/css"
        media="screen" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="SystemError">
            <div class="SystemErrorPage">
                <div class="SystemErrorTop">
                    <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/DefaultBoxTopLeft.gif" runat="server"
                        CssClass="SystemErrorTopImgLeft" />
                    <asp:Label ID="uxDefaultTitle" runat="server" CssClass="SystemErrorTopTitle">
                        <asp:Literal ID="uxHeaderLiteral" runat="server" /></asp:Label>
                    <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/DefaultBoxTopRight.gif"
                        runat="server" CssClass="SystemErrorTopImgRight" />
                    <div class="Clear">
                    </div>
                </div>
                <div class="SystemErrorLeft">
                    <div class="SystemErrorRight">
                        <asp:Literal ID="uxMessageLiteral" runat="server" />
                        <div class="Clear">
                        </div>
                    </div>
                </div>
                <div class="SystemErrorBottom">
                    <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/DefaultBoxBottomLeft.gif"
                        runat="server" CssClass="SystemErrorBottomImgLeft" />
                    <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/DefaultBoxBottomRight.gif"
                        runat="server" CssClass="SystemErrorBottomImgRight" />
                    <div class="Clear">
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
