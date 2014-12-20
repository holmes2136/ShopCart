<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RBSWorldPayNotification.aspx.cs"
    Inherits="Gateway_RBSWorldPayNotification" ValidateRequest="false" Theme="" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>RBSWorldpay</title>
    <style type="text/css">        
        h1.GatewayPosting {  color:#A1C1D9; margin: 30px;   text-align: center;  font-size: 24px;}
        .GatewayPostingWaringColor {  color: #a1c1d9;   font-weight: bold; }
        .GatewayPosting{   margin: 20px;    font-size: 16px;    text-align: center;}
        .font{ font-family:Verdana,Arial,Tahoma,Microsoft Sans Serif; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="font">
            <asp:HiddenField ID="uxUrlHidden" runat="server" />
            <br />
            <h1 class="GatewayPosting">
            <asp:Label ID="uxCheckoutHeaderLabel" runat="server"></asp:Label>

            </h1>
            <p class="GatewayPosting">
                <asp:Label ID="uxCheckoutDetailLabel" runat="server"></asp:Label>
                <br />
                <br />
                <asp:HyperLink ID="uxCheckoutLink" runat="server">Please click here to continue.</asp:HyperLink>
                <asp:HyperLink ID="uxHomeLink" runat="server">Click here to go back to the store.</asp:HyperLink>
            </p>
        </div>
    </form>
</body>
</html>
