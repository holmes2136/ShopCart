<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SelectShippingList.ascx.cs"
    Inherits="Admin_Components_Order_SelectShippingList" %>
<%@ Register Src="../Message.ascx" TagName="Message" TagPrefix="uc1" %>
<asp:Panel ID="uxShippingListPanel" runat="server">
    <asp:Panel ID="uxRealTimeMessagePanel" runat="server" CssClass="ShippingRealTimeMessagePanel">
        <asp:Label ID="uxRealTimeMessageLabel" runat="server" ForeColor="Red" />
    </asp:Panel>
    <uc1:Message ID="uxMessage" runat="server" />
    <asp:RequiredFieldValidator ID="uxShippingMethodRequired" runat="server" ControlToValidate="uxShippingRadioList"
        Display="Dynamic" ValidationGroup="ValidDetails">
        <img src="../Images/Design/Bullet/RequiredFillBullet_Down.gif" />
        Please select shipping method.
        <div class="CommonValidateDiv CommonValidateDivCheckoutShippingMethod">
        </div>
    </asp:RequiredFieldValidator>
    <asp:RadioButtonList ID="uxShippingRadioList" runat="server" ValidationGroup="ValidDetails"
        CssClass="f1">
    </asp:RadioButtonList>
    <div class="ShippingRestrictions">
        <asp:Literal ID="uxRestrictionsLiteral" runat="server"></asp:Literal>
    </div>
    <div class="ShippingRecurringWarring">
        <asp:Label ID="uxRecurringWarringLabel" runat="server"></asp:Label>
    </div>
</asp:Panel>
