<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RecurringSpecial.ascx.cs"
    Inherits="Components_RecurringSpecial" %>
<%@ Import Namespace="Vevo.WebUI" %>
<asp:Panel ID="uxSpecialTrialPanel" runat="server" Visible='<%# IsVisibilityRecurringPeriod() %>'
    CssClass="RecurringSpecialPanel">
    <asp:Panel ID="uxTrialPeriodMoreTR" runat="server" Visible='<%# IsVisibilityTrialPeriodMore() %>'>
        <span class="RecurringSpecialNotice">[$Special] </span>[$TrialMessage1]
        <%# StoreContext.Currency.FormatPrice( Eval( "ProductRecurring.RecurringTrialAmount" ) )%>
        [$TrialMessage2]
        <%# Eval( "ProductRecurring.RecurringNumberOfTrialCycles" )%>
        [$TrialMessage3]
    </asp:Panel>
    <asp:Panel ID="uxTrialPeriodTR" runat="server" Visible='<%# IsVisibilityTrialPeriod() %>'>
        <span class="RecurringSpecialNotice">[$Special] </span>[$TrialMessage4]
        <%# StoreContext.Currency.FormatPrice( Eval( "ProductRecurring.RecurringTrialAmount" ) )%>
        [$TrialMessage5]
    </asp:Panel>
    <asp:Panel ID="uxFreeTrialPeriodTR" runat="server" Visible='<%# IsVisibilityFreeTrialPeriod() %>'>
        <span class="RecurringSpecialNotice">[$Special] </span>[$FreeTrialMessage] [$TrialMessage2]
        <%# Eval( "ProductRecurring.RecurringNumberOfTrialCycles" )%>
        [$TrialMessage3]
    </asp:Panel>
    <asp:Panel ID="uxFreeTrialPeriodMoreTR" runat="server" Visible='<%# IsVisibilityFreeTrialPeriodMore() %>'>
        <span class="RecurringSpecialNotice">[$Special] </span>[$FreeTrialMessage] [$TrialMessage5]
    </asp:Panel>
</asp:Panel>
