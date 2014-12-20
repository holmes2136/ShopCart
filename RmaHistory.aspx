<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="RmaHistory.aspx.cs"
    Inherits="RmaHistory" Title="[$Title]" %>

<%@ Register Src="Components/SearchFilterNew.ascx" TagName="SearchFilter" TagPrefix="uc4" %>
<%@ Register Src="Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc2" %>
<%@ Register Src="Components/ItemsPerPageDrop.ascx" TagName="ItemsPerPageDrop" TagPrefix="uc3" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="RmaHistory">
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
                    <div class="RmaHistoryGridviewDiv">
                        <asp:GridView ID="uxRmaHistoryGrid" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            CssClass="CommonGridView RmaHistoryGridView" GridLines="None" AllowSorting="True"
                            OnSorting="uxHistoryGrid_Sorting">
                            <RowStyle CssClass="CommonGridViewRowStyle RmaHistoryGridViewRowStyle" />
                            <HeaderStyle CssClass="CommonGridViewHeaderStyle" />
                            <AlternatingRowStyle CssClass="CommonGridViewAlternatingRowStyle RmaHistoryGridViewAlternatingRowStyle" />
                            <EmptyDataRowStyle CssClass="CommonGridViewEmptyRowStyle" />
                            <Columns>
                                <asp:TemplateField HeaderText="[$RmaID]" SortExpression="RmaID">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="uxRmaIDLink" runat="server" Text='<%# Eval( "RmaID" ) %>' CssClass="CommonHyperLink"
                                            NavigateUrl='<%# "RmaDetail.aspx?RmaID=" + Eval( "RmaID" ) %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="RmaHistoryID" />
                                    <HeaderStyle CssClass="RmaHistoryHeaderID" />
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="OrderID" HeaderText="[$OrderID]">
                                    <ItemTemplate>
                                        <asp:Label ID="uxOrderIDLabel" runat="server" Text='<%# Eval("OrderID") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="RmaHistoryOrderID" />
                                    <HeaderStyle CssClass="RmaHistoryHeaderOrderID" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="[$ProductName]" SortExpression="ProductName">
                                    <ItemTemplate>
                                        <div class="RmaHistoryProductName">
                                            <asp:Label ID="uxProductNameLabel" runat="server" Text='<%# Eval("ProductName") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="RmaHistoryName" />
                                    <HeaderStyle CssClass="RmaHistoryHeaderName" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="[$RequestStatus]" SortExpression="RequestStatus">
                                    <ItemTemplate>
                                        <asp:Label ID="uxRequestStatusLabel" runat="server" Text='<%# Eval("RequestStatus") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="RmaHistoryStatus" />
                                    <HeaderStyle CssClass="RmaHistoryHeaderStatus" />
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="[$NoData]"></asp:Label>
                            </EmptyDataTemplate>
                        </asp:GridView>
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
