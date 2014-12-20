<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GraphicView.ascx.cs" 
Inherits="AdminAdvanced_Components_Snapshot_GraphicView" %>
<%@ Register Src="../CalendarPopup.ascx" TagName="CalendarPopup" TagPrefix="uc1" %>
<%@ Register Src="../PagingControl.ascx" TagName="PagingControl" TagPrefix="uc3" %>
<%@ Register Src="../Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../Template/AdminUserControlContent.ascx" TagName="AdminUserControlContent"
    TagPrefix="uc1" %>
<%@ Import Namespace="Vevo" %>
<uc1:AdminUserControlContent ID="uxAdminUserControlContent" runat="server">
    <MessageTemplate>
        <uc1:Message ID="uxMessage" runat="server" />
        <uc1:Message ID="uxMessage1" runat="server" Visible="false" />
        <asp:HyperLink ID="uxFileNameLink" runat="server"></asp:HyperLink>
    </MessageTemplate>
    <PlainContentTemplate>
        <div class="DefaultFilter">
            <div class="SearchFilter">
                <asp:Label ID="uxFilterText" runat="server" Text="Select Range :"/>
                <asp:DropDownList ID="uxPeriodGraphicDrop" runat="server" AutoPostBack="true" OnSelectedIndexChanged="uxPeriodGraphicDrop_SelectedIndexChanged"
                    DataTextField="Text" DataValueField="Value">
                </asp:DropDownList>
            </div>
        </div>
        <div class="CommonAdminBorder mgt10">
            <div id="my_chart">
            </div>
            <div class="pdt20">
            </div>
        </div>
        <%--Summary Revenue,tax,shipping,quantity section Begin--%>
        <asp:Panel ID="uxSumPanel" runat="server">
            <div class="DefaultGraphSummary">
                <div class="Section">
                    <asp:Label ID="Label1" runat="server" CssClass="Label">Revenue</asp:Label>
                    <asp:Label ID="uxRevenueValueLabel" runat="server" CssClass="Value"></asp:Label>
                </div>
                <div class="Section">
                    <asp:Label ID="Label2" runat="server" CssClass="Label">Tax</asp:Label>
                    <asp:Label ID="uxTaxValueLabel" runat="server" CssClass="Value"></asp:Label>
                </div>
                <div class="Section">
                    <asp:Label ID="Label3" runat="server" CssClass="Label">Shipping</asp:Label>
                    <asp:Label ID="uxShippingValueLabel" runat="server" CssClass="Value"></asp:Label>
                </div>
                <div class="Section">
                    <asp:Label ID="Label4" runat="server" CssClass="Label">Quantity</asp:Label>
                    <asp:Label ID="uxQuantityValueLabel" runat="server" CssClass="Value"></asp:Label>
                </div>
                <div class="Clear">
                </div>
            </div>
        </asp:Panel>
        <%--Summary Revenue,tax,shipping,quantity section End--%>
    </PlainContentTemplate>
</uc1:AdminUserControlContent>
