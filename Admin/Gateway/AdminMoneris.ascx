<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminMoneris.ascx.cs"
    Inherits="AdminAdvanced_Gateway_AdminMoneris" %>
<%@ Register Src="Components/StoreMultiSelect.ascx" TagName="StoreMultiSelect" TagPrefix="uc1" %>
<div class="CommonRowStyle">
    <div class="Label">
        <asp:Label ID="uxMonerisModeLabel" runat="server" meta:resourcekey="lcMonerisMode"></asp:Label></div>
    <asp:DropDownList ID="uxMonerisModeDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True">Test</asp:ListItem>
        <asp:ListItem Value="False">Live</asp:ListItem>
    </asp:DropDownList>
    <div class="Clear">
    </div>
</div>
<div class="CommonRowStyle">
    <div class="Label">
        <asp:Label ID="uxMonerisStoreIDLabel" runat="server" meta:resourcekey="lcMonerisStoreID">
        </asp:Label></div>
    <asp:TextBox ID="uxMonerisStoreIDText" runat="server" ValidationGroup="PaymentGroup"
        CssClass="fl TextBox">
    </asp:TextBox>
    <div class="validator1 fl">
        <span class="Asterisk">*</span>
    </div>
    <asp:RequiredFieldValidator ID="uxMonerisStoreIDRequired" runat="server" ControlToValidate="uxMonerisStoreIDText"
        Display="Dynamic" ValidationGroup="PaymentValidation" CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Store ID is required.
        <div class="CommonValidateDiv">
        </div>
    </asp:RequiredFieldValidator>
    <div class="Clear">
    </div>
</div>
<div class="CommonRowStyle">
    <div class="Label">
        <asp:Label ID="uxAPITokenLabel" runat="server" meta:resourcekey="lcAPIToken"></asp:Label></div>
    <asp:TextBox ID="uxAPITokenText" runat="server" ValidationGroup="PaymentGroup" CssClass="fl TextBox"></asp:TextBox>
    <div class="validator1 fl">
        <span class="Asterisk">*</span>
    </div>
    <asp:RequiredFieldValidator ID="uxAPITokenRequired" runat="server" ControlToValidate="uxAPITokenText"
        Display="Dynamic" ValidationGroup="PaymentValidation" CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> API Token is required.
        <div class="CommonValidateDiv">
        </div>
    </asp:RequiredFieldValidator>
    <div class="Clear">
    </div>
</div>
<div class="CommonRowStyle">
    <div class="Label">
        <asp:Label ID="uxMonerisUseCVDLabel" runat="server" meta:resourcekey="lcMonerisUseCVD"></asp:Label></div>
    <asp:DropDownList ID="uxMonerisUseCVDDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True">Yes</asp:ListItem>
        <asp:ListItem Value="False">No</asp:ListItem>
    </asp:DropDownList>
    <div class="Clear">
    </div>
</div>
<div class="CommonRowStyle">
    <div class="Label">
        <asp:Label ID="uxMonerisCryptLabel" runat="server" meta:resourcekey="lcMonerisCrypt"></asp:Label></div>
    <asp:DropDownList ID="uxMonerisCryptDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="7">SSL enabled merchant</asp:ListItem>
        <asp:ListItem Value="8">Non Secure Transaction (Web or Email Based)</asp:ListItem>
    </asp:DropDownList>
    <div class="Clear">
    </div>
</div>
<uc1:StoreMultiSelect ID="uxStoreSelect" runat="server" PaymentName="Moneris" />
