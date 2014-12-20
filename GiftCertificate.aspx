<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="GiftCertificate.aspx.cs"
    Inherits="GiftCertificatePage" Title="Gifft Certificate" %>

<%@ Register Src="~/Components/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Import Namespace="Vevo" %>
<%@ Import Namespace="Vevo.WebAppLib" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="GiftCertificate">
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
                    <asp:Literal ID="uxErrorLiteral" runat="server" Visible="false">
                        <h4>
                            You are not authorized to view this page.
                        </h4>
                    </asp:Literal>
                    <div id="uxGiftDetailTitle" runat="server" class="GiftCertificateDetailsDiv">
                        [$Detail]</div>
                    <div id="uxGiftCodeDiv" runat="server" class="GiftCertificateGifCodeDiv">
                        <div class="GiftCertificateDivInner">
                            <span class="GiftCertificateSpan">[$GiftCertificateCode]: </span>
                            <asp:TextBox ID="uxGiftCertificateCodeText" Style="width: 120px;" runat="server"
                                CssClass="GiftCertificateTextBox" />
                            <asp:LinkButton ID="uxVerifyImageButton" runat="server" Text="[$BtnVerify]" OnClick="uxVerifyImageButton_Click"
                                CssClass="GiftCertificateVerifyImageButton BtnStyle2" />
                            <div class="Clear">
                            </div>
                        </div>
                    </div>
                    <div id="uxGiftDetailDiv" runat="server" visible="false" class="GiftCertificateResultDiv">
                        <table class="GiftCertificateResultTable" border="0" cellpadding="2" cellspacing="0"
                            id="GiftCertificateTable" runat="server">
                            <tr>
                                <td class="GiftCertificateLabelColumn">
                                    [$GiftCertificateCode] :
                                </td>
                                <td class="GiftCertificateValueColumn">
                                    <asp:Label ID="uxGiftCertificateCodeLabel" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="GiftCertificateLabelColumn">
                                    [$RemainValue] :
                                </td>
                                <td class="GiftCertificateValueColumn">
                                    <asp:Label ID="uxRemainValueLabel" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="GiftCertificateLabelColumn">
                                    [$ExpireDate] :
                                </td>
                                <td class="GiftCertificateValueColumn">
                                    <asp:Label ID="uxExpireDateLabel" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="GiftCertificateLabelColumn">
                                    [$Status] :
                                </td>
                                <td class="GiftCertificateValueColumn">
                                    <asp:Label ID="uxStatusLabel" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
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
