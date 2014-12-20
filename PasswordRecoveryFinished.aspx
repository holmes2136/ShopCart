<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" Inherits="Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage"
    Title="[$Title]" %>

<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="PasswordRecoveryFinished">
        <div class="CommonPage">
            <div class="CommonPageTop">
                <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/DefaultBoxTopLeft.gif" runat="server"
                    CssClass="CommonPageTopImgLeft" />
                <asp:Label ID="uxDefaultTitle" runat="server" CssClass="CommonPageTopTitle">[$Header]</asp:Label>
                <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/DefaultBoxTopRight.gif"
                    runat="server" CssClass="CommonPageTopImgRight" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonPageLeft">
                <div class="CommonPageRight">
                    <div class="PasswordRecoveryFinishedContent">
                        <p class="PasswordRecoveryFinishedParagraph">
                            <div class="PasswordRecoveryText">
                                [$Text]
                            </div>
                        </p>
                        <p class="PasswordRecoveryFinishedParagraph">
                            <asp:HyperLink ID="uxLoginHyperLink" runat="server" NavigateUrl="UserLogin.aspx"
                                CssClass="CommonHyperLink">Return to log in page</asp:HyperLink>
                        </p>
                    </div>
                    <div class="Clear">
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
