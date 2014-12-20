<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CurrentShoppingCart.ascx.cs"
    Inherits="Components_CurrentShoppingCart" %>
<div class="CurrentShoppingCart" id="uxCurrentShoppingCartDiv" runat="server">
    <div class="SidebarTop">
        <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/CurrentShoppingCartTopLeft.gif"
            runat="server" CssClass="SidebarTopImgLeft" />
        <asp:Label ID="uxCurrentShoppingCartTitle" runat="server" Text="[$Shopping Cart]"
            CssClass="SidebarTopTitle"></asp:Label>
        <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/CurrentShoppingCartTopRight.gif"
            runat="server" CssClass="SidebarTopImgRight" />
        <div class="Clear">
        </div>
    </div>
    <div class="SidebarLeft">
        <div class="SidebarRight">
            <div class="CurrentShoppingCartTable">
                <div class="CurrentShoppingCartQuantityDiv">
                    <div class="CurrentShoppingCartQuantityLabel">
                        [$Quantity] :</div>
                    <div class="CurrentShoppingCartQuantityValue">
                        <asp:Label ID="uxQuantityLabel" runat="server"></asp:Label></div>
                </div>
                <div id="uxDiscountTR" runat="server" class="CurrentShoppingCartDiscountDiv">
                    <div class="CurrentShoppingCartDiscountLabel">
                        [$Discount] :
                    </div>
                    <div class="CurrentShoppingCartDiscountValue">
                        <asp:Label ID="uxDiscountLabel" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="CurrentShoppingCartAmountDiv">
                    <div class="CurrentShoppingCartAmountLabel">
                        [$Amount] :
                    </div>
                    <div class="CurrentShoppingCartAmountValue">
                        <asp:Label ID="uxAmountLabel" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="CurrentShoppingCartLinkBox">
                <div class="CurrentShoppingCartViewCart">
                    <asp:LinkButton ID="uxViewCart" runat="server" CssClass="BtnStyle2" Text="[$View Cart]"
                        PostBackUrl="~/ShoppingCart.aspx" />
                    <asp:LinkButton ID="uxCheckOutButton" runat="server" CssClass="BtnStyle1" Text="[$Check Out]"
                        OnClick="uxCheckOutButton_Click" />
                </div>
            </div>
            <div class="Clear">
            </div>
        </div>
    </div>
    <div class="SidebarBottom">
        <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/CurrentShoppingCartBottomLeft.gif"
            runat="server" CssClass="SidebarBottomImgLeft" />
        <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/CurrentShoppingCartBottomRight.gif"
            runat="server" CssClass="SidebarBottomImgRight" />
        <div class="Clear">
        </div>
    </div>
</div>
