<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Header.ascx.cs" Inherits="Themes_ResponsiveGreen_LayoutControls_Header" %>
<%@ Register Src="~/Components/HeaderLogo.ascx" TagName="HeaderImageLogo" TagPrefix="uc1" %>
<%@ Register Src="../Components/HeaderLogin.ascx" TagName="HeaderLogin" TagPrefix="uc3" %>
<%@ Register Src="~/Components/SwitchLanguage.ascx" TagName="SwitchLanguage" TagPrefix="uc4" %>
<%@ Register Src="~/Components/CurrencyControl.ascx" TagName="CurrencyControl" TagPrefix="uc5" %>
<%@ Register Src="../Components/CurrentShoppingCart.ascx" TagName="CurrentShoppingCart"
    TagPrefix="uc6" %>
<%@ Register Src="../Components/QuickSearch.ascx" TagName="Search" TagPrefix="uc7" %>
<%@ Register Src="~/Components/HeaderMenuResponsive.ascx" TagName="HeaderMenuResponsive"
    TagPrefix="ucMenu" %>
<div class="header-container1">
</div>
<div class="header-container2">
    <div class="row">
        <div class="main-left-2col columns">
            <uc3:HeaderLogin ID="uxHeaderLogin" runat="server" />
            <uc1:HeaderImageLogo ID="uxHeaderImageLogo" runat="server" />
        </div>
        <div class="main-right-2col columns">
            <section class="LanguageSection">
                <uc4:SwitchLanguage ID="uxSwitchLanguage" runat="server" />
                <uc5:CurrencyControl ID="uxCurrencyControl" runat="server" />
            </section>
            <section class="MyAccountSection">
                <asp:Panel ID="uxWishlistAnonymousPanel" runat="server" CssClass="HeaderLoginWishlist">
                    <asp:HyperLink ID="uxWishlistLink" runat="server" CssClass="WishlistLink" NavigateUrl="~/WishList.aspx">
                        [$Wishlist]
                    <asp:Label ID="uxWishListTotalItemText" runat="server" CssClass="WishlistTotalItem" /></asp:HyperLink>
                </asp:Panel>
                <asp:Panel ID="uxMyAccountLinkPanel" runat="server" CssClass="HeaderLoginAccount">
                    <asp:HyperLink ID="uxMyAccountLink" runat="server" CssClass="MyAccountLink">[$MyAccount]</asp:HyperLink>
                </asp:Panel>
            </section>
            <section class="SearchSection">
                <uc7:Search ID="uxSearch" runat="server" />
            </section>
            <section class="ShoppingCartSection">
                <asp:UpdatePanel ID="uxCurrentShoppingCartUpdatePanel" runat="server">
                    <ContentTemplate>
                        <uc6:CurrentShoppingCart ID="uxCurrentShoppingCart" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </section>
        </div>
    </div>
</div>
<div class="header-container3">
    <div class="row">
        <div class="twelve columns">
            <div class="headerMenuNormal">
                <asp:Panel ID="uxHeaderMenuPanel" runat="server">
                </asp:Panel>
            </div>
            <div class="headerMenuResponsive">
                <ucMenu:HeaderMenuResponsive ID="uxHeaderMenuResponsive" runat="server" />
            </div>
        </div>
    </div>
</div>
