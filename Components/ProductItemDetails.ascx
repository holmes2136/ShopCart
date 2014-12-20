<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductItemDetails.ascx.cs"
    Inherits="Components_ProductItemDetails" %>
<%@ Register Src="~/Components/CatalogImage.ascx" TagName="CatalogImage" TagPrefix="uc1" %>
<%@ Register Src="Common/TooltippedText.ascx" TagName="TooltippedText" TagPrefix="uc2" %>
<%@ Register Src="~/Components/AddToCartNotification.ascx" TagName="AddToCartNotification"
    TagPrefix="uc8" %>
<%@ Import Namespace="Vevo" %>
<%@ Import Namespace="Vevo.Shared.Utilities" %>
<%@ Import Namespace="Vevo.WebUI" %>
<table cellpadding="0" cellspacing="0" border="0">
    <tr class="RowDiv">
        <td class="ItemListTD">
            <div class="CompareProductName">
                <asp:Label ID="uxProductNameLabel" runat="server" CssClass="CompareLabel" Visible='<%# ( Eval("ProductID").ToString() == "0" )? true:false  %>'>Name</asp:Label>
                <div class="CompareName">
                    <uc2:TooltippedText ID="uxProductNameTooltippedText" runat="server" MainText='<%# GetDisplayString( Eval( "Name" ).ToString(),35) %> '
                        TooltipText='<%# Eval( "Name" )  %>' Visible='<%#IsTooltipVisible( Eval( "Name" ).ToString(),35)  %>' />
                    <asp:Label ID="uxProductNameValueLabel" runat="server" Visible='<%# !IsTooltipVisible( Eval( "Name" ).ToString(),35)  %>'
                        Text='<%# Eval( "Name" )  %>' />
                </div>
                <asp:Label ID="uxProductIDLabel" runat="server" Visible="false" Text='<%# Eval( "ProductID" )  %>'></asp:Label>
            </div>
        </td>
    </tr>
    <tr class="RowDiv">
        <td class="ItemListTD">
            <div class="CompareProductImage">
                <asp:Label ID="uxProductImage" runat="server" CssClass="CompareLabel" Visible='<%# ( Eval("ProductID").ToString() == "0" )? true:false  %>'>Image</asp:Label>
                <asp:Panel ID="ucImgPanel" runat="server" Visible='<%# ( Eval("ProductID").ToString() == "0" )? false:true  %>'>
                    <asp:HyperLink ID="uxImageLink" runat="server" NavigateUrl='<%# UrlManager.GetProductUrl( Eval( "ProductID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'>
                        <uc1:CatalogImage ID="uxCatalogImage" runat="server" ImageUrl='<%# Eval( "ImageSecondary" ).ToString() %>'
                            MaximumWidth="80px" />
                    </asp:HyperLink>
                </asp:Panel>
            </div>
        </td>
    </tr>
    <tr class="RowDiv">
        <td class="ItemListTD">
            <div class="CompareProductShortDes">
                <asp:Label ID="uxShortDesLabel" runat="server" CssClass="CompareLabel" Visible='<%# ( Eval("ProductID").ToString() == "0" )? true:false  %>'>Short Description</asp:Label>
                <asp:Label ID="uxShortDesValueLabel" runat="server" Visible='<%#!IsTooltipVisible( Eval( "ShortDescription" ).ToString(),100)  %>'
                    Text='<%# Eval( "ShortDescription" )  %>' />
                <uc2:TooltippedText ID="uxShortDesValueTooltippedText" runat="server" TooltipText='<%# Eval( "ShortDescription" )  %>'
                    MainText='<%# GetDisplayString( Eval( "ShortDescription" ).ToString(),100) %> '
                    Visible='<%#IsTooltipVisible( Eval( "ShortDescription" ).ToString(),100)  %>' />
            </div>
        </td>
    </tr>
    <tr class="RowDiv">
        <td class="ItemListTD">
            <div class="CompareProductLongDes">
                <asp:Label ID="uxLongDesLabel" runat="server" CssClass="CompareLabel" Visible='<%# ( Eval("ProductID").ToString() == "0" )? true:false  %>'>Long Description</asp:Label>
                <asp:Label ID="uxLongDesValueLabel" runat="server" Visible='<%# !IsTooltipVisible( Eval( "LongDescription" ).ToString(),145)  %>'
                    Text='<%# Eval( "LongDescription" )  %>' />
                <uc2:TooltippedText ID="uxLongDesValueTooltippedText" runat="server" TooltipText='<%# Eval( "LongDescription" )  %>'
                    MainText='<%# GetDisplayString( Eval( "LongDescription" ).ToString(),145) %> '
                    Visible='<%#IsTooltipVisible( Eval( "LongDescription" ).ToString(),145)  %>' />
            </div>
        </td>
    </tr>
    <tr class="RowDiv">
        <td class="ItemListTD">
            <div class="CompareProductSku">
                <asp:Label ID="uxSkuLabel" runat="server" CssClass="CompareLabel" Visible='<%# ( Eval("ProductID").ToString() == "0" )? true:false  %>'>SKU</asp:Label>
                <asp:Label ID="uxSkuValueLabel" runat="server" Visible='<%# !IsTooltipVisible( Eval( "SKU" ).ToString(),100)  %>'
                    Text='<%# Eval( "SKU" )  %>' />
                <uc2:TooltippedText ID="uxSkuValueTooltippedText" runat="server" TooltipText='<%# Eval( "SKU" )  %>'
                    MainText='<%# GetDisplayString( Eval( "SKU" ).ToString(),100) %> ' Visible='<%# IsTooltipVisible( Eval( "SKU" ).ToString(),100)  %>' />
            </div>
        </td>
    </tr>
    <tr class="RowDiv">
        <td class="ItemListTD">
            <div class="CompareProductRetailPrice">
                <asp:Label ID="uxRetailLabel" runat="server" Text="Retail Price" CssClass="CompareLabel"
                    Visible='<%# ( Eval("ProductID").ToString() == "0" )? true:false  %>'></asp:Label>
                <div id="uxRetailPanal" runat="server" visible='<%# IsRetailPriceEnabled( Eval( "IsFixedPrice" ), Eval( "IsCustomPrice" ), GetRetailPrice( Eval( "ProductID" ) ),  Eval( "IsCallForPrice" ) ) %>'>
                    <asp:Label ID="uxRetailValueLabel" runat="server" Text='<%# GetFormattedRetailPriceFromContainer( Eval( "ProductID" ) ) %> '></asp:Label>
                    <asp:Panel ID="Panel1" runat="server" Visible='<%# IsCallForPrice( Eval( "IsCallForPrice" ) ) %>'>
                        [$CallForPrice]
                    </asp:Panel>
                </div>
            </div>
        </td>
    </tr>
    <tr class="RowDiv">
        <td class="ItemListTD">
            <div class="CompareProductPrice">
                <asp:Label ID="uxPriceLabel" runat="server" Text="Price" CssClass="CompareLabel"
                    Visible='<%# ( Eval("ProductID").ToString() == "0" )? true:false  %>'></asp:Label>
                <div id="uxPricesDisplayPanel" runat="server" visible='<%# ( Eval("ProductID").ToString() == "0" )? false:true  %>'>
                    <div id="uxPricePanal" runat="server" visible='<%# IsFixedPrice( Eval( "IsFixedPrice" ), Eval( "IsCustomPrice" ), Eval( "IsCallForPrice" )  ) %>'>
                        <asp:Label ID="uxPriceValueLabel" runat="server" Text='<%# GetFormattedPriceFromContainer( Eval( "ProductID" ) )%> '></asp:Label>
                        <asp:Label ID="Label6" runat="server" Text="[$TaxIncluded]" CssClass="CommonValidateText"
                            Visible='<%# IsTaxInclude() %>' />
                    </div>
                    <asp:Panel ID="uxCallForPriceTR" runat="server" Visible='<%# IsCallForPrice( Eval( "IsCallForPrice" ) ) %>'>
                        [$CallForPrice]
                    </asp:Panel>
                </div>
            </div>
        </td>
    </tr>
    <tr class="RowDiv">
        <td class="ItemListTD">
            <div class="CompareProductModel">
                <asp:Label ID="uxModelLabel" runat="server" Text="Model" CssClass="CompareLabel"
                    Visible='<%# ( Eval("ProductID").ToString() == "0" )? true:false  %>'></asp:Label>
                <asp:Label ID="uxModelValueLabel" runat="server" Visible='<%# !IsTooltipVisible( Eval( "Model" ).ToString(),100)  %>'
                    Text='<%# Eval( "Model" ) %> '></asp:Label>
                <uc2:TooltippedText ID="uxModelValueTooltippedText" runat="server" TooltipText='<%# Eval( "Model" )  %>'
                    MainText='<%# GetDisplayString( Eval( "Model" ).ToString(),100) %> ' Visible='<%# IsTooltipVisible( Eval( "Model" ).ToString(),100)  %>' />
            </div>
        </td>
    </tr>
    <tr class="RowDiv">
        <td class="ItemListTD">
            <div class="CompareProductWeight">
                <asp:Label ID="uxWeightLabel" runat="server" Text="Weight" CssClass="CompareLabel"
                    Visible='<%# ( Eval("ProductID").ToString() == "0" )? true:false  %>'></asp:Label>
                <asp:Label ID="uxWeightValueLabel" runat="server" Text='<%# Eval( "Weight" ) %> '
                    Visible='<%# ( Eval("ProductID").ToString() == "0" )? false:true  %>'></asp:Label>
            </div>
        </td>
    </tr>
    <tr class="RowDiv">
        <td class="ItemListTD">
            <div class="CompareProductManufacturer">
                <asp:Label ID="uxManufacturerLabel" runat="server" Text="Manufacturer" CssClass="CompareLabel"
                    Visible='<%# ( Eval("ProductID").ToString() == "0" )? true:false  %>'></asp:Label>
                <asp:Label ID="uxManufacturerValueLabel" runat="server" Visible='<%# !IsTooltipVisible( Eval( "Manufacturer" ).ToString(),100)  %>'
                    Text='<%# GetManufacturerName(Eval( "Manufacturer" )) %> '></asp:Label>
                <uc2:TooltippedText ID="uxManufacturerValueTooltippedText" runat="server" TooltipText='<%# Eval( "Manufacturer" )  %>'
                    MainText='<%# GetDisplayString( Eval( "Manufacturer" ).ToString(),100) %> ' Visible='<%# IsTooltipVisible( Eval( "Manufacturer" ).ToString(),100)  %>' />
            </div>
        </td>
    </tr>
    <tr class="RowDiv">
        <td class="ItemListTD">
            <div class="CompareProductManufacturerPartNumber">
                <asp:Label ID="uxManufacturerPartNumberLabel" runat="server" Text="Manufacturer Part Number"
                    CssClass="CompareLabel" Visible='<%# ( Eval("ProductID").ToString() == "0" )? true:false  %>'></asp:Label>
                <asp:Label ID="uxManufacturerPartNumberValueLabel" runat="server" Text='<%#  Eval( "ManufacturerPartNumber" )%> '
                    Visible='<%# ( Eval("ProductID").ToString() == "0" )? false:true  %>'></asp:Label>
            </div>
        </td>
    </tr>
    <tr class="RowDiv">
        <td class="ItemListTD">
            <div class="CompareProductUPC">
                <asp:Label ID="uxUPCLabel" runat="server" Visible='<%# ( Eval("ProductID").ToString() == "0" )? true:false  %>'
                    Text="UPC" CssClass="CompareLabel"></asp:Label>
                <asp:Label ID="uxUPCValueLabel" runat="server" Text='<%# Eval( "UPC" )%> ' Visible='<%# ( Eval("ProductID").ToString() == "0" )? false:true  %>'></asp:Label>
            </div>
        </td>
    </tr>
    <asp:Panel ID="uxSpecificationItemTR" runat="server">
        <asp:Literal ID="uxSpecificationLiteral" runat="server" />
    </asp:Panel>
    <tr class="RowDiv">
        <td>
            <asp:Panel ID="uxAddtoCartPlaceHolder" runat="server" Visible='<%# !CatalogUtilities.IsCatalogMode() %>'
                CssClass="CompareProductAddtoCart">
                <asp:LinkButton ID="uxAddToCartImageButton" runat="server" OnClick="uxAddToCartImageButton_Command"
                    CommandName='<%# Eval( "UrlName" ) %>' CommandArgument='<%# Eval( "ProductID" ) %>'
                    Visible='<%# !(CatalogUtilities.IsOutOfStock( Convert.ToInt32( Eval( "SumStock" ) ) , Convert.ToBoolean(Eval("UseInventory")))) && IsAuthorizedToViewPrice(Eval( "IsCallForPrice" ))%>'
                    ValidationGroup='<%# String.Format( "ProductValid{0}", Eval( "ProductID" ).ToString() ) %>'
                    CssClass="ProductListItemColumn2AddtoCartButton BtnStyle1" Text="[$BtnAddtoCart]" />
            </asp:Panel>
        </td>
    </tr>
</table>
<asp:UpdatePanel ID="uxNotificationUpdate" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <uc8:AddToCartNotification ID="uxAddToCartNotification" runat="server" />
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="uxAddToCartImageButton" />
    </Triggers>
</asp:UpdatePanel>
