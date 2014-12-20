<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="ContactUsFinished.aspx.cs"
    Inherits="ContactUsFinished" Title="[$Title]" %>

<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="ContactUsFinished">
        <div class="CommonPage">
            <div class="CommonPageTop">
                <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/DefaultBoxTopLeft.gif" runat="server"
                    CssClass="CommonPageTopImgLeft" />
                <asp:Label ID="uxDefaultTitle" runat="server" CssClass="CommonPageTopTitle">[$Thank you]</asp:Label>
                <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/DefaultBoxTopRight.gif"
                    runat="server" CssClass="CommonPageTopImgRight" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonPageLeft">
                <div class="CommonPageRight">
                    <div class="ContactUsFinishedContent">
                         <div class="MessageBold">
                            [$Thank you]</div>
                        <div class="MessageNormal">
                            [$GetBack]</div>
                    </div>
                    <div class="Clear">
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
    </div>
</asp:Content>
