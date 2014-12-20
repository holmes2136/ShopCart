<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CalendarPopup.ascx.cs"
    Inherits="Components_CalendarPopup" %>
<asp:TextBox ID="uxDateText" runat="server" CssClass="CommonTextBox" Enabled="false" />
<asp:LinkButton ID="uxDateImageButton" runat="server" CssClass="CommonImageButton">
    <asp:Image ID="uxDateImage" runat="server" ImageUrl="~/Images/Design/Icon/iconcalendar.gif"
        AlternateText="Show calendar" />
</asp:LinkButton>
<ajaxToolkit:CalendarExtender ID="uxDateCalendarExtender" runat="server" TargetControlID="uxDateText"
    PopupButtonID="uxDateImageButton" CssClass="CommonCalendarExtender" />
<%--<asp:RequiredFieldValidator ID="uxRequiredValidator" runat="server" ControlToValidate="uxDateText"
    Display="Dynamic">
    <--
</asp:RequiredFieldValidator>--%>
