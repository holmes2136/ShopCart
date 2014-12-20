<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="AdvancedSearchResult.aspx.cs"
    Inherits="AdvancedSearchResult" Title="[$Title]" EnableViewStateMac="false" EnableSessionState="True"
    EnableEventValidation="false" ValidateRequest="false" ViewStateEncryptionMode="Never" %>

<%@ Register Src="Components/ContentList.ascx" TagName="ContentList" TagPrefix="uc2" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="AdvancedSearchResult">
        <div class="CommonPage">
            <div class="CommonPageTop">
                <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/DefaultBoxTopLeft.gif" runat="server"
                    CssClass="CommonPageTopImgLeft" />
                <asp:Label ID="uxDefaultTitle" runat="server" Text="[$HeadCategory]" CssClass="CommonPageTopTitle"></asp:Label>
                <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/DefaultBoxTopRight.gif"
                    runat="server" CssClass="CommonPageTopImgRight" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonPageLeft">
                <div class="CommonPageRight">
                    <asp:Panel ID="uxCatalogControlPanel" runat="server">
                    </asp:Panel>
                    <div id="uxMessageDiv" runat="server" class="CommonGridViewEmptyRowStyle" visible="false">
                        <asp:Label ID="uxMessageLabel" runat="server" Text="[$NoData]" />
                    </div>
                    <div class="AdvancedSearchResultBackButton">
                        <asp:HyperLink ID="uxBackLink" runat="server" CssClass="CommonHyperLink" Text="[$BackToSearch]"
                            NavigateUrl="~/AdvancedSearch.aspx" />
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
    <div class="AdvancedSearchResultDepartment">
        <asp:Panel ID="uxCheckDepartmentPanel" runat="server">
            <div class="CommonPage">
                <div class="CommonPageTop">
                    <asp:Image ID="uxTopDeptLeft" ImageUrl="~/Images/Design/Box/DefaultBoxTopLeft.gif"
                        runat="server" CssClass="CommonPageTopImgLeft" />
                    <asp:Label ID="uxDefaultDeptTitle" runat="server" CssClass="CommonPageTopTitle">[$HeadDepartment] </asp:Label>
                    <asp:Image ID="uxTopDeptRight" ImageUrl="~/Images/Design/Box/DefaultBoxTopRight.gif"
                        runat="server" CssClass="CommonPageTopImgRight" />
                    <div class="Clear">
                    </div>
                </div>
                <div class="CommonPageLeft">
                    <div class="CommonPageRight">
                        <asp:Panel ID="uxDepartmentPanel" runat="server">
                        </asp:Panel>
                        <div class="Clear">
                        </div>
                    </div>
                </div>
                <div class="CommonPageBottom">
                    <asp:Image ID="uxBottomDeptLeft" ImageUrl="~/Images/Design/Box/DefaultBoxBottomLeft.gif"
                        runat="server" CssClass="CommonPageBottomImgLeft" />
                    <asp:Image ID="uxBottomDeptRigh" ImageUrl="~/Images/Design/Box/DefaultBoxBottomRight.gif"
                        runat="server" CssClass="CommonPageBottomImgRight" />
                    <div class="Clear">
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
    <div class="AdvancedContentSearchResult" runat="server" id="uxAdvancedContentSearchResult">
        <div class="CommonPage">
            <div class="CommonPageTop">
                <asp:Image ID="uxContentListTopLeft" ImageUrl="~/Images/Design/Box/DefaultBoxTopLeft.gif"
                    runat="server" CssClass="CommonPageTopImgLeft" />
                <asp:Label ID="uxContentListDefaultTitle" runat="server" CssClass="CommonPageTopTitle">[$HeadContent] </asp:Label>
                <asp:Image ID="uxContentListTopRight" ImageUrl="~/Images/Design/Box/DefaultBoxTopRight.gif"
                    runat="server" CssClass="CommonPageTopImgRight" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonPageLeft ">
                <div class="CommonPageRight">
                    <uc2:ContentList ID="uxContentList" runat="server" />
                    <div class="Clear">
                    </div>
                </div>
            </div>
            <div class="CommonPageBottom">
                <asp:Image ID="uxContentListBottomLeft" ImageUrl="~/Images/Design/Box/DefaultBoxBottomLeft.gif"
                    runat="server" CssClass="CommonPageBottomImgLeft" />
                <asp:Image ID="uxContentListBottomRight" ImageUrl="~/Images/Design/Box/DefaultBoxBottomRight.gif"
                    runat="server" CssClass="CommonPageBottomImgRight" />
                <div class="Clear">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
