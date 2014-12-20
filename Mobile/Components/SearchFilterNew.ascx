<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SearchFilterNew.ascx.cs"
    Inherits="Mobile_Components_SearchFilterNew" %>
<div class="MobileSearchFilter">
    <div class="MobileProductSorting">
        <asp:Label ID="lcFilterBy" runat="server" Text="[$FilterBy]" />
        <asp:DropDownList ID="uxFilterDrop" runat="server" OnSelectedIndexChanged="uxFilterDrop_SelectedIndexChanged"
            CssClass="SearchFilterDrop">
        </asp:DropDownList>
        <div class="Clear">
        </div>
    </div>
    <asp:Panel ID="uxTextPanel" runat="server" CssClass="MobileSearchFilterPanel" Style="display: none;">
        <asp:Label ID="uxSearchFilterTextLabel" runat="server" Text="[$TextToSearch]" CssClass="MobileSearchFilterLabel"></asp:Label>
        <asp:TextBox ID="uxValueText" runat="server" CssClass="MobileSearchFilterLabelText" />
        <div class="MobileUserLoginControlPanel">
            <asp:LinkButton ID="uxTextSearchImageButton" runat="server" Text="[$SearchButton]"
                OnClick="uxTextSearchButton_Click" CssClass="MobileButton MobileSearchFilterButton" />
        </div>
    </asp:Panel>
    <asp:Panel ID="uxBooleanPanel" runat="server" CssClass="MobileSearchFilterPanel"
        Style="display: none;">
        <asp:Label ID="lcYesNoValue" runat="server" Text="[$YesNoValue]" CssClass="SearchFilterBooleanLabel" />
        <asp:DropDownList ID="uxBooleanDrop" runat="server" CssClass="SearchFilterBooleanDropDown">
            <asp:ListItem Selected="True" Value="True">[$Yes]</asp:ListItem>
            <asp:ListItem Value="False">[$No]</asp:ListItem>
        </asp:DropDownList>
        <div class="MobileUserLoginControlPanel">
            <asp:LinkButton ID="uxBooleanSearchImageButton" runat="server" Text="[$SearchButton]"
                OnClick="uxBooleanSearchButton_Click" CssClass="MobileButton MobileSearchFilterButton" />
        </div>
    </asp:Panel>
    <asp:Panel ID="uxValueRangePanel" runat="server" CssClass="MobileSearchFilterPanel"
        Style="display: none;">
        <asp:Label ID="uxSearchLowerBoundTextLabel" runat="server" Text="[$LowerBound]" CssClass="MobileSearchFilterLabel" />
        <asp:TextBox ID="uxLowerText" runat="server" CssClass="MobileSearchFilterLabelText" />
        <asp:CompareValidator ID="uxLowerCompare" runat="server" ControlToValidate="uxLowerText"
            Operator="DataTypeCheck" Type="Integer" ValidationGroup="ValidValueRange" Display="Dynamic"
            CssClass="MobileCommonValidatorText">
            <div class="MobileCommonValidateDiv MobileSearchFilterValidatorDiv"></div>
            <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Invalid input.
        </asp:CompareValidator>
        <asp:Label ID="uxSearchUpperBoundTextLabel" runat="server" Text="[$UpperBound]" CssClass="MobileSearchFilterLabel" />
        <asp:TextBox ID="uxUpperText" runat="server" CssClass="MobileSearchFilterLabelText" />
        <asp:CompareValidator ID="uxUpperCompare" runat="server" ControlToValidate="uxUpperText"
            Operator="DataTypeCheck" Type="Integer" ValidationGroup="ValidValueRange" Display="Dynamic"
            CssClass="MobileCommonValidatorText">
            <div class="MobileCommonValidateDiv MobileSearchFilterValidatorDiv"></div>
            <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Invalid input.
        </asp:CompareValidator>
        <div class="MobileUserLoginControlPanel">
            <asp:LinkButton ID="uxValueRangeSearchImageButton" runat="server" Text="[$SearchButton]"
                OnClick="uxValueRangeSearchButton_Click" CssClass="MobileButton MobileSearchFilterButton"
                ValidationGroup="ValidValueRange" />
        </div>
    </asp:Panel>
    <asp:Panel ID="uxDateRangePanel" runat="server" Style="display: none;" CssClass="MobileSearchFilterPanel">
        <asp:Label ID="uxSearchLowerBoundTextLabel1" runat="server" Text="[$LowerBound]"
            CssClass="MobileSearchFilterLabel" />
        <asp:TextBox runat="server" ID="uxStartDateText" CssClass="MobileSearchFilterDateText" />
        <asp:ImageButton runat="Server" ID="uxStartDateImage" ImageUrl="~/Images/Design/Icon/iconcalendar.gif"
            AlternateText="Click to show calendar" CssClass="MobileSearchFilterCalendar" /><ajaxToolkit:CalendarExtender
                ID="uxStartCalendarButtonExtender" runat="server" TargetControlID="uxStartDateText"
                PopupButtonID="uxStartDateImage" Format="MM/dd/yyyy" />
        <asp:Label ID="uxSearchUpperBoundTextLabel1" runat="server" Text="[$UpperBound]"
            CssClass="MobileSearchFilterLabel" />
        <asp:TextBox runat="server" ID="uxEndDateText" CssClass="MobileSearchFilterDateText" />
        <asp:ImageButton runat="Server" ID="uxEndDateImage" ImageUrl="~/Images/Design/Icon/iconcalendar.gif"
            AlternateText="Click to show calendar" CssClass="MobileSearchFilterCalendar" />
        <ajaxToolkit:CalendarExtender ID="uxEndCalendarExtender" runat="server" TargetControlID="uxEndDateText"
            PopupButtonID="uxEndDateImage" Format="MM/dd/yyyy" />
        <div class="MobileUserLoginControlPanel">
            <asp:LinkButton ID="uxDateRangeImageButton" runat="server" Text="[$SearchButton]"
                OnClick="uxDateRangeButton_Click" CssClass="MobileButton MobileSearchFilterButton" />
        </div>
    </asp:Panel>
    <asp:Label ID="uxMessageLabel" runat="server" CssClass="MobileSearchFilterMessage" />
</div>
