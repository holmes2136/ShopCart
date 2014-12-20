<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminLinkPoint.ascx.cs"
    Inherits="AdminAdvanced_Gateway_AdminLinkPoint" %>
<%@ Register Src="../Components/Common/Upload.ascx" TagName="Upload" TagPrefix="uc6" %>
<%@ Register Src="Components/StoreMultiSelect.ascx" TagName="StoreMultiSelect" TagPrefix="uc1" %>
<div class="CommonRowStyle">
    <div class="Label">
        <asp:Label ID="lcStoreNumberLabel" runat="server" meta:resourcekey="lcStoreNumber"></asp:Label></div>
    <asp:TextBox ID="uxStoreNumberText" runat="server" CssClass="fl TextBox">
    </asp:TextBox>
    <div class="validator1 fl">
        <span class="Asterisk">*</span></div>
        <asp:RequiredFieldValidator ID="uxStoreNumberTextRequired" runat="server"
            ControlToValidate="uxStoreNumberText" Display="Dynamic" ValidationGroup="PaymentValidation"
            CssClass="CommonValidatorText">
            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Store Name ID is required.
            <div class="CommonValidateDiv">
            </div>
        </asp:RequiredFieldValidator>
    <div class="Clear">
    </div>
</div>
<div class="CommonRowStyle">
    <div class="Label">
        <asp:Label ID="lcHostLabel" runat="server" meta:resourcekey="lcHost"></asp:Label></div>
    <asp:TextBox ID="uxHostText" runat="server" CssClass="fl TextBox">
    </asp:TextBox>
    <div class="validator1 fl">
        <span class="Asterisk">*</span></div>
        <asp:RequiredFieldValidator ID="uxHostTextRequired" runat="server"
            ControlToValidate="uxHostText" Display="Dynamic" ValidationGroup="PaymentValidation"
            CssClass="CommonValidatorText">
            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Host is required.
            <div class="CommonValidateDiv">
            </div>
        </asp:RequiredFieldValidator>
    <div class="Clear">
    </div>
</div>
<div class="CommonRowStyle">
    <div class="Label">
        <asp:Label ID="lcPortLabel" runat="server" meta:resourcekey="lcPort"></asp:Label></div>
    <asp:TextBox ID="uxPortText" runat="server" CssClass="fl TextBox">
    </asp:TextBox>
    <div class="validator1 fl">
        <span class="Asterisk">*</span></div>
        <asp:RequiredFieldValidator ID="uxPortTextRequired" runat="server"
            ControlToValidate="uxPortText" Display="Dynamic" ValidationGroup="PaymentValidation"
            CssClass="CommonValidatorText">
            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Port is required.
            <div class="CommonValidateDiv">
            </div>
        </asp:RequiredFieldValidator>
    <div class="Clear">
    </div>
</div>
<div class="CommonRowStyle">
    <div class="Label">
        <asp:Label ID="lcKeyFileLabel" runat="server" meta:resourcekey="lcKeyFile"></asp:Label>
    </div>
    <div class="fl">
        <asp:LinkButton ID="uxKeyFileLink" runat="server" CssClass="KeyFileLink UnderlineDashed"
            meta:resourcekey="uxKeyFileLink" OnLoad="uxKeyFileLink_Load"></asp:LinkButton>
    </div>
</div>
<div class="CommonRowStyle">
    <div class="Label">
        <asp:Label ID="lcMode" runat="server" meta:resourcekey="lcMode"></asp:Label>
    </div>
    <asp:DropDownList ID="uxModeDrop" runat="server">
        <asp:ListItem Value="Live">Live</asp:ListItem>
        <asp:ListItem Value="Good">Test for an approved response</asp:ListItem>
        <asp:ListItem Value="Decline">Test for a declined response</asp:ListItem>
        <asp:ListItem Value="Duplicate">Test for a duplicate response</asp:ListItem>
    </asp:DropDownList>
</div>
<uc1:StoreMultiSelect ID="uxStoreSelect" runat="server" PaymentName="LinkPoint" />

