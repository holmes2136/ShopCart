<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CountryList.ascx.cs" Inherits="AdminAdvanced_Components_CountryList" %>
<%@ Register Src="Common/HelpIcon.ascx" TagName="HelpIcon" TagPrefix="uc1" %>
<asp:Panel ID="uxPanel" runat="server">
    <asp:Panel ID="uxSubPanel" runat="server" Visible="false">
        <uc1:HelpIcon ID="uxCountryHelp" runat="server" ConfigName="StoreDefaultCountry" />
        <asp:Label ID="uxLabel" runat="server" Text="Store Default Country Dropdown" CssClass="Label">
        </asp:Label>
    </asp:Panel>
    <asp:DropDownList ID="uxCountryDrop" runat="server" Width="155px" OnSelectedIndexChanged="uxDrop_SelectedIndexChanged"
        AutoPostBack="True">
    </asp:DropDownList>
    <span id="uxStar" runat="server" class="ValidateText">*</span>
    <asp:TextBox ID="uxCountryText" runat="server"></asp:TextBox>
    <asp:HiddenField ID="uxSelectedCountryHidden" runat="server" />
</asp:Panel>
