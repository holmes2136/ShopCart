<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="AffiliateGenerateLink.aspx.cs"
    Inherits="AffiliateGenerateLink" Title="[$GenerateLink]" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="AffiliateGenerateLink">
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
                    <div class="AffiliateGenerateLinkDiv">
                        <div class="AffiliateGenerateLinkInner">
                            <b>[$WebSiteLink]</b>
                        </div>
                        <div class="AffiliateGenerateLinkInner">
                            [$WebSiteLinkDetail]
                        </div>
                        <div>
                            <asp:TextBox ID="uxWebSiteLinkText" runat="server" CssClass="CommonTextBox AffiliateGenerateLinkTextBox"
                                TextMode="MultiLine" />
                        </div>
                    </div>
                    <div class="AffiliateGenerateLinkDiv">
                        <div class="AffiliateGenerateLinkInner">
                            <b>[$ProductLink]</b>
                        </div>
                        <div class="AffiliateGenerateLinkInner">
                            [$Category]
                        </div>
                        <div class="AffiliateGenerateLinkInner">
                            <asp:DropDownList ID="uxCategoryDrop" runat="server" AutoPostBack="true" 
                             OnSelectedIndexChanged="uxCategoryDrop_SelectIndexChanged" CssClass="CommonDropDown AffiliateGenerateLinkDropDown">
                            </asp:DropDownList>
                        </div>
                        <asp:Panel ID="uxProductPanel" runat="server" CssClass="AffiliateGenerateLinkInner">
                            <div class="AffiliateGenerateLinkInner">
                                [$Product]</div>
                            <div>
                                <asp:DropDownList ID="uxProductDrop" runat="server" CssClass="CommonDropDown AffiliateGenerateLinkDropDown">
                                </asp:DropDownList>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="uxProductLinkPanel" runat="server" Visible="false">
                            <div class="AffiliateGenerateLinkInner">
                                <asp:Label ID="uxProductLinkLabel" runat="server" />
                            </div>
                            <div>
                                <asp:TextBox ID="uxProductLinkText" runat="server" CssClass="CommonTextBox AffiliateGenerateLinkTextBox"
                                    TextMode="MultiLine" />
                            </div>
                        </asp:Panel>
                    </div>
                    <div class="AffiliateGenerateLinkButtonDiv">
                        <asp:LinkButton ID="uxGenerateButton" runat="server" OnClick="uxGenerateButton_Click"
                            CssClass="AffiliateGenerateLinkLinkButton BtnStyle1" Text="[$BtnGenerateLink]" />
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
