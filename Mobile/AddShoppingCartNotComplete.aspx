<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddShoppingCartNotComplete.aspx.cs"
    MasterPageFile="~/Mobile/Mobile.master" Inherits="Mobile_AddShoppingCartNotComplete"
    Title="Add Shopping Cart Not Complete" %>

<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="AddShoppingCartNotComplete">
        <div class="CommonPage">
            <div class="CommonPageTop">
            </div>
            <div class="CommonPageLeft">
                <div class="CommonPageRight">
                    <asp:Panel ID="uxProductFreeShippingCostPanel" runat="server" Visible="false">
                        <div class="MobileShoppingCartMessage MobileCommonBox">
                            Cannot add product to the cart. The shopping cart already has one or more product
                            that is a free shipping product.
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="uxProductNonFreeShippingCostPanel" runat="server" Visible="false">
                        <div class="MobileShoppingCartMessage MobileCommonBox">
                            Cannot add product to the cart. The shopping cart already has one or more product
                            that not free shipping.
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="uxInvalidSubscriptionPanel" runat="server" Visible="false">
                        <div class="MobileShoppingCartMessage MobileCommonBox">
                            Cannot add product to the cart. You already have higher product subscription level.
                        </div>
                    </asp:Panel>
                </div>
            </div>
            <div class="CommonPageBottom">
            </div>
        </div>
    </div>
</asp:Content>
