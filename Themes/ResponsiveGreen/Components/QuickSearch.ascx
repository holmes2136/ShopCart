<%@ Control Language="C#" AutoEventWireup="true" CodeFile="QuickSearch.ascx.cs" Inherits="Themes_ResponsiveGreen_Components_QuickSearch" %>
<%@ Register Src="~/Components/VevoLinkButton.ascx" TagName="LinkButton" TagPrefix="ucLinkButton" %>
<div class="Search" id="uxSearchDiv" runat="server">
    <div class="SearchTop">
        <asp:Label ID="uxSearchTitle" runat="server" Text="[$Search]" CssClass="SearchTopTitle"></asp:Label>
    </div>
    <div class="SearchLeft">
        <div class="SearchRight">
            <div class="QuickSearch">
                <asp:Label ID="uxSearchLabel" runat="server" Text="Search:" CssClass="QuickSearchLabel" />
                <asp:UpdatePanel ID="uxHeaderUpdatePanel" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div id="uxQuickSearchCategoryDropDiv" runat="server">
                            <asp:DropDownList ID="uxCategoryDropDownList" runat="server" CssClass="QuickSearchDropDown"
                                AutoPostBack="true" OnSelectedIndexChanged="uxCategoryDropDownList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <asp:TextBox ID="uxSearchText" runat="server" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="uxCategoryDropDownList" />
                    </Triggers>
                </asp:UpdatePanel>
                <ucLinkButton:LinkButton ID="uxSearchButton" runat="server" OnBubbleEvent="uxSearchButton_Click"
                    ThemeImageUrl="[$SearchBtn]" CssClass="QuickSearchLinkButton" CssClassImage="NoBorder" />
            </div>
            <asp:HyperLink ID="uxAdvanceSearchLink" runat="server" NavigateUrl="~/AdvancedSearch.aspx"
                CssClass="SearchAdvancedLink" Text="[$Advanced Search]" />
            <div class="Clear">
            </div>
        </div>
    </div>
</div>
