<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ContentNavList.ascx.cs"
    Inherits="Components_ContentNavList" %>
<div class="ContentMenuNavList">
    <div class="SidebarTop">
        <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/CategoryTopLeft.gif" runat="server"
            CssClass="SidebarTopImgLeft" />
        <asp:Label ID="uxContentMenuNavTitle" runat="server" Text="[$Content]" CssClass="SidebarTopTitle"></asp:Label>
        <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/CategoryTopRight.gif" runat="server"
            CssClass="SidebarTopImgRight" />
        <div class="Clear">
        </div>
    </div>
    <div class="SidebarLeft">
        <div class="SidebarRight">
            <asp:DataList ID="uxList" runat="server" ShowFooter="False" CssClass="CategoryNavNormalList">
                <HeaderStyle />
                <ItemTemplate>
                    <asp:HyperLink ID="hyperLink" runat="server" NavigateUrl='<%# GetNavUrl( Container.DataItem ) %>'
                        Text='<%# GetNavName( Container.DataItem ) %>' ToolTip='<%# Eval("Description") %>'>
                    </asp:HyperLink>
                </ItemTemplate>
            </asp:DataList>
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
