<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SiteQuickStart.ascx.cs"
    Inherits="AdminAdvanced_MainControls_SiteQuickStart" EnableViewState="true" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc2" %>
<%@ Register Src="../Components/LanguageControl.ascx" TagName="LanguageControl" TagPrefix="uc4" %>
<%@ Register Src="../Components/Boxset/Boxset.ascx" TagName="BoxSet" TagPrefix="uc2" %>
<%@ Register Src="../Components/SiteSetup/Password.ascx" TagName="Password" TagPrefix="uc6" %>
<%@ Register Src="../Components/SiteSetup/DomainKey.ascx" TagName="DomainKey" TagPrefix="uc7" %>
<%@ Register Src="../Components/Common/HelpIcon.ascx" TagName="HelpIcon" TagPrefix="uc12" %>
<uc1:admincontent id="uxContentTemplate" runat="server" headertext="<%$ Resources:lcHeader %>">
    <MessageTemplate>
        <uc2:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <ValidationSummaryTemplate>
        <asp:ValidationSummary ID="uxValidationSummary" runat="server" HeaderText="<div class='Title'>Please correct the following errors :</div>"
            ValidationGroup="ValidSiteSetup" CssClass="ValidationStyle" />
    </ValidationSummaryTemplate>
    <ValidationDenotesTemplate>
        <div class="topline">
            <div class="RequiredLabel c6">
                <span class="Asterisk">*</span>
                <asp:Label ID="lcRequiredFieldSymbol" runat="server" meta:resourcekey="lcRequiredFieldSymbol" />
            </div>
        </div>
    </ValidationDenotesTemplate>
    <ContentTemplate>
        <div class="Container-Box">
        <div class="CommonConfigTitle mgt0">
            <asp:Label ID="uxAdminLabel" runat="server" meta:resourcekey="lcAdminHeader" /></div>
            <uc6:Password ID="uxPassword" runat="server" ValidationGroup="ValidSiteSetup" />
            <div class="CommonConfigTitle dn">
            <asp:Label ID="uxDomainKeyRequestTitleLabel" runat="server" meta:resourcekey="lcDomainKeyRequestTitle"/></div>
            <uc7:DomainKey ID="uxDomainKey" runat="server" />
            <div class="CommonConfigTitle">
            <asp:Label ID="uxWebsiteTestLabel" runat="server" meta:resourcekey="lcWebsiteTestLabel"/></div>
            <asp:Panel ID="uxFilePermissionTestPanel" runat="server" CssClass="ConfigRow">
                <uc12:HelpIcon ID="uxFilePermissionTestHelp" HelpKeyName="FilePermissionTest" runat="server" />
                <asp:Label ID="uxFilePermissionTestLabel" runat="server" meta:resourcekey="lcFilePermissionTestLabel"
                    CssClass="Label" />
                <vevo:AdvanceButton ID="uxFilePermissionTestButton" runat="server" meta:resourcekey="uxFilePermissionTestButton"
                    CssClassBegin="fl" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxFilePerMissionTestButton_Click"
                    OnClickGoTo="None" />
            </asp:Panel>
            <div class="ConfigRow">
                <asp:Panel ID="uxFilePermissionMessagePanel" runat="Server">
                    <uc2:Message ID="uxFilePermissionTestMessage" runat="server" />
                </asp:Panel>
            </div>
            <div class="ConfigRow">
                <asp:Panel ID="uxFolderListPanel" runat="server">
                    <asp:Label ID="uxFolderListLabel" runat="server" CssClass="Label"></asp:Label>
                </asp:Panel>
            </div>
            <div class="CommonConfigTitle">
            <asp:Label ID="lcStoreHeader" runat="server" meta:resourcekey="lcStoreHeader" /></div>
            <div class="ConfigRow">
                <uc12:HelpIcon ID="uxSiteEditorHelp" ConfigName="HtmlEditor" runat="server" />
                <asp:Label ID="uxSiteEditorLabel" runat="server" meta:resourcekey="lcSiteEditor"
                    CssClass="Label" />
                <asp:DropDownList ID="uxSiteEditorDrop" runat="server" CssClass="fl DropDown mgt5">
                </asp:DropDownList>
            </div>
            <asp:Panel ID="uxOrderSetupPanel" runat="server">
            <div class="CommonConfigTitle">
                <asp:Label ID="uxStartOrderSetupLabel" runat="server" meta:resourcekey="uxStartOrderSetupLabel" /></div>
                <asp:Panel ID="uxAlertMessageRow" runat="server" Visible="false" CssClass="ConfigRow">
                    <asp:Label ID="uxAlertMessageLabel" runat="server" meta:resourcekey="uxAlertMessageLabel"
                        ForeColor="Red" Visible="False"></asp:Label>
                </asp:Panel>
                <div class="ConfigRow">
                    <uc12:HelpIcon ID="uxStartOrderNoSetupHelp" HelpKeyName="StartOrderID" runat="server" />
                    <asp:Label ID="uxStartOrderNoSetupLabel" runat="server" meta:resourcekey="uxStartOrderNoSetupLabel"
                        CssClass="Label" />
                    <vevo:AdvanceButton ID="uxEditOrderNoSetupButton" runat="server" meta:resourcekey="uxEditOrderNoSetupButton"
                        CssClassBegin="fl" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxEditOrderNoSetupButton_Click"
                        OnClickGoTo="None" ValidationGroup="ValidSiteSetup"></vevo:AdvanceButton>
                    <asp:TextBox ID="uxStartOrderNoSetupText" runat="server" CssClass="TextBox" />
                </div>
            </asp:Panel>
            <asp:Panel ID="uxOrderMaximumEnabledTR" runat="server" CssClass="ConfigRow">
                <uc12:HelpIcon ID="uxOrderMaximumEnabledHelp" ConfigName="OrderMaximumEnabled" runat="server" />
                <asp:Label ID="uxOrderMaximumEnabledLabel" runat="server" meta:resourcekey="uxOrderMaximumEnabledLabel"
                    CssClass="Label" />
                <asp:DropDownList ID="uxOrderMaximumEnabledDrop" runat="server" CssClass="fl DropDown">
                    <asp:ListItem Value="True" Text="Yes" />
                    <asp:ListItem Value="False" Text="No" />
                </asp:DropDownList>
            </asp:Panel>
            <asp:Panel ID="uxOrderMaximumAmountTR" runat="server" CssClass="ConfigRow">
                <uc12:HelpIcon ID="uxOrderMaximumAmountHelp" ConfigName="OrderMaximumAmount" runat="server" />
                <asp:Label ID="uxOrderMaximumAmountLabel" runat="server" meta:resourcekey="uxOrderMaximumAmountLabel"
                    CssClass="Label" />
                <asp:TextBox ID="uxOrderMaximumAmountText" runat="Server" CssClass="TextBox" />
                <asp:CompareValidator ID="uxOrderMaximumAmountTextCompare" runat="server" ControlToValidate="uxOrderMaximumAmountText"
                    Operator="DataTypeCheck" Type="Integer" Display="Dynamic" ValidationGroup="ValidSiteSetup"
                    CssClass="CommonValidatorText">
            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number must be an Integer and greater that zero(0).
            <div class="CommonValidateDiv CommonValidateDivCuponDiscount">
            </div>
                </asp:CompareValidator>
            </asp:Panel>
            <asp:Panel ID="uxOrderMinimumEnabaledTR" runat="server" CssClass="ConfigRow">
                <uc12:HelpIcon ID="uxOrderMinimumEnabaledHelp" ConfigName="OrderMinimumEnabled" runat="server" />
                <asp:Label ID="uxOrderMinimumEnabledLabel" runat="server" meta:resourcekey="uxOrderMinimumEnabledLabel"
                    CssClass="Label" />
                <asp:DropDownList ID="uxOrderMinimumEnabledDrop" runat="server" CssClass="fl DropDown">
                    <asp:ListItem Value="True" Text="Yes" />
                    <asp:ListItem Value="False" Text="No" />
                </asp:DropDownList>
            </asp:Panel>
            <asp:Panel ID="uxOrderMinimumAmountTR" runat="server" CssClass="ConfigRow">
                <uc12:HelpIcon ID="uxOrderMinimumAmountHelp" ConfigName="OrderMinimumAmount" runat="server" />
                <asp:Label ID="uxOrderMinimumAmountLabel" runat="server" meta:resourcekey="uxOrderMinimumAmountLabel"
                    CssClass="Label" />
                <asp:TextBox ID="uxOrderMinimumAmountText" runat="Server" CssClass="TextBox" />
                <asp:CompareValidator ID="uxOrderMinimumAmountTextCompare" runat="server" ControlToValidate="uxOrderMinimumAmountText"
                    Operator="DataTypeCheck" Type="Integer" Display="Dynamic" ValidationGroup="ValidSiteSetup"
                    CssClass="CommonValidatorText">
            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number must be an Integer and greater that zero(0).
            <div class="CommonValidateDiv CommonValidateDivCuponDiscount">
            </div>
                </asp:CompareValidator>
            </asp:Panel>
            <div class="Clear">
            </div>
            <div class="CommonRowStyleButton">
                <vevo:AdvanceButton ID="uxUpdateButton" runat="server" meta:resourcekey="uxUpdateButton"
                    CssClassBegin="fr" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxUpdateButton_Click"
                    OnClickGoTo="Top" ValidationGroup="ValidSiteSetup"></vevo:AdvanceButton>
                <asp:Button ID="uxDummyButton" runat="server" Text="" CssClass="dn" />
                <ajaxToolkit:ConfirmButtonExtender ID="uxUpdateConfirmButton" runat="server" TargetControlID="uxUpdateButton"
                    ConfirmText="" DisplayModalPopupID="uxConfirmModalPopup">
                </ajaxToolkit:ConfirmButtonExtender>
                <ajaxToolkit:ModalPopupExtender ID="uxConfirmModalPopup" runat="server" TargetControlID="uxUpdateButton"
                    CancelControlID="uxCancelButton" OkControlID="uxOkButton" PopupControlID="uxConfirmPanel"
                    BackgroundCssClass="ConfirmBackground b7" DropShadow="true" RepositionMode="None">
                </ajaxToolkit:ModalPopupExtender>
                <asp:Panel ID="uxConfirmPanel" runat="server" CssClass="ConfirmPanel b6 ac pdt10"
                    SkinID="ConfirmPanel">
                    <div class="ConfirmTitle">
                        <asp:Label ID="uxConfirmLabel" runat="server" Text="<%$ Resources:OrdersMessages, UpdateStartOrderIDConfirmation %>"></asp:Label></div>
                    <div class="ConfirmButton mgt10">
                        <vevo:AdvanceButton ID="uxOkButton" runat="server" Text="OK" CssClassBegin="fl mgt10 mgb10 mgl10"
                            CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter" OnClickGoTo="Top">
                        </vevo:AdvanceButton>
                        <vevo:AdvanceButton ID="uxCancelButton" runat="server" Text="Cancel" CssClassBegin="fr mgt10 mgb10 mgr10"
                            CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter" OnClickGoTo="None">
                        </vevo:AdvanceButton>
                        <div class="Clear">
                        </div>
                    </div>
                </asp:Panel>
                <vevo:AdvanceButton ID="uxClearCacheButton" runat="server" meta:resourcekey="uxClearCacheButton"
                    CssClassBegin="fr" CssClassEnd="Button1Right"
                    CssClass="ButtonOrange"  OnClick="uxClearCacheButton_Click"
                    OnClickGoTo="Top"></vevo:AdvanceButton>
            </div>
            <div class="Clear">
            </div>
        </div>
    </ContentTemplate>
</uc1:admincontent>
