<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PermissionCheck.aspx.cs"
    Inherits="PermissionCheck_Install" MasterPageFile="InstallMasterPage.master" %>

<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="CommonRowStyle">
        <asp:Image ID="uxStepImage" runat="server" ImageUrl="../Images/Design/Skin/Step1.gif"
            CssClass="WizardStepImg" />
    </div>
    <uc1:Message ID="uxFilePermissionTestMessage" runat="server" />
    <div class="HeaderTitle">
        <asp:Label ID="lcHeader" runat="server" Text="Permission verification" />
    </div>
    <div class="WizardPermissionVerifyRow">
        Please click "Verify" button to verify the permissions of the following folders.
    </div>
    <div class="AdminBorder">
        <div class="CommonGridviewDiv">
            <asp:DataGrid ID="uxPermissionGrid" runat="server" AutoGenerateColumns="false" CssClass="Gridview1"
                GridLines="none" CellPadding="0" CellSpacing="1">
                <Columns>
                    <asp:TemplateColumn HeaderText="Folder">
                        <HeaderStyle HorizontalAlign="Left" CssClass="PermissionGridHeadStyle pdl15" />
                        <ItemStyle Width="40%" HorizontalAlign="left" CssClass="DefaultGridRowStyle pdl15" />
                        <ItemTemplate>
                            <asp:Label ID="uxFolderPath" runat="server" Text='<%# Eval("Folder").ToString() %>' />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="List Folder Contents & Write Permission">
                        <HeaderStyle HorizontalAlign="center" CssClass="PermissionGridHeadStyle" />
                        <ItemStyle Width="60%" HorizontalAlign="center" CssClass="DefaultGridRowStyle" />
                        <ItemTemplate>
                            <asp:Image ID="uxPermissionImage" runat="server" ImageUrl='<%# GetPermissionImage(Eval("Permission")) %>' />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                <HeaderStyle CssClass="DefaultGridHeadStyle" />
                <FooterStyle CssClass="DefaultGridFooterStyle" />
                <ItemStyle CssClass="" />
            </asp:DataGrid>
        </div>
        <div class="CommonRowStyle pdt10 fr">
            <asp:LinkButton ID="uxVerifyButton" runat="server" OnClick="uxVerifyButton_Click"
                CssClass="InstallBtnGray" meta:resourcekey="uxVerifyButton" />
            <asp:LinkButton ID="uxNextButton" runat="server" OnClick="uxNextButton_Click" CssClass="InstallBtnOrange"
                meta:resourcekey="uxNextButton" Enabled="false" />
        </div>
        <div class="Clear" />
    </div>
</asp:Content>
