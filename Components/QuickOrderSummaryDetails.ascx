<%@ Control Language="C#" AutoEventWireup="true" CodeFile="QuickOrderSummaryDetails.ascx.cs"
    Inherits="Components_QuickOrderSummaryDetails" %>
<div class="QuickOrderSummary">
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
            <div class="MiniShoppingCartDetail">
                <div class="MiniShoppingCartTitle">
                    <asp:Label ID="uxCartDetailLabel" runat="server" CssClass="CartDetailLabel" />
                    <asp:Label ID="uxCartSubTotalLabel" runat="server" CssClass="CartDetailSubTotal" />
                    <div class="MiniShoppingCartButton">
                        <div class="MiniShoppingCartViewCart">
                            <asp:LinkButton ID="uxViewCartButton" runat="server" CssClass="MiniShoppingCartNoLink"
                                OnClick="uxViewCartButton_Click" />
                        </div>
                        <div class="MiniShoppingCartCheckOut">
                            <asp:LinkButton ID="uxCheckOutButton" runat="server" CssClass="MiniShoppingCartNoLink"
                                OnClick="uxCheckOutButton_Click" />
                        </div>
                    </div>
                </div>                
            </div>
        </div>
    </div>
    <div class="SidebarBottom">
        <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/BoxBottomLeft.gif" runat="server"
            CssClass="SidebarBottomImgLeft" />
        <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/BoxBottomRight.gif" runat="server"
            CssClass="SidebarBottomImgRight" />
        <div class="Clear">
        </div>
    </div>
</div>
