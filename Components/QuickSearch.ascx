<%@ Control Language="C#" AutoEventWireup="true" CodeFile="QuickSearch.ascx.cs" Inherits="Components_QuickSearch" %>
<%@ Register Src="VevoLinkButton.ascx" TagName="LinkButton" TagPrefix="ucLinkButton" %>
<div class="QuickSearch">
    <asp:Label ID="uxSearchLabel" runat="server" Text="Search:" CssClass="QuickSearchLabel" />
    <div class="QuickSearchDropDownDiv" id="uxQuickSearchCategoryDropDiv" runat="server">
        <asp:DropDownList ID="uxCategoryDropDownList" runat="server" CssClass="QuickSearchDropDown">
        </asp:DropDownList>
    </div>
    <asp:TextBox ID="uxSearchText" runat="server" CssClass="QuickSearchText" />
    <ucLinkButton:LinkButton ID="uxSearchButton" runat="server" OnBubbleEvent="uxSearchButton_Click"
        ThemeImageUrl="[$SearchBtn]" CssClass="QuickSearchLinkButton" CssClassImage="NoBorder" />
    <asp:HiddenField ID="uxStartKeywordTextHidden" runat="server" />
</div>
