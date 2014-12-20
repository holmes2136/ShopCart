<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SpecificationGroupDetails.ascx.cs"
    Inherits="Admin_Components_SpecificationGroupDetails" %>
<%@ Register Src="LanguageControl.ascx" TagName="LanguageControl" TagPrefix="uc3" %>
<%@ Register Src="Message.ascx" TagName="Message" TagPrefix="uc2" %>
<%@ Register Src="Template/AdminContent.ascx" TagName="AdminContent" TagPrefix="uc1" %>
<%@ Register Src="../Components/LanguageLabelPlus.ascx" TagName="LanaguageLabelPlus"
    TagPrefix="uc5" %>
<uc1:AdminContent ID="uxAdminContent" runat="server">
    <MessageTemplate>
        <uc2:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <ValidationDenotesTemplate>
        <div class="topline">
            <div class="RequiredLabel c6">
                <span class="Asterisk">*</span>
                <asp:Label ID="lcRequiredFieldSymbol" runat="server" meta:resourcekey="lcRequiredFieldSymbol" /></div>
        </div>
    </ValidationDenotesTemplate>
    <ValidationSummaryTemplate>
        <asp:ValidationSummary ID="uxValidationSummary" runat="server" meta:resourcekey="uxValidationSummary"
            ValidationGroup="ValidSpecificationGroup" CssClass="ValidationStyle" />
    </ValidationSummaryTemplate>
    <LanguageControlTemplate>
        <uc3:LanguageControl ID="uxLanguageControl" runat="server" />
    </LanguageControlTemplate>
    <ContentTemplate>
        <div class="Container-Box">
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="lcName" runat="server" meta:resourcekey="lcName" /></div>
                <asp:TextBox ID="uxNameText" runat="server" CssClass="fl TextBox"></asp:TextBox>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="uxNameTextValidator" runat="server" ControlToValidate="uxNameText"
                    ValidationGroup="ValidSpecificationGroup" Display="Dynamic" CssClass="CommonValidatorText">
                            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Specification Name is required.
                            <div class="CommonValidateDiv CommonValidateDivProductOptionDetails">
                            </div>
                </asp:RequiredFieldValidator>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="lcDisplayName" runat="server" meta:resourcekey="lcDisplayName"></asp:Label></div>
                <asp:TextBox ID="uxDisplayNameText" runat="server" CssClass="fl TextBox"></asp:TextBox>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="uxDisplayNameTextValidator" runat="server" ControlToValidate="uxDisplayNameText"
                    ValidationGroup="ValidSpecificationGroup" CssClass="CommonValidatorText" Display="Dynamic">
                            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Display Name is required.
                            <div class="CommonValidateDiv CommonValidateDivProductOptionDetails">
                            </div>
                </asp:RequiredFieldValidator>
                <uc5:LanaguageLabelPlus ID="LanaguageLabelPlus1" runat="server" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="lcDescription" runat="server" meta:resourcekey="lcDescription"></asp:Label></div>
                <asp:TextBox ID="uxDescriptionText" runat="server" CssClass="fl TextBox" Height="40px"
                    TextMode="MultiLine" Width="252px"></asp:TextBox>
                <uc5:LanaguageLabelPlus ID="LanaguageLabelPlus2" runat="server" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle mgt10">
                <vevo:AdvanceButton ID="uxAddSpecificationGroupButton" runat="server" meta:resourcekey="lcAddSpecificationGroup"
                    CssClassBegin="mgt10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxAddSpecificationGroupButton_Click"
                    OnClickGoTo="Top" ValidationGroup="ValidSpecificationGroup"></vevo:AdvanceButton>
                <vevo:AdvanceButton ID="uxUpdateSpecificationGroupButton" runat="server" meta:resourcekey="lcUpdateSpecificationGroup"
                    CssClassBegin="mgt10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxUpdateSpecificationGroupButton_Click"
                    OnClickGoTo="Top" ValidationGroup="ValidSpecificationGroup"></vevo:AdvanceButton>
                <div class="Clear">
                </div>
            </div>
        </div>
    </ContentTemplate>
</uc1:AdminContent>
