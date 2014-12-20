<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ComparisonListPopup.aspx.cs"
    Inherits="ComparisonListPopup" Title="Product Compare List" %>
<%@ Register Src="Components/ProductItemDetails.ascx" TagName="ProductItemDetails" TagPrefix="uc2" %>
<%@ Import Namespace="Vevo" %>
<%@ Import Namespace="Vevo.Shared.Utilities" %>
<%@ Import Namespace="Vevo.WebUI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2" runat="server">
</head>
<body class="ComparePopupBody">
    <form id="form1" runat="server">
     <ajaxToolkit:ToolkitScriptManager ID="uxScriptManager" EnableHistory="true" AsyncPostBackTimeout="300"
        EnableSecureHistoryState="false" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div class="ComparePopupDiv">
        <asp:DataList ID="uxList" CssClass="CompareProductListPopUpList" runat="server" ShowFooter="false"
             OnItemDataBound="uxList_ItemDataBound" ShowHeader="false">
            <ItemTemplate>
                <uc2:ProductItemDetails ID="uxItem" runat="server" />
            </ItemTemplate>
             <ItemStyle CssClass="ItemList" /> 
        </asp:DataList>
    </div>
    </form>
</body>
</html>
