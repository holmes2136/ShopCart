<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CultureDetails.ascx.cs"
    Inherits="AdminAdvanced_Components_CultureDetails" %>
<%@ Register Src="Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Src="BoxSet/BoxSet.ascx" TagName="BoxSet" TagPrefix="uc1" %>
<%@ Register Src="Template/AdminUserControlContent.ascx" TagName="AdminUserControlContent"
    TagPrefix="uc2" %>
<uc2:AdminUserControlContent ID="uxAdminUserControlContent" runat="server">
    <MessageTemplate>
        <uc3:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <ValidationSummaryTemplate>
        <asp:ValidationSummary ID="uxValidationSummary" runat="server" meta:resourcekey="uxValidationSummary"
            ValidationGroup="CultureDetails" CssClass="ValidationStyle" />
    </ValidationSummaryTemplate>
    <ValidationDenotesTemplate>
        <div class="RequiredLabel c6">
            <span class="Asterisk">*</span>
            <asp:Label ID="lcRequiredFieldSymbol" runat="server" meta:resourcekey="lcRequiredFieldSymbol" /></div>
    </ValidationDenotesTemplate>
    <ButtonEventTemplate>
        <vevo:AdvancedLinkButton ID="uxLanguagePageLink" runat="server" meta:resourcekey="lcLanguageLink"
            PageName="LanguagePageList.ascx" OnClick="ChangePage_Click" CssClass="CommonAdminButtonIcon AdminButtonIconView fl"
            StatusBarText="Language Page List">
        </vevo:AdvancedLinkButton>
    </ButtonEventTemplate>
    <ContentTemplate>
        <div class="Container-Box">
            <div class="CommonRowStyle">
                <asp:Label ID="lcName" runat="server" meta:resourcekey="lcName" CssClass="Label" />
                <asp:DropDownList ID="uxNameDrop" runat="server" CssClass="DropDown fl">
                </asp:DropDownList>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxRequireNameValid" runat="server" ControlToValidate="uxNameDrop"
                        Display="Dynamic" meta:resourcekey="uxNameRequireValidator" ValidationGroup="CultureDetails"><--</asp:RequiredFieldValidator></div>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcFullName" runat="server" meta:resourcekey="lcFullName" CssClass="Label" />
                <asp:Label ID="uxFullNameLabel" runat="server" CssClass="Label" />
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcDisplayName" runat="server" meta:resourcekey="lcDisplayName" CssClass="Label" />
                <asp:TextBox ID="uxDisplayNameText" runat="server" CssClass="TextBox"></asp:TextBox>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxRequireDisplayValid" runat="server" ControlToValidate="uxDisplayNameText"
                        Display="Dynamic" meta:resourcekey="uxDisplayNameRequireValidator" ValidationGroup="CultureDetails"><--</asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcDisplayNameBase" runat="server" meta:resourcekey="lcDisplayNameBase"
                    CssClass="Label" />
                <asp:DropDownList ID="uxDisplayNameBaseDrop" runat="server" CssClass="fl">
                </asp:DropDownList>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcEnabled" runat="server" meta:resourcekey="lcEnabled" CssClass="Label" />
                <asp:CheckBox ID="uxIsEnableddCheck" runat="server" Checked="true" CssClass="fl CheckBox" />
            </div>
            <div class="mgt10">
                <vevo:AdvanceButton ID="uxAddButton" runat="server" meta:resourcekey="lcAddCulture"
                    CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    OnClick="uxAddButton_Click" OnClickGoTo="Top" ValidationGroup="CultureDetails" />
                <vevo:AdvanceButton ID="uxUpdateButton" runat="server" meta:resourcekey="lcEditCulture"
                    CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    OnClick="uxUpdateButton_Click" OnClickGoTo="Top" ValidationGroup="CultureDetails" />
                <div class="Clear" />
            </div>
        </div>
    </ContentTemplate>
</uc2:AdminUserControlContent>
