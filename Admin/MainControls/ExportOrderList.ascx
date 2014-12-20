<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ExportOrderList.ascx.cs"
    Inherits="AdminAdvanced_MainControls_ExportOrderList" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/ExportDataList.ascx" TagName="ExportDataList" TagPrefix="uc2" %>
<uc1:AdminContent ID="uxAdminContentControl" runat="server" HeaderText="Data Export List">
    <PlainContentTemplate>
        <uc2:ExportDataList ID="uxExportDataList" runat="server" TabIndex="1" />
    </PlainContentTemplate>
</uc1:AdminContent>
