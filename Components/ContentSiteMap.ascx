<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ContentSiteMap.ascx.cs"
    Inherits="Components_ContentSiteMap" %>
<asp:DataList ID="uxContentMenuDataList" runat="server" RepeatDirection="Horizontal"
    RepeatColumns="2" OnItemDataBound="uxContentMenuDataList_ItemDataBound" CssClass="StoreSiteMapProductDataList">
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
                    <asp:Repeater ID="uxContentItemRepeater" runat="server">
                        <HeaderTemplate>
                            <ul class="StoreSiteMapProductList">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li class="StoreSiteMapProductListItem">
                                <asp:HyperLink ID="uxLink" runat="server" 
                                NavigateUrl='<%# Vevo.UrlManager.GetContentUrl( Eval( "ContentID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'>
                                <%# Eval("Title") %> 
                                </asp:HyperLink>
                            </li>
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
