<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SearchFilterNew.ascx.cs"
    Inherits="Components_SearchFilterNew" %>
<div class="SearchFilter">
    <div class="SearchFilterByDiv">
        <asp:Label ID="lcFilterBy" runat="server" Text="[$FilterBy]" />
        <asp:DropDownList ID="uxFilterDrop" runat="server" OnSelectedIndexChanged="uxFilterDrop_SelectedIndexChanged"
            CssClass="SearchFilterDrop">
        </asp:DropDownList>
        <div class="Clear">
        </div>
    </div>
    <asp:Panel ID="uxTextPanel" runat="server" CssClass="SearchFilterTextPanel" Style="display: none;">
        <asp:Label ID="uxSearchLabel" runat="server" Text="[$TextToSearch]" CssClass="SearchFilterLabel" />
        <asp:TextBox ID="uxValueText" runat="server" Width="100px" CssClass="SearchFilterTextBox" />
        <asp:LinkButton ID="uxTextSearchImageButton" runat="server" OnClick="uxTextSearchButton_Click"
            Text="[$BtnSearch]" CssClass="BtnStyle2 SearchFilterTextImageButton" />
        <div class="Clear">
        </div>
    </asp:Panel>
    <asp:Panel ID="uxBooleanPanel" runat="server" CssClass="SearchFilterBooleanPanel"
        Style="display: none;">
        <asp:Label ID="lcYesNoValue" runat="server" Text="[$YesNoValue]" CssClass="SearchFilterLabel" />
        <asp:DropDownList ID="uxBooleanDrop" runat="server" CssClass="SearchFilterBooleanDropDown">
            <asp:ListItem Selected="True" Value="True">[$Yes]</asp:ListItem>
            <asp:ListItem Value="False">[$No]</asp:ListItem>
        </asp:DropDownList>
        <asp:LinkButton ID="uxBooleanSearchImageButton" runat="server" OnClick="uxBooleanSearchButton_Click"
            Text="[$BtnSearch]" CssClass="BtnStyle2 SearchFilterBooleanImageButton" />
        <div class="Clear">
        </div>
    </asp:Panel>
    <asp:Panel ID="uxValueRangePanel" runat="server" CssClass="SearchFilterValueRangePanel "
        Style="display: none;">
        <asp:Label ID="uxSearchLabel1" runat="server" Text="[$LowerBound]" CssClass="SearchFilterLabel" />
        <div class="ValueRangeValidateDiv">
            <asp:TextBox ID="uxLowerText" runat="server" Width="80px" CssClass="SearchFilterTextBox" />
            <asp:CompareValidator ID="uxLowerCompare" runat="server" ControlToValidate="uxLowerText"
                Operator="DataTypeCheck" Type="Integer" ValidationGroup="ValidValueRange" Display="Dynamic"
                CssClass="CommonValidatorTextLowerBound">
            <div class="CommonValidateDiv"></div>
            <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Invalid input.
            </asp:CompareValidator></div>
        <asp:Label ID="uxSearchLabel2" runat="server" Text="[$UpperBound]" CssClass="SearchFilterLabel" />
        <div class="ValueRangeValidateDiv">
            <asp:TextBox ID="uxUpperText" runat="server" Width="80px" CssClass="SearchFilterTextBox" />
            <asp:CompareValidator ID="uxUpperCompare" runat="server" ControlToValidate="uxUpperText"
                Operator="DataTypeCheck" Type="Integer" ValidationGroup="ValidValueRange" Display="Dynamic"
                CssClass="CommonValidatorTextUpperBound">
            <div class="CommonValidateDiv"></div>
            <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Invalid input.
            </asp:CompareValidator>
        </div>
        <asp:LinkButton ID="uxValueRangeSearchImageButton" runat="server" OnClick="uxValueRangeSearchButton_Click"
            Text="[$BtnSearch]" CssClass="BtnStyle2 SearchFilterValueRangeImageButton" ValidationGroup="ValidValueRange" />
        <div class="Clear">
        </div>
    </asp:Panel>
    <asp:Panel ID="uxDateRangePanel" runat="server" Style="display: none;" CssClass="SearchFilterDateRangePanel">
        <asp:Label ID="uxSearchLabel3" runat="server" Text="[$LowerBound]" CssClass="SearchFilterLabel" />
        <asp:TextBox runat="server" ID="uxStartDateText" Width="80px" CssClass="SearchFilterTextBox"
            Enabled="false" />
        <asp:LinkButton ID="uxStartDateButton" runat="server" CssClass="SearchFilterStartDateImageButton">
            <asp:Image ID="uxStartDateImage" runat="server" ImageUrl="~/Images/Design/Icon/iconcalendar.gif"
                AlternateText="Click to show calendar" />
        </asp:LinkButton>
        <ajaxToolkit:CalendarExtender ID="uxStartCalendarButtonExtender" runat="server" TargetControlID="uxStartDateText"
            PopupButtonID="uxStartDateButton" Format="MM/dd/yyyy" />
        <asp:Label ID="uxSearchLabel4" runat="server" Text="[$UpperBound]" CssClass="SearchFilterLabel" />
        <asp:TextBox runat="server" ID="uxEndDateText" Width="80px" CssClass="SearchFilterTextBox"
            Enabled="false" />
        <asp:LinkButton ID="uxEndDateButton" runat="server" CssClass="SearchFilterEndDateImageButton">
            <asp:Image ID="uxEndDateImage" runat="server" ImageUrl="~/Images/Design/Icon/iconcalendar.gif"
                AlternateText="Click to show calendar" />
        </asp:LinkButton>
        <ajaxToolkit:CalendarExtender ID="uxEndCalendarExtender" runat="server" TargetControlID="uxEndDateText"
            PopupButtonID="uxEndDateButton" Format="MM/dd/yyyy" />
        <asp:LinkButton ID="uxDateRangeImageButton" runat="server" OnClick="uxDateRangeButton_Click"
            Text="[$BtnSearch]" CssClass="BtnStyle2 SearchFilterDateRangeImageButton" />
        <div class="Clear">
        </div>
    </asp:Panel>
    <div class="Clear">
    </div>
</div>
<asp:Label ID="uxMessageLabel" runat="server" CssClass="SearchFilterMessageLabel" />