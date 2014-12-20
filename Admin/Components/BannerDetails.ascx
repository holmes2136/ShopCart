<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BannerDetails.ascx.cs"
    Inherits="Admin_Components_BannerDetails" %>
<%@ Register Src="LanguageControl.ascx" TagName="LanguageSelector" TagPrefix="uc1" %>
<%@ Register Src="Template/AdminContent.ascx" TagName="AdminContent" TagPrefix="uc2" %>
<%@ Register Src="Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Src="CalendarPopup.ascx" TagName="CalendarPopup" TagPrefix="uc4" %>
<%@ Register Src="../Components/LanguageLabelPlus.ascx" TagName="LanaguageLabelPlus"
    TagPrefix="uc5" %>
<%@ Register Src="~/Components/TextEditor.ascx" TagName="TextEditor" TagPrefix="uc6" %>
<%@ Register Src="Common/Upload.ascx" TagName="Upload" TagPrefix="uc6" %>
<uc2:AdminContent ID="uxAdminContent" runat="server">
    <MessageTemplate>
        <uc3:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <ValidationDenotesTemplate>
        <div class="topline">
            <div class="RequiredLabel c6">
                <span class="Asterisk">*</span>
                <asp:Label ID="lcRequiredFieldSymbol" runat="server" meta:resourcekey="lcRequiredFieldSymbol" />
            </div>
        </div>
    </ValidationDenotesTemplate>
    <ValidationSummaryTemplate>
        <asp:ValidationSummary ID="uxValidationSummary" runat="server" meta:resourcekey="uxValidationSummary"
            ValidationGroup="ValidBanner" CssClass="ValidationStyle" />
    </ValidationSummaryTemplate>
    <LanguageControlTemplate>
        <uc1:LanguageSelector ID="uxLanguageControl" runat="server" ShowLanguageDescription="true" />
    </LanguageControlTemplate>
    <ContentTemplate>
        <div class="Container-Row">
            <div class="CommonRowStyle">
                <asp:Label ID="uxNameLabel" runat="server" meta:resourcekey="uxName" CssClass="Label" />
                <asp:TextBox ID="uxNameText" runat="server" Width="252px" CssClass="TextBox" />
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="uxNameRequiredValidator" runat="server" ControlToValidate="uxNameText"
                    ValidationGroup="ValidBanner" Display="Dynamic" CssClass="CommonValidatorText">
                    <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Banner Name is required.
                    <div class="CommonValidateDiv CommonValidateDivBannerName">
                    </div>
                </asp:RequiredFieldValidator>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxStoreLabel" runat="server" meta:resourcekey="uxStore" CssClass="Label" />
                <div class="fl">
                    <asp:DropDownList ID="uxStoreDrop" AutoPostBack="false" runat="server" Width="155px"
                        Enabled="false" />
                </div>
            </div>
            <asp:Panel ID="uxImageTR" runat="server" CssClass="CommonRowStyle">
                <asp:Label ID="uxImageLabel" runat="server" meta:resourcekey="uxImage" CssClass="Label" />
                <asp:TextBox ID="uxFileImageText" runat="server" Width="252px" CssClass="TextBox" />
                <asp:LinkButton ID="uxUploadLinkButton" runat="server" OnClick="uxUploadLinkButton_Click"
                    CssClass="fl mgl5">Upload...</asp:LinkButton>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="uxFileImageRequiredValidator" runat="server" ControlToValidate="uxFileImageText"
                    ValidationGroup="ValidBanner" Display="Dynamic" CssClass="CommonValidatorText">
                    <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Banner Image is required.
                    <div class="CommonValidateDiv CommonValidateDivBannerImageText">
                    </div>
                </asp:RequiredFieldValidator>
                <div class="Clear">
                </div>
            </asp:Panel>
            <uc6:Upload ID="uxUpload" runat="server" ShowControl="false" CssClass="CommonRowStyle"
                CheckType="Image" ButtonImage="SelectImages.png" ButtonWidth="105" ButtonHeight="22"
                ShowText="false" />
            <div class="CommonRowStyle">
                <asp:Label ID="uxDescriptionLabel" runat="server" meta:resourcekey="uxDescription"
                    CssClass="Label" />
                <asp:TextBox ID="uxDescriptionText" runat="server" Width="252px" CssClass="TextBox fl"></asp:TextBox>
                <uc5:LanaguageLabelPlus ID="uxDescriptionPlus" runat="server" />
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxLinkURLLabel" runat="server" meta:resourcekey="uxLinkURLLabel" CssClass="Label" />
                <asp:TextBox ID="uxLinkURLText" runat="server" Width="252px" CssClass="TextBox fl"></asp:TextBox>
                <uc5:LanaguageLabelPlus ID="uxLinkURLPlus" runat="server" />
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="Label1" runat="server" CssClass="Label" Text="&nbsp;" />
                <asp:Label ID="uxLinkMessageText" runat="server" meta:resourcekey="uxLinkMessage"
                    CssClass="fl c6" />
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxCreateDateLabel" runat="server" meta:resourcekey="uxCreateDate"
                    CssClass="Label" />
                <uc4:CalendarPopup ID="uxCreateDateCalendarPopup" runat="server" TextBoxEnabled="false" />
                <asp:TextBox ID="uxCreateDateText" runat="server" Width="328px" CssClass="TextBox" />
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxEndDateLabel" runat="server" meta:resourcekey="uxEndDate" CssClass="Label" />
                <uc4:CalendarPopup ID="uxEndDateCalendarPopup" runat="server" TextBoxEnabled="false" />
                <asp:TextBox ID="uxEndDateText" runat="server" Width="328px" CssClass="TextBox" />
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="uxIsEnabledLabel" runat="server" meta:resourcekey="uxIsEnabled" />
                </div>
                <asp:CheckBox ID="uxIsEnabledCheck" runat="server" CssClass="fl" Checked="true" />
            </div>
            <div class="mgt10">
                <vevo:AdvanceButton ID="uxAddButton" runat="server" meta:resourcekey="uxAddButton"
                    CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    OnClick="uxAddButton_Click" OnClickGoTo="Top" ValidationGroup="ValidBanner" />
                <vevo:AdvanceButton ID="uxUpdateButton" runat="server" meta:resourcekey="uxUpdateButton"
                    CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    OnClick="uxUpdateButton_Click" OnClickGoTo="Top" ValidationGroup="ValidBanner" />
                <div class="Clear">
                </div>
            </div>
        </div>
    </ContentTemplate>
</uc2:AdminContent>
