<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DepartmentNavList.ascx.cs"
    Inherits="Components_DepartmentNavList" %>
<%@ Register Src="~/Components/DepartmentNavTreeList.ascx" TagName="TreeList" TagPrefix="uc1" %>
<%@ Register Src="~/Components/DepartmentNavMenuList.ascx" TagName="MenuList" TagPrefix="uc2" %>
<%@ Register Src="~/Components/DepartmentNavNormalList.ascx" TagName="NormalList" TagPrefix="uc3" %>
<div class="DepartmentNavList">
    <div class="SidebarTop">
        <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/DepartmentTopLeft.gif" runat="server"
            CssClass="SidebarTopImgLeft" />
        <asp:Label ID="uxDepartmentNavTitle" runat="server" Text="[$Department]" CssClass="SidebarTopTitle"></asp:Label>
        <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/DepartmentTopRight.gif" runat="server"
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
        <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/DepartmentBottomLeft.gif"
            runat="server" CssClass="SidebarBottomImgLeft" />
        <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/DepartmentBottomRight.gif"
            runat="server" CssClass="SidebarBottomImgRight" />
        <div class="Clear">
        </div>
    </div>
</div>
