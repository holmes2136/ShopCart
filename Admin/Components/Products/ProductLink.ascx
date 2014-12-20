<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductLink.ascx.cs" Inherits="AdminAdvanced_Components_Products_ProductLink" %>
<div>
    <asp:Panel ID="uxAddToCartLinkTR" runat="server" CssClass="ProductDetailsRowTitle mgt10">
        <asp:Label ID="uxAddToCartLinkHeader" runat="server" meta:resourcekey="lcAddToCartLinkHeader" />
    </asp:Panel>
    <div class="ProductDetailsRow">
        <asp:Label ID="uxStoreLabel" runat="server" meta:resourcekey="lcStoreLabel" CssClass="Label" />
        <asp:DropDownList ID="uxStoreDrop" runat="server" CssClass="fl DropDown">
        </asp:DropDownList>
    </div>
    <div class="ProductDetailsRow">
        <asp:Label ID="uxGenerateTypeLabel" runat="server" meta:resourcekey="lcGenerateTypeLabel"
            CssClass="Label" />
        <asp:DropDownList ID="uxGenerateTypeDrop" runat="server" CssClass="fl DropDown">
            <asp:ListItem>A-tag</asp:ListItem>
            <asp:ListItem>iframe</asp:ListItem>
        </asp:DropDownList>
    </div>
    <div class="ProductDetailsRow">
        <asp:Label ID="uxGenerateLinkLabel" runat="server" meta:resourcekey="lcGenerateLinkLabel"
            CssClass="Label" />
        <vevo:AdvanceButton ID="uxGenerateButton" runat="server" meta:resourcekey="uxGenerateButton"
            CssClassBegin="fl" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxGenerateButton_Click" />
    </div>
    <div class="ProductDetailsRow">
        <asp:Label ID="uxLinkLabel" runat="server" meta:resourcekey="lcLinkLabel" CssClass="Label" />
        <asp:TextBox ID="uxLinkText" runat="server" TextMode="MultiLine" Rows="5" Width="210px"
            CssClass="TextBox"></asp:TextBox>
    </div>
</div>
