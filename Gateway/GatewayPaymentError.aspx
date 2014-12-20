<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GatewayPaymentError.aspx.cs"
    Inherits="Gateway_GatewayPaymentError" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>[$Title]</title>
</head>
<body>
    <form id="uxform" runat="server">
        <div style="background-color: White; font-size: 14px; font-family: Verdana, Tahoma, Sans-Serif;
            margin-top: 50px; margin-left: auto; margin-right: auto; text-align:center;">
            <div>
                <h1 class="GatewayPosting">
                    <asp:Literal ID="uxHeaderLiteral" runat="server" Text="[$Header]" />
                </h1>
                <div style="margin: 10px 40px 10px 40px;">
                    <asp:Literal ID="uxMessageLiteral" runat="server" Text="[$Message]" />
                    <div style="margin-top:50px;">
                        <asp:HyperLink ID="uxBackToListLink" runat="server" NavigateUrl="~/Catalog.aspx">[$LinkBack]</asp:HyperLink>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
