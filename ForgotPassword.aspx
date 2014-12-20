<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs"
    Inherits="ForgotPassword" Title="[$Title]" %>

<%@ Register Src="Components/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="ForgotPassword">
        <uc1:Message ID="uxMessage" runat="server" NumberOfNewLines="1" />
        <div class="CommonPage">
            <div class="CommonPageTop">
                <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/DefaultBoxTopLeft.gif" runat="server"
                    CssClass="CommonPageTopImgLeft" />
                <asp:Label ID="uxDefaultTitle" runat="server" CssClass="CommonPageTopTitle">[$Password Title]</asp:Label>
                <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/DefaultBoxTopRight.gif"
                    runat="server" CssClass="CommonPageTopImgRight" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonPageLeft">
                <div class="CommonPageRight">
                    <div class="ForgotPasswordDiv">
                        <div class="MessageNormal">
                            [$Message1]</div>
                        <div class="ForgotPasswordPanel">
                            <div class="ForgotPasswordLabel">
                                [$Username]</div>
                            <div class="ForgotPasswordData">
                                <asp:TextBox ID="uxUserNameText" runat="server" CssClass="ForgotPasswordTextBox"
                                    ValidationGroup="ForgotPassword" />
                            </div>
                            <asp:RequiredFieldValidator ID="uxUsernameValidate" runat="server" ControlToValidate="uxUserNameText"
                                Display="Dynamic" ValidationGroup="ForgotPassword" CssClass="CommonValidatorText ForgotPasswordValidatorText">
                                <div class="CommonValidateDiv ForgotPasswordValidateDiv"></div>
                                <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> [$RequireUsername]
                            </asp:RequiredFieldValidator>
                        </div>
                        <asp:Button ID="uxSubmitButton" runat="server" OnClick="uxSubmitButton_Click" Text="Submit"
                            Visible="false" CssClass="ForgotPasswordSubmitButton" />
                        <asp:LinkButton ID="uxLoginImageButton" OnClick="uxSubmitButton_Click" runat="server"
                            Text="[$BtnSubmit]" ValidationGroup="ForgotPassword" CssClass="ForgotPasswordLoginImageButton BtnStyle1" />
                    </div>
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
