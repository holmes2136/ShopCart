<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HeaderLogin.ascx.cs" Inherits="Themes_ResponsiveGreen_Components_HeaderLogin" %>
<div class="HeaderLogin">
    <asp:LoginView ID="uxLoginView" runat="Server">
        <AnonymousTemplate>
            <span class="HeaderLoginLoginName1">[$WelcomeAnonymous]</span>
            <asp:LoginStatus ID="uxLoginStatus" runat="server" CssClass="HeaderLoginLoginName1"
                LogoutText="[$LogOut]" LoginText="[$Login]" OnLoggedOut="uxLoginStatus_LoggedOut" />
            <span class="HeaderLoginLoginName1">- [$or] - </span>
            <asp:HyperLink ID="uxRegister" runat="server" CssClass="HeaderLoginLoginName1Regis"
                NavigateUrl="~/Register.aspx">[$Register]</asp:HyperLink>
        </AnonymousTemplate>
        <LoggedInTemplate>
            <asp:LoginName ID="uxNameLogin" runat="server" CssClass="HeaderLoginLoginName"
                FormatString="[$Welcome] <span class='NameLabel'>{0}</span>" />
            <asp:LoginStatus ID="uxLoginStatus" runat="server" CssClass="HeaderLoginLoginName"
                LogoutText="[$LogOut]" LoginText="[$Login]" OnLoggedOut="uxLoginStatus_LoggedOut" />
        </LoggedInTemplate>
        <RoleGroups>
            <asp:RoleGroup Roles="Affiliates">
                <ContentTemplate>
                    <asp:LoginName ID="uxNameLogin" runat="server" CssClass="HeaderLoginLoginName"
                        FormatString="[$Welcome] <span class='NameLabel'>{0}</span>" />
                    <asp:LoginStatus ID="uxLoginStatus" runat="server" CssClass="HeaderLoginLoginName NameStatus"
                        LogoutText="[$LogOut]" LoginText="[$Login]" OnLoggedOut="uxLoginStatus_LoggedOut" />
                </ContentTemplate>
            </asp:RoleGroup>
        </RoleGroups>
    </asp:LoginView>
</div>
