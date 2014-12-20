<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MyAccountMenuNavList.ascx.cs"
    Inherits="Components_MyAccountMenuNavList" %>
<div class="MyAccountMenuList">
    <div class="SidebarTop">
        <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/CategoryTopLeft.gif" runat="server"
            CssClass="SidebarTopImgLeft" />
        <asp:Label ID="uxMyAccountMenuListTitle" runat="server" Text="[$AccountTitle]" CssClass="SidebarTopTitle"></asp:Label>
        <asp:LoginView ID="uxLoginViewName" runat="Server">
            <AnonymousTemplate>
                <asp:Label ID="uxAnonymousNameLabel" runat="server" CssClass="MyAccountMenuListLoginName">
[$Welcome] <span class='NameLabel'>[$Visitor]</span>
        
                </asp:Label>
            </AnonymousTemplate>
            <LoggedInTemplate>
                <asp:LoginName ID="uxNameLogin" runat="server" CssClass="MyAccountMenuListLoginName"
                    FormatString="[$Welcome] <span class='NameLabel'>{0}</span>" />
            </LoggedInTemplate>
        </asp:LoginView>
        <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/CategoryTopRight.gif" runat="server"
            CssClass="SidebarTopImgRight" />
        <div class="Clear">
        </div>
    </div>
    <div class="SidebarLeft">
        <div class="SidebarRight">
            <asp:LoginView ID="uxLoginView" runat="Server">
                <LoggedInTemplate>
                    <table class="MyAccountMenuTableList" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <asp:HyperLink ID="uxAccountDashboardLink" runat="server" NavigateUrl="~/accountdashboard.aspx">[$Dashboard]</asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:HyperLink ID="uxAccountInformationLink" runat="server" NavigateUrl="~/accountdetails.aspx">[$Detail]</asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:HyperLink ID="uxShippingAddressBook" runat="server" NavigateUrl="~/shippingaddressbook.aspx">[$AddressBook]</asp:HyperLink>
                            </td>
                        </tr>
                        <tr id="uxRewardPointDiv" runat="server">
                            <td>
                                <asp:HyperLink ID="uxRewardPointLink" runat="server" NavigateUrl="~/rewardPoints.aspx">[$RewardPoint]</asp:HyperLink>
                            </td>
                        </tr>
                        <tr id="uxComparisionListDiv" runat="server">
                            <td>
                                <asp:HyperLink ID="uxComparisionList" runat="server" NavigateUrl="~/comparisonlist.aspx">[$ComparisionList]</asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:HyperLink ID="uxOrderHistoryLink" runat="server" NavigateUrl="~/orderhistory.aspx">[$Order]</asp:HyperLink>
                            </td>
                        </tr>
                        <tr id="uxRmaDiv" runat="server">
                            <td>
                                <asp:HyperLink ID="uxRmaHistoryLink" runat="server" NavigateUrl="~/rmahistory.aspx">[$Rma]</asp:HyperLink>
                            </td>
                        </tr>
                        <tr id="uxSubscriptionDiv" runat="server">
                            <td>
                                <asp:HyperLink ID="uxSubscriptionLink" runat="server" NavigateUrl="~/contentsubscription.aspx">[$ContentSubscriptions]</asp:HyperLink>
                            </td>
                        </tr>
                        <tr id="uxGiftCertificateDiv" runat="server">
                            <td>
                                <asp:HyperLink ID="uxGiftCertificateLink" runat="server" NavigateUrl="~/giftcertificate.aspx">[$GiftCertificate]</asp:HyperLink>
                            </td>
                        </tr>
                        <tr id="uxGiftRegistryDiv" runat="server">
                            <td>
                                <asp:HyperLink ID="uxGiftRegistryLink" runat="server" NavigateUrl="~/giftregistrylist.aspx">[$GiftRegistry]</asp:HyperLink>
                            </td>
                        </tr>
                        <tr id="uxWishlistDiv" runat="server">
                            <td>
                                <asp:HyperLink ID="uxWishListLink" runat="server" NavigateUrl="~/wishlist.aspx">[$WishList]</asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:HyperLink ID="uxChangePassLink" runat="server" NavigateUrl="~/changeuserpassword.aspx">[$ChangePassword]</asp:HyperLink>
                            </td>
                        </tr>
                    </table>
                    <div class="Clear">
                    </div>
                </LoggedInTemplate>
                <AnonymousTemplate>
                    <table class="MyAccountMenuTableList" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <asp:LoginStatus ID="uxLoginStatus" runat="server" LogoutText="[$Logout]" LoginText="[$Login]"
                                    OnLoggedOut="uxLoginStatus_LoggedOut" />
                            </td>
                        </tr>
                        <tr id="uxComparisionListDiv" runat="server">
                            <td>
                                <asp:HyperLink ID="uxComparisionList" runat="server" NavigateUrl="~/comparisonlist.aspx">[$ComparisionList]</asp:HyperLink>
                            </td>
                        </tr>
                    </table>
                    <div class="Clear">
                    </div>
                </AnonymousTemplate>
            </asp:LoginView>
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
