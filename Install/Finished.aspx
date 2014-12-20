<%@ Page Language="C#" MasterPageFile="InstallMasterPage.master" AutoEventWireup="true"
    CodeFile="Finished.aspx.cs" Inherits="Install_Finish" %>

<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="CommonRowStyle">
        <asp:Image ID="uxStepImage" runat="server" ImageUrl="../Images/Design/Skin/Step4.gif"
            CssClass="WizardStepImg" />
    </div>
    <div class="HeaderTitle">
        <asp:Label ID="lcHeader" runat="server" meta:resourcekey="lcHeader" />
    </div>
    <div class="WizardFinish">
        <div class="Title1">
            <asp:Label ID="uxInstallCompleteLabel" runat="server" meta:resourcekey="uxInstallCompleteLabel" />
        </div>
        <div class="Title2">
            <asp:Label ID="uxVisitStoreLabel" runat="server" meta:resourcekey="uxVisitStoreLabel" />
        </div>
    </div>
    <div class="WizardFinishButtonDiv">
        <asp:HyperLink ID="uxRedirectButton" runat="server" NavigateUrl="../default.aspx"
            CssClass="InstallFinBtnOrange" meta:resourcekey="uxRedirectButton" Target="_blank"></asp:HyperLink>
        <asp:HyperLink ID="uxSetupStoreButton" runat="server" NavigateUrl="../Admin/default.aspx"
            CssClass="InstallFinBtnBlue" meta:resourcekey="uxSetupStoreButton" Target="_blank"></asp:HyperLink>
    </div>
</asp:Content>
