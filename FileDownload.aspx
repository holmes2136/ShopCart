<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="FileDownload.aspx.cs"
    Inherits="FileDownload" Title="Untitled Page" %>

<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="FileDownload">
        <div class="CommonPage">
            <div class="CommonPageTop">
                <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/DefaultBoxTopLeft.gif" runat="server"
                    CssClass="CommonPageTopImgLeft" />
                <asp:Label ID="uxDefaultTitle" runat="server" CssClass="CommonPageTopTitle">[$DownloadFailed] </asp:Label>
                <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/DefaultBoxTopRight.gif"
                    runat="server" CssClass="CommonPageTopImgRight" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonPageLeft">
                <div class="CommonPageRight">
                    <div class="FileDownloadErrorContent">
                        <asp:Label ID="uxMessageLabel" runat="server"></asp:Label></div>
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
