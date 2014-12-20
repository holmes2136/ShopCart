<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PromotionItem.ascx.cs"
    Inherits="Components_PromotionItem" %>
<%@ Register Src="PromotionProductGroup.ascx" TagName="ProductGroup" TagPrefix="uc1" %>
<%@ Register Src="~/Components/LikeButton.ascx" TagName="LikeButton" TagPrefix="ucLikeButton" %>
<%@ Register Src="~/Components/AddToCartNotification.ascx" TagName="AddToCartNotification"
    TagPrefix="uc2" %>
<div class="PromotionItem">
    <div class="SidebarTop">
        <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/PromotionTopLeft.gif" runat="server"
            CssClass="SidebarTopImgLeft" />
        <asp:Label ID="uxPromotionTitle" runat="server" Text="[$Promotion]" CssClass="SidebarTopTitle"></asp:Label>
        <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/PromotionTopRight.gif" runat="server"
            CssClass="SidebarTopImgRight" />
        <div class="Clear">
        </div>
    </div>
    <div class="SidebarLeft">
        <div class="SidebarRight">
            <div class="Content">
                <div class="ProductDetailsDefaultLikeButtonDiv">
                    <ucLikeButton:LikeButton ID="uxLikeButton" runat="server" />
                </div>
                <div class="NameLabel">
                    <asp:Label ID="uxNameLabel" runat="server" />
                </div>
                <div class="PriceLabel">
                    <asp:Label ID="uxPriceLabel" runat="server" />
                </div>
                <div class="DiscriptionLabel">
                    <asp:Label ID="uxDescriptionLabel" runat="server" />
                </div>
                <asp:DataList ID="uxList" runat="server" Width="100%">
                    <AlternatingItemTemplate>
                        <div class="AlternatingItem">
                            <asp:Image ID="uxCombineImage" runat="server" ImageUrl="~/Images/Design/Icon/PromotionPlus.gif"
                                ImageAlign="Middle" />
                        </div>
                    </AlternatingItemTemplate>
                    <AlternatingItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemTemplate>
                        <uc1:ProductGroup ID="uxGroup" runat="server" PromotionSubGroupID='<%# Eval( "PromotionSubGroupID" )  %>' />
                    </ItemTemplate>
                    <FooterTemplate>
                        <div class="PromotionDetailsDefaultImageTellFriendImage">
                            <asp:LinkButton ID="uxTellFriend" runat="server" Text="[$BtnTellFriend]" CssClass="BtnStyle4"
                                OnCommand="uxTellFriendButton_Command" />
                        </div>
                        <div class="PromotionDetailsDefaultImageAddtoCart">
                            <asp:LinkButton ID="uxAddToCartButton" runat="server" CssClass="BtnStyle1" Text="[$BtnAddtoCart]"
                                OnClick="uxAddToCartButton_Click" Visible='<%# !IsCatalogMode() && IsAuthorizedToViewPrice() %>' />
                        </div>
                    </FooterTemplate>
                </asp:DataList>
            </div>
        </div>
    </div>
    <div class="SidebarBottom">
        <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/PromotionBottomLeft.gif"
            runat="server" CssClass="SidebarBottomImgLeft" />
        <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/PromotionBottomRight.gif"
            runat="server" CssClass="SidebarBottomImgRight" />
        <div class="Clear">
        </div>
    </div>
</div>
<uc2:AddToCartNotification ID="uxAddToCartNotification" runat="server" />
