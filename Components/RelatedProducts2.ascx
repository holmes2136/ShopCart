<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RelatedProducts2.ascx.cs"
    Inherits="Components_RelatedProducts2" %>
<%@ Import Namespace="Vevo" %>
<%@ Register Src="DotLine.ascx" TagName="DotLine" TagPrefix="uc1" %>
<%@ Register Src="CatalogImage.ascx" TagName="CatalogImage" TagPrefix="uc2" %>
<div class="RelatedProducts">
    <div class="RelatedProductsTitleLeft">
        <div class="RelatedProductsTitleRight">
            [$Title]</div>
    </div>
    <div class="RelatedProductsDiv">
        <div class="RelatedProductsTop">
            <div class="RelatedProductsTopLeft">
            </div>
            <div id="P1" runat="server" class="RelatedProductsTopTitle">
                [$HeaderRelatedProducts]
            </div>
            <div class="RelatedProductsTopRight">
            </div>
            <div class="Clear">
            </div>
        </div>
        <div class="RelatedProductsLeft">
            <div class="RelatedProductsRight">
                <asp:DataList ID="uxRelatedList" runat="server" RepeatColumns="1" RepeatDirection="Horizontal"
                    CssClass="RelatedProductsDatalist">
                    <ItemTemplate>
                        <table class="RelatedProductsItemTable">
                            <tr>
                                <td class="RelatedProductsImageColumn">
                                    <asp:HyperLink ID="uxImageLink" runat="server" NavigateUrl='<%# Vevo.UrlManager.GetProductUrl( Eval( "ProductID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'
                                        CssClass="RelatedProductsImageLink">
                                        <uc2:CatalogImage ID="uxCatalogImage" runat="server" ImageUrl='<%# Eval("ImageSecondary").ToString() %>'
                                            MaximumWidth="100px" />
                                    </asp:HyperLink>
                                </td>
                                <td class="RelatedProductsDetailsColumn">
                                    <div class="RelatedProductsDetailsNameDiv">
                                        <asp:HyperLink ID="uxNameLink" runat="server" CssClass="RelatedProductsNameLink"
                                            NavigateUrl='<%# Vevo.UrlManager.GetProductUrl( Eval( "ProductID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'> 
                            <%# Eval("Name") %> 
                                        </asp:HyperLink></div>
                                    <div class="RelatedProductsDetailsPriceDiv" runat="server" visible='<%# IsAuthorizedToViewPrice() %>'>
                                        <%# GetFormattedPriceFromContainer( Container ) %>
                                    </div>
                                    <div class="RelatedProductsDetailStockDiv">
                                        <asp:Label ID="uxStockLabel" runat="server" ForeColor="Red" Text='<%# Vevo.CatalogUtilities.IsOutOfStock( Convert.ToInt32( Eval( "SumStock" ) ) , Convert.ToBoolean(Eval("UseInventory"))) ? "This product is out of stock " : "" %>'></asp:Label></div>
                                    <div class="Clear">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                    <ItemStyle CssClass="RelatedProductsDatalistItemStyle" />
                </asp:DataList>
            </div>
        </div>
        <div class="RelatedProductsBottom">
            <div class="RelatedProductsBottomLeft">
            </div>
            <div class="RelatedProductsBottomRight">
            </div>
            <div class="Clear">
            </div>
        </div>
    </div>
</div>
