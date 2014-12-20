<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="ShoppingCart.aspx.cs"
    Inherits="ShoppingCartPage" Title="[$Title]" %>

<%@ Register Src="Components/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="Components/Common/TooltippedText.ascx" TagName="TooltippedText"
    TagPrefix="uc2" %>
<%@ Register Src="Components/VevoHyperLink.ascx" TagName="HyperLink" TagPrefix="ucHyperLink" %>
<%@ Register TagPrefix="cc1" Namespace="GCheckout.Checkout" Assembly="GCheckout" %>

<%@ Register Src="Components/ShoppingCartGiftCoupon.ascx" TagName="GiftCouponDetail"
    TagPrefix="uc3" %>
<%@ Register Src="Components/CheckoutShippingEstimator.ascx" TagName="CheckoutShippingEstimator"
    TagPrefix="uc5" %>
<%@ Import Namespace="Vevo" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <uc1:Message ID="uxMessage" runat="server" NumberOfNewLines="1"  />
    <uc1:Message ID="uxMessageLiteral" runat="server" NumberOfNewLines="1" />
    <div class="ShoppingCart">
        <div class="CommonPage">
            <div class="CommonPageTop">
                <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/DefaultBoxTopLeft.gif" runat="server"
                    CssClass="CommonPageTopImgLeft" />
                <asp:Label ID="uxDefaultTitle" runat="server" CssClass="CommonPageTopTitle">[$Your Shopping Cart Items]</asp:Label>
                <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/DefaultBoxTopRight.gif"
                    runat="server" CssClass="CommonPageTopImgRight" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonPageLeft">
                <div class="CommonPageRight">
                    <asp:Panel ID="uxPanel" runat="server" CssClass="ShoppingCartPanel">
                        <table class="ShoppingCartTable">
                            <tr>
                                <td>
                                    <asp:GridView ID="uxGrid" runat="server" AutoGenerateColumns="False" DataKeyNames="CartItemID,ProductID,Options,GiftDetails,OptionCombinationID,IsPromotion,IsProductKit"
                                        OnRowDeleting="uxGrid_RowDeleting" CellPadding="4" CellSpacing="0" GridLines="None"
                                        OnRowDataBound="uxGrid_RowDataBound" CssClass="CommonGridView ShoppingCartGridView">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <div class="ImageItemDiv">
                                                        <asp:HyperLink ID="uxItemImageLink" runat="server" NavigateUrl='<%# GetLink( Container.DataItem ) %>'>
                                                            <asp:Image ID="uxItemImage" runat="server" ImageUrl='<%# GetItemImage( Container.DataItem ) %>'/>
                                                        </asp:HyperLink></div>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="ImageHeader" />
                                                <ItemStyle CssClass="ImageItem" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="[$Name]" SortExpression="Name">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="uxItemNameLink" runat="server" NavigateUrl='<%# GetLink( Container.DataItem ) %>'
                                                        Text='<%# GetName( Container.DataItem ) %>' CssClass="CommonHyperLink">
                                                    </asp:HyperLink>
                                                    <div class="ProductNameDetails">
                                                        <uc2:TooltippedText ID="uxTooltippedText" runat="server" MainText='<%# GetMainText( Container.DataItem ) %>'
                                                            TooltipText='<%# GetTooltipText( Container.DataItem ) %>' Visible='<%# Eval( "IsRecurring" ) %>' />
                                                    </div>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="NameItem" />
                                                <HeaderStyle CssClass="NameHeader" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="[$Unit Price]" SortExpression="Price">
                                                <ItemTemplate>
                                                    <asp:Label ID="uxLabel" runat="server" Text='<%# GetUnitPriceText( Container.DataItem ) %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="PriceItem" />
                                                <HeaderStyle CssClass="PriceHeader" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="[$Quantity]" SortExpression="Quantity">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="uxQuantityText" runat="server" Text='<%#Bind("Quantity") %>'
                                                        ValidationGroup="ShippingCartValid" />
                                                </ItemTemplate>
                                                <ItemStyle CssClass="QuantityItem" />
                                                <HeaderStyle CssClass="QuantityHeader" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="[$Subtotal]" SortExpression="Subtotal">
                                                <ItemStyle CssClass="SubtotalItem" />
                                                <HeaderStyle CssClass="SubtotalHeader" />
                                                <ItemTemplate>
                                                    <asp:Label ID="uxSubtotalLabel" runat="server" Text='<%# GetSubtotalText( Container.DataItem ) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="Del">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="uxDeleteImageButton" runat="server" Text="[$BtnDelete]" CssClass="ButtonDelete"
                                                        CommandName="Delete" />
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="DeleteHeader" />
                                                <ItemStyle CssClass="DeleteItem" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="CommonGridViewHeaderStyle ShoppingCartGridViewFooterStyle" />
                                        <RowStyle CssClass="CommonGridViewRowStyle ShoppingCartGridViewRowStyle" />
                                        <HeaderStyle CssClass="CommonGridViewHeaderStyle ShoppingCartGridViewHeaderStyle" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td class="ShoppingCartButton">
                                    <asp:LinkButton ID="uxUpdateQuantityImageButton" runat="server" OnClick="uxUpdateQuantityImageButton_Click"
                                        ValidationGroup="ShippingCartValid" CssClass="ShoppingCartUpdateQuantity BtnStyle2"
                                        Text="[$UpdateShoppingCart]" />
                                    <asp:LinkButton ID="uxContinueLink" runat="server" CssClass="ShoppingCartContinueShopping BtnStyle2"
                                        Text="[$ContinueShopping]" />
                                    <asp:LinkButton ID="uxClearCart" runat="server" CssClass="ShoppingCartClearCart BtnStyle2"
                                        Text="[$ClearCart]" OnClick="uxClearCartButton_Click" />
                                </td>
                            </tr>
                        </table>
                        <div class="ShoppingCartCheckoutDiv">
                            <div class="ShoppingCartNoteDiv">
                                <h3>
                                    [$EstimateShipping]
                                </h3>
                                <div class="NoteDetail">
                                    [$EstimateShippingDetail]
                                </div>
                                <uc5:CheckoutShippingEstimator ID="uxEstimatedShipping" runat="server" />
                            </div>
                            <div class="ShoppingCartCouponDiv">
                                <uc3:GiftCouponDetail ID="uxGiftCouponDetail" runat="server" />
                            </div>
                            <div class="ShoppingCartOrderTotal">
                                <div id="uxSubTotalTR" runat="server" class="ShoppingCartTableDiscountRow">
                                    <div class="ShoppingCartTableColumn1">
                                        [$Subtotal]:
                                    </div>
                                    <div class="ShoppingCartTableColumn2">
                                        <asp:Label ID="uxSubTotalLabel" runat="server" Text="0.00" />&nbsp;
                                    </div>
                                </div>
                                <div id="uxDiscountTR" runat="server" class="ShoppingCartTableDiscountRow">
                                    <div class="ShoppingCartTableColumn1">
                                        [$Discount Amount]:
                                    </div>
                                    <div class="ShoppingCartTableColumn2 ShoppingCartTotal">
                                        <asp:Label ID="uxDiscountAmountLabel" runat="server" Text="0.00" />&nbsp;
                                    </div>
                                </div>
                                <div id="uxRewardDiscountTR" runat="server" class="ShoppingCartTableDiscountRow">
                                    <div class="ShoppingCartTableColumn1">
                                        [$PointDiscount]:
                                    </div>
                                    <div class="ShoppingCartTableColumn2 ShoppingCartTotal">
                                        <asp:Label ID="uxRewardDiscountLabel" runat="server" Text="0.00" />&nbsp;
                                    </div>
                                </div>
                                <div id="uxGiftDiscountTR" runat="server" class="ShoppingCartTableDiscountRow">
                                    <div class="ShoppingCartTableColumn1">
                                        [$GiftCertificate]:
                                    </div>
                                    <div class="ShoppingCartTableColumn2 ShoppingCartTotal">
                                        <asp:Label ID="uxGiftDiscountLabel" runat="server" Text="0.00" />&nbsp;
                                    </div>
                                </div>
                                <div id="uxTaxTR" runat="server" class="ShoppingCartTableDiscountRow">
                                    <div class="ShoppingCartTableColumn1">
                                        [$Tax]:
                                    </div>
                                    <div class="ShoppingCartTableColumn2">
                                        <asp:Label ID="uxTaxAmountLabel" runat="server" Text="0.00" />&nbsp;
                                    </div>
                                </div>
                                <div id="uxShippingTR" runat="server" class="ShoppingCartTableDiscountRow">
                                    <div class="ShoppingCartTableColumn1">
                                        <asp:Label ID="uxShippingAmount" runat="server" Text="[$Shipping]" />
                                    </div>
                                    <div class="ShoppingCartTableColumn2">
                                        <asp:Label ID="uxShippingAmountLabel" runat="server" Text="0.00" />&nbsp;
                                    </div>
                                </div>
                                <div id="uxHandlingFeeTR" runat="server" class="ShoppingCartTableDiscountRow">
                                    <div class="ShoppingCartTableColumn1">
                                        [$HandlingFee]:
                                    </div>
                                    <div class="ShoppingCartTableColumn2">
                                        <asp:Label ID="uxHandlingFeeLabel" runat="server" Text="0.00" />&nbsp;
                                    </div>
                                </div>
                                <div class="ShoppingCartTableTotalAmountRow">
                                    <div class="ShoppingCartTableColumn1 ShoppingCartTotalAmountLabel">
                                        [$Total Amount]:
                                    </div>
                                    <div class="ShoppingCartTableColumn2 ShoppingCartTotalAmount">
                                        <asp:Label ID="uxTotalAmountLabel" runat="server" Text="0.00" />&nbsp;
                                    </div>
                                </div>
                                <asp:Panel ID="uxTaxIncludePanel" runat="server" CssClass="ShoppingCartTableTaxIncludeColumn">
                                    <div class="TaxNotice" onmouseout="HideToolTip('hideMessage', event)" onmouseover="ShowToolTip('mainMessage','hideMessage',event)"
                                        id="mainMessage">
                                        <asp:Label ID="uxTaxIncludeLabel" runat="server" Text="[$TaxIncluded]"></asp:Label>
                                        <div class="hidecallout" id="hideMessage">
                                            <div class="shadow">
                                                <div class="content">
                                                    <asp:Label ID="uxTaxIncludeMessageLabel" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div id="uxOrTR" runat="server" visible="false">
                                    <div class="ShoppingCartTableAlternativePaymentsDiv">
                                        [$AlternativePayments]
                                    </div>
                                </div>
                                <div id="uxExpressPaymentButtonsTR" runat="server" visible="false">
                                    <div class="ShoppingCartTableExpressPaymentButtonColumn" colspan="2">
                                        <asp:PlaceHolder ID="uxButtonPlaceHolder" runat="server"></asp:PlaceHolder>
                                    </div>
                                </div>
                                <div id="uxRecurringWarning" runat="server" visible="false">
                                    <div class="ShoppingCartTableRecurringPaymentWarningColumn" colspan="2">
                                        [$RecurringPaymentWarning]
                                    </div>
                                </div>
                            </div>
                            <div class="ShoppingCartButton">
                                    <asp:LinkButton ID="uxCheckoutImageButton" runat="server" OnClick="uxCheckoutImageButton_Click"
                                        CssClass="ShoppingCartCheckoutLink BtnStyle1" Text="[$ProceedToCheckout]" />
                                    <asp:LinkButton ID="uxAddToGiftRegistryImageButton" Text="[$AddToGiftRegistry]" runat="server"
                                        CssClass="ShoppingCartGiftRegistryLink BtnStyle1" OnClick="uxAddToGiftRegistryImageButton_Click" />
                                </div>
                        </div>
                    </asp:Panel>
                    <asp:HiddenField ID="uxCartStatusHidden" runat="server" />
                    <div class="ShoppingCartBackHomeLinkDiv">
                        <div id="uxCartEmptyDiv" runat="server" class="ShoppingCartEmpty" visible="false">
                            <asp:Label ID="uxCartEmptyLabel" runat="server"></asp:Label>
                        </div>
                        <asp:HyperLink ID="uxBackHomeLink" runat="server" NavigateUrl="Default.aspx" Visible="False">[$Go back to home page]</asp:HyperLink></div>
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
