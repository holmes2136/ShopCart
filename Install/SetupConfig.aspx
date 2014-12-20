<%@ Page Language="C#" MasterPageFile="InstallMasterPage.master" AutoEventWireup="true"
    CodeFile="SetupConfig.aspx.cs" Inherits="Install_SetupConfig" ValidateRequest="false" %>

<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <asp:Timer ID="uxTimer" runat="server" Interval="2000" OnTick="uxTimer_Tick">
    </asp:Timer>
    <div class="CommonRowStyle">
        <asp:Image ID="uxStepImage" runat="server" ImageUrl="../Images/Design/Skin/Step3.gif"
            CssClass="WizardStepImg" />
    </div>
    <uc3:Message ID="uxMessage" runat="server" NumberOfNewLines="1" />
    <div class="HeaderTitle">
        <asp:Label ID="lcHeader" runat="server" Text="Store Initialization" />
    </div>
    <div class="WizardSetupConfig">
        <div class="Title1">
            <asp:Label ID="uxYourStoreLabel" runat="server" meta:resourcekey="uxYourStoreLabel" />
        </div>
        <div class="Title2">
            <asp:Label ID="uxInitializingLabel" runat="server" meta:resourcekey="uxInitializingLabel" />
        </div>
        <img src="../App_Themes/Install/Images/install loop.gif" alt="loading" />
    </div>
</asp:Content>
