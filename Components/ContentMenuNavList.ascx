<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ContentMenuNavList.ascx.cs"
    Inherits="Components_ContentMenuNavList" %>
<%@ Register Src="~/Components/ContentMenuNavCascadeList.ascx" TagName="MenuList"
    TagPrefix="uc1" %>
<%@ Register Src="~/Components/ContentMenuNavNormalList.ascx" TagName="NormalList"
    TagPrefix="uc2" %>
<div class="ContentMenuNavList">
    <div class="SidebarTop">
        <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/CategoryTopLeft.gif" runat="server"
            CssClass="SidebarTopImgLeft" />
        <asp:Label ID="uxContentMenuNavTitle" runat="server" Text="[$Content]" CssClass="SidebarTopTitle"
            Visible="false"></asp:Label>
        <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/CategoryTopRight.gif" runat="server"
            CssClass="SidebarTopImgRight" />
        <div class="Clear">
        </div>
    </div>
    <div class="SidebarLeft">
        <div class="SidebarRight">
            <uc1:MenuList ID="uxMenuList" runat="server" Visible="false" />
            <uc2:NormalList ID="uxNormalList" runat="server" Visible="false" />
            <div class="Clear">
            </div>
        </div>
    </div>
    <div class="SidebarBottom">
        <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/CategoryBottomLeft.gif"
            runat="server" CssClass="SidebarBottomImgLeft" />
        <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/CategoryBottomRight.gif"
            runat="server" CssClass="SidebarBottomImgRight" />
        <div class="Clear">
        </div>
    </div>
</div>
