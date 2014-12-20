<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MerchantNote.ascx.cs"
    Inherits="AdminAdvanced_Components_MerchantNote" %>
<%@ Register Src="../Components/Template/AdminUserControlContent.ascx" TagName="AdminUserControlContent"
    TagPrefix="uc1" %>
<uc1:AdminUserControlContent ID="uxAdminUserControlContent" runat="server">
    <ContentTemplate>
        <div class="CommonTextTitle1 mgb10">
            <asp:Label ID="lcHeader" runat="server" meta:resourcekey="lcHeader"></asp:Label>
        </div>
        <div class="RequiredLabel">
            <asp:Label ID="uxErrorLabel" runat="server" ForeColor="Red" /></div>
        <div class="CommonRowStyle">
            <asp:Label ID="lcOrderID" runat="server" meta:resourcekey="lcOrderID" CssClass="Label" />
            <asp:Label ID="uxOrderIDLabel" runat="server" CssClass="fl" />
        </div>
        <div class="CommonRowStyle">
            <asp:Label ID="lcUsername" runat="server" meta:resourcekey="lcUsername" CssClass="Label" />
            <asp:TextBox ID="uxNameText" runat="server" Width="450px" CssClass="TextBox"></asp:TextBox>
        </div>
        <div class="CommonRowStyle">
            <asp:Label ID="lcNote" runat="server" meta:resourcekey="lcNote" CssClass="Label" />
            <asp:TextBox ID="uxNoteText" runat="server" Height="100px" TextMode="MultiLine" Width="450px"
                CssClass="TextBox" />
        </div>
        <div class="Clear" />
        <div class="mg10 fr">
            <vevo:AdvanceButton ID="uxSaveNoteButton" runat="server" meta:resourcekey="uxSaveNoteButton"
                CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                OnClick="uxSaveNoteButton_Click" OnClickGoTo="Top" />
        </div>
    </ContentTemplate>
</uc1:AdminUserControlContent>
