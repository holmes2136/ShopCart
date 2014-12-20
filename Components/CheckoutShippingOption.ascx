<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CheckoutShippingOption.ascx.cs"
    Inherits="Components_CheckoutShippingOption" %>
<%@ Register Src="Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Import Namespace="Vevo" %>
<div class="ShippingDiv">
    <div class="SidebarTop">
        <asp:Label ID="uxHeaderCheckoutLabel" runat="server" Text="[$Title]" CssClass="CheckoutAddressTitle"></asp:Label>
    </div>
    <div class="CheckoutInnerTitle" ID="uxIntro" runat="server">
        [$Intro]
    </div>
    <div id="uxFixedShippingMessageDiv" runat="server" visible="false" class="ShippingMessageFixedShippingCost">
        [$MessageFixedShippingCost3]
    </div>
    <asp:Panel ID="uxRealTimeMessagePanel" runat="server" CssClass="ShippingRealTimeMessagePanel"
        Visible="false">
        <asp:Label ID="uxRealTimeMessageLabel" runat="server" ForeColor="Red" />
    </asp:Panel>
    <asp:RequiredFieldValidator ID="uxShippingMethodRequired" runat="server" CssClass="CommonValidatorText ShippingValidatorText"
        ControlToValidate="uxShippingRadioList" Display="Dynamic" ValidationGroup="ShippingValid">
        <img src="Images/Design/Bullet/RequiredFillBullet_Down.gif" /> [$Intro]<div class="CommonValidateDiv ShippingValidateDiv"></div>
    </asp:RequiredFieldValidator>
    <asp:RadioButtonList ID="uxShippingRadioList" runat="server" ValidationGroup="ShippingValid"
        CssClass="ShippingRadioList">
    </asp:RadioButtonList>
    <div class="ShippingRestrictions">
        <asp:Literal ID="uxRestrictionsLiteral" runat="server"></asp:Literal>
    </div>
    <div class="ShippingRecurringWarring">
        <asp:Label ID="uxRecurringWarringLabel" runat="server"></asp:Label>
    </div>
    <div class="Clear">
    </div>
</div>
