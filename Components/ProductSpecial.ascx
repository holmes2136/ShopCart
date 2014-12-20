<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductSpecial.ascx.cs"
    Inherits="Components_ProductSpecial" %>
<asp:Panel ID="uxSpecialLabelPanel" runat="server" CssClass="ProductSpecialLabelPanel" /><div class="ProductSpecial">
    <div class="SidebarTop">
        <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/ProductSpecialTopLeft.gif"
            runat="server" CssClass="SidebarTopImgLeft" />
        <asp:Label ID="uxProductSpecialTitle" runat="server" Text="[$Today]" CssClass="SidebarTopTitle"></asp:Label>
        <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/ProductSpecialTopRight.gif"
            runat="server" CssClass="SidebarTopImgRight" />
        <div class="Clear">
        </div>
    </div>
    <div class="SidebarLeft">
        <div class="SidebarRight">
            <div class="ProductSpecialImage">
                <asp:Repeater ID="uxRepeater" runat="server">
                    <ItemTemplate>
                        <asp:HyperLink ID="uxLinkLink" runat="server" NavigateUrl='<%# GetUrl(Eval("ProductID"),Eval("UrlName")) %>'>
                            <asp:Image ID="uxImage" runat="server" ImageUrl='<%# GetImageUrl(Eval("ProductID"))%>' />
                        </asp:HyperLink>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
    <div class="SidebarBottom">
        <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/ProductSpecialBottomLeft.gif"
            runat="server" CssClass="SidebarBottomImgLeft" />
        <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/ProductSpecialBottomRight.gif"
            runat="server" CssClass="SidebarBottomImgRight" />
        <div class="Clear">
        </div>
    </div>
</div>