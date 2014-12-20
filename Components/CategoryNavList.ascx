<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoryNavList.ascx.cs"
    Inherits="Components_CategoryNavList" %>
<%@ Register Src="~/Components/CategoryNavTreeList.ascx" TagName="TreeList" TagPrefix="uc1" %>
<%@ Register Src="~/Components/CategoryNavMenuList.ascx" TagName="MenuList" TagPrefix="uc2" %>
<%@ Register Src="~/Components/CategoryNavNormalList.ascx" TagName="NormalList" TagPrefix="uc3" %>
<div class="CategoryNavList">
    <div class="SidebarTop">
        <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/CategoryTopLeft.gif" runat="server"
            CssClass="SidebarTopImgLeft" />
        <asp:Label ID="uxCategoryNavTitle" runat="server" Text="[$Categories]" CssClass="SidebarTopTitle"></asp:Label>
        <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/CategoryTopRight.gif" runat="server"
            CssClass="SidebarTopImgRight" />
        <div class="Clear">
        </div>
    </div>
    <div class="SidebarLeft">
        <div class="SidebarRight">
            <uc1:TreeList ID="uxTreeList" runat="server" Visible="false" />
            <uc2:MenuList ID="uxMenuList" runat="server" Visible="false" />
            <uc3:NormalList ID="uxNormalList" runat="server" Visible="false" />
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
