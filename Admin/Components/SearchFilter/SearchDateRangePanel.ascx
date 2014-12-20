<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SearchDateRangePanel.ascx.cs"
    Inherits="AdminAdvanced_Components_SearchFilter_SearchDateRangePanel" %>
<asp:TextBox runat="server" ID="uxStartDateText" Width="80px" CssClass="TextBox" />
<asp:CompareValidator ID="uxStartDateValidator" runat="server" ErrorMessage="" Display="Dynamic"
    ControlToValidate="uxStartDateText" Operator="DataTypeCheck" Type="Date" ValidationGroup="Date"></asp:CompareValidator>
<asp:LinkButton ID="uxStartDateImageButton" runat="server" CssClass="Calendar">
    <asp:Image ID="uxStartDateImage" runat="server" SkinId="Calendar" />
</asp:LinkButton>
<ajaxToolkit:CalendarExtender ID="uxStartCalendarButtonExtender" runat="server" TargetControlID="uxStartDateText"
    PopupButtonID="uxStartDateImageButton" Format="MMM d,yyyy" CssClass="Calendar" />
<asp:TextBox runat="server" ID="uxEndDateText" Width="80px" CssClass="TextBox" />
<asp:CompareValidator ID="uxEndDateValidator" runat="server" ErrorMessage="" Display="Dynamic"
    ControlToValidate="uxEndDateText" Operator="DataTypeCheck" Type="Date" ValidationGroup="Date"></asp:CompareValidator>
<asp:LinkButton ID="uxEndDateImageButton" runat="server" CssClass="Calendar">
    <asp:Image ID="uxEndDateImage" runat="server" SkinId="Calendar" />
</asp:LinkButton>
<ajaxToolkit:CalendarExtender ID="uxEndCalendarExtender" runat="server" TargetControlID="uxEndDateText"
    PopupButtonID="uxEndDateImageButton" Format="MMM d,yyyy" CssClass="Calendar" />
