<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SearchFilter.ascx.cs"
    Inherits="AdminAdvanced_Components_SearchFilter_SearchFilter" %>
<%@ Register Src="SearchTextPanel.ascx" TagName="SearchTextPanel" TagPrefix="uc1" %>
<%@ Register Src="SearchBooleanPanel.ascx" TagName="SearchBooleanPanel" TagPrefix="uc2" %>
<%@ Register Src="SearchRangePanel.ascx" TagName="SearchRangePanel" TagPrefix="uc3" %>
<%@ Register Src="SearchDateRangePanel.ascx" TagName="SearchDateRangePanel" TagPrefix="uc4" %>
<div id="uxFilterTopTR" runat="server">
    <div id="uxFilterLeftTR" runat="server">
        <div id="uxFilterRightTR" runat="server">
            <asp:Label ID="lcFilterBy" CssClass="Label" runat="server" meta:resourcekey="lcFilterBy" />
        </div>
        <asp:DropDownList ID="uxFilterDrop" runat="server" OnSelectedIndexChanged="uxFilterDrop_SelectedIndexChanged"
            CssClass="DropDown">
        </asp:DropDownList>
    </div>
    <div class="clear">
    </div>
    <asp:Panel ID="uxTextPanel" runat="server" Style="display: none;">
        <uc1:SearchTextPanel ID="uxSearchTextPanel" runat="server" />
        <vevo:AdvanceButton ID="uxSearchTextButton" runat="server" meta:resourcekey="uxSearchTextButton"
            CssClassBegin="fr" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxSearchButton_Click"
            OnClickGoTo="None"></vevo:AdvanceButton>
    </asp:Panel>
    <asp:Panel ID="uxBooleanPanel" runat="server" Style="display: none;">
        <uc2:SearchBooleanPanel ID="uxSearchBooleanPanel" runat="server" />
        <vevo:AdvanceButton ID="uxSearchBooleanButton" runat="server" meta:resourcekey="uxSearchBooleanButton"
            CssClassBegin="fr" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxSearchButton_Click"
            OnClickGoTo="None"></vevo:AdvanceButton>
    </asp:Panel>
    <asp:Panel ID="uxValueRangePanel" runat="server" Style="display: none;">
        <uc3:SearchRangePanel ID="uxSearchRangePanel" runat="server" />
        <vevo:AdvanceButton ID="uxSearchRangeButton" runat="server" meta:resourcekey="uxSearchRangeButton"
            CssClassBegin="fr" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxSearchButton_Click"
            OnClickGoTo="None"></vevo:AdvanceButton>
    </asp:Panel>
    <asp:Panel ID="uxDateRangePanel" runat="server" Style="display: none;">
        <uc4:SearchDateRangePanel ID="uxSearchDateRangePanel" runat="server" />
        <vevo:AdvanceButton ID="uxSearchDateRangeButton" runat="server" meta:resourcekey="uxSearchDateRangeButton"
            CssClassBegin="fr" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxSearchButton_Click"
            OnClickGoTo="None"></vevo:AdvanceButton>
    </asp:Panel>
</div>
<div class="Clear">
</div>
<div id="uxFilterSpace" runat="server">
    <div class="Label">
        &nbsp;
    </div>
</div>
<div id="uxFilterMessageTR" runat="server">
    <asp:Label ID="uxMessageLabel" runat="server" />
</div>
