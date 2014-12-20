<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ContentDetails.ascx.cs"
    Inherits="AdminAdvanced_Components_ContentDetails" %>
<%@ Register Src="LanguageControl.ascx" TagName="LanguageControl" TagPrefix="uc2" %>
<%@ Register Src="Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="Template/AdminContent.ascx" TagName="AdminContent" TagPrefix="uc2" %>
<%@ Register Src="../Components/LanguageLabelPlus.ascx" TagName="LanaguageLabelPlus"
    TagPrefix="uc5" %>
<%@ Register Src="~/Components/TextEditor.ascx" TagName="TextEditor" TagPrefix="uc6" %>
<uc2:AdminContent ID="uxAdminContent" runat="server">
    <MessageTemplate>
        <uc1:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <ValidationSummaryTemplate>
        <asp:ValidationSummary ID="uxValidationSummary" runat="server" meta:resourcekey="uxValidationSummary"
            ValidationGroup="VaildContent" CssClass="ValidationStyle" />
    </ValidationSummaryTemplate>
    <ValidationDenotesTemplate>
        <div class="topline">
            <div class="RequiredLabel c6">
                <span class="Asterisk">*</span>
                <asp:Label ID="lcRequiredFieldSymbol" runat="server" meta:resourcekey="lcRequiredFieldSymbol" /></div>
        </div>
    </ValidationDenotesTemplate>
    <LanguageControlTemplate>
        <uc2:LanguageControl ID="uxLanguageControl" runat="server" ShowLanguageDescription="true" />
    </LanguageControlTemplate>
    <ContentTemplate>
        <div class="Container-Box">
            <div id="uxContentNameTR" runat="server" class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="uxContentNameLabel" runat="server" meta:resourcekey="uxContentNameLabel"></asp:Label>
                </div>
                <asp:TextBox ID="uxContentNameText" runat="server" Width="252px" CssClass="TextBox fl"></asp:TextBox>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="uxNameRequiredValidator" runat="server" ControlToValidate="uxContentNameText"
                    ValidationGroup="VaildContent" Display="Dynamic" CssClass="CommonValidatorText">
                            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Content name is required.
                            <div class="CommonValidateDiv CommonValidateDivContentNameLong">
                            </div>
                </asp:RequiredFieldValidator>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="uxContentTitleLabel" runat="server" meta:resourcekey="uxContentTitleLabel"></asp:Label></div>
                <asp:TextBox ID="uxContentTitleText" runat="server" Width="252px" CssClass="TextBox fl">
                </asp:TextBox>
                <div class="pluslabel1 fl">
                    <uc5:LanaguageLabelPlus ID="uxPlus1" runat="server" />
                </div>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="uxContentTitleValidate" runat="server" CssClass="CommonValidatorText"
                    ControlToValidate="uxContentTitleText" ValidationGroup="VaildContent" Display="Dynamic">
                            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Content title is required.
                            <div class="CommonValidateDiv CommonValidateDivContentTitleLong">
                            </div>
                </asp:RequiredFieldValidator>
                <div class="Clear">
                </div>
            </div>
            <div id="uxContentUrlSettingTR" runat="server" class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="uxContentUrlLabel" runat="server" meta:resourcekey="uxContentUrlLabel"></asp:Label></div>
                <asp:Label ID="uxContentShowUrlLabel" runat="server"></asp:Label>
                <asp:HyperLink ID="uxContentUrlLink" runat="server" Target="_blank"></asp:HyperLink>
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="lcContentBodyLabel" runat="server" meta:resourcekey="uxContentBodyLabel"></asp:Label></div>
                <uc6:TextEditor ID="uxLongDescriptionText" runat="Server" PanelClass="freeTextBox1 fl"
                    TextClass="TextBox" />
                <div class="pluslabel1 fl">
                    <uc5:LanaguageLabelPlus ID="LanaguageLabelPlus1" runat="server" />
                </div>
                <div class="Clear">
                </div>
            </div>
            <div id="uxContentMetaTitleTR" runat="server" class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="uxContentMetaTitleLabel" runat="server" meta:resourcekey="uxContentMetaTitleLabel"></asp:Label>
                </div>
                <asp:TextBox ID="uxContentMetaTitleText" runat="server" Width="252px" CssClass="TextBox fl"></asp:TextBox>
                <div class="pluslabel1 fl">
                    <uc5:LanaguageLabelPlus ID="uxPlus2" runat="server" />
                </div>
                <div class="Clear">
                </div>
            </div>
            <div id="uxContentMetaKeywordTR" runat="server" class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="uxContentMetaKeywordLabel" runat="server" meta:resourcekey="uxContentMetaKeywordLabel"></asp:Label>
                </div>
                <asp:TextBox ID="uxContentMetaKeywordText" runat="server" Height="40px" TextMode="MultiLine"
                    Width="252px" CssClass="TextBox fl"></asp:TextBox>
                <div class="pluslabel1 fl">
                    <uc5:LanaguageLabelPlus ID="uxPlus3" runat="server" />
                </div>
                <div class="Clear">
                </div>
            </div>
            <div id="uxContentMetaDescriptionTR" runat="server" class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="uxContentMetaDescriptionLabel" runat="server" meta:resourcekey="uxContentMetaDescriptionLabel"></asp:Label>
                </div>
                <asp:TextBox ID="uxContentMetaDescriptionText" runat="server" Width="252px" TextMode="MultiLine"
                    Rows="4" CssClass="TextBox fl"></asp:TextBox>
                <div class="pluslabel1 fl">
                    <uc5:LanaguageLabelPlus ID="uxPlus4" runat="server" />
                </div>
                <div class="Clear">
                </div>
            </div>
            <div id="uxContentCustomUrlSettingTR" runat="server" class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="uxContentCustomUrlLabel" runat="server" meta:resourcekey="uxContentCustomUrlLabel"></asp:Label>
                </div>
                <div class="fl">
                    <asp:TextBox ID="uxContentCustomUrlText" runat="server" Width="252px" CssClass="TextBox"></asp:TextBox>
                    <asp:Label ID="uxContentCustomComment" runat="server" meta:resourcekey="uxContentCustomComment"
                        CssClass="mgl5"></asp:Label></div>
            </div>
            <div id="uxContentEnabledTR" runat="server" class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="uxContentEnabledLabel" runat="server" meta:resourcekey="uxContentEnabledLabel"></asp:Label></div>
                <asp:CheckBox ID="uxContentEnabledCheck" runat="server" CssClass="fl" />
                <div class="Clear">
                </div>
            </div>
            <div id="uxContentShowInSiteMapTR" runat="server" class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="uxContentShowInSiteMapLabel" runat="server" meta:resourcekey="uxContentShowInSiteMapLabel"></asp:Label></div>
                <asp:CheckBox ID="uxContentShowInSiteMapCheck" runat="server" CssClass="fl" />
                <div class="Clear">
                </div>
            </div>
            <div id="ucContentSubscriptionTR" runat="server" class="CommonRowStyle">
                <asp:Label ID="lcSubscriptionLevel" runat="server" meta:resourcekey="lcSubscriptionLevel"
                    CssClass="Label" />
                <asp:DropDownList ID="uxSubscriptionLevel" runat="server" CssClass="fl DropDown"
                    DataTextField="Name" DataValueField="SubscriptionLevelID">
                </asp:DropDownList>
                <div class="Clear">
                </div>
            </div>
            <div id="uxContentOther1TR" runat="server" class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="uxOther1Label" runat="server" meta:resourcekey="uxOther1Label"></asp:Label></div>
                <asp:TextBox ID="uxOther1Text" runat="server" Width="252px" CssClass="TextBox fl"></asp:TextBox>
                <div class="Clear">
                </div>
            </div>
            <div id="uxContentOther2TR" runat="server" class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="uxOther2Label" runat="server" meta:resourcekey="uxOther2Label"></asp:Label></div>
                <asp:TextBox ID="uxOther2Text" runat="server" Width="252px" CssClass="TextBox fl"></asp:TextBox>
                <div class="Clear">
                </div>
            </div>
            <div id="uxContentOther3TR" runat="server" class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="uxOther3Label" runat="server" meta:resourcekey="uxOther3Label"></asp:Label></div>
                <asp:TextBox ID="uxOther3Text" runat="server" Width="252px" CssClass="TextBox fl"></asp:TextBox>
                <div class="Clear">
                </div>
            </div>
            <div class="mgt10">
                <vevo:AdvanceButton ID="uxAddButton" runat="server" meta:resourcekey="uxAddButton"
                    CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    OnClick="uxAddButton_Click" OnClickGoTo="Top" ValidationGroup="VaildContent" />
                <vevo:AdvanceButton ID="uxEditButton" runat="server" meta:resourcekey="uxEditButton"
                    CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    OnClick="uxEditButton_Click" OnClickGoTo="Top" ValidationGroup="VaildContent" />
                <div class="Clear" />
            </div>
        </div>
    </ContentTemplate>
</uc2:AdminContent>
