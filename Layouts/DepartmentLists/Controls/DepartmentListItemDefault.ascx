<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DepartmentListItemDefault.ascx.cs"
    Inherits="Layouts_DepartmentLists_Controls_DepartmentListItemDefault" %>
<%@ Register Src="~/Components/CatalogImage.ascx" TagName="CatalogImage" TagPrefix="uc1" %>
<table class="DepartmentListItemDefaultTable">
    <tr>
        <td class="DepartmentListItemDefaultImageColumn">
            <div class="DepartmentListItemDefaultImageDiv">
                <asp:HyperLink ID="uxImageLink" runat="server" NavigateUrl='<%# Vevo.UrlManager.GetDepartmentUrl( Eval( "DepartmentID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'>
                    <uc1:CatalogImage ID="CatalogImage1" runat="server" ImageUrl='<%# Eval( "ImageFile" ).ToString() %>'
                        MaximumWidth="150px" Title='<%# Eval( "ImageTitle" ).ToString() %>' AlternateText='<%# Eval( "ImageAlt" ).ToString() %>' />
                </asp:HyperLink>
            </div>
        </td>
    </tr>
    <tr>
        <td class="DepartmentListItemDefaultNameColumn">
            <div class="DepartmentListItemDefaultNameDiv">
                <asp:HyperLink ID="uxNameLink" runat="server" NavigateUrl='<%# Vevo.UrlManager.GetDepartmentUrl( Eval( "DepartmentID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'>
                    <%# Eval("Name") %>
                </asp:HyperLink>
            </div>
        </td>
    </tr>
    <tr>
        <td class="DepartmentListItemDefaultDescriptionColumn">
            <div class="DepartmentListItem1DescriptionDiv">
                <%# Eval("Description") %>
            </div>
        </td>
    </tr>
</table>
