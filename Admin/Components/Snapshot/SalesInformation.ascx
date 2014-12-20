<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SalesInformation.ascx.cs"
    Inherits="AdminAdvanced_Components_Snapshot_SalesInformation" %>
<div class="fr">
    <asp:Label ID="uxSaleFilterDescriptionLabel" runat="server" meta:resourcekey="SaleFilterDescription"></asp:Label>
    <asp:DropDownList ID="uxSalesFilterDropDown" runat="server" AutoPostBack="True" OnSelectedIndexChanged="uxSalesFilterDropDown_SelectedIndexChanged"
        CssClass="DropDown">
        <asp:ListItem Value="All" Text="All" />
        <asp:ListItem Value="Year" Text="This Year" />
        <asp:ListItem Value="Month" Selected="true" Text="This Month" />
        <asp:ListItem Value="LastMonth" Text="Last Month" />
        <asp:ListItem Value="Week" Text="This Week" />
        <asp:ListItem Value="LastWeek" Text="Last Week" />
    </asp:DropDownList>
</div>
<div class="fb c11">
    Paid Transactions
</div>
<div>
    <asp:Label ID="uxDateLabel" runat="server" />
    <br />
    <asp:Label ID="uxSaleAmountLabel" runat="server" />
    <br />
    <asp:Label ID="uxSaleTotalLabel" runat="server" />
</div>
<div class="Clear">
</div>
