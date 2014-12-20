<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DepartmentSiteMap.ascx.cs"
    Inherits="Components_DepartmentSiteMap" %>
<div class="StoreSiteMapDepartmentTop">
    <asp:Image ID="uxProductTopLeftImage" ImageUrl="~/Images/Design/Box/StoreSiteMapProductTopLeft.gif"
        runat="server" CssClass="StoreSiteMapDepartmentTopImgLeft" />
    <asp:Label ID="uxStoreSiteMapDepartmentTitleLabel" runat="server" Text="[$DepartmentsHead]"
        CssClass="StoreSiteMapDepartmentTopTitle"></asp:Label>
    <asp:Image ID="uxProductTopRightImage" ImageUrl="~/Images/Design/Box/StoreSiteMapProductTopRight.gif"
        runat="server" CssClass="StoreSiteMapDepartmentTopImgLeft" />
    <div class="Clear">
    </div>
</div>
<div class="StoreSiteMapDepartmentLeft">
    <div class="StoreSiteMapDepartmentRight">
        <asp:DataList ID="uxDepartmentDataList" runat="server" RepeatDirection="Horizontal"
            RepeatColumns="2" OnItemDataBound="uxDepartmentDataList_ItemDataBound" CssClass="StoreSiteMapDepartmentDataList">
            <ItemTemplate>
                <div class="StoreSiteMapDepartmentItemDiv">
                    <div class="StoreSiteMapDepartmentItemTop">
                        <asp:Image ID="uxStoreSiteMapDepartmentTopImageLeft" ImageUrl="~/Images/Design/Box/StoreSiteMapProductItemTopLeft.gif"
                            runat="server" />
                        <asp:Panel ID="uxBreadcrumbPanel" runat="server" CssClass="StoreSiteMapDepartmentItemTopBreadcrumbPanel">
                        </asp:Panel>
                        <asp:Image ID="uxStoreSiteMapDepartmentTopImageRight" ImageUrl="~/Images/Design/Box/StoreSiteMapProductItemTopRight.gif"
                            runat="server" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="StoreSiteMapDepartmentItemLeft">
                        <div class="StoreSiteMapDepartmentItemRight">
                            <asp:Repeater ID="uxDepartmentItemRepeater" runat="server">
                                <HeaderTemplate>
                                    <ul class="StoreSiteMapDepartmentList">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <li class="StoreSiteMapDepartmentListItem">
                                        <asp:HyperLink ID="uxLink" runat="server" NavigateUrl='<%# Vevo.UrlManager.GetProductUrl( Eval( "ProductID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'>
                                                            <%# Eval("Name") %> 
                                        </asp:HyperLink></li>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </ul></FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <div class="StoreSiteMapDepartmentItemBottom">
                        <asp:Image ID="uxProductItemBottomLeftImage" ImageUrl="~/Images/Design/Box/StoreSiteMapProductItemBottomLeft.gif"
                            runat="server" />
                        <asp:Image ID="uxProductItemBottomRightImage" ImageUrl="~/Images/Design/Box/StoreSiteMapProductItemBottomRight.gif"
                            runat="server" />
                        <div class="Clear">
                        </div>
                    </div>
                </div>
            </ItemTemplate>
            <ItemStyle CssClass="StoreSiteMapDepartmentDataListItemStyle" />
        </asp:DataList>
    </div>
</div>
<div class="StoreSiteMapDepartmentBottom">
    <asp:Image ID="uxProductBottomLeftImage" ImageUrl="~/Images/Design/Box/StoreSiteMapProductBottomLeft.gif"
        runat="server" />
    <asp:Image ID="uxProductBottomRightImage" ImageUrl="~/Images/Design/Box/StoreSiteMapProductBottomRight.gif"
        runat="server" />
    <div class="Clear">
    </div>
</div>
