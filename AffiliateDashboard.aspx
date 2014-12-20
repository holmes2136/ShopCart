<%@ Page Title="[$Title]" Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true"
    CodeFile="AffiliateDashboard.aspx.cs" Inherits="AffiliateDashboard" %>

<%@ Register Src="Components/SearchFilter.ascx" TagName="SearchFilter" TagPrefix="uc1" %>
<%@ Import Namespace="Vevo" %>
<%@ Import Namespace="Vevo.Shared.Utilities" %>
<%@ Import Namespace="Vevo.WebUI" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="AffiliateDashboard">
        <div class="CommonPage">
            <div class="CommonPageTop">
                <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/DefaultBoxTopLeft.gif" runat="server"
                    CssClass="CommonPageTopImgLeft" />
                <asp:Label ID="uxAccountDetailsTitle" runat="server" Text="[$Head]" CssClass="CommonPageTopTitle">
                </asp:Label>
                <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/DefaultBoxTopRight.gif"
                    runat="server" CssClass="CommonPageTopImgRight" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonPageLeft">
                <div class="CommonPageRight">
                    <div class="MyAccountDashboardDiv">
                        <asp:Panel ID="uxMyAccountInfoPanel" runat="server" CssClass="MyAccountInfoPanel">
                            <div class="Title">
                                [$AccountInformation]
                                <div class="MyAccountInfoSubmitButton">
                                    <asp:LinkButton ID="uxEditAccountButton" runat="server" CssClass="ShoppingCartUpdateQuantity BtnStyle4"
                                        Text="[$EditAccount]" OnClick="uxEditAccountButton_OnClick" />
                                </div>
                            </div>
                            <div class="MyAccountDashboardInfoDiv">
                                <div class="Value">
                                    <asp:Label ID="uxFirstNameLabel" runat="server" CssClass="MyAccountDashboardLabel"></asp:Label>
                                    <asp:Label ID="uxLastNameLable" runat="server" CssClass="MyAccountDashboardLabel"></asp:Label></div>
                                <div class="Value">
                                    <asp:Label ID="uxUsernameLabel" runat="server" CssClass="MyAccountDashboardLabel"></asp:Label></div>
                                <div class="Value">
                                    <asp:Label ID="uxEmailLabel" runat="server" CssClass="MyAccountDashboardLabel"></asp:Label></div>
                                <div class="Value">
                                    [$Rate]:
                                    <asp:Label ID="uxCommissionRateLabel" runat="server" CssClass="MyAccountDashboardLabel"></asp:Label></div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="uxBillingAddressPanel" runat="server" CssClass="MyAccountInfoPanel">
                            <div class="Title">
                                [$ContactAddress]
                                <div class="MyAccountInfoSubmitButton">
                                    <asp:LinkButton ID="uxEditAddressButton" runat="server" CssClass="ShoppingCartUpdateQuantity BtnStyle4"
                                        Text="[$EditAddress]" OnClick="uxEditAddressButton_OnClick" />
                                </div>
                            </div>
                            <div class="Value">
                                <asp:Label ID="uxCompanyLabel" runat="server" CssClass="MyAccountDashboardLabel"></asp:Label></div>
                            <div class="Value">
                                <asp:Label ID="uxWebsiteLabel" runat="server" CssClass="MyAccountDashboardLabel"></asp:Label></div>
                            <div class="Value">
                                <asp:Label ID="uxAddress1Label" runat="server" CssClass="MyAccountDashboardLabel"></asp:Label>
                                <asp:Label ID="uxAddress2Label" runat="server" CssClass="MyAccountDashboardLabel"></asp:Label></div>
                            <div class="Value">
                                <asp:Label ID="uxCityLabel" runat="server" CssClass="MyAccountDashboardLabel"></asp:Label>,
                                <asp:Label ID="uxStateLabel" runat="server" CssClass="MyAccountDashboardLabel"></asp:Label></div>
                            <div class="Value">
                                <asp:Label ID="uxCountryLabel" runat="server" CssClass="MyAccountDashboardLabel"></asp:Label>,
                                <asp:Label ID="uxZipLabel" runat="server" CssClass="MyAccountDashboardLabel"></asp:Label></div>
                            <div class="Value">
                                [$Phone]:
                                <asp:Label ID="uxPhoneLabel" runat="server" CssClass="MyAccountDashboardLabel"></asp:Label></div>
                            <div class="Value">
                                [$Fax]:
                                <asp:Label ID="uxFaxLabel" runat="server" CssClass="MyAccountDashboardLabel"></asp:Label></div>
                        </asp:Panel>
                        <div class="MyAccountRecentOrder">
                            <div class="MyAccountRecentOrderDiv">
                                <uc1:SearchFilter ID="uxSearchFilter" runat="server" Visible="false"></uc1:SearchFilter>
                                <asp:Panel ID="uxRecentOrderPanel" runat="server" CssClass="MyAccountBillingPanel">
                                    <div class="CommonPageInnerTitle">
                                        [$RecentCommission]
                                    </div>
                                    <div class="RecentOrderGrid">
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
                                    </div>
                                    <div class="SubmitButton">
                                        <asp:LinkButton ID="uxSearchButton" runat="server" CssClass="ShoppingCartUpdateQuantity BtnStyle2"
                                            Text="[$SearchCommission]" OnClick="uxSearchButton_OnClick" />
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
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
