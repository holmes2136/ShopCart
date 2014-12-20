<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminPayPal.ascx.cs" Inherits="AdminAdvanced_Gateway_AdminPayPal" %>
<%@ Register Src="Components/StoreMultiSelect.ascx" TagName="StoreMultiSelect" TagPrefix="uc1" %>
<div class="CommonRowStyle">
    <div class="Label">
        <asp:Label ID="lcPayPalEmail" runat="server" meta:resourcekey="lcPayPalEmail" /></div>
    <asp:TextBox ID="uxPayPalEmailText" runat="server" Width="250px" CssClass="fl TextBox" />
    <asp:RegularExpressionValidator ID="uxPayPalEmailTextValidator" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
        ControlToValidate="uxPayPalEmailText" ValidationGroup="PaymentValidation" CssClass="CommonValidatorText"
        Display="Dynamic">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Wrong Email Format.
        <div class="CommonValidateDiv CommonValidateDivPromotionProductLong">
        </div>
    </asp:RegularExpressionValidator>
    <div class="Clear">
    </div>
</div>
<div class="CommonRowStyle">
    <div class="Label">
        <asp:Label ID="lcSandboxMode" runat="server" meta:resourcekey="lcSandboxMode" /></div>
    <asp:CheckBox ID="uxPayPalSandboxCheck" runat="server" CssClass="fl CheckBox" />
    <div class="Clear">
    </div>
</div>
<uc1:StoreMultiSelect ID="uxStoreSelect" runat="server" PaymentName="PayPal" />