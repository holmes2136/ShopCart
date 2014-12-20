<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DepartmentDetails.ascx.cs"
    Inherits="AdminAdvanced_Components_DepartmentDetails" %>
<%@ Register Src="QuantityDiscount.ascx" TagName="QuantityDiscount" TagPrefix="uc1" %>
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
            ValidationGroup="VaildDepartment" CssClass="ValidationStyle" />
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
                <asp:TextBox ID="uxNameText" runat="server" Width="250px" ValidationGroup="VaildDepartment"
                    CssClass="TextBox" />
                <uc5:LanaguageLabelPlus ID="uxPlus1" runat="server" />
                <div class="fl validator1">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="uxNameRequiredValidator" runat="server" ControlToValidate="uxNameText"
                    Display="Dynamic" ValidationGroup="VaildDepartment" CssClass="CommonValidatorText">
                      <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Department name is required.
                            <div class="CommonValidateDiv CommonValidateDivCategoryLong">
                            </div>
                </asp:RequiredFieldValidator>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcDescription" runat="server" meta:resourcekey="lcDescription" CssClass="Label" />
                <asp:TextBox ID="uxDescriptionText" runat="server" Height="70px" TextMode="MultiLine"
                    Width="250px" CssClass="TextBox" />
                <uc5:LanaguageLabelPlus ID="LanaguageLabelPlus1" runat="server" />
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcImage" runat="server" meta:resourcekey="lcImage" CssClass="Label" />
                <asp:TextBox ID="uxImageText" runat="server" Width="250px" CssClass="TextBox" />
                <asp:LinkButton ID="uxImageDepartmentLinkButton" runat="server" OnClick="uxImageDepartmentLinkButton_Click"
                    CssClass="fl mgl5">Upload...</asp:LinkButton>
                <uc6:Upload ID="uxImageDepartmentUpload" runat="server" ShowControl="false" CheckType="Image"
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
            <div class="CommonRowStyle">
                <asp:Label ID="lcParent" runat="server" meta:resourcekey="lcParent" CssClass="Label" />
                <asp:DropDownList ID="uxParentDrop" runat="server" Width="250px" CssClass="fl DropDown">
                </asp:DropDownList>
            </div>
            <uc1:QuantityDiscount ID="uxQuantityDiscount" runat="server" Visible="false" />
            <asp:Panel ID="uxMetaInfoSetting" runat="server">
                <div class="CommonRowStyle">
                    <div class="Label">
                        <asp:Label ID="lcMetaKeyword" runat="server" meta:resourcekey="lcMetaKeyword" /><br />
                        <asp:Label ID="lcMetaKeywordComment" runat="server" meta:resourcekey="lcMetaKeywordComment" /></div>
                    <asp:TextBox ID="uxMetaKeywordText" runat="server" Width="250px" CssClass="TextBox"
                        TextMode="MultiLine" Rows="2" />
                    <uc5:LanaguageLabelPlus ID="LanaguageLabelPlus2" runat="server" />
                </div>
                <div class="CommonRowStyle">
                    <div class="Label">
                        <asp:Label ID="lcMetaDescription" runat="server" meta:resourcekey="lcMetaDescription" /><br />
                        <asp:Label ID="lcMetaDescriptionComment" runat="server" meta:resourcekey="lcMetaDescriptionComment" /></div>
                    <asp:TextBox ID="uxMetaDescriptionText" runat="server" Width="250px" CssClass="TextBox"
                        TextMode="MultiLine" Rows="5" />
                    <uc5:LanaguageLabelPlus ID="LanaguageLabelPlus3" runat="server" />
                </div>
            </asp:Panel>
            <asp:Panel ID="uxLayoutSettingPanel" runat="server">
                <asp:Panel ID="uxLayoutOverridePanel" runat="server" CssClass="CommonRowStyle">
                    <asp:Label ID="uxLayoutOverrideLabel" runat="server" meta:resourcekey="lcLayoutOverrideLabel"
                        CssClass="Label" />
                    <asp:DropDownList ID="uxLayoutOverrideDrop" runat="server" CssClass="fl DropDown">
                        <asp:ListItem Value="True">Yes</asp:ListItem>
                        <asp:ListItem Selected="True" Value="False">No</asp:ListItem>
                    </asp:DropDownList>
                </asp:Panel>
                <asp:Panel ID="uxLayoutRadioPanel" runat="server" CssClass="CommonRowStyle">
                    <asp:Label ID="uxLayoutRadioLabel" runat="server" meta:resourcekey="lcLayoutRadioLabel"
                        CssClass="BulletLabel" />
                    <asp:RadioButtonList ID="uxLayoutTypeRadioButtonList" runat="server" CssClass="fl">
                        <asp:ListItem Text="Sub-Departments" Value="Department" Selected="True" />
                        <asp:ListItem Text="Products" Value="Product" />
                    </asp:RadioButtonList>
                </asp:Panel>
                <asp:Panel ID="uxDepartmentLayoutPanel" runat="server" CssClass="CommonRowStyle">
                    <asp:Label ID="uxDepartmentLayoutLabel" runat="server" meta:resourcekey="lcDepartmentLaout"
                        CssClass="BulletLabel" />
                    <asp:DropDownList ID="uxDepartmentLayoutDrop" runat="server" CssClass="fl DropDown">
                    </asp:DropDownList>
                </asp:Panel>
                <asp:Panel ID="uxProductListLayoutPanel" runat="server" CssClass="CommonRowStyle">
                    <asp:Label ID="uxProductListLayoutLabel" runat="server" meta:resourcekey="lcProductListLaout"
                        CssClass="BulletLabel" />
                    <asp:DropDownList ID="uxProductListLayoutDrop" runat="server" CssClass="fl DropDown">
                    </asp:DropDownList>
                    <asp:HiddenField ID="uxHiddenStatus" runat="server" />
                </asp:Panel>
            </asp:Panel>
            <div class="CommonRowStyle">
                <asp:Label ID="lcOtherOne" runat="server" meta:resourcekey="lcOtherOne" CssClass="Label" />
                <asp:TextBox ID="uxOtherOneText" runat="server" Width="250px" CssClass="TextBox" />
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcOtherTwo" runat="server" meta:resourcekey="lcOtherTwo" CssClass="Label" />
                <asp:TextBox ID="uxOtherTwoText" runat="server" Width="250px" CssClass="TextBox" />
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcOtherThree" runat="server" meta:resourcekey="lcOtherThree" CssClass="Label" />
                <asp:TextBox ID="uxOtherThreeText" runat="server" Width="250px" CssClass="TextBox" />
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcOtherFour" runat="server" meta:resourcekey="lcOtherFour" CssClass="Label" />
                <asp:TextBox ID="uxOtherFourText" runat="server" Width="250px" CssClass="TextBox" />
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcOtherFive" runat="server" meta:resourcekey="lcOtherFive" CssClass="Label" />
                <asp:TextBox ID="uxOtherFiveText" runat="server" Width="250px" CssClass="TextBox" />
            </div>
            <div id="uxIsEnabledTR" runat="server" class="CommonRowStyle">
                <asp:Label ID="lcIsEnabled" runat="server" meta:resourcekey="lcIsEnabled" CssClass="Label" />
                <asp:CheckBox ID="uxIsEnabledCheck" runat="server" Checked="true" CssClass="fl CheckBox" />
            </div>
            <asp:Panel ID="uxIsAnchorPanel" runat="server" Visible="false">
                <div id="uxIsAnchorTR" runat="server" class="CommonRowStyle">
                    <asp:Label ID="uxIsAnchorLabel" runat="server" meta:resourcekey="uxIsAnchorLabel"
                        CssClass="Label" />
                    <asp:CheckBox ID="uxIsAnchorCheck" runat="server" Checked="false" CssClass="fl CheckBox" />
                </div>
            </asp:Panel>
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
                        Display="Dynamic" ValidationGroup="VaildDepartment" CssClass="CommonValidatorText">
                        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number of New Arrival is required.
                        <div class="CommonValidateDiv CommonValidateDivDisplayedNewArrival">
                        </div>
                    </asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="RangeZip" runat="server" ControlToValidate="uxNewArrivalAmountText"
                        MinimumValue="00000" MaximumValue="99999" Type="Integer" Display="Dynamic" CssClass="CommonValidatorText"
                        ValidationGroup="VaildDepartment">
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
                    OnClick="uxAddButton_Click" OnClickGoTo="Top" ValidationGroup="VaildDepartment" />
                <vevo:AdvanceButton ID="uxUpdateButton" runat="server" meta:resourcekey="uxUpdateButton"
                    CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    OnClick="uxUpdateButton_Click" OnClickGoTo="Top" ValidationGroup="VaildDepartment" />
            </div>
        </div>
    </ContentTemplate>
</uc2:AdminContent>
<asp:HiddenField ID="uxStatusHidden" runat="server" />
