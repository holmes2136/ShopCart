<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="Product.aspx.cs"
    Inherits="ProductPage" Title="[$Product Detail Title]" ValidateRequest="false" %>

<%@ Register Src="Components/CatalogBreadcrumb.ascx" TagName="CatalogBreadcrumb"
    TagPrefix="uc1" %>
<%@ Import Namespace="Vevo.Domain" %>
<%@ Import Namespace="Vevo.Domain.Products" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="Product">
        <div class="CommonPage">
            <div class="CommonPageTop">
                <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/DefaultBoxTopLeft.gif" runat="server"
                    CssClass="CommonPageTopImgLeft" />
                <uc1:CatalogBreadcrumb ID="uxCatalogBreadcrumb" runat="server" />
                <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/DefaultBoxTopRight.gif"
                    runat="server" CssClass="CommonPageTopImgRight" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonPageLeft">
                <div class="CommonPageRight">
                    <asp:FormView ID="uxProductFormView" runat="server" DataSourceID="uxProductDetailsSource"
                        OnDataBinding="uxProductFormView_DataBinding" OnDataBound="uxProductFormView_DataBound"
                        CssClass="ProductFormView" OnItemCreated="ProductItemCreate">
                        <ItemTemplate>
                        </ItemTemplate>
                    </asp:FormView>
                </div>
            </div>
            <div class="CommonPageBottom">
                <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/DefaultBoxBottomLeft.gif"
                    runat="server" CssClass="CommonPageBottomImgLeft" />
                <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/DefaultBoxBottomRight.gif"
                    runat="server" CssClass="CommonPageBottomImgRight" />
                <div class="Clear">
                </div>
            </div>
            <asp:ObjectDataSource ID="uxProductDetailsSource" runat="server" SelectMethod="GetOne"
                TypeName="Vevo.Data.ProductRepository" OnSelecting="uxProductDetailsSource_Selecting">
                <SelectParameters>
                    <asp:Parameter Name="culture" Type="Object" />
                    <asp:Parameter Name="productID" Type="Int32" />
                    <asp:Parameter Name="storeID" Type="string" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
    </div>
</asp:Content>
