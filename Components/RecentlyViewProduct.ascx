<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RecentlyViewProduct.ascx.cs"
    Inherits="Components_RecentlyViewProduct" %>
<div id="RecentlyViewedDiv" class="RecentlyViewedBoxList" runat="server">
    <div class="SidebarTop">
        <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/CategoryTopLeft.gif" runat="server"
            CssClass="SidebarTopImgLeft" />
        <asp:Label ID="uxTitle" runat="server" Text="[$RecentlyViewProduct]" CssClass="SidebarTopTitle"></asp:Label>
        <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/CategoryTopRight.gif" runat="server"
            CssClass="SidebarTopImgRight" />
        <div class="Clear">
        </div>
    </div>
    <div class="SidebarLeft">
        <div class="SidebarRight">
            <asp:DataList ID="uxList" runat="server" ShowFooter="False" CssClass="RecentlyViewedList">
                <HeaderStyle />
                <ItemTemplate>
                    <asp:HyperLink ID="hyperLink" runat="server" NavigateUrl=' <%# Vevo.UrlManager.GetProductUrl( Eval( "ProductID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'
                        Text='<%# GetNavName( Container.DataItem ) %>'>
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
