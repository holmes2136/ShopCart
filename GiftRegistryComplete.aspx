<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="GiftRegistryComplete.aspx.cs"
    Inherits="GiftRegistryComplete" Title="[$GiftRegistry]" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="GiftRegistryComplete">
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
                    <div class="GiftRegistryCompleteContentDiv">
                        [$AddComplete]
                    </div>
                    <div class="GiftRegistryCompleteContentDiv">
                        [$AddItem]
                    </div>
                    <div class="GiftRegistryCompleteButtonDiv">
                        <asp:LinkButton ID="uxContinueLinkButton" runat="server" OnClick="uxContinueLinkButton_Click"
                            CssClass="GiftRegistryCompleteContinueLinkButton BtnStyle2" Text="[$BtnContinue]" />
                    </div>
                    <div class="Clear">
                    </div>
                </div>
            </div>
            <div class="CommonPageBottom">
                <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/DefaultBoxBottomLeft.gif"
                    runat="server" CssClass="fl ImgLeft" />
                <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/DefaultBoxBottomRight.gif"
                    runat="server" CssClass="fr ImgRight" />
                <div class="Clear">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
