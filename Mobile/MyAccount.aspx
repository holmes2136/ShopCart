<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile/Mobile.master" AutoEventWireup="true"
    CodeFile="MyAccount.aspx.cs" Inherits="Mobile_MyAccount" %>

<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="MobileTitle">
        [$Head]
    </div>
    <ul class="MobileMenuDefault">
        <li>
            <asp:LoginStatus ID="uxLoginStatus" runat="server" LogoutText="[$Logout]" LoginText="[$Login]"
                OnLoggedOut="uxLoginStatus_LoggedOut" CssClass="MobileHyperLink" />
        </li>
        <li>
            <asp:HyperLink ID="uxAccountDetailsLink" runat="server" NavigateUrl="AccountDetails.aspx"
                CssClass="MobileHyperLink">[$Detail]</asp:HyperLink></li>
        <li>
            <asp:HyperLink ID="uxOrderHistoryLink" runat="server" NavigateUrl="OrderHistory.aspx"
                CssClass="MobileHyperLink">[$Order]</asp:HyperLink></li>
        <li id="uxWishListDiv" runat="server">
            <asp:HyperLink ID="uxWishListLink" runat="server" NavigateUrl="WishList.aspx" CssClass="MobileHyperLink">[$WishList]</asp:HyperLink></li>
    </ul>
</asp:Content>
