<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShippingAddressItemDetails.ascx.cs"
    Inherits="Mobile_Components_ShippingAddressItemDetails" %>
<%@ Register Src="~/Components/CountryAndStateList.ascx" TagName="CountryAndState"
    TagPrefix="uc1" %>
<%@ Register Src="~/Components/VevoLinkButton.ascx" TagName="LinkButton" TagPrefix="ucLinkButton" %>
<asp:Panel ID="uxShippingInfoPanel" runat="server" CssClass="MobileUserLoginControlPanel">
    <div class="MobileCommonPageInnerTitle">
        <asp:Label ID="uxShippingAliasName" runat="server" Text='<%# Eval( "AliasName" )%>'>
        </asp:Label></div>
    <div class="MobileUserLoginControl">
        <div class="MobileCommonFormLabel">
            <asp:Label ID="uxShippingFirstName" runat="server" Text='<%# Eval( "FirstName" )%>'
                CssClass="">
            </asp:Label>

            <asp:Label ID="uxShippingLastName" runat="server" Text='<%# Eval( "LastName" )%>'
                CssClass="">
            </asp:Label>
        </div>
    </div>
    <div class="MobileUserLoginControl">
        <div class="MobileCommonFormLabel">
            <asp:Label ID="uxShippingCompany" runat="server" Text='<%# Eval( "Company" )%>'
                CssClass="">
            </asp:Label>
        </div>
        <div class="MobileUserLoginControl">
        </div>
        <div class="MobileCommonFormLabel">
            <asp:Label ID="uxShippingAddress1" runat="server" Text='<%# Eval( "Address1" )%>'
                CssClass="">
            </asp:Label>
        </div>
    </div>
    <div class="MobileUserLoginControl">
        <div class="MobileCommonFormLabel">
            <asp:Label ID="uxShippingAddress2" runat="server" Text='<%# Eval( "Address2" )%>'
                CssClass="">
            </asp:Label>
        </div>
    </div>
    <div class="MobileUserLoginControl">
        <div class="MobileCommonFormLabel">
            <asp:Label ID="uxShippingCity" runat="server" Text='<%# Eval( "City" )%>' CssClass="">
            </asp:Label>
            ,
            <asp:Label ID="uxShippingState" runat="server" Text='<%# Eval( "State" )%>' CssClass="">
            </asp:Label>
            ,
            <asp:Label ID="uxShippingZip" runat="server" Text='<%# Eval( "Zip" )%>' CssClass="">
            </asp:Label>
        </div>
    </div>
    <div class="MobileUserLoginControl">
        <div class="MobileCommonFormLabel">
            <asp:Label ID="uxShippingCountry" runat="server" Text='<%# GetCountry(Eval( "Country" ))%>' CssClass="">
            </asp:Label>
        </div>
    </div>
    <div class="MobileUserLoginControl">
        <div class="MobileCommonFormLabel">
            [$ShippPhone]

            <asp:Label ID="uxShippingPhone" runat="server" Text='<%# Eval( "Phone" )%>' CssClass="">
            </asp:Label>
        </div>
    </div>
    <div class="MobileUserLoginControl">
        <div class="MobileCommonFormLabel">
            [$ShippFax]

            <asp:Label ID="uxShippingFax" runat="server" Text='<%# Eval( "Fax" )%>' CssClass="">
            </asp:Label>
        </div>
    </div>
</asp:Panel>
<asp:Panel ID="uxShippingResidentialPanel" runat="server" CssClass="MobileUserLoginControlPanel">
    <div class="MobileCommonFormLabel">
        [$ShippingResidential]</div>
    <div class="MobileCommonFormData">
        <asp:DropDownList ID="uxShippingResidentialDrop" runat="server">
            <asp:ListItem Value="True" Selected="True">[$Yes]</asp:ListItem>
            <asp:ListItem Value="False">[$No]</asp:ListItem>
        </asp:DropDownList>
    </div>
</asp:Panel>

<div class="MobileShippingAddressLinkPanel">
    <table class="MobileShoppingCartTable" cellpadding="0" cellspacing="0">
        <tr>
            <td class="MobileShoppingCartButtonDiv">
                <asp:LinkButton ID="uxEditButton" runat="server" Text="[$Edit]" CssClass="MobileButton MobileShoppingCartButton"
                    OnClick="uxEditButton_Click" />
            </td>
        </tr>
        <tr>
            <td class="MobileShoppingCartButtonDiv">
                <asp:LinkButton ID="uxDeleteButton" runat="server" Text="[$Delete]" CssClass="MobileButton MobileShoppingCartButton"
                    OnClick="uxDeleteButton_Click" />
            </td>
        </tr>
    </table>
</div>
<div class="Clear">
</div>
