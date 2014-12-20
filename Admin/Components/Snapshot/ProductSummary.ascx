<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductSummary.ascx.cs"
    Inherits="AdminAdvanced_Components_Snapshot_ProductSummary" %>
<div>
    <div>
        <asp:Literal ID="uxProductLiteral" runat="server"></asp:Literal>
        <vevo:AdvancedLinkButton ID="uxAllProductLink" runat="server" PageName="ProductList.ascx"
            Text="Products" OnClick="ChangePage_Click" CssClass="UnderlineDashed" />
    </div>
    <div>
        <asp:Literal ID="uxCategoryLiteral" runat="server"></asp:Literal>
        <vevo:AdvancedLinkButton ID="uxAllCategoryLink" runat="server" PageName="CategoryList.ascx"
            Text="Categories" OnClick="ChangePage_Click" CssClass="UnderlineDashed" />
    </div>
    <div>
        <asp:Literal ID="uxDepartmentLiteral" runat="server"></asp:Literal>
        <vevo:AdvancedLinkButton ID="uxAllDepartmentLink" runat="server" PageName="DepartmentList.ascx"
            Text="Departments" OnClick="ChangePage_Click" CssClass="UnderlineDashed" />
    </div>
    <div>
        <asp:Literal ID="uxOutOfStockLiteral" runat="server"></asp:Literal>
        <vevo:AdvancedLinkButton ID="uxAllProductOnLowStockLink" runat="server" PageName="ProductList.ascx"
            Text="Out-of-Stock Products" OnClick="ChangePage_Click" CssClass="UnderlineDashed" />
    </div>
    <div>
        <asp:Literal ID="uxProductInactiveLiteral" runat="server"></asp:Literal>
        <vevo:AdvancedLinkButton ID="uxAllProductInactiveLink" runat="server" PageName="ProductList.ascx"
            Text="Inactive Products" PageQueryString="Type=Boolean&FieldName=IsEnabled&FieldValue=IsEnabled&Value1=False"
            CssClass="UnderlineDashed" OnClick="ChangePage_Click" />
    </div>
    <asp:Label ID="uxNoPermissionLabel" runat="server" Text=""></asp:Label>
</div>
