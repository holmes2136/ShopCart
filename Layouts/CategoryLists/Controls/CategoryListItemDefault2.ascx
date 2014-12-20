<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoryListItemDefault2.ascx.cs"
    Inherits="Layouts_CategoryLists_Controls_CategoryListItemDefault2" %>
<%@ Register Src="~/Components/CatalogImage.ascx" TagName="CatalogImage" TagPrefix="uc1" %>
<table class="CategoryListItemDefaultTable">
    <tr>
        <td class="CategoryListItemDefaultImageColumn">
            <div class="CategoryListItemDefaultImageDiv">
                <asp:HyperLink ID="uxImageLink" runat="server" NavigateUrl='<%# Vevo.UrlManager.GetCategoryUrl( Eval( "CategoryID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'>
                    <uc1:CatalogImage ID="CatalogImage1" runat="server" ImageUrl='<%# Eval( "ImageFile" ).ToString() %>'
                        MaximumWidth="150px" Title='<%# Eval( "ImageTitle" ).ToString() %>' AlternateText='<%# Eval( "ImageAlt" ).ToString() %>'/>
                </asp:HyperLink>
            </div>
        </td>
    </tr>
    <tr>
        <td class="CategoryListItemDefaultNameColumn">
            <div class="CategoryListItemDefaultNameDiv">
                <asp:HyperLink ID="uxNameLink" runat="server" NavigateUrl='<%# Vevo.UrlManager.GetCategoryUrl( Eval( "CategoryID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'>
                    <%# Eval("Name") %>
                </asp:HyperLink>
            </div>
        </td>
    </tr>
    <tr>
        <td class="CategoryListItemDefaultDescriptionColumn">
            <div class="CategoryListItem1DescriptionDiv">
                <%# Eval("Description") %>
            </div>
        </td>
    </tr>
</table>
