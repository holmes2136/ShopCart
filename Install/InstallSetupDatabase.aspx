<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="InstallMasterPage.master"
    CodeFile="InstallSetupDatabase.aspx.cs" Inherits="Install_InstallSetupDatabase" %>

<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="CommonRowStyle">
        <asp:Image ID="uxStepImage" runat="server" ImageUrl="../Images/Design/Skin/Step2.gif"
            CssClass="WizardStepImg" />
    </div>
    <uc3:Message ID="uxMessage" runat="server" NumberOfNewLines="1" />
    <div class="HeaderTitle">
        <asp:Label ID="lcHeader" runat="server" Text="Database Setup" />
    </div>
    <div class="AdminBorder">
        <div class="WizardSetupDatabase">
            <div class="CommonDenoteDiv">
                <asp:Label ID="lcRequiredFieldSymbol" runat="server" meta:resourcekey="lcRequiredFieldSymbol"></asp:Label>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcDatabaseTypeLabel" runat="server" meta:resourcekey="lcDatabaseTypeLabel"
                    CssClass="Label" />
                <asp:Label ID="uxDatabaseTypeText" runat="server" Text="Microsoft SQL Server" CssClass="MsSqlLabel" />
            </div>
            <asp:Panel ID="uxAccessPanel" runat="server">
                <div class="CommonRowStyle">
                    <asp:Label ID="lcMSAccessPath" runat="server" meta:resourcekey="lcMSAccessPath" CssClass="Label" />
                    <asp:TextBox ID="uxMSAccessPathText" runat="server" CssClass="TextBox" MaxLength="255" />
                    <div class="validator1 fl">
                        <span class="Asterisk">*</span>
                        <asp:RequiredFieldValidator ID="uxMSAccessPathTextValid" runat="server" ControlToValidate="uxMSAccessPathText"
                            Display="Dynamic" meta:resourcekey="uxMSAccessPathTextValid" ValidationGroup="SetupDatabaseAccess"><--</asp:RequiredFieldValidator>
                    </div>
                    <div class="CommonRowStyle">
                        <div class="Label">
                            &nbsp;
                        </div>
                        <div class="denote fl">
                            Example: ~/App_Data/Vevocart.mdb</div>
                    </div>
                </div>
                <div class="CommonRowStyle ar" style="padding-top: 20px;">
                    <asp:LinkButton ID="uxNextAccessButton" runat="server" CssClass="InstallBtnOrange"
                        meta:resourcekey="uxNextButton" OnClick="uxNextAccessButton_Click" ValidationGroup="SetupDatabaseAccess" />
                    <div class="Clear">
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="uxSQLServerlPanel" runat="server" Visible="false">
                <div class="CommonRowStyle">
                    <asp:Label ID="lcServerName" runat="server" meta:resourcekey="lcServerName" CssClass="Label" />
                    <asp:TextBox ID="uxServerNameText" runat="server" CssClass="TextBox" MaxLength="128" />
                    <span class="CommonAsterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxRequireServerNameValid" runat="server" ControlToValidate="uxServerNameText"
                        Display="Dynamic" meta:resourcekey="uxRequireServerNameValid" ValidationGroup="SetupDatabase"
                        CssClass="CommonValidatorText SetupDatabaseValidatorText">
                        <div class="CommonValidateDiv SetupDatabaseValidatorDiv"></div><img src="../Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Server Name
                    </asp:RequiredFieldValidator>
                </div>
                <div class="CommonRowStyle">
                    <asp:Label ID="lcDatabaseName" runat="server" meta:resourcekey="lcDatabaseName" CssClass="Label" />
                    <asp:TextBox ID="uxDatabaseNameText" runat="server" CssClass="TextBox" MaxLength="128" />
                    <span class="CommonAsterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxRequiredDatabaseNameValid" runat="server" ControlToValidate="uxDatabaseNameText"
                        Display="Dynamic" meta:resourcekey="uxRequiredDatabaseNameValid" ValidationGroup="SetupDatabase"
                        CssClass="CommonValidatorText SetupDatabaseValidatorText">
                        <div class="CommonValidateDiv SetupDatabaseValidatorDiv"></div><img src="../Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Database Name
                    </asp:RequiredFieldValidator>
                </div>
                <div class="CommonRowStyle">
                    <asp:Label ID="lcAuthentication" runat="server" meta:resourcekey="lcAuthentication"
                        CssClass="Label" />
                    <asp:DropDownList ID="uxIsSQLAuthenticationDrop" runat="server" CssClass="fl DropDown"
                        OnSelectedIndexChanged="uxIsSQLAuthenticationDrop_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Value="True">SQL Server Authentication</asp:ListItem>
                        <asp:ListItem Value="False">Windows Authentication</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <asp:Panel ID="uxLoginDetailPanel" runat="server">
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcDatabaseUserName" runat="server" meta:resourcekey="lcDatabaseUserName"
                            CssClass="BulletLabel" />
                        <asp:TextBox ID="uxDatabaseUserNameText" runat="server" CssClass="TextBox" MaxLength="128" />
                        <span class="CommonAsterisk">*</span>
                        <asp:RequiredFieldValidator ID="uxRequiredDatabaseUserNameValid" runat="server" ControlToValidate="uxDatabaseUserNameText"
                            Display="Dynamic" meta:resourcekey="uxRequiredDatabaseUserNameValid" ValidationGroup="SetupDatabase"
                            CssClass="CommonValidatorText SetupDatabaseValidatorText">
                            <div class="CommonValidateDiv SetupDatabaseValidatorDiv"></div><img src="../Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Database Username
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcDatabasePassWord" runat="server" meta:resourcekey="lcDatabasePassWord"
                            CssClass="BulletLabel" />
                        <asp:TextBox ID="uxDatabasePassWordText" runat="server" TextMode="Password" CssClass="TextBox"
                            MaxLength="128" />
                        <span class="CommonAsterisk">*</span>
                        <asp:RequiredFieldValidator ID="uxRequiredDatabasePassWordValid" runat="server" ControlToValidate="uxDatabasePassWordText"
                            Display="Dynamic" meta:resourcekey="uxRequiredDatabasePassWordValid" ValidationGroup="SetupDatabase"
                            CssClass="CommonValidatorText SetupDatabaseValidatorText">
                            <div class="CommonValidateDiv SetupDatabaseValidatorDiv"></div><img src="../Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Database Password
                        </asp:RequiredFieldValidator>
                    </div>
                </asp:Panel>
                <div class="CommonRowStyle">
                    <asp:Label ID="lcCreateDatabaseLabel" runat="server" meta:resourcekey="lcCreateDatabaseLabel"
                        CssClass="Label" />
                    <div class="RadioList">
                        <asp:RadioButtonList ID="uxCreateDatabaseRadioButton" runat="server">
                            <asp:ListItem Value="Create" meta:resourcekey="CreateDatabaseNew" />
                            <asp:ListItem Value="Populate" meta:resourcekey="CreateDatabaseEmpty" />
                            <asp:ListItem Value="Connect" meta:resourcekey="CreateDatabaseOld" />
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="CommonRowStyle fr pdt10">
                    <asp:LinkButton ID="uxNextButton" runat="server" CssClass="InstallBtnOrange" meta:resourcekey="uxNextButton"
                        OnClick="uxNextButton_Click" ValidationGroup="SetupDatabase" />
                </div>
                <div class="Clear">
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
