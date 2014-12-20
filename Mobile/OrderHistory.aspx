<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile/Mobile.master" AutoEventWireup="true"
    CodeFile="OrderHistory.aspx.cs" Inherits="Mobile_OrderHistory" %>

<%@ Register Src="Components/SearchFilterNew.ascx" TagName="SearchFilter" TagPrefix="uc4" %>
<%@ Register Src="Components/MobilePagingControl.ascx" TagName="PagingControl" TagPrefix="uc2" %>
<%@ Register Src="~/Components/ItemsPerPageDrop.ascx" TagName="ItemsPerPageDrop"
    TagPrefix="uc3" %>
<%@ Register Src="~/Components/OrderItemDetails.ascx" TagName="OrderItemDetails"
    TagPrefix="uc1" %>
<%@ Import Namespace="Vevo" %>
<%@ Import Namespace="Vevo.Domain" %>
<%@ Import Namespace="Vevo.Domain.Products" %>
<%@ Import Namespace="Vevo.Shared.Utilities" %>
<%@ Import Namespace="Vevo.WebUI" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="MobileTitle">
        [$Head]
    </div>
    <div class="MobileOrderHistoryCommonBox">
        <uc4:SearchFilter ID="uxSearchFilter" runat="server" />
    </div>
    <div class="MobileOrderHistoryListDiv">
        <asp:GridView ID="uxHistoryGrid" runat="server" AutoGenerateColumns="False" CellPadding="4"
            CssClass="MobileOrderHistoryGridView" GridLines="None" AllowSorting="True" OnSorting="uxHistoryGrid_Sorting">
            <RowStyle CssClass="MobileOrderHistoryGridRowStyle" />
            <HeaderStyle CssClass="MobileOrderHistoryGridViewHeaderStyle" />
            <AlternatingRowStyle CssClass="MobileOrderHistoryGridAlternatingRowStyle" />
            <Columns>
                <asp:TemplateField HeaderText="[$OrderID]" SortExpression="OrderID">
                    <ItemTemplate>
                        <asp:HyperLink ID="uxOrderIDLink" runat="server" NavigateUrl='<%# "CheckoutComplete.aspx?OrderID=" + Eval( "OrderID" ) %>'
                            CssClass="MobileOrderHistoryOrderIDLink">
                    <%# Eval( "OrderID" ) %></asp:HyperLink>
                        <span class="MobileOrderHistoryDate">
                            <%# Eval( "OrderDate", "{0:" + SystemConst.FormarDateTime + "}") %></span>
                    </ItemTemplate>
                    <HeaderStyle CssClass="MobileOrderHistoryGridOrderIDHeaderStyle" />
                    <ItemStyle CssClass="MobileOrderHistoryGridOrderIDItemStyle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="[$Total]" SortExpression="Total">
                    <ItemTemplate>
                        <asp:Label ID="uxTotalLabel" runat="server" Text='<%# StoreContext.Currency.FormatPrice( ConvertUtilities.ToDecimal( Eval("Total") ) ) %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="MobileOrderHistoryGridTotalHeaderStyle" />
                    <ItemStyle CssClass="MobileOrderHistoryGridTotalItemStyle" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HyperLink ID="uxReorderLink" runat="server" NavigateUrl='<%# "ProductReOrder.aspx?OrderID=" + Eval( "OrderID" )%>'>[$Reorder]</asp:HyperLink>
                    </ItemTemplate>
                    <ItemStyle CssClass="MobileOrderHistoryGridReOrderItemStyle" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="[$NoData]"></asp:Label>
            </EmptyDataTemplate>
            <EmptyDataRowStyle CssClass="MobileOrderHistoryGridEmptyRowStyle" />
        </asp:GridView>
    </div>
    <div class="MobilePagingControlMainDiv">
        <table cellpadding="0" cellspacing="0" class="MobilePagingControl">
            <tr>
                <uc2:PagingControl ID="uxPagingControl" runat="server" />
            </tr>
        </table>
    </div>
</asp:Content>
