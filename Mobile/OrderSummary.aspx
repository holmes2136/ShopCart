<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile/Mobile.master" AutoEventWireup="true"
    CodeFile="OrderSummary.aspx.cs" Inherits="Mobile_OrderSummary" %>

<%@ Register Src="~/Mobile/Components/MobileCouponMessageDisplay.ascx" TagName="CouponMessageDisplay"
    TagPrefix="uc3" %>
<%@ Register Src="~/Components/Common/TooltippedText.ascx" TagName="TooltippedText"
    TagPrefix="uc2" %>
<%@ Register Src="~/Components/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Import Namespace="Vevo" %>
<%@ Import Namespace="Vevo.Domain.Orders" %>
<%@ Import Namespace="Vevo.WebAppLib" %>
<%@ Import Namespace="Vevo.WebUI.Users" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="MobileTitle">
        [$Head]
    </div>
    <div class="MobileShoppingCartPanel">
        <div id="uxApplyCouponDiv" runat="server" class="MobileOrderSummaryApplyCouponDiv">
            <ul class="MobileOrderSummaryApplyCouponList">
                <li class="MobileOrderSummaryApplyCouponItemList">
                    <asp:HyperLink ID="uxApplyCouponAndGiftLink" runat="server" Text="Apply coupon or gift certificate"
                        NavigateUrl="CouponAndGift.aspx?IsSummaryPage=true" CssClass="MobileOrderSummaryApplyCouponLink">
                    </asp:HyperLink>
                </li>
            </ul>
        </div>
        <div class="MobileOrderSummaryFormViewDiv">
            <asp:FormView ID="uxShippingForm" runat="server" CssClass="MobileOrderSummaryShippingFromFormView"
                BorderWidth="0px" CellPadding="0" CellSpacing="0">
                <ItemTemplate>
                    <table id="T_Detail" cellpadding="3" cellspacing="2" class="MobileOrderSummaryShippingDetailsTable">
                        <tr>
                            <td colspan="2" class="MobileOrderSummaryHeader" style="font-weight: bold;">
                                [$Shipping]
                            </td>
                        </tr>
                        <tr class="MobileOrderSummaryShippingDetailsRow">
                            <td class="MobileOrderSummaryShippingDetailsLabelColumn">
                                [$FName] :
                            </td>
                            <td class="MobileOrderSummaryShippingDetailsDataColumn">
                                <%# Eval( "ShippingAddress.FirstName" )%>
                            </td>
                        </tr>
                        <tr class="MobileOrderSummaryShippingDetailsAlternateRow">
                            <td class="MobileOrderSummaryShippingDetailsLabelColumn">
                                [$LName] :
                            </td>
                            <td class="MobileOrderSummaryShippingDetailsDataColumn">
                                <%# Eval( "ShippingAddress.LastName" )%>
                            </td>
                        </tr>
                        <tr class="MobileOrderSummaryShippingDetailsRow">
                            <td class="MobileOrderSummaryShippingDetailsLabelColumn">
                                [$Company] :
                            </td>
                            <td class="MobileOrderSummaryShippingDetailsDataColumn">
                                <%# Eval( "ShippingAddress.Company" )%>
                            </td>
                        </tr>
                        <tr class="MobileOrderSummaryShippingDetailsAlternateRow">
                            <td class="MobileOrderSummaryShippingDetailsLabelColumn">
                                [$Address] :
                            </td>
                            <td class="MobileOrderSummaryShippingDetailsDataColumn">
                                <%# Eval( "ShippingAddress.Address1" )%>
                            </td>
                        </tr>
                        <tr class="MobileOrderSummaryShippingDetailsRow">
                            <td class="MobileOrderSummaryShippingDetailsLabelColumn">
                            </td>
                            <td class="MobileOrderSummaryShippingDetailsDataColumn">
                                <%# Eval( "ShippingAddress.Address2" )%>
                            </td>
                        </tr>
                        <tr class="MobileOrderSummaryShippingDetailsRow">
                            <td class="MobileOrderSummaryShippingDetailsLabelColumn">
                                [$City] :
                            </td>
                            <td class="MobileOrderSummaryShippingDetailsDataColumn">
                                <%# Eval( "ShippingAddress.City" )%>
                            </td>
                        </tr>
                        <tr class="MobileOrderSummaryShippingDetailsAlternateRow">
                            <td class="MobileOrderSummaryShippingDetailsLabelColumn">
                                [$State] :
                            </td>
                            <td class="MobileOrderSummaryShippingDetailsDataColumn">
                                <%# Eval( "ShippingAddress.State" )%>
                            </td>
                        </tr>
                        <tr class="MobileOrderSummaryShippingDetailsRow">
                            <td class="MobileOrderSummaryShippingDetailsLabelColumn">
                                [$Zip] :
                            </td>
                            <td class="MobileOrderSummaryShippingDetailsDataColumn">
                                <%# Eval( "ShippingAddress.Zip" )%>
                            </td>
                        </tr>
                        <tr class="MobileOrderSummaryShippingDetailsAlternateRow">
                            <td class="MobileOrderSummaryShippingDetailsLabelColumn">
                                [$Country] :
                            </td>
                            <td class="MobileOrderSummaryShippingDetailsDataColumn">
                                <%# AddressUtilities.GetCountryNameByCountryCode( Eval( "ShippingAddress.Country" ).ToString() )%>
                            </td>
                        </tr>
                        <tr class="MobileOrderSummaryShippingDetailsRow">
                            <td class="MobileOrderSummaryShippingDetailsLabelColumn">
                                [$Phone] :
                            </td>
                            <td class="MobileOrderSummaryShippingDetailsDataColumn">
                                <%# Eval( "ShippingAddress.Phone" )%>
                            </td>
                        </tr>
                        <tr class="MobileOrderSummaryShippingDetailsAlternateRow">
                            <td class="MobileOrderSummaryShippingDetailsLabelColumn">
                                [$Fax] :
                            </td>
                            <td class="MobileOrderSummaryShippingDetailsDataColumn">
                                <%# Eval( "ShippingAddress.Fax" )%>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
                <RowStyle CssClass="MobileOrderSummaryShippingFromFormViewRowStyle" />
            </asp:FormView>
            <table cellpadding="0" cellspacing="0" border="0" class="MobileOrderSummaryInnerTable">
                <tr>
                    <td class="MobileOrderSummaryInnerGridViewColumn">
                        <asp:GridView ID="uxGridCart" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            GridLines="None" CssClass="MobileOrderSummaryGridView">
                            <RowStyle CssClass="MobileOrderSummaryGridViewRowStyle" />
                            <HeaderStyle CssClass="MobileOrderSummaryGridViewHeaderStyle" />
                            <AlternatingRowStyle CssClass="MobileOrderSummaryGridViewAlternatingRowStyle" />
                            <Columns>
                                <asp:TemplateField HeaderText="[$Name]">
                                    <ItemTemplate>
                                        <asp:Label ID="uxProductNameLabel" runat="server" Text='<%# GetItemName( Container.DataItem ) %>' />
                                        <div class="ProductNameDetails">
                                            <uc2:TooltippedText ID="uxTooltippedText" runat="server" MainText='<%# GetTooltipMainText( Container.DataItem ) %>'
                                                TooltipText='<%# GetTooltipBody( Container.DataItem )  %>' Visible='<%# Eval( "IsRecurring" ) %>' />
                                        </div>
                                        <div class="MobileShoppingCartGridViewPrice">
                                            <asp:Label ID="uxPriceLabel" runat="server" Text='<%# GetUnitPriceText( (ICartItem) Container.DataItem ) %>'
                                                CssClass="price" />
                                            <span class="multiply">x
                                                <%# Eval( "Quantity" ) %></span>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="MobileOrderSummaryNameHeaderStyle" />
                                    <ItemStyle CssClass="MobileOrderSummaryNameItemStyle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="[$Subtotal]" HeaderStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="uxPriceLabel" runat="server" Text='<%# GetSubtotalText( (ICartItem) Container.DataItem ) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="MobileOrderSummarySubtotalHeaderStyle" />
                                    <ItemStyle CssClass="MobileOrderSummarySubtotalItemStyle" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table id="T_Summary" class="MobileOrderSummarySummaryTable" cellpadding="0" cellspacing="0">
                            <tr class="MobileOrderSummarySummaryRow">
                                <td class="MobileOrderSummarySummaryLabel">
                                    [$Product]
                                </td>
                                <td class="MobileOrderSummarySummaryValue">
                                    <asp:Label ID="uxProductCostLabel" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr class="MobileOrderSummarySummaryRow">
                                <td class="MobileOrderSummarySummaryLabel">
                                    [$Discount]
                                </td>
                                <td class="MobileOrderSummarySummaryValue">
                                    <asp:Label ID="uxDiscountLabel" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr class="MobileOrderSummarySummaryRow">
                                <td class="MobileOrderSummarySummaryLabel">
                                    [$Tax]
                                </td>
                                <td class="MobileOrderSummarySummaryValue">
                                    <asp:Label ID="uxTaxLabel" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr class="MobileOrderSummarySummaryRow">
                                <td class="MobileOrderSummarySummaryLabel">
                                    [$ShippingCost]
                                </td>
                                <td class="MobileOrderSummarySummaryValue">
                                    <asp:Label ID="uxShippingCostLabel" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr id="uxHandlingFeeTR" runat="server" class="MobileOrderSummarySummaryRow">
                                <td class="MobileOrderSummarySummaryLabel">
                                    [$HandlingFee]
                                </td>
                                <td class="MobileOrderSummarySummaryValue">
                                    <asp:Label ID="uxHandlingFeeLabel" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr id="uxGiftCertificateTR" runat="server" class="MobileOrderSummarySummaryRow">
                                <td class="MobileOrderSummarySummaryLabel">
                                    [$GiftCertificate]
                                </td>
                                <td class="MobileOrderSummarySummaryValue">
                                    <asp:Label ID="uxGiftCertificateLabel" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr class="MobileOrderSummarySummaryTotalRow">
                                <td class="MobileOrderSummarySummaryTotalLabel">
                                    [$Total]
                                </td>
                                <td class="MobileOrderSummarySummaryTotalValue">
                                    <asp:Label ID="uxTotalLabel" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <table id="uxOrderSummaryCommentTD" runat="server" border="0" cellpadding="0" cellspacing="0"
            class="MobileOrderSummaryCommentTable">
            <tr>
                <td class="MobileOrderSummaryCommentLabelColumn">
                    [$OrderComment] &nbsp;
                </td>
                <td class="MobileOrderSummaryCommentValueColumn">
                    <asp:Label ID="uxOrderCommentLabel" runat="server" CssClass="MobileOrderSummaryCommentLabel">
                    </asp:Label>&nbsp;
                </td>
            </tr>
        </table>
        <div id="uxMessageDiv" runat="server" class="MobileOrderSummaryMessageDiv" visible="false">
            <asp:Literal ID="uxMessageLiteral" runat="server" />
        </div>
        <div id="uxWarningMessageDiv" runat="server" class="MobileOrderSummaryWarningMessageDiv"
            visible="false">
            <uc3:CouponMessageDisplay ID="uxCouponMessageDisplay" runat="server" />
            <asp:Literal ID="uxWarningMessageLiteral" runat="server" />
        </div>
        <div id="uxStockMessageDiv" runat="server" visible="false" class="MobileOrderSummaryStockMessageDiv">
            <asp:Literal ID="uxStockMessageLiteral" runat="server" />
        </div>
        <div id="uxQuantityMessageDiv" runat="server" visible="false" class="MobileOrderSummaryQuantityMessageDiv">
            <asp:Literal ID="uxQuantityMessageLiteral" runat="server" />
        </div>
        <div class="MobileOrderSummaryBackLinkDiv">
            <asp:HyperLink ID="uxBackHomeLink" runat="server" NavigateUrl="ShoppingCart.aspx"
                Visible="False" CssClass="MobileCommonHyperLink">[$Go back to shopping cart page]</asp:HyperLink></div>
        <div class="MobileOrderSummaryButtonDiv">
            <asp:Label ID="uxButtonDescriptionLabel" runat="server" Text="[$Press]" CssClass="MobileOrderSummaryDescriptionLabel">
            </asp:Label>
            <asp:Button ID="uxFinishButton" runat="server" OnClick="uxFinishButton_Click" Text="Place Order"
                Width="102px" Visible="False" CssClass="MobileOrderSummaryFinishButton" />
            <%-- <vevo:ImageButton ID="uxFinishImageButton" runat="server" OnClick="uxFinishImageButton_Click"
                ThemeImageUrl="[$ImgButton]" CssClass="MobileOrderSummaryFinishImageButton" />--%>
        </div>
        <div class="MobileCommonBox">
                <asp:LinkButton ID="uxFinishImageButton" runat="server" Text="[$PlaceOrder]" OnClick="uxFinishImageButton_Click"
                    CssClass="MobileButton MobileCouponButton" />
            
        </div>
    </div>
</asp:Content>
