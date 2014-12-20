<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ExportCustomerList.ascx.cs"
    Inherits="AdminAdvanced_MainControls_ExportCustomerList" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/ExportDataList.ascx" TagName="ExportDataList" TagPrefix="uc2" %>
<uc1:AdminContent ID="uxAdminContentControl" runat="server" HeaderText="Data Export List">
    <PlainContentTemplate>
        <uc2:ExportDataList ID="uxExportDataList" runat="server" TabIndex="0" />
    </PlainContentTemplate>
</uc1:AdminContent>
