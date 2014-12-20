<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PromotionItem.ascx.cs" Inherits="Mobile_Components_PromotionItem" %>
<%@ Register Src="PromotionProductGroup.ascx" TagName="ProductGroup" TagPrefix="uc1" %>
<div class="MobilePromotionItem">
    <div class="SidebarTop">
        <asp:Label ID="uxPromotionTitle" runat="server" Text="[$Promotion]"></asp:Label>
    </div>
    <div class="SidebarLeft">
        <div class="SidebarRight">
            <div class="Content">
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
                        <div class="TellFriend">
                        	<asp:LinkButton ID="uxTellFriendLinkButton" runat="server" Text="Tell a Friend" OnClick="uxTellFriendLinkButton_Click"
            					Visible='<%# Vevo.Domain.DataAccessContext.Configurations.GetBoolValue( "TellAFriendEnabled" ) %>' CssClass="MobileButton" />
                        </div>
                        <div class="FooterItem">
                            <asp:LinkButton ID="uxAddToCartButton" runat="server" Text="Add to Cart" OnClick="uxAddToCartButton_Click"
                                CssClass="MobileButton" Visible='<%# !IsCatalogMode() && IsAuthorizedToViewPrice() %>' />
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