<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShippingMerchantCountry.ascx.cs"
    Inherits="AdminAdvanced_Components_SiteConfig_ShippingMerchantCountry" %>
<%@ Register Src="../Common/HelpIcon.ascx" TagName="HelpIcon" TagPrefix="uc1" %>
<asp:Panel ID="uxPanel" runat="server" CssClass="CommonConfigRow">
    <uc1:HelpIcon ID="uxCountryHelp" ConfigName="ShippingMerchantCountry" runat="server" />
    <asp:Label ID="uxLabel" runat="server" Text="Set Merchant Country" CssClass="Label">
    </asp:Label>
    <asp:DropDownList ID="uxCountryDrop" runat="server" Width="155px" OnSelectedIndexChanged="uxDrop_SelectedIndexChanged"
        AutoPostBack="True" CssClass="DropDown">
    </asp:DropDownList><span id="uxStar" runat="server" class="ValidateText">*</span>
    <asp:TextBox ID="uxCountryText" runat="server" CssClass="TextBox"></asp:TextBox>
    <asp:HiddenField ID="uxSelectedCountryHidden" runat="server" />
</asp:Panel>
