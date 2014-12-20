<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="CheckoutComplete.aspx.cs"
    Inherits="CheckoutComplete" Title="[$Title]" %>

<%@ Register Src="Components/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Import Namespace="Vevo" %>
<%@ Import Namespace="Vevo.Domain" %>
<%@ Import Namespace="Vevo.WebAppLib" %>
<%@ Import Namespace="Vevo.WebUI" %>
<%@ Import Namespace="Vevo.WebUI.Users" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <uc1:Message ID="uxEmailErrorMessage" runat="server" NumberOfNewLines="1" />
    <uc1:Message ID="uxErrorMessage" runat="server" FontBold="false" NumberOfNewLines="1" />
    <div class="CheckoutComplete">
        <div class="CommonPage">
            <div class="CommonPageTop">
                <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/DefaultBoxTopLeft.gif" runat="server"
                    CssClass="CommonPageTopImgLeft" />
                <asp:Label ID="uxDefaultTitle" runat="server" CssClass="CommonPageTopTitle">[$Header]</asp:Label>
                <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/DefaultBoxTopRight.gif"
                    runat="server" CssClass="CommonPageTopImgRight" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonPageLeft">
                <div class="CommonPageRight">
                    <asp:UpdatePanel ID="uxUpdatePanel" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="uxOrderCompletePanel" runat="server">
                                <h4>
                                    [$Thankyou]
                                </h4>
                                <table>
                                    <tr>
                                        <td>
                                            <div class="CheckoutCompleteText">
                                                [$YourOrderId]
                                            </div>
                                            <div>
                                                <asp:LinkButton ID="uxOrderIDLink" runat="server" CssClass="CheckoutCompleteOrderLink"
                                                    OnClick="uxOrderIDLink_OnClick"></asp:LinkButton>
                                                <asp:Label ID="uxOrderIDLinkLabel" runat="server" Visible="false"></asp:Label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            [$ReceiveEmail]
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="uxOrderSummaryPanel" runat="server">
                                <asp:Panel ID="uxHeadPanel" runat="server" CssClass="CheckoutCompleteHeadPanel">
                                    <h4>
                                        [$Head]
                                    </h4>
                                </asp:Panel>
                                <div id="PrintArea">
                                    <asp:Literal ID="uxErrorLiteral" runat="server" Visible="false">
                                    <h4>
                                        You are not authorized to view this page.
                                    </h4>
                                    </asp:Literal>
                                    <asp:Label ID="uxOrderIDLabel" runat="server" CssClass="CheckoutCompleteOrderIDLabel">
                                    </asp:Label>
                                    <asp:ObjectDataSource ID="uxOrderSource" runat="server" SelectMethod="OrdersGetOne"
                                        TypeName="Vevo.Domain.DataSources.OrdersDataSource">
                                        <SelectParameters>
                                            <asp:QueryStringParameter Name="orderID" QueryStringField="OrderID" Type="String" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                    <asp:ObjectDataSource ID="OrderItemsSource" runat="server" SelectMethod="OrderItemGetByOrderID"
                                        TypeName="Vevo.Domain.DataSources.OrdersDataSource">
                                        <SelectParameters>
                                            <asp:QueryStringParameter Name="orderID" QueryStringField="OrderID" Type="String" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                    <asp:ObjectDataSource ID="uxClientSource" runat="server" SelectMethod="GetDetails"
                                        TypeName="Vevo.DataAccessLib.Cart.ClientAccess">
                                        <SelectParameters>
                                            <asp:QueryStringParameter Name="orderID" QueryStringField="OrderID" Type="String" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                    <asp:PlaceHolder ID="uxSummaryPlaceHolder" runat="server">
                                        <table cellpadding="2" cellspacing="0" class="CheckoutCompleteTable">
                                            <tr>
                                                <td class="CheckoutCompleteColumn">
                                                    <table border="0" cellpadding="0" cellspacing="0" class="CheckoutCompleteOrderSummaryTable">
                                                        <tr>
                                                            <td class="CheckoutCompleteOrderSummaryColumn">
                                                                <asp:GridView ID="uxOrderItemsGrid" runat="server" AutoGenerateColumns="False" DataSourceID="OrderItemsSource"
                                                                    CellPadding="0" GridLines="None" CssClass="CheckoutCompleteGridView">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="[$Name]">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="uxNameProductLabel" runat="server" Text='<%# Eval("Name") %>' />
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass="CheckoutCompleteGridViewNameItemStyle" />
                                                                            <HeaderStyle CssClass="CheckoutCompleteGridViewNameHeaderStyle" />
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="Quantity" HeaderText="[$Quantity]">
                                                                            <ItemStyle CssClass="CheckoutCompleteGridViewQuantityItemStyle" />
                                                                            <HeaderStyle CssClass="CheckoutCompleteGridViewQuantityHeaderStyle" />
                                                                        </asp:BoundField>
                                                                        <asp:TemplateField HeaderText="[$UnitPrice]">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="uxUnitPriceLabel" runat="server" Text='<%# StoreContext.Currency.FormatPrice( Convert.ToDecimal( Eval("UnitPrice") ) )%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass="CheckoutCompleteGridViewUnitPriceItemStyle" />
                                                                            <HeaderStyle CssClass="CheckoutCompleteGridViewUnitPriceHeaderStyle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="[$Total]">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="uxPriceLabel" runat="server" Text='<%# StoreContext.Currency.FormatPrice( Convert.ToInt32( Eval("Quantity") ) * Convert.ToDecimal( Eval("UnitPrice") ) )%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass="CheckoutCompleteGridViewPriceItemStyle" />
                                                                            <HeaderStyle CssClass="CheckoutCompleteGridViewPriceHeaderStyle" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <RowStyle CssClass="CheckoutCompleteGridViewRowStyle" />
                                                                    <HeaderStyle CssClass="CheckoutCompleteGridViewHeaderStyle" />
                                                                    <AlternatingRowStyle CssClass="CheckoutCompleteGridViewAlternatingRowStyle" />
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="CheckoutCompleteOrderSummaryColumn">
                                                                <table class="CheckoutCompleteSubtotalTable" cellspacing="0" cellpadding="0">
                                                                    <tr>
                                                                        <td class="CheckoutCompleteSubtotalLabel">
                                                                            [$Cost] :
                                                                        </td>
                                                                        <td class="CheckoutCompleteSubtotalValue">
                                                                            <asp:Label ID="uxProductCostLabel" runat="server"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="CheckoutCompleteSubtotalLabel">
                                                                            [$Discount] :
                                                                        </td>
                                                                        <td class="CheckoutCompleteSubtotalValue">
                                                                            <asp:Label ID="uxDiscountLabel" runat="server"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <% if (DisplayPointDiscount())
                                                                       { %>
                                                                    <tr>
                                                                        <td class="CheckoutCompleteSubtotalLabel">
                                                                            [$PointDiscount]
                                                                        </td>
                                                                        <td class="CheckoutCompleteSubtotalValue">
                                                                            <asp:Label ID="uxPointDiscountLabel" runat="server"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <%} %>
                                                                    <tr>
                                                                        <td class="CheckoutCompleteSubtotalLabel">
                                                                            [$Tax] :
                                                                        </td>
                                                                        <td class="CheckoutCompleteSubtotalValue">
                                                                            <asp:Label ID="uxTaxLabel" runat="server"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="CheckoutCompleteSubtotalLabel">
                                                                            [$ShippCost] :
                                                                        </td>
                                                                        <td class="CheckoutCompleteSubtotalValue">
                                                                            <asp:Label ID="uxShippingCostLabel" runat="server"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="uxHandlingFeeTR" runat="server">
                                                                        <td class="CheckoutCompleteSubtotalLabel">
                                                                            [$HandlingFee] :
                                                                        </td>
                                                                        <td class="CheckoutCompleteSubtotalValue">
                                                                            <asp:Label ID="uxHandlingFeeLabel" runat="server"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="uxGiftCertificateTR" runat="server">
                                                                        <td class="CheckoutCompleteSubtotalLabel">
                                                                            [$GiftCertificate] :
                                                                        </td>
                                                                        <td class="CheckoutCompleteSubtotalValue">
                                                                            <asp:Label ID="uxGiftCertificateLabel" runat="server"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="CheckoutCompleteSubtotalTotalLabel">
                                                                            [$Total] :
                                                                        </td>
                                                                        <td class="CheckoutCompleteSubtotalTotalPrice">
                                                                            <asp:Label ID="uxTotalLabel" runat="server"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="CheckoutCompleteColumn">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="CheckoutCompleteColumn">
                                                    <asp:FormView ID="uxOrderView" runat="server" DataSourceID="uxOrderSource" CssClass="CheckoutCompleteCustomerFormView">
                                                        <ItemTemplate>
                                                            <table border="0" cellpadding="0" cellspacing="0" class="CheckoutCompleteCustomerTable">
                                                                <tr>
                                                                    <td>
                                                                        <table class="CheckoutCompleteCustomer" border="0" cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td class="CheckoutCompleteCustomerInfo">
                                                                                    <table class="CheckoutCompleteGridView" border="0" cellpadding="0" cellspacing="0">
                                                                                        <tr>
                                                                                            <td class="CheckoutCompleteGridViewHeaderStyle">
                                                                                                [$Customer]
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="CheckoutCompleteCustomerDetailsColumn">
                                                                                                <table border="0" cellpadding="0" cellspacing="0" class="CheckoutCompleteCustomerDetailsTable">
                                                                                                    <tr class="CheckoutCompleteGridViewRowStyle">
                                                                                                        <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                                            [$FirstName] :
                                                                                                        </td>
                                                                                                        <td class="CheckoutCompleteDetailsValueColumn">
                                                                                                            <asp:Label ID="FirstNameLabel" runat="server" Text='<%# Eval("Billing.FirstName") %>'></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr class="CheckoutCompleteGridViewAlternatingRowStyle">
                                                                                                        <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                                            [$LastName] :
                                                                                                        </td>
                                                                                                        <td class="CheckoutCompleteDetailsValueColumn">
                                                                                                            <asp:Label ID="LastNameLabel" runat="server" Text='<%# Eval("Billing.LastName") %>'></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr class="CheckoutCompleteGridViewRowStyle">
                                                                                                        <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                                            [$Company] :
                                                                                                        </td>
                                                                                                        <td class="CheckoutCompleteDetailsValueColumn">
                                                                                                            <asp:Label ID="CompanyLabel" runat="server" Text='<%# Eval("Billing.Company") %>'></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr class="CheckoutCompleteGridViewAlternatingRowStyle">
                                                                                                        <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                                            [$Address] :
                                                                                                        </td>
                                                                                                        <td class="CheckoutCompleteDetailsValueColumn">
                                                                                                            <asp:Label ID="Address1Label" runat="server" Text='<%# Eval("Billing.Address1") %>'></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr id="Tr1" class="CheckoutCompleteGridViewAlternatingRowStyle" visible='<%# GetAddress2Visible( Eval( "Billing.Address2" ) ) %>'
                                                                                                        runat="server">
                                                                                                        <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                                        </td>
                                                                                                        <td class="CheckoutCompleteDetailsValueColumn">
                                                                                                            <asp:Label ID="Address2Label" runat="server" Text='<%# Eval("Billing.Address2") %>'></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr class="CheckoutCompleteGridViewRowStyle">
                                                                                                        <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                                            [$City] :
                                                                                                        </td>
                                                                                                        <td class="CheckoutCompleteDetailsValueColumn">
                                                                                                            <asp:Label ID="CityLabel" runat="server" Text='<%# Eval("Billing.City") %>'></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr class="CheckoutCompleteGridViewAlternatingRowStyle">
                                                                                                        <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                                            [$State] :
                                                                                                        </td>
                                                                                                        <td class="CheckoutCompleteDetailsValueColumn">
                                                                                                            <asp:Label ID="StateLabel" runat="server" Text='<%# Eval("Billing.State") %>'></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr class="CheckoutCompleteGridViewRowStyle">
                                                                                                        <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                                            [$Zip] :
                                                                                                        </td>
                                                                                                        <td class="CheckoutCompleteDetailsValueColumn">
                                                                                                            <asp:Label ID="ZipLabel" runat="server" Text='<%# Eval("Billing.Zip") %>'></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr class="CheckoutCompleteGridViewAlternatingRowStyle">
                                                                                                        <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                                            [$Country] :
                                                                                                        </td>
                                                                                                        <td class="CheckoutCompleteDetailsValueColumn">
                                                                                                            <asp:Label ID="CountryLabel" runat="server" Text='<%# AddressUtilities.GetCountryNameByCountryCode (Eval("Billing.Country").ToString() ) %>'></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr class="CheckoutCompleteGridViewRowStyle">
                                                                                                        <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                                            [$Email] :
                                                                                                        </td>
                                                                                                        <td class="CheckoutCompleteDetailsValueColumn">
                                                                                                            <asp:Label ID="EmailLabel" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr class="CheckoutCompleteGridViewAlternatingRowStyle">
                                                                                                        <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                                            [$Phone] :
                                                                                                        </td>
                                                                                                        <td class="CheckoutCompleteDetailsValueColumn">
                                                                                                            <asp:Label ID="PhoneLabel" runat="server" Text='<%# Eval("Billing.Phone") %>'></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr class="CheckoutCompleteGridViewRowStyle">
                                                                                                        <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                                            [$Fax] :
                                                                                                        </td>
                                                                                                        <td class="CheckoutCompleteDetailsValueColumn">
                                                                                                            <asp:Label ID="FaxLabel" runat="server" Text='<%# Eval("Billing.Fax") %>'></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                                <%if (DataAccessContext.Configurations.GetBoolValue("ShippingAddressMode"))
                                                                                  { %>
                                                                                <td valign="top" class="CheckoutCompleteCustomerShippingInfo">
                                                                                    <table class="CheckoutCompleteGridView" border="0" cellpadding="0" cellspacing="0">
                                                                                        <tr id="InfoShippTR" runat="server">
                                                                                            <td class="CheckoutCompleteGridViewHeaderStyle">
                                                                                                [$InfoShipp]
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr id="ShippDetailsTR" runat="server">
                                                                                            <td class="CheckoutCompleteCustomerDetailsColumn">
                                                                                                <table border="0" cellpadding="0" cellspacing="1" class="CheckoutCompleteShippingTable">
                                                                                                    <tr class="CheckoutCompleteGridViewRowStyle">
                                                                                                        <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                                            [$FirstName] :
                                                                                                        </td>
                                                                                                        <td class="CheckoutCompleteDetailsValueColumn">
                                                                                                            <asp:Label ID="ShippingFirstNameLabel" runat="server" Text='<%# Eval("Shipping.FirstName") %>'></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr class="CheckoutCompleteGridViewAlternatingRowStyle">
                                                                                                        <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                                            [$LastName] :
                                                                                                        </td>
                                                                                                        <td class="CheckoutCompleteDetailsValueColumn">
                                                                                                            <asp:Label ID="ShippingLastNameLabel" runat="server" Text='<%# Eval("Shipping.LastName") %>'></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr class="CheckoutCompleteGridViewRowStyle">
                                                                                                        <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                                            [$Company] :
                                                                                                        </td>
                                                                                                        <td class="CheckoutCompleteDetailsValueColumn">
                                                                                                            <asp:Label ID="ShippingCompanyLabel" runat="server" Text='<%# Eval("Shipping.Company") %>'></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr class="CheckoutCompleteGridViewAlternatingRowStyle">
                                                                                                        <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                                            [$Address] :
                                                                                                        </td>
                                                                                                        <td class="CheckoutCompleteDetailsValueColumn">
                                                                                                            <asp:Label ID="ShippingAddress1Label" runat="server" Text='<%# Eval("Shipping.Address1") %>'></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr id="Tr2" class="CheckoutCompleteGridViewAlternatingRowStyle" visible='<%# GetAddress2Visible( Eval( "Shipping.Address2" ) ) %>'
                                                                                                        runat="server">
                                                                                                        <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                                        </td>
                                                                                                        <td class="CheckoutCompleteDetailsValueColumn">
                                                                                                            <asp:Label ID="ShippingAddress2Label" runat="server" Text='<%# Eval("Shipping.Address2") %>'></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr class="CheckoutCompleteGridViewRowStyle">
                                                                                                        <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                                            [$City] :
                                                                                                        </td>
                                                                                                        <td class="CheckoutCompleteDetailsValueColumn">
                                                                                                            <asp:Label ID="ShippingCityLabel" runat="server" Text='<%# Eval("Shipping.City") %>'></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr class="CheckoutCompleteGridViewAlternatingRowStyle">
                                                                                                        <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                                            [$State] :
                                                                                                        </td>
                                                                                                        <td class="CheckoutCompleteDetailsValueColumn">
                                                                                                            <asp:Label ID="ShippingStateLabel" runat="server" Text='<%# Eval("Shipping.State") %>'></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr class="CheckoutCompleteGridViewRowStyle">
                                                                                                        <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                                            [$Zip] :
                                                                                                        </td>
                                                                                                        <td class="CheckoutCompleteDetailsValueColumn">
                                                                                                            <asp:Label ID="ShippingZipLabel" runat="server" Text='<%# Eval("Shipping.Zip") %>'></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr class="CheckoutCompleteGridViewAlternatingRowStyle">
                                                                                                        <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                                            [$Country] :
                                                                                                        </td>
                                                                                                        <td class="CheckoutCompleteDetailsValueColumn">
                                                                                                            <asp:Label ID="ShippingCountryLabel" runat="server" Text='<%# AddressUtilities.GetCountryNameByCountryCode ( Eval("Shipping.Country").ToString()) %>'></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr class="CheckoutCompleteGridViewRowStyle">
                                                                                                        <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                                            [$Phone] :
                                                                                                        </td>
                                                                                                        <td class="CheckoutCompleteDetailsValueColumn">
                                                                                                            <asp:Label ID="ShippingPhoneLabel" runat="server" Text='<%# Eval("Shipping.Phone") %>'></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr class="CheckoutCompleteGridViewAlternatingRowStyle">
                                                                                                        <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                                            [$Fax] :
                                                                                                        </td>
                                                                                                        <td class="CheckoutCompleteDetailsValueColumn">
                                                                                                            <asp:Label ID="ShippingFaxLabel" runat="server" Text='<%# Eval("Shipping.Fax") %>'></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                                <%} %>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="CheckoutCompleteGridViewHeaderStyle infocolumn">
                                                                        [$InfoPay]
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="CheckoutCompleteCustomerDetailsColumn">
                                                                        <table border="0" cellpadding="0" cellspacing="1" class="CheckoutCompleteOrderCommentTable">
                                                                            <tr class="CheckoutCompleteGridViewRowStyle">
                                                                                <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                    [$PaymentType] :
                                                                                </td>
                                                                                <td class="CheckoutCompleteDetailsValueColumn">
                                                                                    <asp:Label ID="PaymentMethodLabel" runat="server" Text='<%# ConvertPaymentMethod( Eval("PaymentMethod").ToString() ) %>'></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="CheckoutCompleteGridViewAlternatingRowStyle" id="uxPONumberTR" runat="server">
                                                                                <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                    [$PONumber] :
                                                                                </td>
                                                                                <td class="CheckoutCompleteDetailsValueColumn">
                                                                                    <asp:Label ID="uxPONumber" runat="server" Text='<%# Bind("PONumber") %>'></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="CheckoutCompleteGridViewRowStyle" id="uxOrderDateTR" runat="server">
                                                                                <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                    [$OrderDate] :
                                                                                </td>
                                                                                <td class="CheckoutCompleteDetailsValueColumn">
                                                                                    <asp:Label ID="OrderDateLabel" runat="server" Text='<%# Bind("OrderDate") %>'></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr id="uxTrackingTR" runat="server" class="CheckoutCompleteGridViewAlternatingRowStyle">
                                                                                <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                    [$TrackingNumber]
                                                                                </td>
                                                                                <td class="CheckoutCompleteDetailsValueColumn">
                                                                                    <asp:HyperLink ID="TrackingNumberLink" runat="server" Text='<%# Bind("TrackingNumber") %>'
                                                                                        Target="_blank" NavigateUrl='<%# GetTrackingUrl(Eval("TrackingMethod"), Eval("TrackingNumber")) %>'></asp:HyperLink>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="CheckoutCompleteGridViewHeaderStyle infocolumn">
                                                                        [$InfoOrderComment]
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="CheckoutCompleteCustomerDetailsColumn">
                                                                        <table border="0" cellpadding="0" cellspacing="1" class="CheckoutCompleteOrderCommentTable">
                                                                            <%if (DataAccessContext.Configurations.GetBoolValue("SaleTaxExempt"))
                                                                              { %>
                                                                            <tr class="CheckoutCompleteGridViewRowStyle">
                                                                                <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                    [$TaxExemptID] :
                                                                                </td>
                                                                                <td class="CheckoutCompleteDetailsValueColumn">
                                                                                    <asp:Label ID="uxTaxExemptIDLabel" runat="server" Text='<%# WebUtilities.ReplaceNewLine( Eval("TaxExemptID").ToString() ) %>'></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="CheckoutCompleteGridViewAlternatingRowStyle">
                                                                                <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                    [$TaxExemptCountry] :
                                                                                </td>
                                                                                <td class="CheckoutCompleteDetailsValueColumn">
                                                                                    <asp:Label ID="uxTaxExemptCountryLabel" runat="server" Text='<%# WebUtilities.ReplaceNewLine( Eval("TaxExemptCountry").ToString() ) %>'></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="CheckoutCompleteGridViewRowStyle">
                                                                                <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                    [$TaxExemptState] :
                                                                                </td>
                                                                                <td class="CheckoutCompleteDetailsValueColumn">
                                                                                    <asp:Label ID="uxTaxExemptStateLabel" runat="server" Text='<%# WebUtilities.ReplaceNewLine( Eval("TaxExemptState").ToString() ) %>'></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <%} %>
                                                                            <tr class="CheckoutCompleteGridViewAlternatingRowStyle">
                                                                                <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                    [$OrderComment] :
                                                                                </td>
                                                                                <td class="CheckoutCompleteDetailsValueColumn">
                                                                                    <asp:Label ID="uxCustomerCommentsLabel" runat="server" Text='<%# WebUtilities.ReplaceNewLine( Eval("CustomerComments").ToString() ) %>'></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <% if (DisplayPointEarned())
                                                                               { %>
                                                                            <tr class="CheckoutCompleteGridViewRowStyle">
                                                                                <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                    [$PointEarned] :
                                                                                </td>
                                                                                <td class="CheckoutCompleteDetailsValueColumn">
                                                                                    <asp:Label ID="uxPointEarnedLabel" runat="server" Text='<%# WebUtilities.ReplaceNewLine( Eval("PointEarned").ToString() ) %>'></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <%} %>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:FormView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="CheckoutCompleteColumn">
                                                    <asp:FormView ID="uxClientForm" runat="server" DataSourceID="uxClientSource" CssClass="CheckoutCompleteClientFormView">
                                                        <ItemTemplate>
                                                            <table id="Table1" cellpadding="3" cellspacing="2" class="CheckoutCompleteCustomerTable">
                                                                <tr>
                                                                    <td class="CheckoutCompleteCustomerHeaderColumn">
                                                                        [$Billing]
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="CheckoutCompleteCustomerDetailsColumn">
                                                                        <table border="0" cellpadding="2" cellspacing="2" class="CheckoutCompleteCustomerDetailsTable">
                                                                            <tr class="CheckoutCompleteGridViewRowStyle">
                                                                                <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                    [$FName] :
                                                                                </td>
                                                                                <td class="CheckoutCompleteDetailsValueColumn">
                                                                                    <%# Eval( "Billing.FirstName" )%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="CheckoutCompleteGridViewAlternatingRowStyle">
                                                                                <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                    [$LName] :
                                                                                </td>
                                                                                <td class="CheckoutCompleteDetailsValueColumn">
                                                                                    <%# Eval( "Billing.LastName" )%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="CheckoutCompleteGridViewRowStyle">
                                                                                <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                    [$Company] :
                                                                                </td>
                                                                                <td class="CheckoutCompleteDetailsValueColumn">
                                                                                    <%# Eval( "Billing.Company" )%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="CheckoutCompleteGridViewAlternatingRowStyle">
                                                                                <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                    [$Address] :
                                                                                </td>
                                                                                <td class="CheckoutCompleteDetailsValueColumn">
                                                                                    <%# Eval( "Billing.Address1" )%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr id="Tr3" runat="server" class="CheckoutCompleteGridViewAlternatingRowStyle" visible='<%# GetAddress2Visible( Eval( "Billing.Address2" ) ) %>'>
                                                                                <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                </td>
                                                                                <td class="CheckoutCompleteDetailsValueColumn">
                                                                                    <%# Eval( "Billing.Address2" )%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="CheckoutCompleteGridViewRowStyle">
                                                                                <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                    [$City] :
                                                                                </td>
                                                                                <td class="CheckoutCompleteDetailsValueColumn">
                                                                                    <%# Eval( "Billing.City" )%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="CheckoutCompleteGridViewAlternatingRowStyle">
                                                                                <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                    [$State] :
                                                                                </td>
                                                                                <td class="CheckoutCompleteDetailsValueColumn">
                                                                                    <%# Eval( "Billing.State" )%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="CheckoutCompleteGridViewRowStyle">
                                                                                <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                    [$Zip] :
                                                                                </td>
                                                                                <td class="CheckoutCompleteDetailsValueColumn">
                                                                                    <%# Eval( "Billing.Zip" )%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="CheckoutCompleteGridViewAlternatingRowStyle">
                                                                                <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                    [$Country] :
                                                                                </td>
                                                                                <td class="CheckoutCompleteDetailsValueColumn">
                                                                                    <%# Eval( "Billing.Country" )%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="CheckoutCompleteGridViewRowStyle">
                                                                                <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                    [$Phone] :
                                                                                </td>
                                                                                <td class="CheckoutCompleteDetailsValueColumn">
                                                                                    <%# Eval( "Billing.Phone" )%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="CheckoutCompleteGridViewAlternatingRowStyle">
                                                                                <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                    [$Fax] :
                                                                                </td>
                                                                                <td class="CheckoutCompleteDetailsValueColumn">
                                                                                    <%# Eval( "Billing.Fax" )%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="CheckoutCompleteGridViewRowStyle">
                                                                                <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                    [$Email] :
                                                                                </td>
                                                                                <td class="CheckoutCompleteDetailsValueColumn">
                                                                                    <%# Eval( "Email" )%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="CheckoutCompleteGridViewAlternatingRowStyle">
                                                                                <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                    [$UrlClient] :
                                                                                </td>
                                                                                <td class="CheckoutCompleteDetailsValueColumn">
                                                                                    <%# Eval( "UrlClient" )%>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                </tr>
                                                                <tr>
                                                                    <td class="CheckoutCompleteCustomerHeaderColumn">
                                                                        [$Shipping]
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="CheckoutCompleteCustomerDetailsColumn">
                                                                        <table border="0" cellpadding="2" cellspacing="2" class="CheckoutCompleteShippingTable">
                                                                            <tr class="CheckoutCompleteGridViewRowStyle">
                                                                                <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                    [$FName] :
                                                                                </td>
                                                                                <td class="CheckoutCompleteDetailsValueColumn">
                                                                                    <%# Eval( "Shipping.FirstName" )%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="CheckoutCompleteGridViewAlternatingRowStyle">
                                                                                <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                    [$LName] :
                                                                                </td>
                                                                                <td class="CheckoutCompleteDetailsValueColumn">
                                                                                    <%# Eval( "Shipping.LastName" )%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="CheckoutCompleteGridViewRowStyle">
                                                                                <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                    [$Company] :
                                                                                </td>
                                                                                <td class="CheckoutCompleteDetailsValueColumn">
                                                                                    <%# Eval( "Shipping.Company" )%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="CheckoutCompleteGridViewAlternatingRowStyle">
                                                                                <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                    [$Address] :
                                                                                </td>
                                                                                <td class="CheckoutCompleteDetailsValueColumn">
                                                                                    <%# Eval( "Shipping.Address1" )%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr id="Tr4" class="CheckoutCompleteGridViewAlternatingRowStyle" visible='<%# GetAddress2Visible( Eval( "Shipping.Address2" ) ) %>'
                                                                                runat="server">
                                                                                <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                </td>
                                                                                <td class="CheckoutCompleteDetailsValueColumn">
                                                                                    <%# Eval( "Shipping.Address2" )%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="CheckoutCompleteGridViewRowStyle">
                                                                                <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                    [$City] :
                                                                                </td>
                                                                                <td class="CheckoutCompleteDetailsValueColumn">
                                                                                    <%# Eval( "Shipping.City" )%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="CheckoutCompleteGridViewAlternatingRowStyle">
                                                                                <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                    [$State] :
                                                                                </td>
                                                                                <td class="CheckoutCompleteDetailsValueColumn">
                                                                                    <%# Eval( "Shipping.State" )%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="CheckoutCompleteGridViewRowStyle">
                                                                                <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                    [$Zip] :
                                                                                </td>
                                                                                <td class="CheckoutCompleteDetailsValueColumn">
                                                                                    <%# Eval( "Shipping.Zip" )%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="CheckoutCompleteGridViewAlternatingRowStyle">
                                                                                <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                    [$Country] :
                                                                                </td>
                                                                                <td class="CheckoutCompleteDetailsValueColumn">
                                                                                    <%# Eval( "Shipping.Country" )%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="CheckoutCompleteGridViewRowStyle">
                                                                                <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                    [$Phone] :
                                                                                </td>
                                                                                <td class="CheckoutCompleteDetailsValueColumn">
                                                                                    <%# Eval( "Shipping.Phone" )%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="CheckoutCompleteGridViewAlternatingRowStyle">
                                                                                <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                    [$Fax] :
                                                                                </td>
                                                                                <td class="CheckoutCompleteDetailsValueColumn">
                                                                                    <%# Eval( "Shipping.Fax" )%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="CheckoutCompleteGridViewRowStyle">
                                                                                <td class="CheckoutCompleteDetailsLabelColumn">
                                                                                    [$MerchantNotes] :
                                                                                </td>
                                                                                <td class="CheckoutCompleteDetailsValueColumn">
                                                                                    <%# Eval( "MerchantNotes" )%>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:FormView>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:PlaceHolder>
                                </div>
                            </asp:Panel>
                            <div class="CheckoutCompletePrintLinkDiv">
                                <asp:LinkButton ID="uxPrintLink" runat="server" CssClass="ShoppingCartContinueShopping BtnStyle1"
                                    Text="[$Print]" />
                            </div>
                            <div class="CheckoutCompleteBackLinkDiv">
                                <asp:HyperLink ID="uxHomepageHyperLink" runat="server" NavigateUrl="~/Default.aspx"
                                    CssClass="ShoppingCartContinueShopping BtnStyle2">[$ContinueShopping]</asp:HyperLink>
                            </div>
                            <div class="Clear">
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
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
