<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AffiliateMenuNavList.ascx.cs"
    Inherits="Components_AffiliateMenuNavList" %>
<div class="MyAccountMenuList">
    <div class="SidebarTop">
        <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/CategoryTopLeft.gif" runat="server"
            CssClass="SidebarTopImgLeft" />
        <asp:Label ID="uxCategoryNavTitle" runat="server" Text="[$AffiliateTitle]" CssClass="SidebarTopTitle"></asp:Label>
        <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/CategoryTopRight.gif" runat="server"
            CssClass="SidebarTopImgRight" />
        <div class="Clear">
        </div>
    </div>
    <div class="SidebarLeft">
        <div class="SidebarRight">
            <table class="MyAccountMenuTableList" cellpadding="0" cellspacing="0">
                <%--<tr>
                    <td>
                        <asp:LoginStatus ID="uxLoginStatus" runat="server" LogoutText="[$Logout]" LoginText="[$Login]"
                            OnLoggedOut="uxLoginStatus_LoggedOut" CssClass="MyAccountInformationItemLink" />
                    </td>
                </tr>--%>
                <tr>
                    <td>
                        <asp:HyperLink ID="uxAccountDashboard" runat="server" NavigateUrl="~/affiliatedashboard.aspx">[$Dashboard]</asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:HyperLink ID="uxAccountLink" runat="server" NavigateUrl="~/affiliatedetails.aspx">[$Detail]</asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:HyperLink ID="uxCommissionLink" runat="server" NavigateUrl="~/affiliatecommission.aspx">[$Commission]</asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:HyperLink ID="uxGenerateLinkLink" runat="server" NavigateUrl="~/affiliategenerateLink.aspx">[$GenerateLink]</asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:HyperLink ID="uxChangePassLink" runat="server" NavigateUrl="~/changeaffiliateuserpassword.aspx">[$ChangePassword]</asp:HyperLink>
                    </td>
                </tr>
            </table>
            <div class="Clear">
            </div>
        </div>
    </div>
    <div class="SidebarBottom">
        <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/CategoryBottomLeft.gif"
            runat="server" CssClass="SidebarBottomImgLeft" />
        <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/CategoryBottomRight.gif"
            runat="server" CssClass="SidebarBottomImgRight" />
        <div class="Clear">
        </div>
    </div>
</div>
