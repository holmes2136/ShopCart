<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="OrderSummary.aspx.cs"
    Inherits="OrderSummary" Title="[$Title]" %>

<%@ Register Src="Components/CouponMessageDisplay.ascx" TagName="CouponMessageDisplay"
    TagPrefix="uc3" %>
<%@ Register Src="Components/Common/TooltippedText.ascx" TagName="TooltippedText"
    TagPrefix="uc2" %>
<%@ Register Src="Components/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="Components/CheckoutIndicator.ascx" TagName="CheckoutIndicator"
    TagPrefix="uc1" %>
<%@ Import Namespace="Vevo" %>
<%@ Import Namespace="Vevo.Domain" %>
<%@ Import Namespace="Vevo.Domain.Orders" %>
<%@ Import Namespace="Vevo.WebAppLib" %>
<%@ Import Namespace="Vevo.WebUI" %>
<%@ Import Namespace="Vevo.WebUI.Users" %>
<asp:Content ID="uxTopContent" ContentPlaceHolderID="uxTopPlaceHolder" runat="Server">
    <uc1:Message ID="uxMessageLiteral" runat="server" NumberOfNewLines="1" />
    <uc1:Message ID="uxMessage" runat="server" NumberOfNewLines="1" />
    <uc3:CouponMessageDisplay ID="uxCouponMessageDisplay" runat="server" />
    <uc1:CheckoutIndicator ID="uxCheckoutIndicator" runat="server" StepID="5" Title="[$Head]" />
</asp:Content>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="OrderSummary">
        <div class="CommonPage">
            <div class="CommonPageLeft">
                <div class="CommonPageRight">
                    <div class="SidebarTop">
                        <asp:Label ID="uxHeaderCheckoutLabel" runat="server" Text="[$Summary]" CssClass="CheckoutAddressTitle"></asp:Label>
                    </div>
                    <div id="uxApplyCouponDiv" runat="server" class="OrderSummaryApplyCouponDiv">
                        <ul class="OrderSummaryApplyCouponList">
                            <li class="OrderSummaryApplyCouponItemList">
                                <asp:HyperLink ID="uxApplyCouponAndGiftLink" runat="server" Text="Apply coupon or gift certificate"
                                    NavigateUrl="CouponAndGift.aspx?IsSummaryPage=true" CssClass="OrderSummaryApplyCouponLink">
                                </asp:HyperLink>
                            </li>
                        </ul>
                    </div>
                    <div class="OrderSummaryFormViewDiv">
                        <asp:FormView ID="uxShippingForm" runat="server" CssClass="OrderSummaryShippingFromFormView"
                            BorderWidth="0px" CellPadding="0" CellSpacing="0">
                            <ItemTemplate>
                                <div class="BillingAddressDiv">
                                    <div class="CommonPageInnerTitle">
                                        [$Billing]</div>
                                    <div class="CommonAddressDetail">
                                        <div class="CommonAddressRow">
                                            <%# Eval("BillingAddress.FirstName")%>
                                            <%# Eval( "BillingAddress.LastName" )%></div>
                                        <div class="CommonAddressRow">
                                            <%# Eval( "BillingAddress.Company" )%></div>
                                        <div class="CommonAddressRow">
                                            <%# Eval( "BillingAddress.Address1" )%>
                                            <%# Eval( "BillingAddress.Address2" )%></div>
                                        <div class="CommonAddressRow">
                                            <%# Eval( "BillingAddress.City" )%></div>
                                        <div class="CommonAddressRow">
                                            <%# Eval( "BillingAddress.State" )%></div>
                                        <div class="CommonAddressRow">
                                            <%# AddressUtilities.GetCountryNameByCountryCode( Eval( "BillingAddress.Country" ).ToString() )%>
                                            <%# Eval( "BillingAddress.Zip" )%></div>
                                        <div class="CommonAddressRow">
                                            [$Phone] :
                                            <%# Eval( "BillingAddress.Phone" )%></div>
                                        <div class="CommonAddressRow">
                                            [$Fax] :
                                            <%# Eval( "BillingAddress.Fax" )%></div>
                                    </div>
                                </div>
                                <div class="ShippingAddressDiv">
                                    <div class="CommonPageInnerTitle">
                                        [$Shipping]</div>
                                    <div class="CommonAddressDetail">
                                        <div class="CommonAddressRow">
                                            <%# Eval("ShippingAddress.FirstName")%> 
                                            <%# Eval( "ShippingAddress.LastName" )%></div>
                                        <div class="CommonAddressRow">
                                            <%# Eval( "ShippingAddress.Company" )%></div>
                                        <div class="CommonAddressRow">
                                            <%# Eval( "ShippingAddress.Address1" )%>
                                            <%# Eval( "ShippingAddress.Address2" )%></div>
                                        <div class="CommonAddressRow">
                                            <%# Eval( "ShippingAddress.City" )%></div>
                                        <div class="CommonAddressRow">
                                            <%# Eval( "ShippingAddress.State" )%></div>
                                        <div class="CommonAddressRow">
                                            <%# AddressUtilities.GetCountryNameByCountryCode( Eval( "ShippingAddress.Country" ).ToString() )%>
                                            <%# Eval( "ShippingAddress.Zip" )%></div>
                                        <div class="CommonAddressRow">
                                            [$Phone] :
                                            <%# Eval( "ShippingAddress.Phone" )%></div>
                                        <div class="CommonAddressRow">
                                            [$Fax] :
                                            <%# Eval( "ShippingAddress.Fax" )%></div>
                                    </div>
                                </div>
                            </ItemTemplate>
                            <RowStyle CssClass="OrderSummaryShippingFromFormViewRowStyle" />
                        </asp:FormView>
                        <table cellpadding="0" cellspacing="0" border="0" class="OrderSummaryInnerTable CommonGridView">
                            <tr>
                                <td class="OrderSummaryInnerGridViewColumn">
                                    <asp:GridView ID="uxGridCart" runat="server" AutoGenerateColumns="False" CellPadding="0"
                                        GridLines="None" CssClass="OrderSummaryGridView CommonGridView">
                                        <RowStyle CssClass="CommonGridViewRowStyle" />
                                        <HeaderStyle CssClass="CommonGridViewHeaderStyle" />
                                        <AlternatingRowStyle CssClass="CommonGridViewAlternatingRowStyle" />
                                        <EmptyDataRowStyle CssClass="CommonGridViewEmptyRowStyle" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="[$Name]">
                                                <ItemTemplate>
                                                    <asp:Label ID="uxProductNameLabel" runat="server" Text='<%# GetItemName( Container.DataItem ) %>' />
                                                    <div class='ProductNameDetails'>
                                                        <uc2:TooltippedText ID="uxTooltippedText" runat="server" MainText='<%# GetTooltipMainText( Container.DataItem ) %>'
                                                            TooltipText='<%# GetTooltipBody( Container.DataItem )  %>' Visible='<%# Eval( "IsRecurring" ) %>' />
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="OrderSummaryNameHeaderStyle" />
                                                <ItemStyle CssClass="OrderSummaryNameItemStyle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="[$UnitPrice]" HeaderStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="uxPriceLabel" runat="server" Text='<%# GetUnitPriceText( (ICartItem) Container.DataItem ) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="OrderSummaryUnitPriceHeaderStyle" />
                                                <ItemStyle CssClass="OrderSummaryUnitPriceItemStyle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="[$Quantity]">
                                                <ItemTemplate>
                                                    <asp:Label ID="uxQuantityText" runat="server" Text='<%#Bind("Quantity") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="OrderSummaryQuantityItemStyle" />
                                                <HeaderStyle CssClass="OrderSummaryQuantityHeaderStyle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="[$Subtotal]" HeaderStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="uxPriceLabel" runat="server" Text='<%# GetSubtotalText( (ICartItem) Container.DataItem ) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="OrderSummarySubtotalHeaderStyle" />
                                                <ItemStyle CssClass="OrderSummarySubtotalItemStyle" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td class="OrderSummaryInnerSummaryColumn">
                                    <table id="T_Summary" class="OrderSummarySummaryTable" cellpadding="0" cellspacing="0" >
                                        <tr class="OrderSummarySummaryRow">
                                            <td class="OrderSummarySummaryLabel">
                                                [$Product]
                                            </td>
                                            <td class="OrderSummarySummaryValue">
                                                <asp:Label ID="uxProductCostLabel" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr class="OrderSummarySummaryRow">
                                            <td class="OrderSummarySummaryLabel">
                                                [$Discount]
                                            </td>
                                            <td class="OrderSummarySummaryValue">
                                                <asp:Label ID="uxDiscountLabel" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <% if (DataAccessContext.Configurations.GetBoolValue("PointSystemEnabled", StoreContext.CurrentStore) && KeyUtilities.IsDeluxeLicense(Vevo.Shared.DataAccess.DataAccessHelper.DomainRegistrationkey, Vevo.Shared.DataAccess.DataAccessHelper.DomainName))
                                           { %>
                                        <tr class="OrderSummarySummaryRow">
                                            <td class="OrderSummarySummaryLabel">
                                                [$PointDiscount]
                                            </td>
                                            <td class="OrderSummarySummaryValue">
                                                <asp:Label ID="uxPointDiscountLabel" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <%} %>
                                        <tr class="OrderSummarySummaryRow">
                                            <td class="OrderSummarySummaryLabel">
                                                [$Tax]
                                            </td>
                                            <td class="OrderSummarySummaryValue">
                                                <asp:Label ID="uxTaxLabel" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr class="OrderSummarySummaryRow">
                                            <td class="OrderSummarySummaryLabel">
                                                [$ShippingCost]
                                            </td>
                                            <td class="OrderSummarySummaryValue">
                                                <asp:Label ID="uxShippingCostLabel" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="uxHandlingFeeTR" runat="server" class="OrderSummarySummaryRow">
                                            <td class="OrderSummarySummaryLabel">
                                                [$HandlingFee]
                                            </td>
                                            <td class="OrderSummarySummaryValue">
                                                <asp:Label ID="uxHandlingFeeLabel" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="uxGiftCertificateTR" runat="server" class="OrderSummarySummaryRow">
                                            <td class="OrderSummarySummaryLabel">
                                                [$GiftCertificate]
                                            </td>
                                            <td class="OrderSummarySummaryValue">
                                                <asp:Label ID="uxGiftCertificateLabel" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr class="OrderSummarySummaryTotalRow">
                                            <td class="OrderSummarySummaryTotalLabel">
                                                [$Total]
                                            </td>
                                            <td class="OrderSummarySummaryTotalValue">
                                                <asp:Label ID="uxTotalLabel" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table class="OrderSummaryOfferDetailsTable" cellpadding="0" cellspacing="0">
                            <tr>
                                <th class="CommonGridViewHeaderStyle" style="text-align: left; padding-left: 5px;"
                                    colspan="2">
                                    [$OtherInformation]
                                </th>
                            </tr>
                            <tr class="CommonGridViewRowStyle">
                                <td class="OrderSummaryShippingDetailsLabelColumn">
                                    [$OrderComment]
                                </td>
                                <td class="OrderSummaryShippingDetailsDataColumn">
                                    <asp:Label ID="uxCustomerCommentsLabel" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <% if (DisplayPointEarned())
                               { %>
                            <tr class="CommonGridViewAlternatingRowStyle">
                                <td class="OrderSummaryShippingDetailsLabelColumn">
                                    [$RewardPointEarned1]
                                </td>
                                <td class="OrderSummaryShippingDetailsDataColumn">
                                    <asp:Label ID="uxPointEarnedLabel" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <%} %>
                        </table>
                    </div>
                    <div id="uxStockMessageDiv" runat="server" visible="false" class="CommonDisplayMessageDiv">
                        <asp:Literal ID="uxStockMessageLiteral" runat="server" />
                    </div>
                    <div id="uxQuantityMessageDiv" runat="server" visible="false" class="CommonDisplayMessageDiv">
                        <asp:Literal ID="uxQuantityMessageLiteral" runat="server" />
                    </div>
                    <div class="OrderSummaryBackLinkDiv">
                        <asp:HyperLink ID="uxBackHomeLink" runat="server" NavigateUrl="ShoppingCart.aspx"
                            Visible="False" CssClass="CommonHyperLink">
                        [$Go back to shopping cart page]
                        </asp:HyperLink>
                    </div>
                    <div class="OrderSummaryButtonDiv">
                        <asp:Label ID="uxButtonDescriptionLabel" runat="server" Text="[$Press]" CssClass="OrderSummaryDescriptionLabel">
                        </asp:Label>
                        <asp:Button ID="uxFinishButton" runat="server" OnClick="uxFinishButton_Click" Text="Place Order"
                            Width="102px" Visible="False" CssClass="OrderSummaryFinishButton" />
                        <asp:LinkButton ID="uxFinishImageButton" runat="server" OnClick="uxFinishImageButton_Click"
                            Text="[$BtnPlaceOrder]" CssClass="OrderSummaryFinishImageButton BtnStyle1" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="Clear">
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
