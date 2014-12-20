<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShoppingCartDetails.ascx.cs"
    Inherits="Components_ShoppingCartDetails" %>
<div id="MiniShoppingCartDiv" runat="server" class="MiniShoppingCart">
    <div class="SidebarTop">
        <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/BoxTopLeft.gif" runat="server"
            CssClass="SidebarTopImgLeft" />
        <asp:Label ID="uxShoppingCartTitle" runat="server" Text="[$ShoppingCart]" CssClass="MiniShoppingCartSidebarTopTitle" />
        <div class="MiniShoppingCartShowHidePanel">
            <asp:LinkButton ID="uxShowHideButton" runat="server" OnClick="uxShowHideButton_Click">
                <asp:Image ID="uxShowHideImage" runat="server" Width="18" ImageUrl="~/Images/Design/Icon/CartShowPanel.png" />
            </asp:LinkButton>
            <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/BoxTopRight.gif" runat="server"
                CssClass="SidebarTopImgRight" />
            <div class="Clear">
            </div>
        </div>
    </div>
    <div class="SidebarLeft">
        <div class="SidebarRight">
            <div class="MiniShoppingCartDetail">
                <div class="MiniShoppingCartTitle">
                    <asp:Label ID="uxCartDetailLabel" runat="server" CssClass="CartDetailLabel" />
                    <asp:Label ID="uxCartSubTotalLabel" runat="server" CssClass="CartDetailSubTotal" />
                    <div class="MiniShoppingCartButton">
                        <asp:LinkButton ID="uxViewCartButton" runat="server" CssClass="MiniShoppingCartNoLink BtnStyle2"
                            Text="[$View Cart]" OnClick="uxViewCartButton_Click" />
                        <asp:LinkButton ID="uxCheckOutButton" runat="server" CssClass="MiniShoppingCartNoLink BtnStyle1"
                            Text="[$Check Out]" OnClick="uxCheckOutButton_Click" />
                    </div>
                </div>
                <div class="Clear">
                </div>
                <asp:Panel ID="uxRecentlyPanel" runat="server" CssClass="MiniShoppingCartRecentlyPanel">
                    <div class="MiniShoppingCartRecentlyTitle">
                        <asp:Label ID="uxRecentlyLabel" runat="server" Text="[$Recently]" />
                    </div>
                    <asp:GridView ID="uxRecentlyGrid" runat="server" GridLines="None" ShowHeader="False"
                        EmptyDataRowStyle-CssClass="RecentlyGridEmpty" CssClass="RecentlyAddGrid" AutoGenerateColumns="False"
                        EmptyDataText="[$EmptyCart]" CellPadding="4" OnRowDeleting="uxRecentlyGrid_RowDeleting"
                        DataKeyNames="CartItemID">
                        <Columns>
                            <asp:TemplateField ShowHeader="False" ItemStyle-VerticalAlign="Top">
                                <ItemTemplate>
                                    <asp:Image ID="uxItemImage" runat="server" ImageUrl='<%# GetItemImage( Container.DataItem ) %>'
                                        Width="25" Height="25" ImageAlign="Middle" CssClass="RecentlyItemImageDiv" />
                                </ItemTemplate>
                                <ItemStyle CssClass="RecentlyItemImage" />
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False" ItemStyle-Width="100%">
                                <ItemTemplate>
                                    <asp:HyperLink ID="uxProductNameLink" runat="server" NavigateUrl='<%# GetLink( Container.DataItem ) %>'
                                        Text='<%# GetName( Container.DataItem ) %>' CssClass="MiniShoppingCartLink" />
                                    <div class="MiniShoppingCartQuantityAndPrice">
                                        <asp:Label ID="uxQuantityAndPriceLabel" runat="server" Text='<%# GetQuantityAndPrice( Container.DataItem ) %>' />
                                    </div>
                                </ItemTemplate>
                                <ItemStyle CssClass="RecentlyItemName" />
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False" ItemStyle-VerticalAlign="Top">
                                <ItemTemplate>
                                    <asp:LinkButton ID="uxDeleteImageButton" runat="server" Text="[$BtnDelete]" CssClass="ButtonDelete"
                                        CommandName="Delete" />
                                </ItemTemplate>
                                <ItemStyle CssClass="RecentlyItemDelete" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
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
