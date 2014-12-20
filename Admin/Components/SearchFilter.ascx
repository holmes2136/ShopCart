<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SearchFilter.ascx.cs"
    Inherits="AdminAdvanced_Components_SearchFilter" %>
<div class="SearchFilter">
    <div class="SearchFilterDefault">
        <asp:Label ID="lcFilterBy" runat="server" meta:resourcekey="lcFilterBy" CssClass="Label" />
        <asp:DropDownList ID="uxFilterDrop" runat="server" OnSelectedIndexChanged="uxFilterDrop_SelectedIndexChanged"
            CssClass="DropDown">
        </asp:DropDownList>
        <div class="clear">
        </div>
    </div>
    <asp:Panel ID="uxTextPanel" runat="server" CssClass="SearchFilterValuePanel" Style="display: none;">
        <asp:Label ID="lcTextToSearch" runat="server" meta:resourcekey="lcTextToSearch" CssClass="Label" />
        <asp:TextBox ID="uxValueText" runat="server" Width="100px" CssClass="TextBox" />
        <vevo:AdvanceButton ID="uxTextSearchButton" runat="server" meta:resourcekey="uxTextSearchButton"
            CssClassBegin="fl" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxTextSearchButton_Click"
            OnClickGoTo="None"></vevo:AdvanceButton>
    </asp:Panel>
    <asp:Panel ID="uxBooleanPanel" runat="server" CssClass="SearchFilterValuePanel" Style="display: none;">
        <asp:Label ID="lcYesNoValue" runat="server" meta:resourcekey="lcYesNoValue" CssClass="Label" />
        <asp:DropDownList ID="uxBooleanDrop" runat="server" CssClass="DropDown">
            <asp:ListItem Value="True">Yes</asp:ListItem>
            <asp:ListItem Value="False">No</asp:ListItem>
        </asp:DropDownList>
        <vevo:AdvanceButton ID="uxBooleanSearchButton" runat="server" meta:resourcekey="uxBooleanSearchButton"
            CssClassBegin="fl" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxBooleanSearchButton_Click"
            OnClickGoTo="None"></vevo:AdvanceButton>
    </asp:Panel>
    <asp:Panel ID="uxValueRangePanel" runat="server" CssClass="SearchFilterValuePanel"
        Style="display: none;">
        <asp:Label ID="lcLowerBound" runat="server" meta:resourcekey="lcLowerBound" CssClass="Label" />
        <asp:TextBox ID="uxLowerText" runat="server" Width="80px" CssClass="TextBox" />
        <asp:Label ID="lcUpperBound" runat="server" meta:resourcekey="lcUpperBound" CssClass="Label" />
        <asp:TextBox ID="uxUpperText" runat="server" Width="80px" CssClass="TextBox" />
        <vevo:AdvanceButton ID="uxValueRangeSearchButton" runat="server" meta:resourcekey="uxValueRangeSearchButton"
            CssClassBegin="fl" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxValueRangeSearchButton_Click"
            OnClickGoTo="None"></vevo:AdvanceButton>
    </asp:Panel>
    <asp:Panel ID="uxDateRangePanel" runat="server" Style="display: none;" CssClass="SearchFilterValuePanel">
        <asp:TextBox runat="server" ID="uxStartDateText" Width="80px" CssClass="TextBox" />
        <asp:LinkButton ID="uxStartDateImageButton" runat="server" AlternateText="Click to show calendar"
            CssClass="Calendar">
            <asp:Image ID="uxStartDateImage" runat="server" SkinId="Calendar" />
        </asp:LinkButton>
        <ajaxToolkit:CalendarExtender ID="uxStartCalendarButtonExtender" runat="server" TargetControlID="uxStartDateText"
            PopupButtonID="uxStartDateImageButton" Format="MMM d,yyyy" CssClass="Calendar" />
        <asp:TextBox runat="server" ID="uxEndDateText" Width="80px" CssClass="TextBox" />
        <asp:LinkButton ID="uxEndDateImageButton" runat="server" AlternateText="Click to show calendar"
            CssClass="Calendar">
            <asp:Image ID="uxEndDateImage" runat="server" SkinId="Calendar" />
        </asp:LinkButton>
        <ajaxToolkit:CalendarExtender ID="uxEndCalendarExtender" runat="server" TargetControlID="uxEndDateText"
            PopupButtonID="uxEndDateImageButton" Format="MMM d,yyyy" CssClass="Calendar" />
        <vevo:AdvanceButton ID="uxDateRangeButton" runat="server" meta:resourcekey="uxDateRangeButton"
            CssClassBegin="fl" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxDateRangeButton_Click"
            OnClickGoTo="None"></vevo:AdvanceButton>
    </asp:Panel>
</div>
<div class="Clear">
</div>
<div class="SearchFilterMessage">
    <asp:Label ID="uxMessageLabel" runat="server" />
</div>
