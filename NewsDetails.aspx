<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="NewsDetails.aspx.cs"
    Inherits="NewsDetails" Title="[$Title]" %>

<%@ Register Src="Components/LikeButton.ascx" TagName="LikeButton" TagPrefix="ucLikeButton" %>
<%@ Register Src="Components/CatalogBreadcrumb.ascx" TagName="Breadcrumb" TagPrefix="uc1" %>
<%@ Register Src="Components/CatalogImage.ascx" TagName="CatalogImage" TagPrefix="uc2" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <uc1:Breadcrumb ID="uxCatalogBreadcrumb" runat="server" />
    <div class="NewsDetails">
        <div class="CommonPage">
            <div class="CommonPageTop">
                <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/DefaultBoxTopLeft.gif" runat="server"
                    CssClass="CommonPageTopImgLeft" />
                <asp:Label ID="uxTopicLabel" runat="server" CssClass="NewsDetailsTopic" />
                <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/DefaultBoxTopRight.gif"
                    runat="server" CssClass="CommonPageTopImgRight" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonPageLeft">
                <div class="CommonPageRight">
                    <div class="NewsDetailsDiv">
                        <div class="PostedDate">
                            Posted :
                            <asp:Label ID="uxNewsDate" runat="server" />
                        </div>
                        <div class="NewsDetailsData">
                            <div class="NewsDetailsImageDiv">
                                <uc2:CatalogImage ID="uxCatalogImage" runat="server" MaximumWidth="250px" />
                            </div>
                            <div class="NewsDetailsContentDiv">
                                <asp:Literal ID="uxDescriptionLiteral" runat="server" />
                            </div>
                        </div>
                        <div class="Clear">
                        </div>
                        <div class="NewsPagingBox">
                            <div class="NewsLinkToTopDiv">
                                <asp:LinkButton ID="uxGoToTopLink" runat="server" CssClass="ProductLinkToTop" Text="[$GoToTop]" />
                            </div>
                            <div class="NewsSocialButton">
                                <ucLikeButton:LikeButton ID="uxLikeButton" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="CommonPageBottom">
                <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/DefaultBoxBottomLeft.gif"
                    runat="server" CssClass="CommonPageBottomImgLeft" />
                <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/DefaultBoxBottomRight.gif"
                    runat="server" CssClass="CommonPageBottomImgRight" />
                <div class="Clear">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
