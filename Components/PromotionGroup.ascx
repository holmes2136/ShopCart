<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PromotionGroup.ascx.cs"
    Inherits="Components_PromotionGroup" %>
<div class="PromotionGroup" id="uxPromotionGroupDiv" runat="server">
    <div class="CenterBlockTop">
        <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/ProductBestSellingTopLeft.gif"
            runat="server" CssClass="CenterBlockTopImgLeft" />
        <div class="CenterBlockTopTitle">
            <asp:Label ID="uxProductBestSellingTitle" runat="server" Text="[$PromotionGroups]"></asp:Label>
            <asp:HyperLink ID="uxMoreLink" runat="server" NavigateUrl="~/PromotionGroupList.aspx"
                Text="[$ViewMore]" CssClass="PromotionGroupLinkViewMore" /></div>
        <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/ProductBestSellingTopRight.gif"
            runat="server" CssClass="CenterBlockTopImgRight" />
        <div class="Clear">
        </div>
    </div>
    <div class="CenterBlockLeft">
        <div class="CenterBlockRight">
            <asp:UpdatePanel ID="uxPromotionGroupListUpdate" runat="server" UpdateMode="Conditional"
                ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:DataList ID="uxList" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        CellSpacing="5" CellPadding="5" CssClass="PromotionGroupDatalist" GridLines="None">
                        <ItemTemplate>
                            <table class="PromotionGroupDetailsTable" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td class="PromotionGroupImage">
                                        <asp:HyperLink ID="uxImageLink" runat="server" NavigateUrl='<%# Vevo.UrlManager.GetPromotionUrl( Eval( "PromotionGroupID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'>
                                            <asp:Image ID="uxImage" runat="server" ImageUrl='<%# GetImageUrl(Eval("PromotionGroupID"))%>' />
                                        </asp:HyperLink>
                                    </td>
                                    <td class="PromotionGroupDescription">
                                        <table class="PromotionGroupDetailsTable" cellpadding="" cellspacing="0" border="0">
                                            <tr>
                                                <td class="PromotionGroupDescription">
                                                    <div class="PromotionGroupName">
                                                        <asp:HyperLink ID="uxNameLink" runat="server" CssClass="PromotionGroupNameLink" NavigateUrl='<%# Vevo.UrlManager.GetPromotionUrl( Eval( "PromotionGroupID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'> 
                                                                <%# Eval("Name") %> 
                                                        </asp:HyperLink>
                                                    </div>
                                                    <div class="PromotionGroupShortDescription">
                                                        <%# CreateShortDescritpion( Eval( "Description" ) )%>
                                                        <asp:HyperLink ID="uxSeeMoreLink" runat="server" NavigateUrl='<%# Vevo.UrlManager.GetPromotionUrl( Eval( "PromotionGroupID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'
                                                            Text="..." Visible='<%#IsShowSeeMore(Eval("Description")) %>' CssClass="PromotionGroupSeemore"></asp:HyperLink>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="PromotionGroupPriceDetails">
                                                    <asp:Panel ID="uxRetailPriceTR" runat="server" CssClass="PromotionGroupOurPricePanel"
                                                        Visible='<%# IsAuthorizedToViewPrice() %>'>
                                                        <div class="PromotionGroupOurPriceValue">
                                                            <div>
                                                                <%# Vevo.WebUI.StoreContext.Currency.FormatPrice( Eval( "Price" ) )%>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" valign="bottom">
                                        <asp:Panel ID="uxPromotilTitlePanel" runat="server" CssClass="PromotionGroupTitle" />
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                        <ItemStyle CssClass="PromotionGroupDatalistItemStyle" />
                    </asp:DataList>
                    <div class="Clear">
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="CenterBlockBottom">
        <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/ProductBestSellingBottomLeft.gif"
            runat="server" CssClass="CenterBlockBottomImgLeft" />
        <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/ProductBestSellingBottomRight.gif"
            runat="server" CssClass="CenterBlockBottomImgRight" />
        <div class="Clear">
        </div>
    </div>
</div>
