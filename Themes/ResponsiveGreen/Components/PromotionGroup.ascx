<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PromotionGroup.ascx.cs"
    Inherits="Themes_ResponsiveGreen_Components_PromotionGroup" %>
<%@ Register Src="~/Components/PromotionAds.ascx" TagName="PromotionAds" TagPrefix="uc" %>
<div class="PromotionGroup" id="uxPromotionGroupDiv" runat="server">
    <div class="CenterBlockTop">
        <div class="CenterBlockTopTitle">
            <asp:Label ID="uxPromotionGroupTitle" runat="server" Text="[$PromotionGroups]"></asp:Label>
        </div>
        <div class="Clear">
        </div>
    </div>
    <div class="CenterBlockLeft">
        <div class="CenterBlockRight">
            <div class="row">
                <div class="default-promotionads-col column paddingleft">
                    <uc:PromotionAds ID="uxPromotionAds" runat="server" />
                </div>
                <div class="default-promotionlist-col column paddingright">
                    <asp:UpdatePanel ID="uxPromotionGroupListUpdate" runat="server" UpdateMode="Conditional"
                        ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <div class="PromotionGroupDataList">
                                <asp:Repeater ID="uxList" runat="server" OnItemDataBound="uxList_ItemDataBound">
                                    <ItemTemplate>
                                        <div class="CommonProductItemStyle">
                                            <div class="CommonProductName">
                                                <asp:HyperLink ID="uxNameLink" runat="server" CssClass="CommonProductNameLink" NavigateUrl='<%# Vevo.UrlManager.GetPromotionUrl( Eval( "PromotionGroupID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'> 
                                                                <%# Eval("Name") %> 
                                                </asp:HyperLink>
                                            </div>
                                            <div class="CommonProductImage PromotionGroupImage">
                                                <asp:Panel ID="uxPromotilTitlePanel" runat="server" CssClass="PromotionGroupTitle" />
                                                <asp:Panel ID="uxImagePanel" runat="server" CssClass="CommonProductImagePanel">
                                                    <table class="CommonProductImage" cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td valign="middle">
                                                                <asp:HyperLink ID="uxImageLink" runat="server" NavigateUrl='<%# Vevo.UrlManager.GetPromotionUrl( Eval( "PromotionGroupID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'
                                                                    CssClass="ProductLink">
                                                                    <asp:Image ID="uxImage" runat="server" ImageUrl='<%# GetImageUrl(Eval("PromotionGroupID"))%>' />
                                                                </asp:HyperLink>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <asp:Panel ID="uxQuickViewButtonPanel" runat="server" CssClass="ViewButtonPanel" Visible='<%# !Vevo.UrlManager.IsMobileDevice(Request) %>'>
                                                        <asp:HyperLink ID="uxPromotionDetailLink" runat="server" Text="View More" CssClass="ViewButton"
                                                            NavigateUrl='<%# Vevo.UrlManager.GetPromotionUrl( Eval( "PromotionGroupID" ).ToString(), Eval( "UrlName" ).ToString() ) %>' />
                                                    </asp:Panel>
                                                </asp:Panel>
                                            </div>
                                            <asp:Panel ID="uxPriceTR" runat="server" Visible='<%# IsAuthorizedToViewPrice() %>'
                                                CssClass="OurPricePanel">
                                                <div class="OurPriceLabel">
                                                    [$BundlePrice]
                                                </div>
                                                <div class="OurPriceValue">
                                                    <%# Vevo.WebUI.StoreContext.Currency.FormatPrice( Eval( "Price" ) )%>
                                                </div>
                                            </asp:Panel>
                                            <div class="CommonProductDescription">
                                                <%# CreateShortDescritpion( Eval( "Description" ) )%>
                                                <asp:HyperLink ID="uxSeeMoreLink" runat="server" NavigateUrl='<%# Vevo.UrlManager.GetPromotionUrl( Eval( "PromotionGroupID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'
                                                    Text="..." Visible='<%#IsShowSeeMore(Eval("Description")) %>' CssClass="PromotionGroupSeemore"></asp:HyperLink>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <div class="CenterBlockBottom">
        <div class="Clear">
        </div>
    </div>
</div>
