<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustomerTracking.ascx.cs"
    Inherits="AdminAdvanced_Components_CustomerTracking" %>
<%@ Register Src="../Components/Template/AdminUserControlContent.ascx" TagName="AdminUserControlContent"
    TagPrefix="uc1" %>
<uc1:AdminUserControlContent ID="uxAdminUserControlContent" runat="server">
    <ContentTemplate>
        <div class="CommonTextTitle1 mgb10">
            <asp:Label ID="lcHeader" runat="server" meta:resourcekey="lcHeader"></asp:Label>
        </div>
        <div class="CommonRowStyle">
            <asp:Label ID="lcOrderID" runat="server" meta:resourcekey="lcOrderID" CssClass="Label" />
            <asp:Label ID="uxOrderIDLabel" runat="server" CssClass="fl" />
        </div>
        <div class="CommonRowStyle">
            <asp:Label ID="lcCustomerEmail" runat="server" meta:resourcekey="lcCustomerEmail"
                CssClass="Label" />
            <asp:Label ID="uxCustomerEmailLabel" runat="server" CssClass="fl" />
        </div>
        <div class="CommonRowStyle">
            <asp:Label ID="lcSenderName" runat="server" meta:resourcekey="lcSenderName" CssClass="Label" />
            <asp:TextBox ID="uxSenderNameText" runat="server" Width="450px" CssClass="TextBox"></asp:TextBox>
        </div>
        <div class="CommonRowStyle">
            <asp:Label ID="lxSenderEmail" runat="server" meta:resourcekey="lxSenderEmail" CssClass="Label" />
            <asp:TextBox ID="uxSenderEmailText" runat="server" Width="450px" CssClass="TextBox"></asp:TextBox>
        </div>
        <div class="CommonRowStyle">
            <asp:Label ID="lcMessage" runat="server" meta:resourcekey="lcMessage" CssClass="Label" />
            <asp:TextBox ID="uxMessageText" runat="server" Height="100px" TextMode="MultiLine"
                Width="450px" CssClass="TextBox"></asp:TextBox>
        </div>
        <div class="Clear fr mg10">
            <vevo:AdvanceButton ID="uxSendButton" runat="server" meta:resourcekey="uxSendButton"
                CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                OnClick="uxSendButton_Click" OnClickGoTo="Top" />
            <div class="Clear">
            </div>
        </div>
    </ContentTemplate>
</uc1:AdminUserControlContent>
