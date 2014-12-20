<%@ Control Language="C#" AutoEventWireup="true" CodeFile="IntroductionMessage.ascx.cs"
    Inherits="Components_IntroductionMessage" %>
<asp:Panel ID="uxIntroductionMessagePanel" runat="server" CssClass="IntroductionMessagePanel" Visible="false">
    <div class="CenterBlockTop">
        <asp:Label ID="uxWelcomeText" runat="server" CssClass="CenterBlockTopTitle SecondaryColor"
            Text="[$Welcome]" />
        <asp:Label ID="uxIntroductionMessageTitle" runat="server" CssClass="CenterBlockTopTitle" />
    </div>
    <div class="CenterBlockLeft">
        <div class="CenterBlockRight">
            <div class="IntroductionMessage">
                <asp:Literal ID="uxIntroductionMessageText" runat="server" Mode="PassThrough" /></div>
        </div>
    </div>
</asp:Panel>
