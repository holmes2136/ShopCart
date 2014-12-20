<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminConfig.ascx.cs" Inherits="AdminAdvanced_MainControls_AdminConfig" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc2" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <MessageTemplate>
        <uc2:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <ContentTemplate>
        <asp:Panel ID="uxAdminConfigPanel" runat="server" CssClass="Container-Row">
            <div class="CommonConfigTitle mgt0">
                <asp:Label ID="lcSystemHeader" runat="server" meta:resourcekey="lcSystemHeader" />
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="lcEnableErrorLogEmail" runat="server" meta:resourcekey="lcEnableErrorLogEmail" />
                </div>
                <asp:DropDownList ID="uxEnableErrorLogEmailDrop" runat="server" CssClass="fl DropDown">
                    <asp:ListItem Value="True" Text="Yes" />
                    <asp:ListItem Value="False" Text="No" />
                </asp:DropDownList>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="lcErrorLogEmail" runat="server" meta:resourcekey="lcErrorLogEmail" />
                </div>
                <asp:TextBox ID="uxErrorLogEmailText" runat="server" CssClass="fl TextBox" />
                <div class="Clear">
                </div>
            </div>
            <div>
                <vevo:AdvanceButton ID="uxUpdateButton" runat="server" meta:resourcekey="uxUpdateButton" CssClassBegin="fr"
                    CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxUpdateButton_Click"
                    OnClickGoTo="Top" ValidationGroup="AdminConfigValid"></vevo:AdvanceButton>
            </div>
            <div class="Clear" />
        </asp:Panel>
    </ContentTemplate>
</uc1:AdminContent>
