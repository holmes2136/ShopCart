<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RecentPost.ascx.cs" Inherits="Blog_Components_RecentPost" %>
<%@ Register Src="RecentPostNavNormalList.ascx" TagName="NormalList" TagPrefix="uc1" %>
<div class="BlogNavList" id="uxRecentPostDiv" runat="server">
    <div class="RecentPost">
        <div class="BlogSidebarTop">
            <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/BlogTopLeft.gif" runat="server"
                CssClass="BlogSidebarTopImgLeft" />
            <asp:Label ID="uxRecentPostNavTitle" runat="server" Text="[$RecentPostTitle]" CssClass="BlogSidebarTopTitle"></asp:Label>
            <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/BlogTopRight.gif" runat="server"
                CssClass="BlogSidebarTopImgRight" />
            <div class="Clear">
            </div>
        </div>
        <div class="BlogSidebarLeft">
            <div class="BlogSidebarRight">
                <uc1:NormalList ID="uxNormalList" runat="server" />
                <div class="Clear">
                </div>
            </div>
        </div>
        <div class="BlogSidebarBottom">
            <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/BlogBottomLeft.gif" runat="server"
                CssClass="BlogSidebarBottomImgLeft" />
            <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/BlogBottomRight.gif"
                runat="server" CssClass="BlogSidebarBottomImgRight" />
            <div class="Clear">
            </div>
        </div>
    </div>
</div>
