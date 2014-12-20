<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShippingDetails.ascx.cs"
    Inherits="AdminAdvanced_Components_ShippingDetails" %>
<%@ Register Src="Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="LanguageControl.ascx" TagName="LanguageControl" TagPrefix="uc2" %>
<%@ Register Src="Template/AdminContent.ascx" TagName="AdminContent" TagPrefix="uc2" %>
<%@ Register Src="../Components/LanguageLabelPlus.ascx" TagName="LanaguageLabelPlus"
    TagPrefix="uc5" %>
<%@ Register Src="BoxSet/Boxset.ascx" TagName="BoxSet" TagPrefix="uc6" %>
<%@ Register Src="MultiShippingZone.ascx" TagName="MultiShippingZone" TagPrefix="uc3" %>
<uc2:AdminContent ID="uxAdminContent" runat="server">
    <MessageTemplate>
        <uc1:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <ValidationSummaryTemplate>
        <asp:ValidationSummary ID="uxValidationSummary" runat="server" meta:resourcekey="uxValidationSummary"
            ValidationGroup="ValidShipping" CssClass="ValidationStyle" />
    </ValidationSummaryTemplate>
    <LanguageControlTemplate>
        <uc2:LanguageControl ID="uxLanguageControl" runat="server" ShowLanguageDescription="true" />
    </LanguageControlTemplate>
    <ValidationDenotesTemplate>
        <div class="topline">
            <div class="RequiredLabel c6">
                <span class="Asterisk">*</span>
                <asp:Label ID="lcRequiredFieldSymbol" runat="server" meta:resourcekey="lcRequiredFieldSymbol" /></div>
        </div>
    </ValidationDenotesTemplate>
    <ContentTemplate>
        <div class="Container-Box">
            <div class="CommonConfigTitle mgt0">
                <asp:Label ID="lcSetEnabled" runat="server" meta:resourcekey="lcSetEnabled"></asp:Label>
            </div>
            <div class="mgl5 CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="lcIsEnabled" runat="server" meta:resourcekey="lcIsEnabled"></asp:Label></div>
                <asp:DropDownList ID="uxIsEnabledDrop" runat="server" CssClass="fl DropDown">
                    <asp:ListItem Value="True" Text="Yes"></asp:ListItem>
                    <asp:ListItem Value="False" Text="No"></asp:ListItem>
                </asp:DropDownList>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonConfigTitle">
                <asp:Label ID="lcShippingSetUp" runat="server" meta:resourcekey="lcShippingSetUp"></asp:Label>
            </div>
            <div class="mgl5">
                <div class="CommonRowStyle">
                    <div class="Label">
                        <asp:Label ID="lcShippingName" runat="server" meta:resourcekey="lcName"></asp:Label>
                    </div>
                    <asp:TextBox ID="uxShippingNameText" runat="server" CssClass="fl TextBox"></asp:TextBox>
                    <uc5:LanaguageLabelPlus ID="uxPlus1" runat="server" />
                    <div class="validator1 fl">
                        <span class="Asterisk">*</span>
                    </div>
                    <asp:RequiredFieldValidator ID="uxShippingNameTextRequired" runat="server" ControlToValidate="uxShippingNameText"
                        Display="Dynamic" ValidationGroup="ValidShipping" CssClass="CommonValidatorText">
                        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Shipping name is required.
                        <div class="CommonValidateDiv CommonValidateDivShippingName">
                        </div>
                    </asp:RequiredFieldValidator>
                    <div class="Clear">
                    </div>
                </div>
                <div class="CommonRowStyle">
                    <div class="Label">
                        <asp:Label ID="lcShippingOption" runat="server" meta:resourcekey="lcShippingOption"></asp:Label></div>
                    <asp:Label ID="uxShippingOption" runat="server" CssClass="fl" />
                    <div class="Clear">
                    </div>
                </div>
                <div class="CommonRowStyle">
                    <div class="Label">
                        <asp:Label ID="lcFreeShipping" runat="server" meta:resourcekey="lcFreeShipping"></asp:Label></div>
                    <asp:DropDownList ID="uxFreeShippingDrop" runat="server" AutoPostBack="true" CssClass="fl DropDown">
                        <asp:ListItem Value="False">No</asp:ListItem>
                        <asp:ListItem Value="True">Yes</asp:ListItem>
                    </asp:DropDownList>
                    <div class="Clear">
                    </div>
                </div>
                <asp:Panel ID="FreeValueTR" runat="server" CssClass="CommonRowStyle">
                    <div class="Label">
                        <asp:Label ID="lcFreeShippingValue" runat="server" meta:resourcekey="lcFreeShippingValue"></asp:Label></div>
                    <asp:TextBox ID="uxFreeShippingValueText" runat="server" CssClass="fl TextBox"></asp:TextBox>
                    <div class="validator1 fl">
                        <span class="Asterisk">*</span>
                    </div>
                    <asp:RequiredFieldValidator ID="uxFreeShippingValueRequired" runat="server" ControlToValidate="uxFreeShippingValueText"
                        Display="Dynamic" ValidationGroup="ValidShipping" CssClass="CommonValidatorText">
                        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Free shipping is required.
                        <div class="CommonValidateDiv">
                        </div>
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="uxFreeShippingCompare" runat="server" ControlToValidate="uxFreeShippingValueText"
                        Operator="DataTypeCheck" Type="Integer" Display="Dynamic" ValidationGroup="ValidShipping"
                        CssClass="CommonValidatorText">
                        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Free shipping is invalid.
                        <div class="CommonValidateDiv">
                        </div>
                    </asp:CompareValidator>
                    <div class="Clear">
                    </div>
                </asp:Panel>
                <asp:Panel ID="uxHandlingFeeTR" runat="server" CssClass="CommonRowStyle">
                    <div class="Label">
                        <asp:Label ID="lcHandlingFee" runat="server" meta:resourcekey="lcHandlingFee"></asp:Label></div>
                    <asp:TextBox ID="uxHandlingFeeText" runat="server" CssClass="fl TextBox"></asp:TextBox>
                    <asp:CompareValidator ID="uxHandlingFeeCompare" runat="server" ControlToValidate="uxHandlingFeeText"
                        Operator="DataTypeCheck" Type="Double" Display="Dynamic" ValidationGroup="ValidShipping"
                        CssClass="CommonValidatorText">
                        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Handling fee is invalid.
                        <div class="CommonValidateDiv CommonValidatDivShippingHandlingFee">
                        </div>
                    </asp:CompareValidator>
                    <div class="Clear">
                    </div>
                </asp:Panel>
                <asp:Panel ID="uxAllowedTypeTR" runat="server" CssClass="CommonRowStyle">
                    <div class="Label">
                        <asp:Label ID="uxShippingZonesLabel" runat="server" meta:resourcekey="lcShippingZones"></asp:Label></div>
                    <asp:RadioButtonList ID="uxSelectedAllowedTypeRadio" runat="server" CssClass="fl"
                        OnSelectedIndexChanged="uxSelectedAllowedTypeRadio_SelectedIndexChanged" AutoPostBack="True">
                        <asp:ListItem Text="World wide" Value="Worldwide" Selected="True" />
                        <asp:ListItem Text="Allow Zones" Value="Allow" />
                        <asp:ListItem Text="Deny Zones" Value="Deny" />
                    </asp:RadioButtonList>
                    <asp:Panel ID="uxSelectedZonesPanel" runat="server" CssClass="CommonRowStyle">
                        <asp:Panel ID="uxSelectedMultiZonesPanel" runat="server" CssClass="CommonRowStyle">
                            <asp:Label ID="uxSelectedZonesLabel" runat="server" meta:resourcekey="lcSelectedZonesLabel"
                                CssClass="BulletLabel" />
                            <uc3:MultiShippingZone ID="uxMultiZones" runat="server" />
                        </asp:Panel>
                    </asp:Panel>
                    <div class="Clear">
                    </div>
                </asp:Panel>
                <asp:Panel ID="uxFixedCostTR" runat="server" CssClass="CommonRowStyle">
                    <div class="Label">
                        <asp:Label ID="lcFixedCost" runat="server" meta:resourcekey="lcFixCost"></asp:Label></div>
                    <asp:TextBox ID="uxFixCostText" runat="server" CssClass="fl TextBox"></asp:TextBox>
                    <div class="validator1 fl">
                        <span class="Asterisk">*</span>
                    </div>
                    <asp:RequiredFieldValidator ID="uxFixcostRequired" runat="server" ControlToValidate="uxFixCostText"
                        Display="Dynamic" ValidationGroup="ValidShipping" CssClass="CommonValidatorText">
                        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Fixed Shipping Cost is required.
                        <div class="CommonValidateDiv">
                        </div>
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="uxFixcostCompare" runat="server" ControlToValidate="uxFixCostText"
                        Operator="GreaterThanEqual" ValueToCompare="0" Type="Double" Display="Dynamic"
                        ValidationGroup="ValidShipping" CssClass="CommonValidatorText">
                        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Fixed Shipping Cost must be a positive Integer.
                        <div class="CommonValidateDiv">
                        </div>
                    </asp:CompareValidator>
                    <div class="Clear">
                    </div>
                </asp:Panel>
                <asp:Panel ID="uxPerItemTR" runat="server">
                    <div class="fb">
                        <asp:Label ID="lxPerItem" runat="server" meta:resourcekey="lxPerItem"></asp:Label>
                    </div>
                    <div class="CommonRowStyle">
                        <div class="Label">
                            <asp:Label ID="lcFirstItem" runat="server" meta:resourcekey="lcFirstCost"></asp:Label></div>
                        <asp:TextBox ID="uxFirstItemText" runat="server" CssClass="fl TextBox"></asp:TextBox>
                        <div class="validator1 fl">
                            <span class="Asterisk">*</span>
                        </div>
                        <asp:RequiredFieldValidator ID="uxFirstItemRequiredField" runat="server" ControlToValidate="uxFirstItemText"
                            Display="Dynamic" ValidationGroup="ValidShipping" CssClass="CommonValidatorText">
                            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> First Item is required.
                            <div class="CommonValidateDiv">
                            </div>
                        </asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="uxFirstItemCompare" runat="server" ControlToValidate="uxFirstItemText"
                            Operator="DataTypeCheck" Type="Double" Display="Dynamic" ValidationGroup="ValidShipping"
                            CssClass="CommonValidatorText">
                            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> First Item is invalid.
                            <div class="CommonValidateDiv">
                            </div>
                        </asp:CompareValidator>
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <div class="Label">
                            <asp:Label ID="lcNextItem" runat="server" meta:resourcekey="lcNextCost"></asp:Label></div>
                        <asp:TextBox ID="uxNextItemText" runat="server" CssClass="fl TextBox"></asp:TextBox>
                        <div class="validator1 fl">
                            <span class="Asterisk">*</span>
                        </div>
                        <asp:RequiredFieldValidator ID="uxNextItemRequired" runat="server" ControlToValidate="uxNextItemText"
                            Display="Dynamic" ValidationGroup="ValidShipping" CssClass="CommonValidatorText">
                            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Next Item is required.
                            <div class="CommonValidateDiv">
                            </div>
                        </asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="uxNextItemCompare" runat="server" ControlToValidate="uxNextItemText"
                            Operator="DataTypeCheck" Type="Double" Display="Dynamic" ValidationGroup="ValidShipping"
                            CssClass="CommonValidatorText">
                            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Next Item is invalid.
                            <div class="CommonValidateDiv">
                            </div>
                        </asp:CompareValidator>
                        <div class="Clear">
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="uxByWeightTR" runat="server">
                </asp:Panel>
                <asp:Panel ID="uxByOrderTotalTR" runat="server">
                </asp:Panel>
                <div class="Clear">
                </div>
            </div>
            <div>
                <vevo:AdvanceButton ID="uxAddButton" runat="server" meta:resourcekey="uxAddButton"
                    CssClassBegin="mgt10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxAddButton_Click"
                    OnClickGoTo="Top" ValidationGroup="ValidShipping"></vevo:AdvanceButton>
                <vevo:AdvanceButton ID="uxUpdateButton" runat="server" meta:resourcekey="uxUpdateButton"
                    CssClassBegin="mgt10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxUpdateButton_Click"
                    OnClickGoTo="Top" ValidationGroup="ValidShipping"></vevo:AdvanceButton>
                <vevo:AdvanceButton ID="uxAddWeightRateButton" runat="server" meta:resourcekey="uxAddWeightRateButton"
                    CssClassBegin="mgt10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxAddWeightRateButton_Click"
                    OnClickGoTo="Top"></vevo:AdvanceButton>
                <vevo:AdvanceButton ID="uxAddOrderTotalRateButton" runat="server" meta:resourcekey="uxAddOrderTotalRateButton"
                    CssClassBegin="mgt10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxAddOrderTotalRateButton_Click"
                    OnClickGoTo="Top"></vevo:AdvanceButton>
                <div class="Clear">
                </div>
            </div>
        </div>
    </ContentTemplate>
</uc2:AdminContent>
