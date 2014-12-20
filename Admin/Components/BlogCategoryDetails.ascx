<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BlogCategoryDetails.ascx.cs" 
Inherits="Admin_Components_BlogCategoryDetails" %>
<%@ Register Src="~/Components/TextEditor.ascx" TagName="TextEditor" TagPrefix="uc1" %>
<%@ Register Src="LanguageControl.ascx" TagName="LanguageControl" TagPrefix="uc4" %>
<%@ Register Src="Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Src="Common/Upload.ascx" TagName="Upload" TagPrefix="uc6" %>
<%@ Register Src="Template/AdminContent.ascx" TagName="AdminContent" TagPrefix="uc2" %>
<%@ Register Src="../Components/LanguageLabelPlus.ascx" TagName="LanaguageLabelPlus"
    TagPrefix="uc5" %>

<uc2:AdminContent ID="uxAdminContent" runat="server">
    <MessageTemplate>
        <uc3:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <ValidationSummaryTemplate>
        <asp:ValidationSummary ID="uxValidationSummary" runat="server" meta:resourcekey="uxValidationSummary"
            ValidationGroup="VaildBlogCategory" CssClass="ValidationStyle" />
    </ValidationSummaryTemplate>
    <ValidationDenotesTemplate>
        <div class="topline">
            <div class="RequiredLabel c6">
                <span class="Asterisk">*</span>
                <asp:Label ID="lcRequiredFieldSymbol" runat="server" meta:resourcekey="lcRequiredFieldSymbol" /></div>
        </div>
    </ValidationDenotesTemplate>
    <LanguageControlTemplate>
        <uc4:LanguageControl ID="uxLanguageControl" runat="server" ShowLanguageDescription="true" />
    </LanguageControlTemplate>
    <ContentTemplate>
        <div class="Container-Row">
            <div class="CommonRowStyle">
                <asp:Label ID="lcName" runat="server" meta:resourcekey="lcName" CssClass="Label" />
                <asp:TextBox ID="uxNameText" runat="server" Width="250px" ValidationGroup="VaildBlogCategory"
                    CssClass="TextBox" />
                <uc5:LanaguageLabelPlus ID="uxPlus1" runat="server" />
                <div class="fl validator1">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="uxNameRequiredValidator" runat="server" ControlToValidate="uxNameText"
                    Display="Dynamic" ValidationGroup="VaildBlogCategory" CssClass="CommonValidatorText">
                            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Blog Category name is required.
                            <div class="CommonValidateDiv CommonValidateDivCategoryLong">
                            </div>
                </asp:RequiredFieldValidator>
            </div>            
            <div class="CommonRowStyle">
                <asp:Label ID="lcDescription" runat="server" meta:resourcekey="lcDescription"
                    CssClass="Label" />
                <asp:TextBox ID="uxDescriptionText" runat="server" Height="70px" TextMode="MultiLine"
                    Width="250px" CssClass="TextBox" />
                <uc5:LanaguageLabelPlus ID="LanaguageLabelPlus1" runat="server" />
            </div>
            <asp:Panel ID="uxMetaInfoSetting" runat="server">
                <div class="CommonRowStyle">
                    <div class="Label">
                        <asp:Label ID="lcMetaKeyword" runat="server" meta:resourcekey="lcMetaKeyword" /><br />
                        <asp:Label ID="lcMetaKeywordComment" runat="server" meta:resourcekey="lcMetaKeywordComment" /></div>
                    <asp:TextBox ID="uxMetaKeywordText" runat="server" Width="250px" CssClass="TextBox"
                        TextMode="MultiLine" Rows="2" />
                    <uc5:LanaguageLabelPlus ID="LanaguageLabelPlus3" runat="server" />
                </div>
                <div class="CommonRowStyle">
                    <div class="Label">
                        <asp:Label ID="lcMetaDescription" runat="server" meta:resourcekey="lcMetaDescription" /><br />
                        <asp:Label ID="lcMetaDescriptionComment" runat="server" meta:resourcekey="lcMetaDescriptionComment" /></div>
                    <asp:TextBox ID="uxMetaDescriptionText" runat="server" Width="250px" CssClass="TextBox"
                        TextMode="MultiLine" Rows="5" />
                    <uc5:LanaguageLabelPlus ID="LanaguageLabelPlus7" runat="server" />
                </div>
            </asp:Panel>
            <div class="CommonRowStyle" id="uxIsEnabledTR" runat="server">
                <asp:Label ID="lcIsEnabled" runat="server" meta:resourcekey="lcIsEnabled" CssClass="Label" />
                <asp:CheckBox ID="uxIsEnabledCheck" runat="server" Checked="true" CssClass="fl CheckBox" />
                <div class="Clear">
                </div>
            </div>
            <div class="Clear" />
            <div class="mgt20">
                <vevo:AdvanceButton ID="uxAddButton" runat="server" meta:resourcekey="uxAddButton"
                    CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    OnClick="uxAddButton_Click" OnClickGoTo="Top" ValidationGroup="VaildBlogCategory" />
                <vevo:AdvanceButton ID="uxUpdateButton" runat="server" meta:resourcekey="uxUpdateButton"
                    CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    OnClick="uxUpdateButton_Click" OnClickGoTo="Top" ValidationGroup="VaildBlogCategory" />
            </div>
        </div>
    </ContentTemplate>
</uc2:AdminContent>
<asp:HiddenField ID="uxStatusHidden" runat="server" />