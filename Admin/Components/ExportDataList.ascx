<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ExportDataList.ascx.cs"
    Inherits="AdminAdvanced_Components_ExportDataList" %>
<%@ Register Src="Template/AdminContent.ascx" TagName="AdminContent" TagPrefix="uc1" %>
<%@ Register Src="ExportCustomer.ascx" TagName="ExportCustomer" TagPrefix="uc2" %>
<%@ Register Src="ExportOrder.ascx" TagName="ExportOrder" TagPrefix="uc3" %>
<%@ Register Src="ExportProduct.ascx" TagName="ExportProduct" TagPrefix="uc4" %>
<%@ Register Src="ExportCategory.ascx" TagName="ExportCategory" TagPrefix="uc5" %>
<%@ Register Src="ExportDepartment.ascx" TagName="ExportDepartment" TagPrefix="uc6" %>
<ajaxToolkit:TabContainer ID="uxTabContainer" runat="server" CssClass="DefaultTabContainer Container-Box1">
    <ajaxToolkit:TabPanel ID="uxExportCustomerTabPanel" runat="server" CssClass="DefaultTabPanel">
        <HeaderTemplate>
            <div>
                <asp:Label ID="uxExportCustomerLabel" runat="server" meta:resourcekey="uxExportCustomerLabel" />
            </div>
        </HeaderTemplate>
        <ContentTemplate>
            <uc2:ExportCustomer ID="uxExportCustomer" runat="server" />
        </ContentTemplate>
    </ajaxToolkit:TabPanel>
    <ajaxToolkit:TabPanel ID="uxExportOrderTabPanel" runat="server" CssClass="DefaultTabPanel">
        <HeaderTemplate>
            <div>
                <asp:Label ID="uxExportOrderLabel" runat="server" meta:resourcekey="uxExportOrderLabel" />
            </div>
        </HeaderTemplate>
        <ContentTemplate>
            <uc3:ExportOrder ID="uxExportOrder" runat="server" />
        </ContentTemplate>
    </ajaxToolkit:TabPanel>
    <ajaxToolkit:TabPanel ID="uxExportProductTabPanel" runat="server" CssClass="DefaultTabPanel">
        <HeaderTemplate>
            <div>
                <asp:Label ID="uxExportProductLabel" runat="server" meta:resourcekey="uxExportProductLabel" />
            </div>
        </HeaderTemplate>
        <ContentTemplate>
            <uc4:ExportProduct ID="uxProductOrder" runat="server" />
        </ContentTemplate>
    </ajaxToolkit:TabPanel>
    <ajaxToolkit:TabPanel ID="uxExportCategoryTabPanel" runat="server" CssClass="DefaultTabPanel">
        <HeaderTemplate>
            <div>
                <asp:Label ID="uxExportCategoryLabel" runat="server" meta:resourcekey="uxExportCategoryLabel" />
            </div>
        </HeaderTemplate>
        <ContentTemplate>
            <uc5:ExportCategory ID="uxExportCategory" runat="server" />
        </ContentTemplate>
    </ajaxToolkit:TabPanel>
    <ajaxToolkit:TabPanel ID="uxExportDepartmentTabPanel" runat="server" CssClass="DefaultTabPanel">
        <HeaderTemplate>
            <div>
                <asp:Label ID="uxExportDepartmentLabel" runat="server" meta:resourcekey="uxExportDepartmentLabel" />
            </div>
        </HeaderTemplate>
        <ContentTemplate>
            <uc6:ExportDepartment ID="uxExportDepartment" runat="server" />
        </ContentTemplate>
    </ajaxToolkit:TabPanel>
</ajaxToolkit:TabContainer>
