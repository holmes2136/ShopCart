<%@ Page Title="[$Title]" Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true"
    CodeFile="AccountDashboard.aspx.cs" Inherits="AccountDashboard" %>

<%@ Import Namespace="Vevo.Domain" %>
<%@ Import Namespace="Vevo.Shared.Utilities" %>
<%@ Import Namespace="Vevo.WebUI" %>
<%@ Register Src="Components/SearchFilterNew.ascx" TagName="SearchFilter" TagPrefix="uc4" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="MyAccountDashboard">
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
                        <asp:Panel ID="uxMyAccountMessagePanel" runat="server" CssClass="MyAccountMessagePanel" Visible="false">
                            <div class="Title">
                                [$WelcomeMessageTitle]
                                <asp:Label ID="uxGreetingCustomerName" runat="server" /></div>
                            <div class="Value">
                                <asp:Label ID="uxGreetingText" runat="server" /></div>
                        </asp:Panel>
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
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="uxBillingAddressPanel" runat="server" CssClass="MyAccountInfoPanel">
                            <div class="Title">
                                [$BillingAddress]
                                <div class="MyAccountInfoSubmitButton">
                                    <asp:LinkButton ID="uxEditBillingAddressButton" runat="server" CssClass="ShoppingCartUpdateQuantity BtnStyle4"
                                        Text="[$EditBillingAddress]" OnClick="uxEditBillingAddressButton_OnClick" />
                                </div>
                            </div>
                            <div class="MyAccountDashboardInfoDiv">
                                <div class="Value">
                                    <asp:Label ID="uxBillingFirstName" runat="server" CssClass="MyAccountDashboardLabel"></asp:Label>
                                    <asp:Label ID="uxBillingLastName" runat="server" CssClass="MyAccountDashboardLabel"></asp:Label></div>
                                <div class="Value">
                                    [$BillingCompany]:
                                    <asp:Label ID="uxBillingCompany" runat="server" CssClass="MyAccountDashboardLabel"></asp:Label></div>
                                <div class="Value">
                                    <asp:Label ID="uxBillingAddress1" runat="server" CssClass="MyAccountDashboardLabel"></asp:Label>
                                    <asp:Label ID="uxBillingAddress2" runat="server" CssClass="MyAccountDashboardLabel"></asp:Label></div>
                                <div class="Value">
                                    <asp:Label ID="uxBillingCity" runat="server" CssClass="MyAccountDashboardLabel"></asp:Label>,
                                    <asp:Label ID="uxBillingState" runat="server" CssClass="MyAccountDashboardLabel"></asp:Label></div>
                                <div class="Value">
                                    <asp:Label ID="uxBillingCountry" runat="server" CssClass="MyAccountDashboardLabel"></asp:Label>,
                                    <asp:Label ID="uxBillingZip" runat="server" CssClass="MyAccountDashboardLabel"></asp:Label></div>
                                <div class="Value">
                                    [$BillingPhone]:
                                    <asp:Label ID="uxBillingPhone" runat="server" CssClass="MyAccountDashboardLabel"></asp:Label></div>
                                <div class="Value">
                                    [$BillingFax]:
                                    <asp:Label ID="uxBillingFax" runat="server" CssClass="MyAccountDashboardLabel"></asp:Label></div>
                            </div>
                        </asp:Panel>
                        <div class="MyAccountRecentOrder">
                            <div class="MyAccountRecentOrderDiv">
                                <uc4:SearchFilter ID="uxSearchFilter" runat="server" Visible="false"></uc4:SearchFilter>
                                <asp:Panel ID="uxRecentOrderPanel" runat="server" CssClass="MyAccountInfoPanel">
                                    <div class="Title">
                                        [$RecentOrder]
                                        <div class="MyAccountInfoSubmitButton">
                                            <asp:LinkButton ID="uxViewOrders" runat="server" CssClass="ShoppingCartUpdateQuantity BtnStyle4"
                                                Text="[$ViewOrders]" OnClick="uxViewOrders_OnClick" />
                                        </div>
                                    </div>
                                    <div class="RecentOrderGrid">
                                        <asp:GridView ID="uxHistoryGrid" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                            CssClass="CommonGridView OrderHistoryGridView" GridLines="None">
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
                                                <asp:BoundField DataField="Status" HeaderText="[$Status]" SortExpression="Status"
                                                    HeaderStyle-CssClass="OrderHistoryGridStatusHeaderStyle" ItemStyle-CssClass="OrderHistoryGridStatusItemStyle" />
                                                <asp:BoundField DataField="PaymentComplete" HeaderText="[$PaymentComplete]" SortExpression="PaymentComplete"
                                                    HeaderStyle-CssClass="OrderHistoryGridStatusHeaderStyle" ItemStyle-CssClass="OrderHistoryGridStatusItemStyle" />
                                                <asp:BoundField DataField="TrackingNumber" HeaderText="[$TrackingNumber]" SortExpression="TrackingNumber"
                                                    HeaderStyle-CssClass="OrderHistoryGridTrackingHeaderStyle" ItemStyle-CssClass="OrderHistoryGridTrackingItemStyle" />
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="[$NoData]"></asp:Label>
                                            </EmptyDataTemplate>
                                            <EmptyDataRowStyle CssClass="CommonGridViewEmptyRowStyle" />
                                        </asp:GridView>
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
