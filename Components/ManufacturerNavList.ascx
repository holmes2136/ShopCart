<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ManufacturerNavList.ascx.cs" Inherits="Components_ManufacturerNavList" %>
<%@ Register Src="~/Components/ManufacturerNavNormalList.ascx" TagName="NormalList" TagPrefix="uc1" %>
<%@ Register Src="~/Components/ManufacturerNavDropDown.ascx" TagName="DropDown" TagPrefix="uc2" %>
<div class="ManufacturerNavList">
    <div class="SidebarTop">
        <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/DepartmentTopLeft.gif" runat="server"
            CssClass="SidebarTopImgLeft" />
        <asp:Label ID="uxDepartmentNavTitle" runat="server" Text="[$Manufacturer]" CssClass="SidebarTopTitle"></asp:Label>
        <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/DepartmentTopRight.gif" runat="server"
            CssClass="SidebarTopImgRight" />
        <div class="Clear">
        </div>
    </div>

    <div class="SidebarLeft">
        <div class="SidebarRight">
            <uc1:NormalList ID="uxNormalList" runat="server" Visible="false" />
            <uc2:DropDown ID="uxDropDown" runat="server" Visible="false" />
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