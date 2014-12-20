<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CalendarPopup.ascx.cs"
    Inherits="AdminAdvanced_Components_CalendarPopup" %>
<asp:TextBox ID="uxDateText" runat="server" />
<asp:LinkButton ID="uxDateImageButton" runat="server" CssClass="Calendar">
    <asp:Image ID="uxDateImage" runat="server" ImageUrl="~/Images/Design/Icon/iconcalendar.gif"
        AlternateText="Click to show calendar" />
</asp:LinkButton>
<ajaxToolkit:CalendarExtender ID="uxDateCalendarExtender" runat="server" TargetControlID="uxDateText"
    PopupButtonID="uxDateImageButton" CssClass="fl mgl5 b1" />
