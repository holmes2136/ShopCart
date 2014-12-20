<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductOptionsDetails.ascx.cs"
    Inherits="Components_ProductOptionsDetails" %>
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
            ValidationGroup="ValidProductOption" CssClass="ValidationStyle" />
    </ValidationSummaryTemplate>
    <LanguageControlTemplate>
        <uc3:LanguageControl ID="uxLanguageControl" runat="server" />
    </LanguageControlTemplate>
    <ContentTemplate>
        <div class="Container-Box">
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="lcOptionType" runat="server" meta:resourcekey="lcOptionType" /></div>
                <asp:DropDownList ID="uxTypeDrop" runat="server" CssClass="fl DropDown">
                    <asp:ListItem Selected="True">Radio</asp:ListItem>
                    <asp:ListItem Value="DropDown">Drop Down</asp:ListItem>
                    <asp:ListItem>Text</asp:ListItem>
                    <asp:ListItem Value="InputList">Input List</asp:ListItem>
                    <asp:ListItem>Upload</asp:ListItem>
                    <asp:ListItem>UploadRequired</asp:ListItem>
                </asp:DropDownList>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="lcOptionName" runat="server" meta:resourcekey="lcOptionName" /></div>
                <asp:TextBox ID="uxNameOptionText" runat="server" CssClass="fl TextBox"></asp:TextBox>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="uxNameOptionTextValidator" runat="server" ControlToValidate="uxNameOptionText"
                    ValidationGroup="ValidProductOption" CssClass="CommonValidatorText" Display="Dynamic">
                            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Option Name is required.
                            <div class="CommonValidateDiv CommonValidateDivProductOptionDetails">
                            </div>
                </asp:RequiredFieldValidator>
                <uc5:LanaguageLabelPlus ID="uxPlus1" runat="server" />
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
                    ValidationGroup="ValidProductOption" CssClass="CommonValidatorText" Display="Dynamic">
                            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Display Name is required.
                            <div class="CommonValidateDiv CommonValidateDivProductOptionDetails">
                            </div>
                </asp:RequiredFieldValidator>
                <uc5:LanaguageLabelPlus ID="LanaguageLabelPlus1" runat="server" />
                <div class="Clear">
                </div>
            </div>
            <div class="Clear" />
            <div class="CommonRowStyle mgt10">
                <vevo:AdvanceButton ID="uxAddOptionGroupButton" runat="server" meta:resourcekey="lcAddOptionGroup"
                    CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    OnClick="uxAddOptionGroupButton_Click" OnClickGoTo="Top" ValidationGroup="ValidProductOption" />
                <vevo:AdvanceButton ID="uxUpdateOptionGroupButton" runat="server" meta:resourcekey="lcUpdateOptionGroup"
                    CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    OnClick="uxUpdateOptionGroupButton_Click" OnClickGoTo="Top"
                    ValidationGroup="ValidProductOption" />
            </div>
        </div>
    </ContentTemplate>
</uc1:AdminContent>
