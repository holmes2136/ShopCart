<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StoreFacebookConfig.ascx.cs"
    Inherits="Admin_Components_StoreConfig_StoreFacebookConfig" %>
<%@ Register Src="../Common/HelpIcon.ascx" TagName="HelpIcon" TagPrefix="uc1" %>
<%@ Register Src="../SiteConfig/WidgetDetails.ascx" TagName="WidgetConfig" TagPrefix="uc2" %>
<div class="CommonConfigTitle  mgt0">
<asp:Label ID="uxWidgetLikeBoxTitle" runat="server" meta:resourcekey="uxWidgetLikeBoxLabel"/></div>
<uc2:WidgetConfig ID="uxWidgetLikeBoxConfig" WidgetStyle="LikeBox" ParameterName="Fanpage URL"
    ValidationGroup="SiteConfigValid" runat="server" />
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxEnableFacebookLoginHelp" ConfigName="FacebookLoginEnabled" runat="server" />
    <div class="Label">
        <asp:Label ID="uxEnableFacebookLoginLabel" runat="server" meta:resourcekey="uxEnableFacebookLoginLabel"
            CssClass="fl">
        </asp:Label>
    </div>
    <asp:DropDownList ID="uxEnableFacebookLoginDrop" runat="server" CssClass="fl DropDown"
        AutoPostBack="true">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
<asp:Panel ID="uxFacebookParameter" runat="server">
    <div class="ConfigRow">
        <uc1:HelpIcon ID="uxFacebookAPIKeyHelp" ConfigName="FacebookAPIKey" runat="server" />
        <div class="BulletLabel">
            <asp:Label ID="uxFacebookAPIKeyLabel" runat="server" meta:resourcekey="uxFacebookAPIKeyLabel"
                CssClass="fl" />
        </div>
        <asp:TextBox ID="uxFacebookAPIKeyTextbox" runat="server" CssClass="TextBox" />
        <div class="validator1 fl">
            <span class="Asterisk">*</span>
        </div>
        <asp:RequiredFieldValidator ID="uxFacebookAPIKeyRequiredValidator" runat="server"
            ValidationGroup="SiteConfigValid" ControlToValidate="uxFacebookAPIKeyTextbox"
            Display="Dynamic" CssClass="CommonValidatorText">
            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> App ID/API key is required.
            <div class="CommonValidateDiv">
            </div>
        </asp:RequiredFieldValidator>
    </div>
    <div class="ConfigRow">
        <uc1:HelpIcon ID="uxFacebookTabAppSecretHelp" ConfigName="FacebookAppSecret" runat="server" />
        <div class="BulletLabel">
            <asp:Label ID="uxFacebookAppSecretLabel" runat="server" meta:resourcekey="uxFacebookAppSecretLabel"
                CssClass="fl" />
        </div>
        <asp:TextBox ID="uxFacebookAppSecretTextbox" runat="server" CssClass="TextBox" />
        <div class="validator1 fl">
            <span class="Asterisk">*</span>
        </div>
        <asp:RequiredFieldValidator ID="uxFacebookAppSecretRequiredValidator" runat="server"
            ValidationGroup="SiteConfigValid" ControlToValidate="uxFacebookAppSecretTextbox"
            Display="Dynamic" CssClass="CommonValidatorText">
            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> App Secret is required.
            <div class="CommonValidateDiv">
            </div>
        </asp:RequiredFieldValidator>
    </div>
    <div class="ConfigRow">
        <uc1:HelpIcon ID="uxFacebookAccessTokenHelp" ConfigName="FacebookAccessToken" runat="server"
            Visible="false" />
        <div class="BulletLabel">
            <asp:Label ID="uxFacebookAccessTokenLabel" runat="server" meta:resourcekey="uxFacebookAccessTokenLabel"
                CssClass="fl" Visible="false" />
        </div>
        <asp:TextBox ID="uxFacebookAccessTokenTextbox" runat="server" CssClass="TextBox"
            Visible="false" />
    </div>
</asp:Panel>
