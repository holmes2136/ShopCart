<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductItemDetails.ascx.cs"
    Inherits="Admin_Components_Order_ProductItemDetails" %>
<%@ Register Src="ProductOptionGroupDetails.ascx" TagName="ProductOptionGroupDetails"
    TagPrefix="uc1" %>
<%@ Register Src="ProductQuantityDiscount.ascx" TagName="ProductQuantityDiscount"
    TagPrefix="uc2" %>
<%@ Register Src="ProductGiftCertificateDetails.ascx" TagName="GiftCertificateDetails"
    TagPrefix="uc3" %>
<%@ Register Src="../Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Import Namespace="Vevo" %>
<%@ Import Namespace="Vevo.Domain" %>
<%@ Import Namespace="Vevo.Shared.Utilities" %>
<%@ Import Namespace="Vevo.WebAppLib" %>
<%@ Import Namespace="Vevo.WebUI" %>
<asp:Panel ID="uxAddItemPanel" runat="server" CssClass="b6 pdl10 pdr10 pdb10 pdt10">
    <asp:Label ID="uxMessageLabel" runat="server" Visible="false" Font-Bold="true" Font-Size="1.1em"></asp:Label>
    <uc3:Message ID="uxMessage" runat="server" />
    <div class="ProductDetailsRowTitle mgt10">
        <asp:Label ID="uxProductNameLabel" runat="server" CssClass="Label" />
    </div>
    <asp:ValidationSummary ID="uxValidationSummary" runat="server" meta:resourcekey="uxValidationSummary"
        ValidationGroup="VaildProduct" CssClass="ValidationStyle" />
    <div class="ProductDetailsRow">
        <asp:Label ID="Label2" runat="server" CssClass="Label">&nbsp;</asp:Label>
        <asp:Image ID="uxProductImage" runat="server" SkinID="EmptyImages" />
        <div class="Clear">
        </div>
    </div>
    <div class="ProductDetailsRow" runat="server">
        <%--<asp:TextBox ID="uxPriceText" runat="server" Width="210px" CssClass="TextBox" />--%>
        <asp:Panel ID="uxSpecialTrialPanel" runat="server" Visible='<%# IsShowRecurringPeriod() %>'>
            <asp:Label ID="uxRecurringLabel" runat="server" Text="Recurring Details" CssClass="Label"></asp:Label>
            <div class="Label fr">
                <asp:Panel ID="uxRecurringPeriodTR" runat="server" Visible='<%# IsShowRecurringPeriod() %>'>
                    Recurring Amount Every
                    <asp:Label ID="uxRecurringCyclesLabel" runat="server"></asp:Label>
                    days
                </asp:Panel>
                <asp:Panel ID="uxTrialPeriodMoreTR" runat="server" Visible='<%# IsShowTrialPeriodMore() %>'>
                    <span class="RecurringSpecialNotic ">Special </span>Only
                    <asp:Label ID="uxRecurringTrialMoreAmountLabel" runat="server"></asp:Label>
                    <%-- <%# StoreContext.Currency.FormatPrice( Eval( "ProductRecurring.RecurringTrialAmount" ) )%>--%>
                    for the first
                    <%--  <%# Eval( "ProductRecurring.RecurringNumberOfTrialCycles" )%>--%>
                    <asp:Label ID="uxRecurringMoreNumberOfTrialCyclesLabel" runat="server"></asp:Label>
                    payments
                </asp:Panel>
                <asp:Panel ID="uxTrialPeriodTR" runat="server" Visible='<%# IsShowTrialPeriod() %>'>
                    <span class="RecurringSpecialNotice">Special </span>Only
                    <asp:Label ID="uxRecurringTrialAmountLabel" runat="server"></asp:Label>
                    <%-- <%# StoreContext.Currency.FormatPrice( Eval( "ProductRecurring.RecurringTrialAmount" ) )%>--%>
                    for the first payment
                </asp:Panel>
                <asp:Panel ID="uxFreeTrialPeriodTR" runat="server" Visible='<%# IsShowFreeTrialPeriod() %>'>
                    <span class="uxRecurringSpecialNotice">Special </span>Free Trial for the first
                    <%--  <%# Eval( "ProductRecurring.RecurringNumberOfTrialCycles" )%>--%>
                    <asp:Label ID="uxRecurringNumberOfTrialCyclesLabel" runat="server"></asp:Label>
                    payments
                </asp:Panel>
                <asp:Panel ID="uxFreeTrialPeriodMoreTR" runat="server" Visible='<%# IsShowFreeTrialPeriodMore() %>'>
                    <span class="RecurringSpecialNotice">Special </span>Free Trial for the first payment
                </asp:Panel>
            </div>
        </asp:Panel>
        <div class="Clear">
        </div>
    </div>
    <div class="ProductDetailsRow">
        <div id="uxCustomPriceDiv" runat="server">
            <asp:Label ID="uxPriceLable" runat="server" Text="Price" CssClass="Label" />
            <asp:TextBox ID="uxEnterAmountText" runat="server" CssClass="TextBox fl" MaxLength="10" />
            <div class="validator1 fl">
                <span class="Asterisk">*</span>
            </div>
            <asp:RequiredFieldValidator ID="uxEnterAmountTextRequired" runat="server" Display="Dynamic"
                ControlToValidate="uxEnterAmountText" CssClass="CommonValidatorText">
                <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Price is required.
                <div class="CommonValidateDiv CommonValidateDivCreateOrderProductPrice">
                </div>
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="uxEnterAmountTextCompare" runat="server" Display="Dynamic"
                Type="Currency" Operator="DataTypeCheck" ControlToValidate="uxEnterAmountText"
                CssClass="CommonValidatorText">
                <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Price is invalid.
                <div class="CommonValidateDiv CommonValidateDivCreateOrderProductPrice">
                </div>
            </asp:CompareValidator>
        </div>
        <div id="uxCustomPriceNote" runat="server" class="ProductDetailsRow" visible="false">
            <asp:Label ID="uxMinPriceLabel" runat="server" CssClass="BulletLabel" />
        </div>
        <div class="Clear">
        </div>
    </div>
    <div id="uxQuantityPanel" runat="server">
        <div class="ProductDetailsRow">
            <asp:Label ID="uxQuantityLabel" runat="server" Text="Quantity" CssClass="Label" />
            <asp:TextBox ID="uxQuantityText" runat="server" CssClass="TextBox fl" />
            <div class="validator1 fl">
                <span class="Asterisk">*</span>
            </div>
            <asp:RequiredFieldValidator ID="uxQuantityRequiredValidator" runat="server" ControlToValidate="uxQuantityText"
                Display="Dynamic" CssClass="CommonValidatorText">
                <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Quantity is required.
                <div class="CommonValidateDiv CommonValidateDivCreateOrderProductQty">
                </div>
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="uxQuantityCompare" runat="server" ControlToValidate="uxQuantityText"
                Operator="GreaterThan" ValueToCompare="0" Type="Integer" Display="Dynamic" CssClass="CommonValidatorText">
                <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Quantity is invalid.
                <div class="CommonValidateDiv CommonValidateDivCreateOrderProductQty">
                </div>
            </asp:CompareValidator>
        </div>
        <div class="ProductDetailsRow">
            <asp:Label ID="uxMinQuantityLabel" runat="server" Visible="false" CssClass="BulletLabel" />
            <asp:Label ID="uxMaxQuantityLabel" runat="server" Visible="false" CssClass="BulletLabel" />
        </div>
        <div class="Clear">
        </div>
    </div>
    <div id="uxQuantityDiscountPanel" class="ProductDetailsRow" runat="server">
        <asp:Label ID="uxQuantityDiscountLabel" runat="server" Text="Quantity Discount" CssClass="Label" />
        <div class="fl">
            <uc2:ProductQuantityDiscount ID="uxQuantityDiscount" runat="server" />
        </div>
        <div class="Clear">
        </div>
    </div>
    <div class="ProductDetailsRow" id="uxProductOptionGroupPanel" runat="server">
        <asp:Label ID="uxOptionLabel" runat="server" Text="Product Options" CssClass="Label" />
        <uc1:ProductOptionGroupDetails ID="uxProductOptionGroupDetails" runat="server"></uc1:ProductOptionGroupDetails>
        <div class="Clear">
        </div>
    </div>
    <div id="uxGiftPanel" class="ProductDetailsRow" runat="server">
        <asp:Label ID="uxGiftLabel" runat="server" Text="GiftCertificate Details" CssClass="Label" />
        <uc3:GiftCertificateDetails ID="uxGiftCertificateDetails" runat="server" />
        <div class="Clear">
        </div>
    </div>
    <%--   <vevo:AdvanceButton ID="uxUpdateButton" runat="server" Text="Update Order Item" CssClassBegin="AdminButtonUpdateOrderItem fr"
        CssClassEnd="Button1Right" CssClass="AdminButtonUpdateOrderItem" ShowText="false"
        OnClick="uxUpdateButton_Click" OnClickGoTo="Top" ValidationGroup="OrderItemsDetails">
    </vevo:AdvanceButton>--%>
</asp:Panel>
