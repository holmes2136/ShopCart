<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="AffiliateMain.aspx.cs"
    Inherits="AffiliateMain" Title="[$AffiliateMain]" %>

<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="AffiliateMain">
        <div class="CommonPage">
            <div class="CommonPageTop">
                <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/MyAccountBoxTopLeft.gif"
                    runat="server" CssClass="CommonPageTopImgLeft" />
                <asp:Label ID="uxUserLoginTitle" runat="server" CssClass="CommonPageTopTitle">[$Head]</asp:Label>
                <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/MyAccountBoxTopRight.gif"
                    runat="server" CssClass="CommonPageTopImgRight" />
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
                                        <asp:HyperLink ID="uxAccountLink" runat="server" NavigateUrl="~/AffiliateDetails.aspx"
                                            CssClass="MyAccountInformationItemLink">[$Detail]</asp:HyperLink>
                                    </li>
                                    <li class="MyAccountInformationListItem">
                                        <asp:HyperLink ID="uxChangePassLink" runat="server" NavigateUrl="~/ChangeAffiliateUserPassword.aspx"
                                            CssClass="MyAccountInformationItemLink">[$ChangePassword]</asp:HyperLink>
                                    </li>
                                    <li class="MyAccountInformationListItem">
                                        <asp:HyperLink ID="uxCommissionLink" runat="server" NavigateUrl="~/AffiliateCommission.aspx"
                                            CssClass="MyAccountInformationItemLink">[$Commission]</asp:HyperLink>
                                    </li>
                                    <li class="MyAccountInformationListItem">
                                        <asp:HyperLink ID="uxGenerateLinkLink" runat="server" NavigateUrl="~/AffiliateGenerateLink.aspx"
                                            CssClass="MyAccountInformationItemLink">[$GenerateLink]</asp:HyperLink></li>
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
