<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddToCart.aspx.cs" Inherits="Mobile_AddToCart"
    MasterPageFile="~/Mobile/Mobile.master" Title="Add To Shopping Cart" %>

<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="CommonPage">
        <div class="CommonPageLeft">
            <div class="CommonPageRight">
                <asp:Panel ID="uxProductNotExistMessagePanel" runat="server" Visible="false">
                    <div class="MobileShoppingCartMessage MobileCommonBox">
                        Failed adding product to the cart. The product doesn't exist in our shop.<br />
                        Please click the link below to return to catalog page.
                    </div>
                    <asp:HyperLink ID="uxContinueLink" runat="server" CssClass="CommonHyperLink" NavigateUrl="Catalog.aspx">Go to Product List</asp:HyperLink>
                </asp:Panel>
                <asp:Panel ID="uxProductNotAvailableMessagePanel" runat="server" Visible="false">
                    <div class="MobileShoppingCartMessage MobileCommonBox">
                        Failed adding product to the cart. The product is not available (deleted / disabled).<br />
                        Please contact the merchant for this product.
                    </div>
                    <asp:HyperLink ID="uxGoBackLink" runat="server" CssClass="CommonHyperLink" NavigateUrl="Catalog.aspx">Go to Product List</asp:HyperLink>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
