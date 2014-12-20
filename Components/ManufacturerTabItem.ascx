<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ManufacturerTabItem.ascx.cs"
    Inherits="Components_ManufacturerTabItem" %>
<%@ Register Src="~/Components/CatalogImage.ascx" TagName="CatalogImage" TagPrefix="uc1" %>
<table class="ManufacturerTabItemDefaultTable">
    <tr>
        <td class="ManufacturerTabItemDefaultImageColumn">
            <div class="ManufacturerTabItemDefaultImageDiv">
                <asp:HyperLink ID="uxImageLink" runat="server" NavigateUrl='<%# Vevo.UrlManager.GetManufacturerUrl( Eval( "ManufacturerID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'>
                    <uc1:CatalogImage ID="CatalogImage1" runat="server" ImageUrl='<%# Eval( "ImageFile" ).ToString() %>'
                        MaximumWidth="400px" Title='<%# Eval( "ImageTitle" ).ToString() %>' AlternateText='<%# Eval( "ImageAlt" ).ToString() %>' />
                </asp:HyperLink>
            </div>
        </td>
    </tr>
</table>
