<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ContentMenuItemsDetails.ascx.cs"
    Inherits="AdminAdvanced_Components_ContentMenuItemsDetails" %>
<%@ Register Src="LanguageControl.ascx" TagName="LanguageControl" TagPrefix="uc4" %>
<%@ Register Src="Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Src="Template/AdminContent.ascx" TagName="AdminContent" TagPrefix="uc2" %>
<%@ Register Src="../Components/LanguageLabelPlus.ascx" TagName="LanaguageLabelPlus"
    TagPrefix="uc5" %>
<%@ Register Src="ContentMenuStructureDrop.ascx" TagName="ContentMenuStructureDrop"
    TagPrefix="uc6" %>
<uc2:AdminContent ID="AdminContent" runat="server">
    <MessageTemplate>
        <uc3:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <ValidationSummaryTemplate>
        <asp:ValidationSummary ID="uxValidationSummary" runat="server" meta:resourcekey="uxValidationSummary"
            ValidationGroup="VaildContentMenuItem" CssClass="ValidationStyle" />
    </ValidationSummaryTemplate>
    <ValidationDenotesTemplate>
        <div class="topline">
            <div class="RequiredLabel c6">
                <span class="Asterisk">*</span>
                <asp:Label ID="lcRequiredFieldSymbol" runat="server" meta:resourcekey="lcRequiredFieldSymbol" /></div>
        </div>
    </ValidationDenotesTemplate>
    <LanguageControlTemplate>
        <uc4:LanguageControl ID="uxLanguageControl" runat="server" ShowTitle="true" />
    </LanguageControlTemplate>
    <ContentTemplate>
        <div class="Container-Box">
            <div class="CommonTextTitle1 mgb10">
                <asp:Label ID="uxBreadcrumbLabel" runat="server"></asp:Label>
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="uxContentMenuItemNameLabel" runat="server" meta:resourcekey="uxContentMenuItemNameLabel"></asp:Label></div>
                <asp:TextBox ID="uxContentMenuItemNameText" runat="server" Width="250px" CssClass="TextBox fl"
                    ValidationGroup="VaildContentMenuItem">
                </asp:TextBox>
                <div class="pluslabel1 fl">
                    <uc5:LanaguageLabelPlus ID="uxPlus1" runat="server" />
                </div>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="uxNameRequiredValidator" runat="server" ControlToValidate="uxContentMenuItemNameText"
                    ValidationGroup="VaildContentMenuItem" CssClass="CommonValidatorText" Display="Dynamic">
                            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Name is required.
                            <div class="CommonValidateDiv CommonValidateDivContentMenuLong">
                            </div>
                </asp:RequiredFieldValidator>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="uxContentMenuItemDescriptionLabel" runat="server" meta:resourcekey="uxContentMenuItemDescriptionLabel"></asp:Label></div>
                <asp:TextBox ID="uxContentMenuItemDescriptionText" runat="server" Height="70px" TextMode="MultiLine"
                    Width="250px" CssClass="fl TextBox" />
                <div class="pluslabel1 fl">
                    <uc5:LanaguageLabelPlus ID="LanaguageLabelPlus1" runat="server" />
                </div>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="uxParentLabel" runat="server" meta:resourcekey="uxParentLabel" /></div>
                <uc6:ContentMenuStructureDrop ID="uxParentDrop" runat="server" Width="250px" CssClass="fl DropDown" />
                <div class="Clear">
                </div>
            </div>
            <asp:Panel ID="uxItemTypePanel" runat="server" CssClass="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="uxContentMenuItemTypeLabel" runat="server" meta:resourcekey="uxContentMenuItemTypeLabel">
                    </asp:Label></div>
                <asp:DropDownList ID="uxContentMenuItemTypeDrop" runat="server" OnSelectedIndexChanged="uxContentMenuItemTypeDrop_SelectedIndexChanged"
                    AutoPostBack="true" CssClass="fl DropDown">
                    <asp:ListItem Value="0" Text="Sub Menu" />
                    <asp:ListItem Value="1" Text="Link to Content" />
                </asp:DropDownList>
                <div class="Clear">
                </div>
            </asp:Panel>
            <asp:Panel ID="uxContentListPanel" runat="server" CssClass="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="uxContentMenuItemContentListLabel" runat="server" meta:resourcekey="uxContentMenuItemContentListLabel">
                    </asp:Label></div>
                <asp:DropDownList ID="uxContentListDrop" runat="server" DataTextField="Title" DataValueField="ContentID"
                    CssClass="fl DropDown" AppendDataBoundItems="true" Width="250px">
                </asp:DropDownList>
            </asp:Panel>
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="uxContentMenuItemEnabledLabel" runat="server" meta:resourcekey="uxContentMenuItemEnabledLabel"></asp:Label></div>
                <asp:CheckBox ID="uxContentMenuItemEnabledCheck" runat="server" CssClass="fl" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="uxOther1Label" runat="server" meta:resourcekey="uxOther1Label"></asp:Label></div>
                <asp:TextBox ID="uxOther1Text" runat="server" Width="250px" CssClass="TextBox fl"></asp:TextBox>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="uxOther2Label" runat="server" meta:resourcekey="uxOther2Label"></asp:Label></div>
                <asp:TextBox ID="uxOther2Text" runat="server" Width="250px" CssClass="TextBox fl"></asp:TextBox>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="uxOther3Label" runat="server" meta:resourcekey="uxOther3Label"></asp:Label></div>
                <asp:TextBox ID="uxOther3Text" runat="server" Width="250px" CssClass="TextBox fl"></asp:TextBox>
                <div class="Clear">
                </div>
            </div>
            <div class="mgt10">
                <vevo:AdvanceButton ID="uxAddButton" runat="server" meta:resourcekey="uxAddButton"
                    CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    OnClick="uxAddButton_Click" OnClickGoTo="Top" ValidationGroup="VaildContentMenuItem">
                </vevo:AdvanceButton>
                <vevo:AdvanceButton ID="uxEditButton" runat="server" meta:resourcekey="uxEditButton"
                    CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    OnClick="uxEditButton_Click" OnClickGoTo="Top" ValidationGroup="VaildContentMenuItem">
                </vevo:AdvanceButton>
                <div class="Clear" />
            </div>
        </div>
    </ContentTemplate>
</uc2:AdminContent>
