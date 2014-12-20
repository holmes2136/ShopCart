<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ManufacturerDetails.ascx.cs"
    Inherits="Admin_Components_ManufacturerDetails" %>
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
            ValidationGroup="VaildManufacturer" CssClass="ValidationStyle" />
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
                <asp:TextBox ID="uxNameText" runat="server" Width="250px" ValidationGroup="VaildManufacturer"
                    CssClass="TextBox" />
                <uc5:LanaguageLabelPlus ID="uxPlus1" runat="server" />
                <div class="fl validator1">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="uxNameRequiredValidator" runat="server" ControlToValidate="uxNameText"
                    Display="Dynamic" ValidationGroup="VaildManufacturer" CssClass="CommonValidatorText">
                            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Manufacturer name is required.
                            <div class="CommonValidateDiv CommonValidateDivCategoryLong">
                            </div>
                </asp:RequiredFieldValidator>
            </div>            
            <div class="CommonRowStyle">
                <asp:Label ID="lcShortDescription" runat="server" meta:resourcekey="lcShortDescription"
                    CssClass="Label" />
                <asp:TextBox ID="uxShortDescriptionText" runat="server" Height="70px" TextMode="MultiLine"
                    Width="250px" CssClass="TextBox" />
                <uc5:LanaguageLabelPlus ID="LanaguageLabelPlus1" runat="server" />
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcLongDescription" runat="server" meta:resourcekey="lcLongDescription"
                    CssClass="Label" />
                <asp:TextBox ID="uxLongDescriptionText" runat="server" Height="150px" TextMode="MultiLine"
                    Width="250px" CssClass="TextBox" />
                <uc5:LanaguageLabelPlus ID="LanaguageLabelPlus2" runat="server" />
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcImage" runat="server" meta:resourcekey="lcImage" CssClass="Label" />
                <asp:TextBox ID="uxImageText" runat="server" Width="250px" CssClass="TextBox" />
                <uc5:LanaguageLabelPlus ID="LanaguageLabelPlus4" runat="server" />
                <asp:LinkButton ID="uxImageManufacturerLinkButton" runat="server" OnClick="uxImageManufacturerLinkButton_Click"
                    CssClass="fl mgl5">Upload...</asp:LinkButton>
                <uc6:Upload ID="uxImageManufacturerUpload" runat="server" ShowControl="false" CheckType="Image"
                    CssClass="CommonRowStyle" ButtonImage="SelectImages.png" ButtonWidth="105" ButtonHeight="22"
                    ShowText="false" />
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="uxImgAlternateTextLabel" runat="server" Text="Image Alternate Text" /><br />
                </div>
                <asp:TextBox ID="uxImgAlternateTextbox" runat="server" Width="250px" CssClass="TextBox"
                    TextMode="MultiLine" Rows="2" />
                <uc5:LanaguageLabelPlus ID="LanaguageLabelPlus5" runat="server" />
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="uxImgTitleLabel" runat="server" Text="Image Title" /><br />
                </div>
                <asp:TextBox ID="uxImgTitleTextbox" runat="server" Width="250px" CssClass="TextBox"
                    TextMode="MultiLine" Rows="2" />
                <uc5:LanaguageLabelPlus ID="LanaguageLabelPlus6" runat="server" />
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
            <div id="uxIsShowNewArrivalTR" runat="server" class="CommonRowStyle">
                <asp:Label ID="uxIsShowNewArrivalLabel" runat="server" meta:resourcekey="uxIsShowNewArrivalLabel"
                    CssClass="Label" />
                <asp:CheckBox ID="uxIsShowNewArrivalCheck" runat="server" CssClass="fl CheckBox"
                    AutoPostBack="true" OnCheckedChanged="uxIsShowNewArrivalCheck_CheckedChanged" />
            </div>
            <asp:Panel ID="uxNewArrivalAmountPanel" runat="server">
                <div id="uxNewArrivalAmountTR" runat="server" class="CommonRowStyle">
                    <asp:Label ID="uxNewArrivalAmountLabel" runat="server" meta:resourcekey="uxNewArrivalAmountLabel"
                        CssClass="Label" />
                    <asp:TextBox ID="uxNewArrivalAmountText" runat="server" Width="70px" CssClass="TextBox" />
                    <div class="fl validator1">
                        <span class="Asterisk">*</span>
                    </div>
                    <asp:RequiredFieldValidator ID="uxNewArrivalAmountTextValidator" runat="server" ControlToValidate="uxNewArrivalAmountText"
                        Display="Dynamic" ValidationGroup="VaildManufacturer" CssClass="CommonValidatorText">
                        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number of New Arrival is required.
                        <div class="CommonValidateDiv CommonValidateDivDisplayedNewArrival">
                        </div>
                    </asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="RangeZip" runat="server" ControlToValidate="uxNewArrivalAmountText"
                        MinimumValue="00000" MaximumValue="99999" Type="Integer" Display="Dynamic" CssClass="CommonValidatorText"
                        ValidationGroup="VaildManufacturer">
                        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Value is invalid.
                        <div class="CommonValidateDiv CommonValidateDivDisplayedNewArrival">
                        </div>
                    </asp:RangeValidator>
                </div>
            </asp:Panel>
            <div class="Clear" />
            <div class="mgt20">
                <vevo:AdvanceButton ID="uxAddButton" runat="server" meta:resourcekey="uxAddButton"
                    CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    OnClick="uxAddButton_Click" OnClickGoTo="Top" ValidationGroup="VaildManufacturer" />
                <vevo:AdvanceButton ID="uxUpdateButton" runat="server" meta:resourcekey="uxUpdateButton"
                    CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    OnClick="uxUpdateButton_Click" OnClickGoTo="Top" ValidationGroup="VaildManufacturer" />
            </div>
        </div>
    </ContentTemplate>
</uc2:AdminContent>
<asp:HiddenField ID="uxStatusHidden" runat="server" />
