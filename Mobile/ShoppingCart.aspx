<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile/Mobile.master" AutoEventWireup="true"
    CodeFile="ShoppingCart.aspx.cs" Inherits="Mobile_ShoppingCart" %>

<%@ Register Src="~/Components/Common/TooltippedText.ascx" TagName="TooltippedText"
    TagPrefix="uc2" %>
<%@ Register Src="~/Components/VevoHyperLink.ascx" TagName="HyperLink" TagPrefix="ucHyperLink" %>
<%@ Register Src="Components/MobileMessage.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Import Namespace="Vevo" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="MobileTitle">
        [$Your Shopping Cart Items]
    </div>
    <uc1:Message ID="uxMessage" runat="server" NumberOfNewLines="0" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ShippingCartValid" />
    <asp:Panel ID="uxPanel" runat="server" CssClass="MobileShoppingCartPanel">
        <table class="MobileShoppingCartTable" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="2">
                    <asp:GridView ID="uxGrid" runat="server" AutoGenerateColumns="False" DataKeyNames="CartItemID,ProductID,Options,GiftDetails,OptionCombinationID,IsPromotion"
                        OnRowDeleting="uxGrid_RowDeleting" CellPadding="4" GridLines="None" OnRowDataBound="uxGrid_RowDataBound"
                        CssClass="MobileShoppingCartGridView">
                        <Columns>
                            <asp:TemplateField SortExpression="Del">
                                <ItemTemplate>
                                    <asp:LinkButton ID="uxDeleteImageButton" runat="server" Text="x" CommandName="Delete"
                                        CssClass="MobileShoppingCartDeleteButton" />
                                </ItemTemplate>
                                <HeaderStyle CssClass="MobileShoppingCartGridViewDeleteHeaderStyle" />
                                <ItemStyle CssClass="MobileShoppingCartGridViewDeleteItemStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="[$Name]" SortExpression="Name">
                                <ItemTemplate>
                                    <asp:Label ID="uxNameLabel" runat="server" Text='<%# GetName( Container.DataItem ) %>'>
                                    </asp:Label>
                                    <div class="ProductNameDetails">
                                        <uc2:TooltippedText ID="uxTooltippedText" runat="server" MainText='<%# GetMainText( Container.DataItem ) %>'
                                            TooltipText='<%# GetTooltipText( Container.DataItem ) %>' Visible='<%# Eval( "IsRecurring" ) %>' />
                                    </div>
                                    <div class="MobileShoppingCartGridViewPrice">
                                        <asp:Label ID="uxLabel" runat="server" Text='<%# GetUnitPriceText( Container.DataItem ) %>'
                                            CssClass="price" />
                                        <span class="multiply">x</span>
                                        <asp:TextBox ID="uxQuantityText" runat="server" Text='<%#Bind("Quantity") %>' Width="30px"
                                            ValidationGroup="ShippingCartValid" />
                                        <asp:CompareValidator ID="uxQuantityCompare" runat="server" ControlToValidate="uxQuantityText"
                                            ErrorMessage='<%# GetName( Container.DataItem ) + " quantity is invalid" %>'
                                            Operator="GreaterThanEqual" ValueToCompare="0" ValidationGroup="ShippingCartValid">*</asp:CompareValidator></div>
                                </ItemTemplate>
                                <ItemStyle CssClass="MobileShoppingCartGridViewNameItemStyle" />
                                <HeaderStyle CssClass="MobileShoppingCartGridViewNameHeaderStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="[$Subtotal]" SortExpression="Subtotal">
                                <ItemStyle CssClass="MobileShoppingCartGridViewSubtotalItemStyle" />
                                <HeaderStyle CssClass="MobileShoppingCartGridViewSubtotalHeaderStyle" />
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# GetSubtotalText( Container.DataItem ) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle CssClass="MobileCommonGridViewFooterStyle ShoppingCartGridViewFooterStyle" />
                        <RowStyle CssClass="MobileCommonGridViewRowStyle ShoppingCartGridViewRowStyle" />
                        <HeaderStyle CssClass="MobileCommonGridViewHeaderStyle ShoppingCartGridViewHeaderStyle" />
                    </asp:GridView>
                </td>
            </tr>
            <tr id="uxDiscountTR" runat="server" class="MobileShoppingCartTableDiscountRow">
                <td class="MobileShoppingCartTableColumn1">
                    [$Discount Amount]:
                </td>
                <td class="MobileShoppingCartTableColumn2">
                    <asp:Label ID="uxDiscountAmountLabel" runat="server" Text="0.00" />
                </td>
            </tr>
            <tr class="MobileShoppingCartTableTotalAmountRow">
                <td class="MobileShoppingCartTableColumn1">
                    [$Total Amount]:
                </td>
                <td class="MobileShoppingCartTableColumn2">
                    <asp:Label ID="uxTotalAmountLabel" runat="server" Text="0.00" />
                </td>
            </tr>
            <tr>
                <td align="right" style="text-align: right">
                </td>
                <td align="right">
                    <asp:Panel ID="uxTaxIncludePanel" runat="server" CssClass="MobileShoppingCartTableTaxIncludeColumn">
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
                </td>
            </tr>
            <tr>
                <td align="right">
                </td>
                <td align="right">
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tr>
                            <td class="MobileShoppingCartButtonDiv">
                                <asp:LinkButton ID="uxUpdateQuantityImageButton" runat="server" Text="[$UpdateQuantity]"
                                    OnClick="uxUpdateQuantityButton_Click" CssClass="MobileButton MobileShoppingCartButton"
                                    ValidationGroup="ShoppingCartValid" />
                            </td>
                        </tr>
                        <tr>
                            <td class="MobileShoppingCartButtonDiv">
                                <asp:LinkButton ID="uxContinueLink" runat="server" Text="[$ContinueShopping]" OnClick="uxContinueButton_Click"
                                    CssClass="MobileButton MobileShoppingCartButton" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="uxOrTR" runat="server" visible="false">
                <td colspan="2">
                    <div class="MobileShoppingCartTableAlternativePaymentsDiv">
                        [$AlternativePayments]
                    </div>
                </td>
            </tr>
            <tr id="uxExpressPaymentButtonsTR" runat="server" visible="false">
                <td class="MobileShoppingCartTableExpressPaymentButtonColumn" colspan="2">
                    <asp:PlaceHolder ID="uxButtonPlaceHolder" runat="server"></asp:PlaceHolder>
                </td>
            </tr>
            <tr id="uxRecurringWarning" runat="server" visible="false">
                <td class="MobileShoppingCartTableRecurringPaymentWarningColumn" colspan="2">
                    [$RecurringPaymentWarning]
                </td>
            </tr>
            <tr id="uxSubscriptionWarning" runat="server" visible="false">
                <td class="MobileShoppingCartTableRecurringPaymentWarningColumn" colspan="2">
                    [$SubscriptionPaymentWarning]
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:HiddenField ID="uxCartStatusHidden" runat="server" />
    <div class="MobileShoppingCartBackHomeLinkDiv">
        <div id="uxShoppingCartDiv" runat="server" class="MobileShoppingCartEmpty" visible="false">
            <asp:Label ID="uxMessageLabel" runat="server" />
        </div>
        <asp:HyperLink ID="uxBackHomeLink" runat="server" NavigateUrl="Default.aspx" Visible="False">[$Go back to home page]</asp:HyperLink></div>
    <div class="Clear">
    </div>
</asp:Content>
