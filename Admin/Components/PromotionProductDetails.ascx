<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PromotionProductDetails.ascx.cs"
    Inherits="Admin_Components_PromotionProductDetails" %>
<%@ Register Src="Order/ProductOptionGroupDetails.ascx" TagName="ProductOptionGroupDetails"
    TagPrefix="uc1" %>
<%@ Register Src="Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Import Namespace="Vevo" %>
<%@ Import Namespace="Vevo.Domain" %>
<%@ Import Namespace="Vevo.Shared.Utilities" %>
<%@ Import Namespace="Vevo.WebAppLib" %>
<%@ Import Namespace="Vevo.WebUI" %>
<asp:Panel ID="uxAddItemPanel" runat="server" CssClass="b6 pdl10 pdr10 pdb10 pdt10">
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
    <div id="Div1" class="ProductDetailsRow" runat="server">
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
                    for the first
                    <asp:Label ID="uxRecurringMoreNumberOfTrialCyclesLabel" runat="server"></asp:Label>
                    payments
                </asp:Panel>
                <asp:Panel ID="uxTrialPeriodTR" runat="server" Visible='<%# IsShowTrialPeriod() %>'>
                    <span class="RecurringSpecialNotice">Special </span>Only
                    <asp:Label ID="uxRecurringTrialAmountLabel" runat="server"></asp:Label>
                    for the first payment
                </asp:Panel>
                <asp:Panel ID="uxFreeTrialPeriodTR" runat="server" Visible='<%# IsShowFreeTrialPeriod() %>'>
                    <span class="uxRecurringSpecialNotice">Special </span>Free Trial for the first
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
            <asp:Label ID="uxEnterAmountLabel" runat="server" CssClass="Label fl" MaxLength="10" />
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
<%--            <asp:RequiredFieldValidator ID="uxQuantityRequiredValidator" runat="server" ControlToValidate="uxQuantityText"
                meta:resourcekey="uxStockRequiredValidator" ValidationGroup="VaildProduct" Display="Dynamic">
                            <--
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="uxQuantityCompare" runat="server" ControlToValidate="uxQuantityText"
                ErrorMessage="Your Quantity is invalid" Operator="DataTypeCheck" Type="Integer"
                ValidationGroup="VaildProduct" Display="Dynamic">
                <--
                <asp:Label ID="Label1" runat="server" Text="Your Quantity is invalid" CssClass="Label" />
            </asp:CompareValidator>--%>
            <asp:RequiredFieldValidator ID="uxQuantityTextRequired" runat="server" ControlToValidate="uxQuantityText"
                Display="Dynamic" ValidationGroup="VaildProduct" CssClass="CommonValidatorText">
                <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Quantity is required.
                <div class="CommonValidateDiv CommonValidateDivPromotionProductLong">
                </div>
            </asp:RequiredFieldValidator>

        <asp:CompareValidator ID="uxQuantityTextCompare" runat="server" ControlToValidate="uxQuantityText"
            Operator="DataTypeCheck" Type="Integer" Display="Dynamic" ValidationGroup="VaildProduct"
            CssClass="CommonValidatorText">
            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Your quantity is invalid
            <div class="CommonValidateDiv CommonValidateDivPromotionProductLong">
            </div>
        </asp:CompareValidator>
        </div>
    </div>
    <div class="ProductDetailsRow" id="uxAllOptionCheckPanel" runat="server">
        <asp:Label ID="uxAllOptionLabel" runat="server" Text="All Option" CssClass="Label" />
        <asp:CheckBox ID="uxAllOptionCheck" runat="server" AutoPostBack="true" OnCheckedChanged="uxAllOptionCheck_OnCheckedChanged"
            CssClass="CheckBox fl" />
    </div>
    <div class="ProductDetailsRow" id="uxProductOptionGroupPanel" runat="server">
        <asp:Label ID="uxOptionLabel" runat="server" Text="Product Options" CssClass="Label" />
        <uc1:ProductOptionGroupDetails ID="uxProductOptionGroupDetails" runat="server"></uc1:ProductOptionGroupDetails>
        <div class="Clear">
        </div>
    </div>
</asp:Panel>
