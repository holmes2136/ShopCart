<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Newsletter.ascx.cs" Inherits="Components_Newsletter" %>
<div class="Newsletter">
    <div class="SidebarTop">
        <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/NewsLetterTopLeft.gif" runat="server"
            CssClass="SidebarTopImgLeft" />
        <asp:Label ID="uxNewsLetterTitle" runat="server" Text="[$Email]" CssClass="SidebarTopTitle"></asp:Label>
        <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/NewsLetterTopRight.gif"
            runat="server" CssClass="SidebarTopImgRight" />
        <div class="Clear">
        </div>
    </div>
    <div class="SidebarLeft">
        <div class="SidebarRight">
            <asp:Label ID="uxEmailDescription" runat="server" Text="[$EmailDescription]" CssClass="NewsletterLabel"></asp:Label>
            <div class="NewsletterForm">
                <asp:TextBox ID="uxEmailSubscribeText" runat="server" MaxLength="50" CssClass="NewsletterTextBox"></asp:TextBox>
                <asp:LinkButton ID="uxEmailImageButton" runat="server" Text="[$BtnSubscribe]" OnClick="uxEmailImageButton_Click"
                    ValidationGroup="ValidSubscribe" CssClass="NewsletterSubmit BtnStyle3" />
                <asp:RegularExpressionValidator ID="uxRegularEmailValidator" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    ControlToValidate="uxEmailSubscribeText" ValidationGroup="ValidSubscribe" Display="Dynamic"
                    CssClass="CommonValidatorText CommonValidatorTextNewsLetter">
                    <div class="CommonValidateDiv CommonValidateDivNewsLetter"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Wrong Email Format
                </asp:RegularExpressionValidator>
            </div>
            <div class="Clear">
            </div>
        </div>
    </div>
    <div class="SidebarBottom">
        <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/NewsLetterBottomLeft.gif"
            runat="server" CssClass="SidebarBottomImgLeft" />
        <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/NewsLetterBottomRight.gif"
            runat="server" CssClass="SidebarBottomImgRight" />
        <div class="Clear">
        </div>
    </div>
</div>
