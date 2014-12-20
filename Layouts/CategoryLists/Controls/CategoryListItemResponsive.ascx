<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoryListItemResponsive.ascx.cs"
    Inherits="Layouts_CategoryLists_Controls_CategoryListItemResponsive" %>
<%@ Register Src="~/Components/CatalogImage.ascx" TagName="CatalogImage" TagPrefix="uc1" %>
<div class="CommonCategoryItemStyle CategoryListItemDefault3">
    <div class="CommonCategoryImage">
        <asp:Panel ID="uxImagePanel" runat="server" CssClass="CommonCategoryImagePanel">
            <table class="CommonCategoryImage" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td valign="middle">
                        <asp:HyperLink ID="uxProductLink" runat="server" NavigateUrl='<%# Vevo.UrlManager.GetCategoryUrl( Eval( "CategoryID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'
                            CssClass="ProductLink">
                            <uc1:CatalogImage ID="uxCatalogImage" runat="server" ImageUrl='<%# Eval( "ImageFile" ).ToString() %>'
                                MaximumWidth="150px" Title='<%# Eval( "ImageTitle" ).ToString() %>' AlternateText='<%# Eval( "ImageAlt" ).ToString() %>' />
                        </asp:HyperLink>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <div class="CommonCategoryName">
        <asp:HyperLink ID="uxNameLink" runat="server" NavigateUrl='<%# Vevo.UrlManager.GetCategoryUrl( Eval( "CategoryID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'
            CssClass="CommonCategoryNameLink">
                    <%# Eval("Name") %>
        </asp:HyperLink>
    </div>
    <div class="CommonCategoryDescription">
        <%# Eval("Description") %>
    </div>
</div>
