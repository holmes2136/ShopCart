<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RelatedProducts.ascx.cs"
    Inherits="Mobile_Components_RelatedProducts" %>
<%@ Import Namespace="Vevo" %>
<%@ Register Src="~/Components/CatalogImage.ascx" TagName="CatalogImage" TagPrefix="uc1" %>
<div class="MobileRelatedProducts">
    <div class="MobileRelatedProductsTitle">
        [$Title]</div>
    <div class="MobileRelatedProductsDiv">
        <div id="P1" runat="server" class="MobileRelatedProductsTopTitle">
            [$HeaderRelatedProducts]
        </div>
    </div>
    <div class="MobileRelatedProductsLeft">
        <div class="MobileRelatedProductsRight">
            <asp:DataList ID="uxRelatedList" runat="server" RepeatColumns="1" RepeatDirection="Horizontal"
                CssClass="MobileRelatedProductsDatalist">
                <ItemTemplate>
                 <asp:HyperLink ID="uxNameLink" runat="server" 
                                        NavigateUrl='<%# Vevo.UrlManager.GetMobileProductUrl( Eval( "ProductID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'> 
                            
                    <table class="MobileRelatedProductsItemTable">
                        <tr>
                            <td class="MobileRelatedProductsImageColumn">

                                    <uc1:CatalogImage ID="uxCatalogImage" runat="server" MaximumWidth="85" ImageUrl='<%# GetRelatedProductsImage( Eval( "ProductID" ).ToString()) %>'>
                                    </uc1:CatalogImage>

                            </td>
                            <td class="MobileRelatedProductsDetailsColumn">
                                <div class="MobileRelatedProductsDetailsNameDiv">
                                   <%# Eval("Name") %> 
                                    </div>
                                <div class="MobileRelatedProductsDetailsPriceDiv">
                                    <%# GetFormattedPriceFromContainer( Container ) %>
                                </div>
                                <div class="MobileRelatedProductsDetailStockDiv">
                                    <asp:Label ID="uxStockLabel" runat="server" ForeColor="Red" Text='<%# Vevo.CatalogUtilities.IsOutOfStock( Convert.ToInt32( Eval( "SumStock" ) ) , Convert.ToBoolean(Eval("UseInventory"))) ? "This product is out of stock " : "" %>'></asp:Label></div>
                                <div class="Clear">
                                </div>
                            </td>
                        </tr>
                    </table>
                    </asp:HyperLink>
                </ItemTemplate>
                <ItemStyle CssClass="MobileRelatedProductsDatalistItemStyle" />
            </asp:DataList>
        </div>
    </div>
    <div class="MobileRelatedProductsBottom">
        <div class="MobileRelatedProductsBottomLeft">
        </div>
        <div class="MobileRelatedProductsBottomRight">
        </div>
        <div class="Clear">
        </div>
    </div>
</div>
