<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile/Mobile.master" AutoEventWireup="true"
    CodeFile="CheckoutComplete.aspx.cs" Inherits="Mobile_CheckoutComplete" %>

<%@ Register Src="Components/MobileMessage.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Import Namespace="Vevo" %>
<%@ Import Namespace="Vevo.Domain" %>
<%@ Import Namespace="Vevo.WebAppLib" %>
<%@ Import Namespace="Vevo.WebUI" %>
<%@ Import Namespace="Vevo.WebUI.Users" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="MobileTitle">
        [$Head]
    </div>
    <div class="MobileCommonValidateText MobileCommonBox">
        <uc1:Message ID="uxEmailErrorMessage" runat="server" />
        <uc1:Message ID="uxErrorMessage" runat="server" FontBold="false" />
    </div>
    <div id="PrintArea">
        <asp:Literal ID="uxErrorLiteral" runat="server" Visible="false">
            <div class="MobileShoppingCartMessage MobileCommonBox">
                You are not authorized to view this page.
            </div>
        </asp:Literal>
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
            <asp:Label ID="uxOrderIDLabel" runat="server" CssClass="MobileCheckoutCompleteOrderIDLabel">
            </asp:Label>
            <table cellpadding="2" cellspacing="0" class="MobileCheckoutCompleteTable">
                <tr>
                    <td class="MobileCheckoutCompleteColumn">
                        <table border="0" cellpadding="0" cellspacing="0" class="MobileCheckoutCompleteOrderSummaryTable">
                            <tr>
                                <td class="MobileCheckoutCompleteOrderSummaryColumn">
                                    <asp:GridView ID="uxOrderItemsGrid" runat="server" AutoGenerateColumns="False" DataSourceID="OrderItemsSource"
                                        CellPadding="0" GridLines="None" CssClass="MobileCheckoutCompleteGridView">
                                        <Columns>
                                            <asp:TemplateField HeaderText="[$Name]">
                                                <ItemTemplate>
                                                    <asp:Label ID="uxNameProductLabel" runat="server" Text='<%# Eval("Name") %>' />
                                                    <div class="MobileShoppingCartGridViewPrice">
                                                        <asp:Label ID="uxUnitPriceLabel" runat="server" Text='<%# StoreContext.Currency.FormatPrice( Convert.ToDecimal( Eval("UnitPrice") ) )%>'
                                                            CssClass="price" />
                                                        <span class="multiply">x
                                                            <%# Eval( "Quantity" ) %></span>
                                                    </div>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="MobileCheckoutCompleteGridViewNameItemStyle" />
                                                <HeaderStyle CssClass="MobileCheckoutCompleteGridViewNameHeaderStyle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="[$Total]">
                                                <ItemTemplate>
                                                    <asp:Label ID="uxPriceLabel" runat="server" Text='<%# StoreContext.Currency.FormatPrice( Convert.ToInt32( Eval("Quantity") ) * Convert.ToDecimal( Eval("UnitPrice") ) )%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="MobileCheckoutCompleteGridViewPriceItemStyle" />
                                                <HeaderStyle CssClass="MobileCheckoutCompleteGridViewPriceHeaderStyle" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="MobileCheckoutCompleteGridViewRowStyle" />
                                        <HeaderStyle CssClass="MobileCheckoutCompleteGridViewHeaderStyle" />
                                        <AlternatingRowStyle CssClass="MobileCheckoutCompleteGridViewAlternatingRowStyle" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td class="MobileCheckoutCompleteOrderSummaryColumn">
                                    <table class="MobileCheckoutCompleteSubtotalTable" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td class="MobileCheckoutCompleteSubtotalLabel">
                                                [$Cost] :
                                            </td>
                                            <td class="MobileCheckoutCompleteSubtotalValue">
                                                <asp:Label ID="uxProductCostLabel" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="MobileCheckoutCompleteSubtotalLabel">
                                                [$Discount] :
                                            </td>
                                            <td class="MobileCheckoutCompleteSubtotalValue">
                                                <asp:Label ID="uxDiscountLabel" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="MobileCheckoutCompleteSubtotalLabel">
                                                [$Tax] :
                                            </td>
                                            <td class="MobileCheckoutCompleteSubtotalValue">
                                                <asp:Label ID="uxTaxLabel" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="MobileCheckoutCompleteSubtotalLabel">
                                                [$ShippCost] :
                                            </td>
                                            <td class="MobileCheckoutCompleteSubtotalValue">
                                                <asp:Label ID="uxShippingCostLabel" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="uxHandlingFeeTR" runat="server">
                                            <td class="MobileCheckoutCompleteSubtotalLabel">
                                                [$HandlingFee] :
                                            </td>
                                            <td class="MobileCheckoutCompleteSubtotalValue">
                                                <asp:Label ID="uxHandlingFeeLabel" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="uxGiftCertificateTR" runat="server">
                                            <td class="MobileCheckoutCompleteSubtotalLabel">
                                                [$GiftCertificate] :
                                            </td>
                                            <td class="MobileCheckoutCompleteSubtotalValue">
                                                <asp:Label ID="uxGiftCertificateLabel" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="MobileCheckoutCompleteSubtotalTotalLabel">
                                                [$Total] :
                                            </td>
                                            <td class="MobileCheckoutCompleteSubtotalTotalPrice">
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
                    <td class="MobileCheckoutCompleteColumn">
                    </td>
                </tr>
                <tr>
                    <td class="MobileCheckoutCompleteColumn">
                        <asp:FormView ID="uxOrderView" runat="server" DataSourceID="uxOrderSource" CssClass="MobileCheckoutCompleteCustomerFormView">
                            <ItemTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" class="MobileCheckoutCompleteCustomerTable">
                                    <tr>
                                        <td class="MobileCheckoutCompleteCustomerHeaderColumn">
                                            [$Customer]
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="MobileCheckoutCompleteCustomerDetailsColumn">
                                            <table border="0" cellpadding="2" cellspacing="2" class="MobileCheckoutCompleteCustomerDetailsTable">
                                                <tr class="MobileCheckoutCompleteDetailsRow">
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                        [$FirstName] :
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
                                                        <asp:Label ID="FirstNameLabel" runat="server" Text='<%# Eval("Billing.FirstName") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="MobileCheckoutCompleteDetailsAlternateRow">
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                        [$LastName] :
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
                                                        <asp:Label ID="LastNameLabel" runat="server" Text='<%# Eval("Billing.LastName") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="MobileCheckoutCompleteDetailsRow">
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                        [$Company] :
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
                                                        <asp:Label ID="CompanyLabel" runat="server" Text='<%# Eval("Billing.Company") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="MobileCheckoutCompleteDetailsAlternateRow">
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                        [$Address] :
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
                                                        <asp:Label ID="Address1Label" runat="server" Text='<%# Eval("Billing.Address1") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr id="Tr1" class="MobileCheckoutCompleteDetailsAlternateRow" visible='<%# GetAddress2Visible( Eval( "Billing.Address2" ) ) %>'
                                                    runat="server">
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
                                                        <asp:Label ID="Address2Label" runat="server" Text='<%# Eval("Billing.Address2") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="MobileCheckoutCompleteDetailsRow">
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                        [$City] :
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
                                                        <asp:Label ID="CityLabel" runat="server" Text='<%# Eval("Billing.City") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="MobileCheckoutCompleteDetailsAlternateRow">
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                        [$State] :
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
                                                        <asp:Label ID="StateLabel" runat="server" Text='<%# Eval("Billing.State") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="MobileCheckoutCompleteDetailsRow">
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                        [$Zip] :
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
                                                        <asp:Label ID="ZipLabel" runat="server" Text='<%# Eval("Billing.Zip") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="MobileCheckoutCompleteDetailsAlternateRow">
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                        [$Country] :
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
                                                        <asp:Label ID="CountryLabel" runat="server" Text='<%# AddressUtilities.GetCountryNameByCountryCode (Eval("Billing.Country").ToString() ) %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="MobileCheckoutCompleteDetailsRow">
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                        [$Email] :
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
                                                        <asp:Label ID="EmailLabel" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="MobileCheckoutCompleteDetailsAlternateRow">
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                        [$Phone] :
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
                                                        <asp:Label ID="PhoneLabel" runat="server" Text='<%# Eval("Billing.Phone") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="MobileCheckoutCompleteDetailsRow">
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                        [$Fax] :
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
                                                        <asp:Label ID="FaxLabel" runat="server" Text='<%# Eval("Billing.Fax") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="MobileCheckoutCompleteCustomerHeaderColumn">
                                            [$InfoPay]
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="MobileCheckoutCompleteCustomerDetailsColumn">
                                            <table border="0" cellpadding="2" cellspacing="2" class="MobileCheckoutCompletePaymentTable">
                                                <tr class="MobileCheckoutCompleteDetailsRow">
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                        [$PaymentType] :
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
                                                        <asp:Label ID="PaymentMethodLabel" runat="server" Text='<%# ConvertPaymentMethod( Eval("PaymentMethod").ToString() ) %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="MobileCheckoutCompleteDetailsAlternateRow">
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                        [$OrderDate] :
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
                                                        <asp:Label ID="OrderDateLabel" runat="server" Text='<%# Bind("OrderDate") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr id="uxTrackingTR" runat="server" class="MobileCheckoutCompleteDetailsRow">
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                        [$TrackingNumber]
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
                                                        <asp:HyperLink ID="TrackingNumberLink" runat="server" Text='<%# Bind("TrackingNumber") %>'
                                                            Target="_blank" NavigateUrl='<%# GetTrackingUrl(Eval("TrackingMethod"), Eval("TrackingNumber")) %>'></asp:HyperLink>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="MobileCheckoutCompleteCustomerHeaderColumn">
                                            [$InfoOrderComment]
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="MobileCheckoutCompleteCustomerDetailsColumn">
                                            <table border="0" cellpadding="2" cellspacing="2" class="MobileCheckoutCompleteOrderCommentTable">
                                                <tr class="MobileCheckoutCompleteDetailsRow">
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                        [$OrderComment] :
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
                                                        <asp:Label ID="uxCustomerCommentsLabel" runat="server" Text='<%# WebUtilities.ReplaceNewLine( Eval("CustomerComments").ToString() ) %>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <%if (DataAccessContext.Configurations.GetBoolValue( "ShippingAddressMode" ))
                                      { %>
                                    <tr id="InfoShippTR" runat="server">
                                        <td class="MobileCheckoutCompleteCustomerHeaderColumn">
                                            [$InfoShipp]
                                        </td>
                                    </tr>
                                    <tr id="ShippDetailsTR" runat="server">
                                        <td class="MobileCheckoutCompleteCustomerDetailsColumn">
                                            <table border="0" cellpadding="2" cellspacing="2" class="MobileCheckoutCompleteShippingTable">
                                                <tr class="MobileCheckoutCompleteDetailsRow">
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                        [$FirstName] :
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
                                                        <asp:Label ID="ShippingFirstNameLabel" runat="server" Text='<%# Eval("Shipping.FirstName") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="MobileCheckoutCompleteDetailsAlternateRow">
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                        [$LastName] :
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
                                                        <asp:Label ID="ShippingLastNameLabel" runat="server" Text='<%# Eval("Shipping.LastName") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="MobileCheckoutCompleteDetailsRow">
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                        [$Company] :
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
                                                        <asp:Label ID="ShippingCompanyLabel" runat="server" Text='<%# Eval("Shipping.Company") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="MobileCheckoutCompleteDetailsAlternateRow">
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                        [$Address] :
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
                                                        <asp:Label ID="ShippingAddress1Label" runat="server" Text='<%# Eval("Shipping.Address1") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr id="Tr2" class="MobileCheckoutCompleteDetailsAlternateRow" visible='<%# GetAddress2Visible( Eval( "Shipping.Address2" ) ) %>'
                                                    runat="server">
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
                                                        <asp:Label ID="ShippingAddress2Label" runat="server" Text='<%# Eval("Shipping.Address2") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="MobileCheckoutCompleteDetailsRow">
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                        [$City] :
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
                                                        <asp:Label ID="ShippingCityLabel" runat="server" Text='<%# Eval("Shipping.City") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="MobileCheckoutCompleteDetailsAlternateRow">
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                        [$State] :
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
                                                        <asp:Label ID="ShippingStateLabel" runat="server" Text='<%# Eval("Shipping.State") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="MobileCheckoutCompleteDetailsRow">
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                        [$Zip] :
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
                                                        <asp:Label ID="ShippingZipLabel" runat="server" Text='<%# Eval("Shipping.Zip") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="MobileCheckoutCompleteDetailsAlternateRow">
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                        [$Country] :
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
                                                        <asp:Label ID="ShippingCountryLabel" runat="server" Text='<%# AddressUtilities.GetCountryNameByCountryCode ( Eval("Shipping.Country").ToString()) %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="MobileCheckoutCompleteDetailsRow">
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                        [$Phone] :
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
                                                        <asp:Label ID="ShippingPhoneLabel" runat="server" Text='<%# Eval("Shipping.Phone") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="MobileCheckoutCompleteDetailsAlternateRow">
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                        [$Fax] :
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
                                                        <asp:Label ID="ShippingFaxLabel" runat="server" Text='<%# Eval("Shipping.Fax") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <%} %>
                                </table>
                            </ItemTemplate>
                        </asp:FormView>
                    </td>
                </tr>
                <tr>
                    <td class="MobileCheckoutCompleteColumn">
                        <asp:FormView ID="uxClientForm" runat="server" DataSourceID="uxClientSource" CssClass="MobileCheckoutCompleteClientFormView">
                            <ItemTemplate>
                                <table id="Table1" cellpadding="3" cellspacing="2" class="MobileCheckoutCompleteCustomerTable">
                                    <tr>
                                        <td class="MobileCheckoutCompleteCustomerHeaderColumn">
                                            [$Billing]
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="MobileCheckoutCompleteCustomerDetailsColumn">
                                            <table border="0" cellpadding="2" cellspacing="2" class="MobileCheckoutCompleteCustomerDetailsTable">
                                                <tr class="MobileCheckoutCompleteDetailsRow">
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                        [$FName] :
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
                                                        <%# Eval( "Billing.FirstName" )%>
                                                    </td>
                                                </tr>
                                                <tr class="MobileCheckoutCompleteDetailsAlternateRow">
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                        [$LName] :
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
                                                        <%# Eval( "Billing.LastName" )%>
                                                    </td>
                                                </tr>
                                                <tr class="MobileCheckoutCompleteDetailsRow">
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                        [$Company] :
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
                                                        <%# Eval( "Billing.Company" )%>
                                                    </td>
                                                </tr>
                                                <tr class="MobileCheckoutCompleteDetailsAlternateRow">
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                        [$Address] :
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
                                                        <%# Eval( "Billing.Address1" )%>
                                                    </td>
                                                </tr>
                                                <tr id="Tr3" runat="server" class="MobileCheckoutCompleteDetailsAlternateRow" visible='<%# GetAddress2Visible( Eval( "Billing.Address2" ) ) %>'>
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
                                                        <%# Eval( "Billing.Address2" )%>
                                                    </td>
                                                </tr>
                                                <tr class="MobileCheckoutCompleteDetailsRow">
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                        [$City] :
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
                                                        <%# Eval( "Billing.City" )%>
                                                    </td>
                                                </tr>
                                                <tr class="MobileCheckoutCompleteDetailsAlternateRow">
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                        [$State] :
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
                                                        <%# Eval( "Billing.State" )%>
                                                    </td>
                                                </tr>
                                                <tr class="MobileCheckoutCompleteDetailsRow">
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                        [$Zip] :
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
                                                        <%# Eval( "Billing.Zip" )%>
                                                    </td>
                                                </tr>
                                                <tr class="MobileCheckoutCompleteDetailsAlternateRow">
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                        [$Country] :
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
                                                        <%# Eval( "Billing.Country" )%>
                                                    </td>
                                                </tr>
                                                <tr class="MobileCheckoutCompleteDetailsRow">
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                        [$Phone] :
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
                                                        <%# Eval( "Billing.Phone" )%>
                                                    </td>
                                                </tr>
                                                <tr class="MobileCheckoutCompleteDetailsAlternateRow">
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                        [$Fax] :
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
                                                        <%# Eval( "Billing.Fax" )%>
                                                    </td>
                                                </tr>
                                                <tr class="MobileCheckoutCompleteDetailsRow">
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                        [$Email] :
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
                                                        <%# Eval( "Email" )%>
                                                    </td>
                                                </tr>
                                                <tr class="MobileCheckoutCompleteDetailsAlternateRow">
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                        [$UrlClient] :
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
                                                        <%# Eval( "UrlClient" )%>
                                                    </td>
                                                </tr>
                                            </table>
                                    </tr>
                                    <tr>
                                        <td class="MobileCheckoutCompleteCustomerHeaderColumn">
                                            [$Shipping]
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="MobileCheckoutCompleteCustomerDetailsColumn">
                                            <table border="0" cellpadding="2" cellspacing="2" class="MobileCheckoutCompleteShippingTable">
                                                <tr class="MobileCheckoutCompleteDetailsRow">
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                        [$FName] :
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
                                                        <%# Eval( "Shipping.FirstName" )%>
                                                    </td>
                                                </tr>
                                                <tr class="MobileCheckoutCompleteDetailsAlternateRow">
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                        [$LName] :
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
                                                        <%# Eval( "Shipping.LastName" )%>
                                                    </td>
                                                </tr>
                                                <tr class="MobileCheckoutCompleteDetailsRow">
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                        [$Company] :
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
                                                        <%# Eval( "Shipping.Company" )%>
                                                    </td>
                                                </tr>
                                                <tr class="MobileCheckoutCompleteDetailsAlternateRow">
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                        [$Address] :
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
                                                        <%# Eval( "Shipping.Address1" )%>
                                                    </td>
                                                </tr>
                                                <tr id="Tr4" class="MobileCheckoutCompleteDetailsAlternateRow" visible='<%# GetAddress2Visible( Eval( "Shipping.Address2" ) ) %>'
                                                    runat="server">
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
                                                        <%# Eval( "Shipping.Address2" )%>
                                                    </td>
                                                </tr>
                                                <tr class="MobileCheckoutCompleteDetailsRow">
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                        [$City] :
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
                                                        <%# Eval( "Shipping.City" )%>
                                                    </td>
                                                </tr>
                                                <tr class="MobileCheckoutCompleteDetailsAlternateRow">
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                        [$State] :
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
                                                        <%# Eval( "Shipping.State" )%>
                                                    </td>
                                                </tr>
                                                <tr class="MobileCheckoutCompleteDetailsRow">
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                        [$Zip] :
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
                                                        <%# Eval( "Shipping.Zip" )%>
                                                    </td>
                                                </tr>
                                                <tr class="MobileCheckoutCompleteDetailsAlternateRow">
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                        [$Country] :
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
                                                        <%# Eval( "Shipping.Country" )%>
                                                    </td>
                                                </tr>
                                                <tr class="MobileCheckoutCompleteDetailsRow">
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                        [$Phone] :
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
                                                        <%# Eval( "Shipping.Phone" )%>
                                                    </td>
                                                </tr>
                                                <tr class="MobileCheckoutCompleteDetailsAlternateRow">
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                        [$Fax] :
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
                                                        <%# Eval( "Shipping.Fax" )%>
                                                    </td>
                                                </tr>
                                                <tr class="MobileCheckoutCompleteDetailsRow">
                                                    <td class="MobileCheckoutCompleteLabelColumn">
                                                        [$MerchantNotes] :
                                                    </td>
                                                    <td class="MobileCheckoutCompleteDataColumn">
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
    <div class="MobileShoppingCartBackHomeLinkDiv">
        <asp:HyperLink ID="uxHomepageHyperLink" runat="server" NavigateUrl="Default.aspx"
            CssClass="MobileCommonHyperLink">[$GoHome]</asp:HyperLink>
    </div>
</asp:Content>
