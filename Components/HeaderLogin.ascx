<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HeaderLogin.ascx.cs" Inherits="Components_HeaderLogin" %>
<div class="HeaderLogin">
    <asp:LoginView ID="uxLoginView" runat="Server">
        <AnonymousTemplate>
            <span class="HeaderLoginLoginName1">Hello, </span>
            <asp:LoginStatus ID="uxLoginStatus" runat="server" CssClass="HeaderLoginLoginName1"
                LogoutText="[$LogOut]" LoginText="[$Login]" OnLoggedOut="uxLoginStatus_LoggedOut" />
            <span class="HeaderLoginLoginName1">/</span>
            <asp:HyperLink ID="uxRegister" runat="server" CssClass="HeaderLoginLoginName1Regis"
                NavigateUrl="~/Register.aspx">[$Register]</asp:HyperLink>
            <span class="HeaderLoginLoginName2">|</span>
            <asp:HyperLink ID="uxMyAccount" runat="server" CssClass="HeaderLoginLoginName" NavigateUrl="~/accountdashboard.aspx">[$My Account]</asp:HyperLink>
            <asp:Panel ID="uxWishlistAnonymousDiv" runat="server" CssClass="HeaderLoginWishlist">
                <span class="HeaderLoginLoginName2">|</span>
                <asp:HyperLink ID="uxWishlistLink" runat="server" CssClass="HeaderLoginLoginName"
                    NavigateUrl="../WishList.aspx">[$Wish List]</asp:HyperLink>
            </asp:Panel>
        </AnonymousTemplate>
        <LoggedInTemplate>
            <asp:LoginName ID="uxNameLogin" runat="server" CssClass="HeaderLoginLoginName NameLabel"
                FormatString="Hello, <label class='Username'>{0}</label>" />
            <span class="HeaderLoginLoginName2">|</span>
            <asp:HyperLink ID="uxMyAccount1" runat="server" CssClass="HeaderLoginLoginName NameLabel">[$My Account]</asp:HyperLink>
            <asp:Panel ID="uxWishlistLoggedInDiv" runat="server" CssClass="HeaderLoginWishlist">
                <span class="HeaderLoginLoginName2">|</span>
                <asp:HyperLink ID="uxWishlistLink2" runat="server" CssClass="HeaderLoginLoginName NameLabel"
                    NavigateUrl="../WishList.aspx">[$Wish List]</asp:HyperLink></asp:Panel>
            <span class="HeaderLoginLoginName2">|</span>
            <asp:LoginStatus ID="uxLoginStatus" runat="server" CssClass="HeaderLoginLoginName NameStatus"
                LogoutText="[$LogOut]" LoginText="[$Login]" OnLoggedOut="uxLoginStatus_LoggedOut" />
        </LoggedInTemplate>
        <RoleGroups>
            <asp:RoleGroup Roles="Affiliates">
                <ContentTemplate>
                    <asp:LoginName ID="uxNameLogin" runat="server" CssClass="HeaderLoginLoginName NameLabel"
                        FormatString="Hello, <label class='Username'>{0}</label>" />
                    <span class="HeaderLoginLoginName2">|</span>
                    <asp:HyperLink ID="uxMyAccount1" runat="server" CssClass="HeaderLoginLoginName NameLabel">[$My Account]</asp:HyperLink>
                    <span class="HeaderLoginLoginName2">|</span>
                    <asp:LoginStatus ID="uxLoginStatus" runat="server" CssClass="HeaderLoginLoginName NameStatus"
                        LogoutText="[$LogOut]" LoginText="[$Login]" OnLoggedOut="uxLoginStatus_LoggedOut" />
                </ContentTemplate>
            </asp:RoleGroup>
        </RoleGroups>
    </asp:LoginView>
</div>
