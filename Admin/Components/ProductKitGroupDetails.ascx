<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductKitGroupDetails.ascx.cs"
    Inherits="Admin_Components_ProductKitGroupDetails" %>
<%@ Register Src="Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Src="LanguageControl.ascx" TagName="LanguageSelector" TagPrefix="uc1" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Register Src="~/Components/TextEditor.ascx" TagName="TextEditor" TagPrefix="uc2" %>
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
            ValidationGroup="ValidProductKitGroup" CssClass="ValidationStyle" />
    </ValidationSummaryTemplate>
    <ContentTemplate>
        <div class="Container-Box">
            <div class="CommonRowStyle">
                <asp:Label ID="uxProductKitGroupNameLabel" runat="server" meta:resourcekey="uxProductKitGroupNameLabel"
                    CssClass="Label" />
                <asp:TextBox ID="uxProductKitGroupNameText" runat="server" ValidationGroup="ValidProductKitGroup"
                    Width="150px" CssClass="TextBox"></asp:TextBox>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="uxRequiredProductKitGroupNameValidator" runat="server"
                    ControlToValidate="uxProductKitGroupNameText" CssClass="CommonValidatorText"
                    Display="Dynamic" ValidationGroup="ValidProductKitGroup">
                            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Group Name is required.
                            <div class="CommonValidateDiv CommonValidateDivProductOptionDetails">
                            </div>
                </asp:RequiredFieldValidator>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxTypeLabel" runat="server" meta:resourcekey="uxTypeLabel" CssClass="Label" />
                <asp:DropDownList ID="uxTypeDrop" runat="server">
                    <asp:ListItem Text="RadioButton" Value="Radio"></asp:ListItem>
                    <asp:ListItem Text="DropDownList" Value="DropDown"></asp:ListItem>
                    <asp:ListItem Text="CheckBox" Value="Checkbox"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxIsRequiredLabel" runat="server" meta:resourcekey="uxIsRequiredLabel"
                    CssClass="Label" />
                <asp:CheckBox ID="uxIsRequiredCheck" runat="server" CssClass="CheckBox" Checked="false" />
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxDescriptionLabel" runat="server" meta:resourcekey="uxDescriptionLabel"
                    CssClass="Label" />
                <uc2:TextEditor ID="uxDescriptionText" runat="server" PanelClass="freeTextBox1 fl"
                    TextClass="TextBox" Height="300px" />
            </div>
            <div class="Clear" />
            <div class="mgt10">
                <vevo:AdvanceButton ID="uxAddButton" runat="server" meta:resourcekey="uxAddButton"
                    CssClassBegin="mgt20 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    OnClick="uxAddButton_Click" OnClickGoTo="Top" ValidationGroup="ValidProductKitGroup" />
                <vevo:AdvanceButton ID="uxUpdateButton" runat="server" meta:resourcekey="uxUpdateButton"
                    CssClassBegin="mgt20 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    OnClick="uxUpdateButton_Click" OnClickGoTo="Top" ValidationGroup="ValidProductKitGroup" />
            </div>
        </div>
    </ContentTemplate>
</uc1:AdminContent>
