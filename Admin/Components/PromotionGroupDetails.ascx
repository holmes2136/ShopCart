<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PromotionGroupDetails.ascx.cs"
    Inherits="Admin_Components_PromotionGroupDetails" %>
<%@ Register Src="Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Src="LanguageControl.ascx" TagName="LanguageSelector" TagPrefix="uc1" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Register Src="~/Components/TextEditor.ascx" TagName="TextEditor" TagPrefix="uc2" %>
<%@ Register Src="MultiSubGroup.ascx" TagName="MultiSubGroup" TagPrefix="uc3" %>
<%@ Register Src="PromotionGroupImage.ascx" TagName="PromotionImage" TagPrefix="uc4" %>
<uc1:AdminContent ID="uxAdminContent" runat="server">
    <MessageTemplate>
        <uc3:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <LanguageControlTemplate>
        <uc1:LanguageSelector ID="uxLanguageControl" runat="server" ShowLanguageDescription="true" />
    </LanguageControlTemplate>
    <ValidationDenotesTemplate>
        <div class="topline">
            <div class="RequiredLabel c6">
                <span class="Asterisk">*</span>
                <asp:Label ID="uxRequiredFieldSymbol" runat="server" meta:resourcekey="uxRequiredFieldSymbol" />
            </div>
        </div>
    </ValidationDenotesTemplate>
    <ValidationSummaryTemplate>
        <asp:ValidationSummary ID="uxValidationSummary" runat="server" meta:resourcekey="uxValidationSummary"
            ValidationGroup="ValidPromotionGroup" CssClass="ValidationStyle" />
    </ValidationSummaryTemplate>
    <ContentTemplate>
        <div class="Container-Box">
            <div class="CommonRowStyle">
                <asp:Label ID="uxPromotionGroupNameLabel" runat="server" meta:resourcekey="uxPromotionGroupNameLabel"
                    CssClass="Label" />
                <asp:TextBox ID="uxPromotionGroupNameText" runat="server" ValidationGroup="ValidPromotionGroup"
                    Width="150px" CssClass="TextBox"></asp:TextBox>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="uxRequiredPromotionGroupNameValidator" runat="server"
                    ControlToValidate="uxPromotionGroupNameText" Display="Dynamic" ValidationGroup="ValidPromotionGroup"
                    CssClass="CommonValidatorText">
                        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Group Name is required.
                        <div class="CommonValidateDiv">
                        </div>
                </asp:RequiredFieldValidator>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxPriceLabel" runat="server" meta:resourcekey="uxProductGroupPriceLabel"
                    CssClass="Label" />
                <asp:TextBox ID="uxPriceText" runat="server" ValidationGroup="ValidPromotionGroup"
                    Width="150px" CssClass="TextBox"></asp:TextBox>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                </div>
                <asp:CompareValidator ID="uxPriceCompare" runat="server" ControlToValidate="uxPriceText"
                    Operator="DataTypeCheck" Type="Currency" CssClass="CommonValidatorText" ValidationGroup="ValidPromotionGroup"
                    Display="Dynamic">
                            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Your price is invalid.
                        <div class="CommonValidateDiv">
                        </div>
                </asp:CompareValidator>
                <asp:RequiredFieldValidator ID="uxRequiredPriceValidator" runat="server" ControlToValidate="uxPriceText"
                    ValidationGroup="ValidPromotionGroup" Display="Dynamic" CssClass="CommonValidatorText">
                            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Group Price is required.
                        <div class="CommonValidateDiv">
                        </div>
                </asp:RequiredFieldValidator>
            </div>
            <div class="CommonRowStyle">
                <uc4:PromotionImage ID="uxPromotionImage" runat="server" />
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxIsEnabledLabel" runat="server" meta:resourcekey="uxIsEnabledLabel"
                    CssClass="Label" />
                <asp:CheckBox ID="uxIsEnabeldCheck" runat="server" CssClass="CheckBox" Checked="true" />
            </div>
            <asp:Panel ID="uxStorePanel" runat="server" CssClass="CommonRowStyle">
                <asp:Label ID="uxStoreLabel" runat="server" meta:resourcekey="uxStoreLabel" CssClass="Label" />
                <div class="fl">
                    <asp:DropDownList ID="uxStoreDrop" runat="server" Width="155px" />
                </div>
            </asp:Panel>
            <div class="CommonRowStyle">
                <asp:Label ID="uxDescriptionLabel" runat="server" meta:resourcekey="uxDescriptionLabel"
                    CssClass="Label" />
                <uc2:TextEditor ID="uxDescriptionText" runat="server" PanelClass="freeTextBox1 fl"
                    TextClass="TextBox" Height="300px" />
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxSubGroupLabel" runat="server" meta:resourcekey="uxSubGroupLabel"
                    CssClass="Label" />
                <uc3:MultiSubGroup ID="uxMultiSubGroup" runat="server" />
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxWeightLabel" runat="server" meta:resourcekey="uxWeightLabel" CssClass="Label" />
                <asp:TextBox ID="uxWeightText" runat="server" Width="70px" CssClass="TextBox" ValidationGroup="ValidPromotionGroup" />
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="uxRequiredWeightValidator" runat="server" ControlToValidate="uxWeightText"
                    ValidationGroup="ValidPromotionGroup" CssClass="CommonValidatorText" Display="Dynamic">
                        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Weight is required
                        <div class="CommonValidateDiv CommonValidateDivPromotionGroupShot">
                        </div>
                </asp:RequiredFieldValidator>
                <asp:CompareValidator ID="uxWeightTextCompare" runat="server" ControlToValidate="uxWeightText"
                    Operator="DataTypeCheck" Type="Integer" Display="Dynamic" ValidationGroup="ValidPromotionGroup"
                    CssClass="CommonValidatorText">
                        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Your weight is invalid .
                        <div class="CommonValidateDiv CommonValidateDivPromotionGroupWeight">
                        </div>
                </asp:CompareValidator>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxIsFreeShippingLabel" runat="server" meta:resourcekey="uxIsFreeShippingLabel"
                    CssClass="Label" />
                <asp:CheckBox ID="uxIsFreeShippingCheck" runat="server" CssClass="CheckBox" Checked="false" />
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxTaxClassLabel" runat="server" meta:resourcekey="uxTaxClassLabel"
                    CssClass="Label" />
                <asp:DropDownList ID="uxTaxClassDrop" runat="server" CssClass="DropDown">
                </asp:DropDownList>
            </div>
            <div class="Clear" />
            <div class="mgt10">
                <vevo:AdvanceButton ID="uxAddButton" runat="server" meta:resourcekey="uxAddButton"
                    CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    OnClick="uxAddButton_Click" OnClickGoTo="Top" ValidationGroup="ValidPromotionGroup">
                </vevo:AdvanceButton>
                <vevo:AdvanceButton ID="uxUpdateButton" runat="server" meta:resourcekey="uxUpdateButton"
                    CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    OnClick="uxUpdateButton_Click" OnClickGoTo="Top" ValidationGroup="ValidPromotionGroup">
                </vevo:AdvanceButton>
            </div>
        </div>
    </ContentTemplate>
</uc1:AdminContent>
