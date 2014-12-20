<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminPayPalPro.ascx.cs"
    Inherits="AdminAdvanced_Gateway_AdminPayPalPro" %>
<%@ Register Src="Components/StoreMultiSelect.ascx" TagName="StoreMultiSelect" TagPrefix="uc1" %>
<div class="CommonRowStyle">
    <div class="Label">
        <asp:Label ID="lcUserName" runat="server" meta:resourcekey="lcUserName" /></div>
    <asp:TextBox ID="uxUserNameText" runat="server" Width="250px" CssClass="fl TextBox" />
    <div class="validator1 fl mgl5">
        <div class="validator1 fl">
            <span class="Asterisk">*</span>
        </div>
        <asp:RequiredFieldValidator ID="uxUserNameTextRequiredValidator" runat="server" ControlToValidate="uxUserNameText"
            Display="Dynamic" ValidationGroup="PaymentValidation" CssClass="CommonValidatorText">
            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Username is required.
            <div class="CommonValidateDiv CommonValidateDivPaymentLong">
            </div>
        </asp:RequiredFieldValidator>
    </div>
    <div class="Clear">
    </div>
</div>
<div class="CommonRowStyle">
    <div class="Label">
        <asp:Label ID="lcPassword" runat="server" meta:resourcekey="lcPassword" /></div>
    <asp:TextBox ID="uxPasswordText" runat="server" Width="250px" CssClass="fl TextBox" />
    <div class="validator1 fl mgl5">
        <div class="validator1 fl">
            <span class="Asterisk">*</span>
        </div>
        <asp:RequiredFieldValidator ID="uxPasswordTextRequireValidator" runat="server" ControlToValidate="uxPasswordText"
            Display="Dynamic" ValidationGroup="PaymentValidation" CssClass="CommonValidatorText">
            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Password is required.
            <div class="CommonValidateDiv CommonValidateDivPaymentLong">
            </div>
        </asp:RequiredFieldValidator>
    </div>
    <div class="Clear">
    </div>
</div>
<div class="CommonRowStyle">
    <div class="Label">
        <asp:Label ID="lcSignature" runat="server" meta:resourcekey="lcSignature" /></div>
    <asp:TextBox ID="uxSignatureText" runat="server" Width="250px" CssClass="fl TextBox" />
    <div class="validator1 fl mgl5">
        <div class="validator1 fl">
            <span class="Asterisk">*</span>
        </div>
        <asp:RequiredFieldValidator ID="uxSignatureTextRequiredValidator" runat="server"
            ControlToValidate="uxSignatureText" Display="Dynamic" ValidationGroup="PaymentValidation"
            CssClass="CommonValidatorText">
            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Signature is required.
            <div class="CommonValidateDiv CommonValidateDivPaymentLong">
            </div>
        </asp:RequiredFieldValidator>
    </div>
    <div class="Clear">
    </div>
</div>
<div class="CommonRowStyle">
    <div class="Label">
        <asp:Label ID="lcEnvironment" runat="server" meta:resourcekey="lcEnvironment" /></div>
    <asp:DropDownList ID="uxEnvironmentDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="live">Live</asp:ListItem>
        <asp:ListItem Value="sandbox">Sandbox</asp:ListItem>
        <asp:ListItem Value="beta-sandbox">Beta-Sandbox</asp:ListItem>
    </asp:DropDownList>
    <div class="Clear">
    </div>
</div>
<uc1:StoreMultiSelect ID="uxStoreSelect" runat="server" PaymentName="PayPal Pro" />
