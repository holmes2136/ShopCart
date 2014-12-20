<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CouponDetails.ascx.cs"
    Inherits="Components_CouponDetails" %>
<%@ Register Src="CouponCondition.ascx" TagName="CouponCondition" TagPrefix="uc2" %>
<%@ Register Src="Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="Template/AdminUserControlContent.ascx" TagName="AdminUserControlContent"
    TagPrefix="uc2" %>
<%@ Register Src="../Components/LanguageLabelPlus.ascx" TagName="LanaguageLabelPlus"
    TagPrefix="uc5" %>
<%@ Register Src="CalendarPopup.ascx" TagName="CalendarPopup" TagPrefix="uc4" %>
<uc2:AdminUserControlContent ID="uxAdminUserControlContent" runat="server">
    <MessageTemplate>
        <uc1:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <ValidationSummaryTemplate>
        <asp:ValidationSummary ID="uxValidationSummary" runat="server" CssClass="ValidationStyle"
            meta:resourcekey="uxValidationSummary" ValidationGroup="CouponVaild" />
    </ValidationSummaryTemplate>
    <ValidationDenotesTemplate>
        <div class="topline">
            <div class="RequiredLabel c6">
                <span class="Asterisk">*</span>
                <asp:Label ID="lcRequiredFieldSymbol" runat="server" meta:resourcekey="lcRequiredFieldSymbol" /></div>
        </div>
    </ValidationDenotesTemplate>
    <TopContentBoxTemplate>
        <div class="CommonRowStyle">
            <div class="Label">
                <asp:Label ID="uxCouponIDLabel" runat="server" meta:resourcekey="uxCouponIDLabel"
                    CssClass="fl"></asp:Label>
            </div>
            <asp:TextBox ID="uxCouponIDText" runat="server" ValidationGroup="CouponVaild" CssClass="TextBox"></asp:TextBox>
            <div class="validator1 fl">
                <span class="Asterisk">*</span>
            </div>
            <asp:RequiredFieldValidator ID="uxCouponIDValid" runat="server" ControlToValidate="uxCouponIDText"
                CssClass="CommonValidatorText" Display="Dynamic" ValidationGroup="CouponVaild">
                <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> CouponID is required
                <div class="CommonValidateDiv">
                </div>
            </asp:RequiredFieldValidator>
            <vevo:AdvanceButton ID="uxGenerateIDButton" runat="server" meta:resourcekey="uxGenerateIDButton"
                CssClassBegin="fl mgl10" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxGenerateIDButton_Click"
                OnClickGoTo="None" />
        </div>
        <div class="CommonRowStyle">
            <div class="Label">
                <asp:Label ID="uxDiscountTypeLabel" runat="server" meta:resourcekey="uxDiscountTypeLabel"></asp:Label></div>
            <asp:DropDownList ID="uxDiscountTypeDrop" runat="server" AutoPostBack="True" OnSelectedIndexChanged="uxDiscountTypeDrop_SelectedIndexChanged"
                CssClass="fl DropDown">
                <asp:ListItem Value="Price">Price</asp:ListItem>
                <asp:ListItem Value="Percentage">Percentage</asp:ListItem>
                <asp:ListItem Value="BuyXDiscountYPrice">Buy X Discount Y By Price</asp:ListItem>
                <asp:ListItem Value="BuyXDiscountYPercentage">Buy X Discount Y By Percentage</asp:ListItem>
                <asp:ListItem Value="FreeShipping">Free Shipping</asp:ListItem>
            </asp:DropDownList>
        </div>
        <asp:Panel ID="uxBuyXGetYTR" runat="server" CssClass="CommonRowStyle">
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="uxMinimumQuantityLabel" runat="server" meta:resourcekey="uxMinimumQuantityLabel"></asp:Label>
                </div>
                <asp:TextBox ID="uxMinimumQuantityText" runat="server" CssClass="TextBox"></asp:TextBox>
                <asp:CompareValidator ID="uxMinimumQuantityTextCompare" runat="server" ControlToValidate="uxMinimumQuantityText"
                    Operator="DataTypeCheck" Type="Integer" Display="Dynamic" ValidationGroup="CouponVaild"
                    CssClass="CommonValidatorText">
                    <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number must be an Integer.
                    <div class="CommonValidateDiv CommonValidateDivCuponDiscount">
                    </div>
                </asp:CompareValidator>
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="uxPromotionQuantityLabel" runat="server" meta:resourcekey="uxPromotionQuantityLabel"></asp:Label>
                </div>
                <asp:TextBox ID="uxPromotionQuantityText" runat="server" CssClass="TextBox"></asp:TextBox>
                <asp:CompareValidator ID="uxPromotionQuantityTextCompare" runat="server" ControlToValidate="uxPromotionQuantityText"
                    Operator="DataTypeCheck" Type="Integer" Display="Dynamic" ValidationGroup="CouponVaild"
                    CssClass="CommonValidatorText">
                    <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number must be an Integer.
                    <div class="CommonValidateDiv CommonValidateDivCuponDiscount">
                    </div>
                </asp:CompareValidator>
            </div>
        </asp:Panel>
        <asp:Panel ID="uxAmountTR" runat="server" CssClass="CommonRowStyle">
            <div class="Label">
                <asp:Label ID="uxDiscountAmountLabel" runat="server" meta:resourcekey="uxDiscountAmountLabel"></asp:Label></div>
            <asp:TextBox ID="uxDiscountAmountText" runat="server" ValidationGroup="CouponVaild"
                CssClass="TextBox"></asp:TextBox>
            <asp:CompareValidator ID="uxDiscountAmountTextCompare" runat="server" ControlToValidate="uxDiscountAmountText"
                Operator="DataTypeCheck" Type="Double" Display="Dynamic" ValidationGroup="CouponVaild"
                CssClass="CommonValidatorText">
                <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number must be a Decimal.
                <div class="CommonValidateDiv CommonValidateDivCuponDiscount">
                </div>
            </asp:CompareValidator>
        </asp:Panel>
        <asp:Panel ID="uxPercentageTR" runat="server" CssClass="CommonRowStyle">
            <div class="Label">
                <asp:Label ID="uxPercentageLabel" runat="server" meta:resourcekey="uxPercentageLabel"></asp:Label>
            </div>
            <asp:TextBox ID="uxPercentageText" runat="server" CssClass="TextBox"></asp:TextBox>
            <asp:CompareValidator ID="uxPercentageTextCompare" runat="server" ControlToValidate="uxPercentageText"
                Operator="DataTypeCheck" Type="Integer" Display="Dynamic" ValidationGroup="CouponVaild"
                CssClass="CommonValidatorText">
                <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number must be an Integer (in percentage).
                <div class="CommonValidateDiv CommonValidateDivCuponDiscount">
                </div>
            </asp:CompareValidator>
        </asp:Panel>
        <asp:Panel ID="uxRepeatDiscountTR" runat="server" CssClass="CommonRowStyle">
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="uxRepeatDiscountLabel" runat="server" meta:resourcekey="uxRepeatDiscountLabel"></asp:Label>
                </div>
                <asp:CheckBox ID="uxRepeatDiscountCheckBox" runat="server" />
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    &nbsp;
                </div>
                <asp:Label ID="uxDiscountMessageLabel" runat="server" meta:resourcekey="uxDiscountMessageLabel"
                    ForeColor="Red"></asp:Label>
            </div>
        </asp:Panel>
        <asp:Panel ID="uxFreeShippingTR" runat="server" CssClass="CommonRowStyle">
            <div class="Label">
                <asp:Label ID="uxFreeShippingTypeLabel" runat="server" meta:resourcekey="uxFreeShippingTypeLabel"></asp:Label>
            </div>
            <asp:DropDownList ID="uxFreeShippingTypeDrop" runat="server" AutoPostBack="True"
                CssClass="fl DropDown">
                <asp:ListItem Value="MatchingItemsOnly">For Matching Items Only</asp:ListItem>
                <asp:ListItem Value="ShipmentWithMatchingItem">For Shipment With Matching Items</asp:ListItem>
            </asp:DropDownList>
        </asp:Panel>
        <div class="CommonRowStyle">
            <div class="Label">
                <asp:Label ID="uxExpirationTypeLabel" runat="server" meta:resourcekey="uxExpirationTypeLabel"></asp:Label></div>
            <asp:DropDownList ID="uxExpirationTypeDrop" runat="server" OnSelectedIndexChanged="uxExpirationTypeDrop_SelectedIndexChanged"
                AutoPostBack="True" CssClass="fl DropDown">
                <asp:ListItem Text="Date" Value="Date"></asp:ListItem>
                <asp:ListItem Text="Quantity" Value="Quantity"></asp:ListItem>
                <asp:ListItem Text="Both date & quantity" Value="Both"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <asp:Panel ID="uxExpirationDateTR" runat="server" CssClass="CommonRowStyle">
            <div class="Label">
                <asp:Label ID="uxExpirationDateLabel" runat="server" meta:resourcekey="uxExpirationDateLabel"></asp:Label></div>
            <uc4:CalendarPopup ID="uxRegisterDateCalendarPopup" runat="server" TextBoxEnabled="false"
                TextBoxWidth="150" />
            <div id="uxAsteriskDiv" runat="server" class="validator1 fl">
                <span class="Asterisk">*</span>
            </div>
        </asp:Panel>
        <asp:Panel ID="uxExpirationQuantityTR" runat="server" CssClass="CommonRowStyle">
            <div class="Label">
                <asp:Label ID="uxExpirationQuantityLabel" runat="server" meta:resourcekey="uxExpirationQuantityLabel"></asp:Label></div>
            <asp:TextBox ID="uxExpirationQuantityText" runat="server" CssClass="TextBox"></asp:TextBox>
            <asp:CompareValidator ID="uxExpirationQuantityTextCompare" runat="server" ControlToValidate="uxExpirationQuantityText"
                Operator="DataTypeCheck" Type="Integer" Display="Dynamic" ValidationGroup="CouponVaild"
                CssClass="CommonValidatorText">
                <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number must be an Integer.
                <div class="CommonValidateDiv CommonValidateDivCuponDiscount">
                </div>
            </asp:CompareValidator>
        </asp:Panel>
        <asp:Panel ID="uxCurrentQuantityTR" runat="server" CssClass="CommonRowStyle">
            <div class="Label">
                <asp:Label ID="uxCurrentQuantityLabel" runat="server" meta:resourcekey="uxCurrentQuantityLabel"></asp:Label></div>
            <asp:TextBox ID="uxCurrentQuantityText" runat="server" CssClass="TextBox"></asp:TextBox>
            <asp:CompareValidator ID="uxCurrentQuantityTextCompare" runat="server" ControlToValidate="uxCurrentQuantityText"
                Operator="DataTypeCheck" Type="Integer" Display="Dynamic" ValidationGroup="CouponVaild"
                CssClass="CommonValidatorText">
                <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number must be an Integer.
                <div class="CommonValidateDiv CommonValidateDivCuponDiscount">
                </div>
            </asp:CompareValidator>
        </asp:Panel>
        <div class="CommonRowStyle">
            <div class="Label">
                <asp:Label ID="uxMinimumSubtotalLabel" runat="server" meta:resourcekey="lcMinimumSubtotal"></asp:Label></div>
            <asp:TextBox ID="uxMinimumSubtotalText" runat="server" MaxLength="255" CssClass="TextBox"></asp:TextBox>
            <asp:CompareValidator ID="uxMinimumSubtotalTextCompare" runat="server" ControlToValidate="uxMinimumSubtotalText"
                Operator="DataTypeCheck" Type="Double" Display="Dynamic" ValidationGroup="CouponVaild"
                CssClass="CommonValidatorText">
                <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number must be a Decimal Number.
                <div class="CommonValidateDiv CommonValidateDivCuponDiscount">
                </div>
            </asp:CompareValidator>
        </div>
        <div class="CommonRowStyle">
            <div class="Label">
                <asp:Label ID="uxDiscountByLabel" runat="server" meta:resourcekey="lcDiscountBy"
                    CssClass="BulletLabel"></asp:Label>
            </div>
            <div class="fl">
                <asp:RadioButton ID="uxAllProductRadio" runat="server" GroupName="DiscountByGroup"
                    meta:resourcekey="lcAllProduct" Checked="True" />
                <br />
                <asp:RadioButton ID="uxEligibleProductRadio" runat="server" GroupName="DiscountByGroup"
                    meta:resourcekey="lcEligibleProduct" />
            </div>
        </div>
        <div class="CommonRowStyle pdb10">
            <div class="Label">
                <asp:Label ID="uxMerchantNotesLabel" runat="server" meta:resourcekey="uxMerchantNotesLabel"></asp:Label></div>
            <asp:TextBox ID="uxMerchantNotesText" runat="server" Height="120px" TextMode="MultiLine"
                Width="400px" CssClass="TextBox"></asp:TextBox>
        </div>
        <div class="mgl60 mgr60">
            <uc2:CouponCondition ID="uxCouponCondition" runat="server" />
        </div>
        </div>
        <div class="mgt10">
            <vevo:AdvanceButton ID="uxAddButton" runat="server" meta:resourcekey="uxAddButton"
                CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                OnClick="uxAddButton_Click" OnClickGoTo="Top" ValidationGroup="CouponVaild" />
            <vevo:AdvanceButton ID="uxUpdateButton" runat="server" meta:resourcekey="uxUpdateButton"
                CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                OnClick="uxUpdateButton_Click" OnClickGoTo="Top" ValidationGroup="CouponVaild" />
        </div>
        </div>
    </TopContentBoxTemplate>
</uc2:AdminUserControlContent>
