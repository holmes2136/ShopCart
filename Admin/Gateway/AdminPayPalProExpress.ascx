<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminPayPalProExpress.ascx.cs"
    Inherits="AdminAdvanced_Gateway_AdminPayPalProExpress" %>
<%@ Register Src="Components/StoreMultiSelect.ascx" TagName="StoreMultiSelect" TagPrefix="uc1" %>
<div class="CommonRowStyle">
    <div class="Label">
        <asp:Label ID="lcUserName" runat="server" meta:resourcekey="lcUser" /></div>
    <asp:TextBox ID="uxUserNameText" runat="server" Width="250px" CssClass="fl TextBox" />
    <div class="Clear">
    </div>
</div>
<div class="CommonRowStyle">
    <div class="Label">
        <asp:Label ID="lcPassword" runat="server" meta:resourcekey="lcPassword" /></div>
    <asp:TextBox ID="uxPasswordText" runat="server" Width="250px" CssClass="fl TextBox" />
    <div class="Clear">
    </div>
</div>
<div class="CommonRowStyle">
    <div class="Label">
        <asp:Label ID="lcSignature" runat="server" meta:resourcekey="lcSignature" /></div>
    <asp:TextBox ID="uxSignatureText" runat="server" Width="250px" CssClass="fl TextBox" />
    <div class="Clear">
    </div>
</div>
<div class="CommonRowStyle">
    <div class="Label">
        <asp:Label ID="lcEnvironment" runat="server" meta:resourcekey="lcEnvironment" /></div>
    <asp:DropDownList ID="uxEnvironmentDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="live">Live</asp:ListItem>
        <asp:ListItem Value="sandbox">Sandbox</asp:ListItem>
    </asp:DropDownList>
    <div class="Clear">
    </div>
</div>
<uc1:StoreMultiSelect ID="uxStoreSelect" runat="server" PaymentName="PayPal Pro Express" />
