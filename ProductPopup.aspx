<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductPopup.aspx.cs" Inherits="ProductPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Product Detail</title>
</head>
<body class="ProductPopup">
    <div class="ProductPopupTop">
        <asp:Image ID="uxTopLeftImage" runat="server" CssClass="ProductPopupTopImgLeft" ImageUrl="~/Images/Design/Box/ProductPopupTopLeft.gif" />
        <asp:Label ID="uxNameLable" runat="server" CssClass="ProductPopupTopTitle"></asp:Label>
        <asp:Image ID="uxTopRightImage" runat="server" CssClass="ProductPopupTopImgRight"
            ImageUrl="~/Images/Design/Box/ProductPopupTopRight.gif" />
        <div class="Clear">
        </div>
    </div>
    <div class="ProductPopupLeft">
        <div class="ProductPopupRight">
            <table id="ProductTable" cellpadding="4" border="0" cellspacing="0" class="ProductPopupTable">
                <tr>
                    <td class="ProductPopupImageColumn">
                        <asp:Image ID="uxImage" runat="server" CssClass="ProductPopupImage" />
                    </td>
                </tr>
                <tr>
                    <td class="ProductPopupTitleColumn">
                        Summary
                    </td>
                </tr>
                <tr>
                    <td class="ProductPopupDetailsColumn">
                        <asp:Literal ID="uxSummary" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="ProductPopupTitleColumn">
                        Description
                    </td>
                </tr>
                <tr>
                    <td class="ProductPopupDetailsColumn">
                        <asp:Literal ID="uxDescription" runat="server"></asp:Literal>
                    </td>
                </tr>
            </table>
            <div class="Clear">
            </div>
        </div>
    </div>
    <div class="ProductPopupBottom">
        <asp:Image ID="uxBottomLeftImage" runat="server" CssClass="ProductPopupBottomImgLeft"
            ImageUrl="~/Images/Design/Box/ProductPopupBottomLeft.gif" />
        <asp:Image ID="uxBottomRightImage" runat="server" CssClass="ProductPopupBottomImgRight"
            ImageUrl="~/Images/Design/Box/ProductPopupBottomRight.gif" />
        <div class="Clear">
        </div>
    </div>
    <p class="ProductPopupBottomParagraph">
        <asp:HyperLink ID="uxTopLink" runat="server" Text="^Top" NavigateUrl="#ProductTable"
            CssClass="ProductPopupTopLink"></asp:HyperLink>
        |
        <asp:HyperLink ID="uxCloseWindow" runat="server" Text="Close Window" CssClass="ProductPopupCloseWindowsLink"></asp:HyperLink>
    </p>
</body>
</html>
