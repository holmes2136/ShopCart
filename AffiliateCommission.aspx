<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="AffiliateCommission.aspx.cs"
    Inherits="AffiliateCommission" Title="[$AffiliateCommissiom]" %>

<%@ Register Src="Components/SearchFilter.ascx" TagName="SearchFilter" TagPrefix="uc2" %>
<%@ Register Src="Components/ItemsPerPageDrop.ascx" TagName="ItemsPerPageDrop" TagPrefix="uc1" %>
<%@ Register Src="~/components/pagingcontrol.ascx" TagName="PagingControl" TagPrefix="uc3" %>
<%@ Import Namespace="Vevo" %>
<%@ Import Namespace="Vevo.Shared.Utilities" %>
<%@ Import Namespace="Vevo.WebUI" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="AffiliateCommission">
        <div class="CommonPage">
            <div class="CommonPageTop">
                <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/DefaultBoxTopLeft.gif" runat="server"
                    CssClass="CommonPageTopImgLeft" />
                <asp:Label ID="uxDefaultTitle" runat="server" CssClass="CommonPageTopTitle">[$AffiliateCommission]</asp:Label>
                <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/DefaultBoxTopRight.gif"
                    runat="server" CssClass="CommonPageTopImgRight" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonPageLeft">
                <div class="CommonPageRight">
                    <div class="AffiliateCommissionSearchLinkDiv">
                        <asp:HyperLink ID="uxSearchLink" runat="server" NavigateUrl="~/AffiliateCommissionSearch.aspx"
                            CssClass="CommonHyperLink">[$SearchCommission]</asp:HyperLink>
                    </div>
                    <uc2:SearchFilter ID="uxSearchFilter" runat="server" />
                    <div class="CommonGridViewPageItemDiv">
                        <div class="CommonGridViewItemsPerPageDiv">
                            <uc1:ItemsPerPageDrop ID="uxItemsPerPageDrop" runat="server" />
                        </div>
                        <div class="CommonGridViewPagingDiv">
                            <uc3:PagingControl ID="uxPagingControl" runat="server" />
                        </div>
                        <div class="Clear">
                        </div>
                    </div>
                    <asp:Panel ID="uxNoResultPanel" runat="server" Visible="false" CssClass="AffiliateCommissionNoResultPanel">
                        <div class="AffiliateCommissionNoResultDiv">
                            [$NoResult]
                        </div>
                        <div class="AffiliateCommissionNoResultLinkDiv">
                            <asp:HyperLink ID="uxBackLink" runat="server" NavigateUrl="~/AffiliateCommissionSearch.aspx"
                                CssClass="CommonHyperLink">[$BackToSearch]</asp:HyperLink>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="uxListPanel" runat="server" CssClass="AffiliateCommissionListItemPanel">
                        <asp:GridView ID="uxGrid" runat="server" AutoGenerateColumns="False" GridLines="None"
                            CellPadding="4" CssClass="CommonGridView AffiliateCommissionGridView" OnRowCreated="SetFooter"
                            ShowFooter="true">
                            <Columns>
                                <asp:BoundField HeaderText="[$OrderID]" DataField="OrderID" SortExpression="OrderID">
                                    <HeaderStyle CssClass="AffiliateCommissionOrderIDHeaderStyle" />
                                    <ItemStyle CssClass="AffiliateCommissionOrderIDItemStyle" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="[$ProductCost]*" SortExpression="ProductCost">
                                    <ItemTemplate>
                                        <asp:Label ID="uxProductCostLabel" runat="server" Text='<%# StoreContext.Currency.FormatPrice( Eval("ProductCost") ) %>' />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="AffiliateCommissionProductCostHeaderStyle" />
                                    <ItemStyle CssClass="AffiliateCommissionProductCostItemStyle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="[$Commission]" SortExpression="Commission">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="uxCommissionHidden" runat="server" Value='<%# Eval("Commission")%>' />
                                        <asp:Label ID="uxCommissionLabel" runat="server" Text='<%# StoreContext.Currency.FormatPrice( Eval("Commission")) %>' />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="AffiliateCommissionCommissionHeaderStyle" />
                                    <ItemStyle CssClass="AffiliateCommissionCommissionItemStyle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="[$OrderDate]" SortExpression="OrderDate">
                                    <ItemTemplate>
                                        <asp:Label ID="uxOrderDate" runat="server" Text='<%# ShowOnlyDate( Eval("OrderDate") ) %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="AffiliateCommissionOrderDateHeaderStyle" />
                                    <ItemStyle CssClass="AffiliateCommissionOrderDateItemStyle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="[$PaymentStatus]" SortExpression="PaymentStatus">
                                    <ItemTemplate>
                                        <asp:Label ID="uxPaymentStatus" runat="server" Text='<%# ConvertUtilities.ToYesNo( Eval("PaymentStatus") ) %>' />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="AffiliateCommissionPaymentStatusHeaderStyle" />
                                    <ItemStyle CssClass="AffiliateCommissionPaymentStatusItemStyle" />
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="[$NoData]"></asp:Label>
                            </EmptyDataTemplate>
                            <FooterStyle CssClass="CommonGridViewFooterStyle AffiliateCommissionGridViewFooterStyle" />
                            <RowStyle CssClass="CommonGridViewRowStyle" />
                            <HeaderStyle CssClass="CommonGridViewHeaderStyle" />
                            <AlternatingRowStyle CssClass="CommonGridViewAlternatingRowStyle" />
                            <EmptyDataRowStyle CssClass="CommonGridViewEmptyRowStyle" />
                        </asp:GridView>
                        <div class="AffiliateCommissionRemarkDiv">
                            <asp:Label ID="uxRemarkLabel" runat="server" Text="[$Remark]" CssClass="AffiliateCommissionRemarkLabel"></asp:Label>
                        </div>
                    </asp:Panel>
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
