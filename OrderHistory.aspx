<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="OrderHistory.aspx.cs"
    Inherits="OrderHistory" Title="[$Title]" %>

<%@ Register Src="Components/SearchFilterNew.ascx" TagName="SearchFilter" TagPrefix="uc4" %>
<%@ Register Src="Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc2" %>
<%@ Register Src="Components/ItemsPerPageDrop.ascx" TagName="ItemsPerPageDrop" TagPrefix="uc3" %>
<%@ Register Src="Components/OrderItemDetails.ascx" TagName="OrderItemDetails" TagPrefix="uc1" %>
<%@ Import Namespace="Vevo" %>
<%@ Import Namespace="Vevo.Domain" %>
<%@ Import Namespace="Vevo.Domain.Products" %>
<%@ Import Namespace="Vevo.Shared.Utilities" %>
<%@ Import Namespace="Vevo.WebUI" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="OrderHistory">
        <div class="CommonPage">
            <div class="CommonPageTop">
                <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/DefaultBoxTopLeft.gif" runat="server"
                    CssClass="CommonPageTopImgLeft" />
                <asp:Label ID="uxDefaultTitle" runat="server" CssClass="CommonPageTopTitle">[$Head]</asp:Label>
                <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/DefaultBoxTopRight.gif"
                    runat="server" CssClass="CommonPageTopImgRight" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonPageLeft">
                <div class="CommonPageRight">
                    <uc4:SearchFilter ID="uxSearchFilter" runat="server"></uc4:SearchFilter>
                    <div class="CommonGridViewPageItemDiv">
                        <div class="CommonGridViewItemsPerPageDiv">
                            <uc3:ItemsPerPageDrop ID="uxItemsPerPageDrop" runat="server"></uc3:ItemsPerPageDrop>
                        </div>
                        <div class="CommonGridViewPagingDiv">
                            <uc2:PagingControl ID="uxPagingControl" runat="server" />
                        </div>
                    </div>
                    <div class="OrderHistoryGridviewDiv">
                        <asp:GridView ID="uxHistoryGrid" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            CssClass="CommonGridView OrderHistoryGridView" GridLines="None" AllowSorting="True"
                            OnSorting="uxHistoryGrid_Sorting">
                            <RowStyle CssClass="CommonGridViewRowStyle" />
                            <HeaderStyle CssClass="CommonGridViewHeaderStyle" />
                            <AlternatingRowStyle CssClass="CommonGridViewAlternatingRowStyle" />
                            <EmptyDataRowStyle CssClass="CommonGridViewEmptyRowStyle" />
                            <Columns>
                                <asp:TemplateField HeaderText="[$OrderID]" SortExpression="OrderID">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="uxOrderIDLink" runat="server" Text='<%# Eval( "OrderID" ) %>'
                                            NavigateUrl='<%# "CheckoutComplete.aspx?showorder=true&OrderID=" + Eval( "OrderID" ) %>'
                                            CssClass="CommonHyperLink"></asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="OrderHistoryGridOrderIDHeaderStyle" />
                                    <ItemStyle CssClass="OrderHistoryGridOrderIDItemStyle" />
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="OrderDate" HeaderText="[$OrderDate]">
                                    <ItemTemplate>
                                        <%# Eval( "OrderDate", "{0:" + SystemConst.FormarDateTime + "}" ) %>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="OrderHistoryGridOrderDateHeaderStyle" />
                                    <ItemStyle CssClass="OrderHistoryGridOrderDateItemStyle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="[$Total]" SortExpression="Total">
                                    <ItemTemplate>
                                        <asp:Label ID="uxTotalLabel" runat="server" Text='<%# StoreContext.Currency.FormatPrice( ConvertUtilities.ToDecimal( Eval("Total") ) ) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="OrderHistoryGridTotalHeaderStyle" />
                                    <ItemStyle CssClass="OrderHistoryGridTotalItemStyle" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="TrackingNumber" HeaderText="[$TrackingNumber]" SortExpression="TrackingNumber"
                                    HeaderStyle-CssClass="OrderHistoryGridTrackingHeaderStyle" ItemStyle-CssClass="OrderHistoryGridTrackingItemStyle">
                                    <HeaderStyle CssClass="OrderHistoryGridTrackingHeaderStyle"></HeaderStyle>
                                    <ItemStyle CssClass="OrderHistoryGridTrackingItemStyle"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="uxReorderLink" Visible='<%# Eval("ContainsProductOnly")%>' runat="server"
                                            CssClass="CommonHyperLink" NavigateUrl='<%# "ProductReOrder.aspx?OrderID=" + Eval( "OrderID" )%>'>[$Reorder]</asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="OrderHistoryGridReOrderHeaderStyle" />
                                    <ItemStyle CssClass="OrderHistoryGridReOrderItemStyle" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="uxRmaLink" runat="server" CssClass="CommonHyperLink" Visible='<%# IsRmaVisible() %>'
                                            NavigateUrl='<%# "ProductReturn.aspx?OrderID=" + Eval( "OrderID" )%>'>[$RmaLink]</asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="OrderHistoryGridRmaHeaderStyle" />
                                    <ItemStyle CssClass="OrderHistoryGridRmaItemStyle" />
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="[$NoData]"></asp:Label>
                            </EmptyDataTemplate>
                            <EmptyDataRowStyle CssClass="CommonGridViewEmptyRowStyle" />
                        </asp:GridView>
                    </div>
                    <div class="Clear">
                    </div>
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
        </div>
    </div>
</asp:Content>
