<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddToCartNotification.ascx.cs"
    Inherits="Components_AddToCartNotification" %>
<asp:Button ID="FakeShowDetail" runat="server" Style="display: none;" />
<asp:Panel ID="uxPopupPanel" runat="server">
    <ajaxToolkit:ModalPopupExtender ID="uxAddToCartPopup" runat="server" TargetControlID="FakeShowDetail"
        PopupControlID="uxAddToCartPanel" RepositionMode="None" BackgroundCssClass="ModalBackground"
        CancelControlID="uxCloseButton">
    </ajaxToolkit:ModalPopupExtender>
</asp:Panel>
<asp:Panel ID="uxAddToCartPanel" runat="server" CssClass="AddToCartPanel" Style="display: none">
    <div class="AddToCartTitle">
        <asp:Label ID="uxMessage" runat="server" />
    </div>
    <asp:LinkButton ID="uxCloseButton" runat="server" Text="[$BtnDelete]" CssClass="close" />
    <div class="AddToCartDetail">
        <div class="AddToCartImage">
            <asp:Image ID="uxProductImage" runat="server" Width="60px" ImageAlign="Middle" />
        </div>
        <div class="AddToCartName">
            <asp:HyperLink ID="uxProductNameLink" runat="server" CssClass="AddToCartNameLink" />
        </div>
        <div class="AddToCartPrice">
            <div class="CommonLabel">
                [$Quantity]</div>
            <asp:Label ID="uxQuantityLabel" runat="server" CssClass="CommonValue" />
            <div class="CommonLabel">
                [$UnitPrice]</div>
            <asp:Label ID="uxPriceLabel" runat="server" CssClass="PiceValue" />
        </div>
    </div>
    <div class="AddToCartBottom">
        <div class="AddToCartContinueButton">
            <asp:LinkButton ID="uxContinueShoppingButton" runat="server" CssClass="AddToCartContinue"
                Text="[$BtnContinueShopping]" OnClick="uxContinueShoppingButton_Click" /></div>
        <asp:LinkButton ID="uxCheckoutImageButton" runat="server" CssClass="AddCart" Text="[$BtnCheckout]"
            OnClick="uxCheckoutImageButton_Click" />
    </div>
</asp:Panel>
