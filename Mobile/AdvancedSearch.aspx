<%@ Page Language="C#" MasterPageFile="~/Mobile/Mobile.master" AutoEventWireup="true"
    CodeFile="AdvancedSearch.aspx.cs" Inherits="Mobile_AdvancedSearch" Title="[$Title]" %>

<asp:Content ID="Content1" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="MobileTitle">
        <asp:Label ID="uxDefaultTitle" runat="server">
        [$Head]</asp:Label>
    </div>
    <div class="MobileCommonBox">
        <asp:Label ID="uxNameTitle" runat="server" Text="[$Name]" CssClass="MobileAdvancedSearchLabel" />
        <asp:TextBox ID="uxKeywordText" runat="server" MaxLength="50" CssClass="MobileAdvancedSearchText" />
        <div class="MobileAdvancedSearchPriceDiv">
            <div class="MobileAdvancedSearchLabel">
                <asp:Label ID="uxPriceTitle" runat="server" Text="[$Price]" />
            </div>
            <div class="MobileAdvancedSearchPriceText">
                <asp:TextBox ID="uxPrice1Text" runat="server" MaxLength="50" CssClass="MobileAdvancedSearchPriceText MobileAdvancedSearchPriceBox" />
                <asp:CompareValidator ID="uxPrice1Compare" runat="server" ControlToValidate="uxPrice1Text"
                    Operator="DataTypeCheck" Type="Currency" ValidationGroup="ValidSearch" Display="Dynamic"
                    CssClass="MobileCommonValidatorText MobileAdvancedSearchValidatorText">
                <div class="MobileCommonValidateDiv MobileAdvancedSearchValidateDiv"></div>
                <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Price is invalid.
                </asp:CompareValidator>
            </div>
            <div class="MobileAdvancedSearchToLabel">
                <asp:Label ID="uxPriceTo" runat="server" Text="[$To]" CssClass="MobileAdvancedSearchPriceLabel" />
            </div>
            <div class="MobileAdvancedSearchPriceText">
                <asp:TextBox ID="uxPrice2Text" runat="server" MaxLength="50" CssClass="MobileAdvancedSearchPriceText MobileAdvancedSearchPriceBox" />
                <asp:CompareValidator ID="uxPrice2Compare" runat="server" ControlToValidate="uxPrice2Text"
                    Operator="DataTypeCheck" Type="Currency" ValidationGroup="ValidSearch" Display="Dynamic"
                    CssClass="MobileCommonValidatorText MobileAdvancedSearchValidatorText">
                <div class="MobileCommonValidateDiv MobileAdvancedSearchValidateDiv"></div>
                <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Price is invalid.
                </asp:CompareValidator>
            </div>
        </div>
        <div class="MobileUserLoginControlPanel">
            <asp:LinkButton ID="uxSearchButton" runat="server" Text="[$SearchSubmit]" OnClick="uxSearchButton_Click"
                CssClass="MobileButton MobileAdvancedSearchButton" ValidationGroup="ValidSearch" />
        </div>
    </div>
</asp:Content>
