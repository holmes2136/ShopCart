<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShippingAddressItemDetails.ascx.cs"
    Inherits="Components_ShippingAddressItemDetails" %>
<%@ Register Src="CountryAndStateList.ascx" TagName="CountryAndState" TagPrefix="uc1" %>
<asp:Panel ID="uxShippingInfoPanel" runat="server" CssClass="AccountDetailsShippingItemInfoPanel">
    <div class="ShippingAliasNameTitle">
        <asp:Label ID="uxShippingAliasName" runat="server" Text='<%# Eval( "AliasName" )%>' />
    </div>
    <div class="ShippingAddress">
        <div class="ShippingAddressData">
            <asp:Label ID="uxShippingFirstName" runat="server" CssClass="ShippingAddressLabel"
                Text='<%# Eval( "FirstName" )%>' />
            <asp:Label ID="uxShippingLastName" runat="server" CssClass="ShippingAddressLabel"
                Text='<%# Eval( "LastName" )%>' />
        </div>
        <div class="ShippingAddressData">
            <asp:Label ID="uxShippingCompany" runat="server" Text='<%# Eval( "Company" )%>' />
        </div>
        <div class="ShippingAddressData">
            <asp:Label ID="uxShippingAddress1" runat="server" Text='<%# Eval( "Address1" )%>' />
            <asp:Label ID="uxShippingAddress2" runat="server" Text='<%# Eval( "Address2" )%>' />
        </div>
        <div class="ShippingAddressData">
            <asp:Label ID="uxShippingCity" runat="server" Text='<%# Eval( "City" )%>' />,
            <asp:Label ID="uxState" runat="server" Text='<%# (String.IsNullOrEmpty( Convert.ToString( ( Eval("State") ) ) ) ) ? "" : Eval("State", "&nbsp;{0}") %>' />
        </div>
        <div class="ShippingAddressData">
            <asp:Label ID="uxCountry" runat="server" Text='<%# GetFullCountryName ( Eval( "Country" ).ToString()) %>' />,
            <asp:Label ID="uxShippingZip" runat="server" Text='<%# Eval( "Zip" )%>' />
        </div>
        <div class="ShippingAddressData">
            [$Phone]:
            <asp:Label ID="uxShippingPhone" runat="server" Text='<%# Eval( "Phone" )%>' />
        </div>
        <div class="ShippingAddressData">
            [$Fax]
            <asp:Label ID="uxShippingFax" runat="server" Text='<%# Eval( "Fax" )%>' />
        </div>
        <asp:Panel ID="uxShippingResidentialPanel" runat="server" CssClass="ShippingAddressShippingResidentialPanel">
            <div class="ShippingAddressData">
                [$ShippingResidential]:
                <asp:DropDownList ID="uxShippingResidentialDrop" Enabled="false" runat="server">
                    <asp:ListItem Value="True" Selected="True">[$Yes]</asp:ListItem>
                    <asp:ListItem Value="False">[$No]</asp:ListItem>
                </asp:DropDownList>
            </div>
        </asp:Panel>
    </div>
    <div class="ShippingAddressLinkPanel">
        <asp:LinkButton ID="uxEditButton" runat="server" OnClick="uxEditButton_Click" CssClass="BtnStyle2"
            Text="[$BtnUpdateShippingAddress]" />
        <asp:LinkButton ID="uxDeleteButton" runat="server" OnClick="uxDeleteButton_Click"
            CssClass="BtnStyle2" Text="[$BtnDeleteShippingAddress]" />
    </div>
</asp:Panel>
