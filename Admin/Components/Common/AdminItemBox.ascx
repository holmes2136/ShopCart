<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminItemBox.ascx.cs"
    Inherits="AdminAdvanced_Components_Common_AdminItemBox" %>
<%@ Register Src="SearchBox.ascx" TagName="SearchBox" TagPrefix="uc1" %>
<%@ Import Namespace="Vevo.Domain" %>
<%--For Stroefront Stat Box Style--%>
<div class="AdminItemBox1" id="Div1" runat="server">
    <div class="SidebarTop">
        <asp:Label ID="Label1" runat="server" Text="Storefront Statistics :" CssClass="SidebarTopTitle SideLeftStatIcon"></asp:Label>
    </div>
    <div class="SidebarLeft">
        <div class="SidebarRight">
            <div class="AdminStatDiv">
                <asp:Label ID="Label2" runat="server" CssClass="AdminStatLable">Today&#39;s Sales</asp:Label>
                <asp:Label ID="uxSaleTodayLabel" runat="server" CssClass="AdminStatValue"></asp:Label>
            </div>
            <div class="AdminStatDiv">
                <asp:Label ID="Label4" runat="server" CssClass="AdminStatLable">Month&#39;s Sales</asp:Label>
                <asp:Label ID="uxSaleMonthLabel" runat="server" CssClass="AdminStatValue"></asp:Label>
            </div>
            <div class="AdminStatDiv">
                <asp:Label ID="Label6" runat="server" CssClass="AdminStatLable">Year&#39;s Sales</asp:Label>
                <asp:Label ID="uxSaleYearLabel" runat="server" CssClass="AdminStatValue"></asp:Label>
            </div>
            <div class="AdminStatDiv">
                <asp:Label ID="Label8" runat="server" CssClass="AdminStatLable">Customers</asp:Label>
                <vevo:AdvancedLinkButton ID="uxCustomerListLink" runat="server" ToolTip="Customer list"
                    PageName="CustomerList.ascx" OnClick="ChangePage_Click" CssClass="AdminStatValue">
                <asp:Label ID="uxCustomerLabel" runat="server" CssClass="AdminStatValue"></asp:Label>
                </vevo:AdvancedLinkButton>
            </div>
            <div class="Clear">
            </div>
        </div>
    </div>
</div>
<%--End Stroefront Stat Box Style--%>
<%--For Summary Box Style--%>
<div class="AdminItemBox" id="Div4" runat="server">
    <div class="SidebarTop">
        <asp:Label ID="Label3" runat="server" Text="Catalog Summary :" CssClass="SidebarTopTitle SideLeftSummaryIcon"></asp:Label>
    </div>
    <div class="SidebarLeft">
        <div class="SidebarRight">
            <div class="AdminSummaryDiv">
                <asp:Label ID="Label5" runat="server" CssClass="AdminStatLable">Products</asp:Label>
                <vevo:AdvancedLinkButton ID="uxAllProductLink" runat="server" PageName="ProductList.ascx"
                    Text="Products" OnClick="ChangePage_Click" CssClass="UnderlineDashed fr" />
            </div>
            <div class="AdminSummaryDiv">
                <asp:Label ID="Label9" runat="server" CssClass="AdminStatLable">Categories</asp:Label>
                <vevo:AdvancedLinkButton ID="uxAllCategoryLink" runat="server" PageName="CategoryList.ascx"
                    Text="Categories" OnClick="ChangePage_Click" CssClass="UnderlineDashed fr" />
            </div>
            <div class="AdminSummaryDiv">
                <asp:Label ID="Label14" runat="server" CssClass="AdminStatLable">Departments</asp:Label>
                <vevo:AdvancedLinkButton ID="uxAllDepartmentLink" runat="server" PageName="DepartmentList.ascx"
                    Text="Departments" OnClick="ChangePage_Click" CssClass="UnderlineDashed fr" />
            </div>
            <div class="AdminSummaryDiv">
                <asp:Label ID="Label16" runat="server" CssClass="AdminStatLable">Out-of-Stock Products</asp:Label>
                <vevo:AdvancedLinkButton ID="uxAllProductOnLowStockLink" runat="server" PageName="ProductList.ascx"
                    Text="Out-of-Stock Products" OnClick="ChangePage_Click" CssClass="UnderlineDashed fr" />
            </div>
            <div class="AdminSummaryDiv">
                <asp:Label ID="Label18" runat="server" CssClass="AdminStatLable">Inactive Products</asp:Label>
                <vevo:AdvancedLinkButton ID="uxAllProductInactiveLink" runat="server" PageName="ProductList.ascx"
                    Text="Inactive Products" PageQueryString="Type=Boolean&FieldName=IsEnabled&FieldValue=IsEnabled&Value1=False"
                    CssClass="UnderlineDashed fr" OnClick="ChangePage_Click" />
            </div>
            <div class="Clear">
            </div>
        </div>
    </div>
</div>
<%--End Summary Stat Box Style--%>
<%--For Order Search Box Style--%>
<div class="AdminItemBox" id="Div3" runat="server">
    <div class="SidebarTop">
        <asp:Label ID="Label12" runat="server" Text="Search In Orders :" CssClass="SidebarTopTitle SideLeftOrderIcon"></asp:Label>
    </div>
    <div class="SidebarLeft">
        <div class="SidebarRight">
            <uc1:SearchBox ID="uxOrderSearchBox" runat="server" ValidationGroup="OrderSearchBox" />
            <div class="Clear">
            </div>
        </div>
    </div>
</div>
<%--End Order Search Box Style--%>
<div class="AdminItemBox" id="Div2" runat="server">
    <div class="SidebarTop">
        <asp:Label ID="Label10" runat="server" Text="Search In Customers :" CssClass="SidebarTopTitle SideLeftCustomerIcon"></asp:Label>
    </div>
    <div class="SidebarLeft">
        <div class="SidebarRight">
            <uc1:SearchBox ID="uxCustomerSearchBox" runat="server" ValidationGroup="CustomerSearchBox" />
            <div class="Clear">
            </div>
        </div>
    </div>
</div>
