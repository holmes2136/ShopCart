<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ManufacturerListItemDefault.ascx.cs"
    Inherits="Layouts_ManufacturerLists_Controls_ManufacturerListItemDefault" %>
<%@ Register Src="~/Components/CatalogImage.ascx" TagName="CatalogImage" TagPrefix="uc1" %>
<table class="ManufacturerListItemDefaultTable">
    <tr>
        <td class="ManufacturerListItemDefaultImageColumn">
            <div class="ManufacturerListItemDefaultImageDiv">
                <asp:HyperLink ID="uxImageLink" runat="server" NavigateUrl='<%# Vevo.UrlManager.GetManufacturerUrl( Eval( "ManufacturerID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'>
                    <uc1:CatalogImage ID="CatalogImage1" runat="server" ImageUrl='<%# Eval( "ImageFile" ).ToString() %>'
                        MaximumWidth="150px" Title='<%# Eval( "ImageTitle" ).ToString() %>' AlternateText='<%# Eval( "ImageAlt" ).ToString() %>' />
                </asp:HyperLink>
            </div>
        </td>
    </tr>
    <tr>
        <td class="ManufacturerListItemDefaultNameColumn">
            <div class="ManufacturerListItemDefaultNameDiv">
                <asp:HyperLink ID="uxNameLink" runat="server" NavigateUrl='<%# Vevo.UrlManager.GetManufacturerUrl( Eval( "ManufacturerID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'>
                    <%# Eval("Name") %>
                </asp:HyperLink>
            </div>
        </td>
    </tr>
    <tr>
        <td class="ManufacturerListItemDefaultDescriptionColumn">
            <div class="ManufacturerListItem1DescriptionDiv">
                <%# Eval("Description") %>
            </div>
        </td>
    </tr>
</table>
