<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile/Mobile.master" AutoEventWireup="true"
    CodeFile="Product.aspx.cs" Inherits="Mobile_Product" %>

<%@ Import Namespace="Vevo.Domain" %>
<%@ Import Namespace="Vevo.Domain.Products" %>
<asp:Content ID="Content1" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <asp:FormView ID="uxProductFormView" runat="server" DataSourceID="uxProductDetailsSource"
        OnDataBinding="uxProductFormView_DataBinding" OnDataBound="uxProductFormView_DataBound"
        CssClass="ProductFormView" OnItemCreated="ProductItemCreate">
        <ItemTemplate>
        </ItemTemplate>
    </asp:FormView>
    <asp:ObjectDataSource ID="uxProductDetailsSource" runat="server" SelectMethod="GetOne"
        TypeName="Vevo.Data.ProductRepository" OnSelecting="uxProductDetailsSource_Selecting">
        <SelectParameters>
            <asp:Parameter Name="culture" Type="Object" />
            <asp:Parameter Name="productID" Type="Int32" />
            <asp:Parameter Name="storeID" Type="string" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
