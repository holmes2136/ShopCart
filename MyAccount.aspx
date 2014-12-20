<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="MyAccount.aspx.cs"
    Inherits="MyAccount" Title="[$Title]" %>

<asp:Content ID="Content" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="MyAccount">
        <div class="CommonPage">
            <div class="CommonPageTop">
                <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/MyAccountBoxTopLeft.gif"
                    runat="server" CssClass="CommonPageImgLeft" />
                <asp:Label ID="uxUserLoginTitle" runat="server" Text="[$Head]" CssClass="CommonPageTopTitle"></asp:Label>
                <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/MyAccountBoxTopRight.gif"
                    runat="server" CssClass="CommonPageImgRight" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonPageLeft">
                <div class="CommonPageRight">
                    <div class="MyAccountInformation">
                        <div class="MyAccountInformationTop">
                            <asp:Image ID="Image3" ImageUrl="~/Images/Design/Box/MyAccountInformationBoxTopLeft.gif"
                                runat="server" CssClass="MyAccountInformationTopImgLeft" />
                            <asp:Label ID="Label1" runat="server" Text="[$AccountTitle]" CssClass="MyAccountInformationTopTitle"></asp:Label>
                            <asp:Image ID="Image4" ImageUrl="~/Images/Design/Box/MyAccountInformationBoxTopRight.gif"
                                runat="server" CssClass="MyAccountInformationTopImgRight" />
                            <div class="Clear">
                            </div>
                        </div>
                        <div class="MyAccountInformationLeft">
                            <div class="MyAccountInformationRight">
                                <ul class="MyAccountInformationList">
                                    <li class="MyAccountInformationListItem">
                                        <asp:LoginStatus ID="uxLoginStatus" runat="server" LogoutText="[$Logout]" LoginText="[$Login]"
                                            OnLoggedOut="uxLoginStatus_LoggedOut" CssClass="MyAccountInformationItemLink" />
                                    </li>
                                    <li class="MyAccountInformationListItem">
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/AccountDetails.aspx"
                                            CssClass="MyAccountInformationItemLink">[$Detail]</asp:HyperLink></li>
                                    <li class="MyAccountInformationListItem">
                                        <asp:HyperLink ID="uxChangePassLink" runat="server" NavigateUrl="~/ChangeUserPassword.aspx"
                                            CssClass="MyAccountInformationItemLink">[$ChangePassword]</asp:HyperLink></li>
                                    <li class="MyAccountInformationListItem">
                                        <asp:HyperLink ID="uxRewardPointLink" runat="server" NavigateUrl="~/RewardPoints.aspx"
                                            CssClass="MyAccountInformationItemLink">[$RewardPoint]</asp:HyperLink>
                                    </li>                                  
                                </ul>
                                <div class="Clear">
                                </div>
                            </div>
                        </div>
                        <div class="MyAccountInformationBottom">
                            <asp:Image ID="Image5" ImageUrl="~/Images/Design/Box/MyAccountInformationBoxBottomLeft.gif"
                                runat="server" CssClass="MyAccountInformationBottomImgLeft" />
                            <asp:Image ID="Image6" ImageUrl="~/Images/Design/Box/MyAccountInformationBoxBottomRight.gif"
                                runat="server" CssClass="MyAccountInformationBottomImgRight" />
                            <div class="Clear">
                            </div>
                        </div>
                    </div>
                    <div class="MyAccountInformation">
                        <div class="MyAccountInformationTop">
                            <asp:Image ID="Image7" ImageUrl="~/Images/Design/Box/MyAccountInformationBoxTopLeft.gif"
                                runat="server" CssClass="MyAccountInformationTopImgLeft" />
                            <asp:Label ID="Label2" runat="server" Text="[$History]" CssClass="MyAccountInformationTopTitle"></asp:Label>
                            <asp:Image ID="Image8" ImageUrl="~/Images/Design/Box/MyAccountInformationBoxTopRight.gif"
                                runat="server" CssClass="MyAccountInformationTopImgRight" />
                            <div class="Clear">
                            </div>
                        </div>
                        <div class="MyAccountInformationLeft">
                            <div class="MyAccountInformationRight">
                                <ul class="MyAccountInformationList">
                                    <li class="MyAccountInformationListItem">
                                        <asp:HyperLink ID="uxOrderHistoryLink" runat="server" NavigateUrl="~/OrderHistory.aspx"
                                            CssClass="MyAccountInformationItemLink">[$Order]</asp:HyperLink>
                                    </li>
                                    <li id="uxRmaDiv" runat="server" class="MyAccountInformationListItem">
                                        <asp:HyperLink ID="uxRmaHistoryLink" runat="server" NavigateUrl="~/RmaHistory.aspx"
                                            CssClass="MyAccountInformationItemLink">[$Rma]</asp:HyperLink>
                                    </li>
                                     <li class="MyAccountInformationListItem">
                                        <asp:HyperLink ID="uxSubscriptionLink" runat="server" NavigateUrl="~/ContentSubscription.aspx"
                                            CssClass="MyAccountInformationItemLink">[$ContentSubscriptions]</asp:HyperLink>
                                    </li>
                                </ul>
                                <div class="Clear">
                                </div>
                            </div>
                        </div>
                        <div class="MyAccountInformationBottom">
                            <asp:Image ID="Image9" ImageUrl="~/Images/Design/Box/MyAccountInformationBoxBottomLeft.gif"
                                runat="server" CssClass="MyAccountInformationBottomImgLeft" />
                            <asp:Image ID="Image10" ImageUrl="~/Images/Design/Box/MyAccountInformationBoxBottomRight.gif"
                                runat="server" CssClass="MyAccountInformationBottomImgRight" />
                            <div class="Clear">
                            </div>
                        </div>
                    </div>
                    <asp:Panel ID="uxGiftPanel" runat="server" CssClass="MyAccountInformation">
                        <div class="MyAccountInformationTop">
                            <asp:Image ID="Image11" ImageUrl="~/Images/Design/Box/MyAccountInformationBoxTopLeft.gif"
                                runat="server" CssClass="MyAccountInformationTopImgLeft" />
                            <asp:Label ID="Label3" runat="server" Text="[$Gift]" CssClass="MyAccountInformationTopTitle"></asp:Label>
                            <asp:Image ID="Image12" ImageUrl="~/Images/Design/Box/MyAccountInformationBoxTopRight.gif"
                                runat="server" CssClass="MyAccountInformationTopImgRight" />
                            <div class="Clear">
                            </div>
                        </div>
                        <div class="MyAccountInformationLeft">
                            <div class="MyAccountInformationRight">
                                <ul class="MyAccountInformationList">
                                    <li id="uxGiftCertificateDiv" runat="server" class="MyAccountInformationListItem">
                                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/GiftCertificate.aspx"
                                            CssClass="MyAccountInformationItemLink">[$GiftCertificate]</asp:HyperLink></li>
                                    <li id="uxGiftRegistryDiv" runat="server" class="MyAccountInformationListItem">
                                        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/GiftRegistryList.aspx"
                                            CssClass="MyAccountInformationItemLink">[$GiftRegistry]</asp:HyperLink></li>
                                </ul>
                                <div class="Clear">
                                </div>
                            </div>
                        </div>
                        <div class="MyAccountInformationBottom">
                            <asp:Image ID="Image13" ImageUrl="~/Images/Design/Box/MyAccountInformationBoxBottomLeft.gif"
                                runat="server" CssClass="MyAccountInformationBottomImgLeft" />
                            <asp:Image ID="Image14" ImageUrl="~/Images/Design/Box/MyAccountInformationBoxBottomRight.gif"
                                runat="server" CssClass="MyAccountInformationBottomImgRight" />
                            <div class="Clear">
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="uxWishlistPanel" runat="server" CssClass="MyAccountInformation">
                        <div class="MyAccountInformationTop">
                            <asp:Image ID="Image15" ImageUrl="~/Images/Design/Box/MyAccountInformationBoxTopLeft.gif"
                                runat="server" CssClass="MyAccountInformationTopImgLeft" />
                            <asp:Label ID="Label4" runat="server" Text="[$WishList]" CssClass="MyAccountInformationTopTitle"></asp:Label>
                            <asp:Image ID="Image16" ImageUrl="~/Images/Design/Box/MyAccountInformationBoxTopRight.gif"
                                runat="server" CssClass="MyAccountInformationTopImgRight" />
                            <div class="Clear">
                            </div>
                        </div>
                        <div class="MyAccountInformationLeft">
                            <div class="MyAccountInformationRight">
                                <ul class="MyAccountInformationList">
                                    <li class="MyAccountInformationListItem">
                                        <asp:HyperLink ID="uxWishListLink" runat="server" NavigateUrl="~/WishList.aspx" CssClass="MyAccountInformationItemLink">[$WishList]</asp:HyperLink></li>
                                </ul>
                                <div class="Clear">
                                </div>
                            </div>
                        </div>
                        <div class="MyAccountInformationBottom">
                            <asp:Image ID="Image17" ImageUrl="~/Images/Design/Box/MyAccountInformationBoxBottomLeft.gif"
                                runat="server" CssClass="MyAccountInformationBottomImgLeft" />
                            <asp:Image ID="Image18" ImageUrl="~/Images/Design/Box/MyAccountInformationBoxBottomRight.gif"
                                runat="server" CssClass="MyAccountInformationBottomImgRight" />
                            <div class="Clear">
                            </div>
                        </div>
                    </asp:Panel>
                    <div class="Clear">
                    </div>
                </div>
            </div>
            <div class="CommonPageBottom">
                <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/MyAccountBoxBottomLeft.gif"
                    runat="server" CssClass="CommonPageBottomImgLeft" />
                <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/MyAccountBoxBottomRight.gif"
                    runat="server" CssClass="CommonPageBottomImgRight" />
                <div class="Clear">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
