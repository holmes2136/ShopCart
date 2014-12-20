<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ContentLayout.ascx.cs"
    Inherits="Components_ContentLayout" %>
<div class="ContentLayout">
    <div class="ContentLayoutTop">
        <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/ContentLayoutTopLeft.gif"
            runat="server" CssClass="ContentLayoutTopImgLeft" />
        <asp:Label ID="uxDefaultTitle" runat="server" CssClass="ContentLayoutTopTitle">
            <asp:Literal ID="uxTitleLiteral" runat="server"></asp:Literal></asp:Label>
        <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/ContentLayoutTopRight.gif"
            runat="server" CssClass="ContentLayoutTopImgRight" />
        <div class="Clear">
        </div>
    </div>
    <div class="ContentLayoutLeft">
        <div class="ContentLayoutRight">
            <asp:Literal ID="uxBodyLiteral" runat="server"></asp:Literal>
            <div class="Clear">
            </div>
            <asp:Panel ID="uxProductSubscriptionListPanel" runat="server">
                <div class="StoreSiteMapProductItemLeft">
                    <div class="StoreSiteMapProductItemRight">
                        <asp:Repeater ID="uxProductRepeater" runat="server">
                            <HeaderTemplate>
                                <ul class="StoreSiteMapProductList">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <li class="StoreSiteMapProductListItem">
                                    <asp:HyperLink ID="uxLink" runat="server" NavigateUrl='<%#  Vevo.UrlManager.GetProductUrl( Eval( "ProductID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'
                                        Text='<%# GetNavName( Eval( "ProductID" ).ToString() )%>'>
                                    </asp:HyperLink>
                                </li>
                            </ItemTemplate>
                            <FooterTemplate>
                                </ul></FooterTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </asp:Panel>
            <div class="Clear">
            </div>
        </div>
    </div>
    <div class="ContentLayoutBottom">
        <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/ContentLayoutBottomLeft.gif"
            runat="server" CssClass="ContentLayoutBottomImgLeft" />
        <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/ContentLayoutBottomRight.gif"
            runat="server" CssClass="ContentLayoutBottomImgRight" />
        <div class="Clear">
        </div>
    </div>
</div>
