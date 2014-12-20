<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoryListItem.ascx.cs"
    Inherits="Components_CategoryListItem" %>
<%@ Register Src="CatalogImage.ascx" TagName="CatalogImage" TagPrefix="uc1" %>
<table class="CategoryListItemTable">
    <tr>
        <td class="CategoryListItemImage">
            <div class="CategoryListItemImageDiv">
                <asp:HyperLink ID="uxImageLink" runat="server" NavigateUrl='<%# Vevo.UrlManager.GetCategoryUrl( Eval( "CategoryID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'>
                    <uc1:CatalogImage ID="CatalogImage1" runat="server" ImageUrl='<%# Eval( "ImageFile" ).ToString() %>'
                        MaximumWidth="150px" />
                </asp:HyperLink>
            </div>
        </td>
    </tr>
    <tr>
        <td class="CategoryListItemName">
            <div class="CategoryListItemNameDiv">
                <asp:HyperLink ID="uxNameLink" runat="server" NavigateUrl='<%# Vevo.UrlManager.GetCategoryUrl( Eval( "CategoryID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'>
                    <%# Eval("Name") %>
                </asp:HyperLink>
            </div>
        </td>
    </tr>
    <tr>
        <td class="CategoryListItemDescription">
            <div class="CategoryListItemDescriptionDiv">
                <%# Eval("Description") %>
            </div>
        </td>
    </tr>
</table>
