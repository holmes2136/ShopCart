<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="Newsletter.aspx.cs"
    Inherits="Newsletter" Title="[$Title]" %>

<%@ Register Src="Components/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="NewsletterPage">
        <uc1:Message ID="uxMessage" runat="server" NumberOfNewLines="1" />
        <div class="CommonPage">
            <div class="CommonPageTop">
                <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/DefaultBoxTopLeft.gif" runat="server"
                    CssClass="CommonPageTopImgLeft" />
                <asp:Label ID="uxDefaultTitle" runat="server" CssClass="CommonPageTopTitle">[$Head]</asp:Label>
                <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/DefaultBoxTopRight.gif"
                    runat="server" CssClass="CommonPageTopImgRight" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonPageLeft">
                <div class="CommonPageRight">
                    <table border="0" cellpadding="4" cellspacing="4" class="NewsletterPageTable">
                        <tr id="uxNewsletterMessageTR" runat="server">
                            <td>
                                <div class="NewsletterPageMsg" id="EmailDiv" runat="server">
                                    [$YourEmailAddress]
                                    <asp:Label ID="uxEmailLabel" runat="server" />
                                </div>
                                <div class="NewsletterPageMsg">
                                    <asp:Label ID="uxSubscribeLabel" runat="server" Text=""></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr id="uxNewsletterSubScribeTR" runat="server">
                            <td class="NewsletterPageTableTD">
                                <div class="NewsletterDiv">
                                    <div class="MessageNormal">
                                        [$Message1]</div>
                                    <div class="NewsletterPanel">
                                        <div class="NewsletterPageLabel">
                                            [$EmailDescription]</div>
                                        <div class="NewsletterPageData">
                                            <asp:TextBox ID="uxEmailSubscribeText" runat="server" CssClass="NewsletterPageTextBox"
                                                ValidationGroup="ValidNewsletter" />
                                            <asp:RequiredFieldValidator ID="uxRequiredEmailValidator" runat="server" ControlToValidate="uxEmailSubscribeText"
                                                ValidationGroup="ValidNewsletter" Display="Dynamic" CssClass="CommonValidatorText"> <div class="CommonValidateDiv"></div>
                                                <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> [$Required Email Address] </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="uxRegularEmailValidator" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                ControlToValidate="uxEmailSubscribeText" ValidationGroup="ValidNewsletter" Display="Dynamic"
                                                CssClass="CommonValidatorText"> <div class="CommonValidateDiv"></div>
                                                <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> [$Wrong Email Format] </asp:RegularExpressionValidator></div>
                                    </div>
                                    <asp:LinkButton ID="uxNewsletterImageButton" runat="server" Text="[$BtnSubscribe]" OnClick="uxNewsletterImageButton_Click"
                                        ValidationGroup="ValidNewsletter" CssClass="BtnStyle1 NewsletterLoginImageButton" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="CommonPageBottom">
                <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/DefaultBoxBottomLeft.gif"
                    runat="server" CssClass="CommonPageBottomImgLeft" />
                <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/DefaultBoxBottomRight.gif"
                    runat="server" CssClass="CommonPageBottomImgRight" />
                <div class="Clear">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
