<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategorySiteMap.ascx.cs"
    Inherits="Components_CategorySiteMap" %>
<div class="StoreSiteMapProductTop">
    <asp:Image ID="uxProductTopLeftImage" ImageUrl="~/Images/Design/Box/StoreSiteMapProductTopLeft.gif"
        runat="server" CssClass="StoreSiteMapProductTopImgLeft" />
    <asp:Label ID="uxStoreSiteMapProductTitleLabel" runat="server" Text="[$CategoriesHead]"
        CssClass="StoreSiteMapProductTopTitle"></asp:Label>
    <asp:Image ID="uxProductTopRightImage" ImageUrl="~/Images/Design/Box/StoreSiteMapProductTopRight.gif"
        runat="server" CssClass="StoreSiteMapProductTopImgLeft" />
    <div class="Clear">
    </div>
</div>
<div class="StoreSiteMapProductLeft">
    <div class="StoreSiteMapProductRight">
        <asp:DataList ID="uxProductDataList" runat="server" RepeatDirection="Horizontal"
            RepeatColumns="2" OnItemDataBound="uxProductDataList_ItemDataBound" CssClass="StoreSiteMapProductDataList">
            <ItemTemplate>
                <div class="StoreSiteMapProductItemDiv">
                    <div class="StoreSiteMapProductItemTop">
                        <asp:Image ID="uxStoreSiteMapProductTopImageLeft" ImageUrl="~/Images/Design/Box/StoreSiteMapProductItemTopLeft.gif"
                            runat="server" />
                        <asp:Panel ID="uxBreadcrumbPanel" runat="server" CssClass="StoreSiteMapProductItemTopBreadcrumbPanel">
                        </asp:Panel>
                        <asp:Image ID="uxStoreSiteMapProductTopImageRight" ImageUrl="~/Images/Design/Box/StoreSiteMapProductItemTopRight.gif"
                            runat="server" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="StoreSiteMapProductItemLeft">
                        <div class="StoreSiteMapProductItemRight">
                            <asp:Repeater ID="uxProductItemRepeater" runat="server">
                                <HeaderTemplate>
                                    <ul class="StoreSiteMapProductList">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <li class="StoreSiteMapProductListItem">
                                        <asp:HyperLink ID="uxLink" runat="server" NavigateUrl='<%# Vevo.UrlManager.GetProductUrl( Eval( "ProductID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'>
                                                            <%# Eval("Name") %> 
                                        </asp:HyperLink></li>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </ul></FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <div class="StoreSiteMapProductItemBottom">
                        <asp:Image ID="uxProductItemBottomLeftImage" ImageUrl="~/Images/Design/Box/StoreSiteMapProductItemBottomLeft.gif"
                            runat="server" />
                        <asp:Image ID="uxProductItemBottomRightImage" ImageUrl="~/Images/Design/Box/StoreSiteMapProductItemBottomRight.gif"
                            runat="server" />
                        <div class="Clear">
                        </div>
                    </div>
                </div>
            </ItemTemplate>
            <ItemStyle CssClass="StoreSiteMapProductDataListItemStyle" />
        </asp:DataList>
        <asp:Panel ID="uxCategoryOnlyPanel" runat="server">
            <asp:Repeater ID="uxCategoryItemRepeater" runat="server">
                <HeaderTemplate>
                    <ul class="StoreSiteMapProductList">
                </HeaderTemplate>
                <ItemTemplate>
                    <li class="StoreSiteMapProductListItem">
                        <asp:HyperLink ID="uxCategoryLink" runat="server" NavigateUrl='<%# Vevo.UrlManager.GetCategoryUrl( Eval( "CategoryID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'>
                        	<%# Eval("Name") %> 
                        </asp:HyperLink></li>
                </ItemTemplate>
                <FooterTemplate>
                    </ul></FooterTemplate>
            </asp:Repeater>
        </asp:Panel>
    </div>
</div>
<div class="StoreSiteMapProductBottom">
    <asp:Image ID="uxProductBottomLeftImage" ImageUrl="~/Images/Design/Box/StoreSiteMapProductBottomLeft.gif"
        runat="server" />
    <asp:Image ID="uxProductBottomRightImage" ImageUrl="~/Images/Design/Box/StoreSiteMapProductBottomRight.gif"
        runat="server" />
    <div class="Clear">
    </div>
</div>
