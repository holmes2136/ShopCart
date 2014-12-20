<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Footer.ascx.cs" Inherits="Blog_Themes_ResponsiveGreen_LayoutControls_Footer" %>
<%@ Register Src="ContentFooter.ascx" TagName="ContentFooter" TagPrefix="uc3" %>
<uc3:ContentFooter ID="uxContentFooter" runat="server" />
<div class="LayoutFooterLicense">
    <div class="row">
        <div class="twelve columns text-center">
            <asp:HyperLink ID="uxShoppingCartLink" runat="server" Target="_blank" NavigateUrl="http://www.vevocart.com">Shopping Cart</asp:HyperLink>
            Software by VevoCart <span class="footer-divider">&copy; 2006-2013 Vevo Systems Co., Ltd. </span>
        </div>
    </div>
</div>
