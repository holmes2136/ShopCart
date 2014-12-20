<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GatewayPayPalPost.aspx.cs" Inherits="Gateway_GatewayPayPalPost" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Payment Processing</title>
    <style type="text/css">        
        h1.GatewayPosting {  color:#A1C1D9; margin: 30px;   text-align: center;  font-size: 24px;}
        .GatewayPostingWaringColor {  color: #a1c1d9;   font-weight: bold; }
        .GatewayPosting{   margin: 20px;    font-size: 16px;    text-align: center;}
        .font{ font-family:Verdana,Arial,Tahoma,Microsoft Sans Serif; }
        body{ background: rgb(255, 255, 255) url() no-repeat scroll 0% 0%; -moz-background-clip: border; -moz-background-origin: padding; -moz-background-inline-policy: continuous; font-family:Arial,Verdana,Tahoma,Microsoft Sans Serif;}
        form{ background: rgb(255, 255, 255) url() no-repeat scroll 0% 0%; -moz-background-clip: border; -moz-background-origin: padding; -moz-background-inline-policy: continuous;}        
    </style>
</head>
<body style="background-color: White; text-align: center;">
    <form id="aspnetForm" runat="server">
        <div>
            <asp:Literal ID="uxLiteral" runat="server" />
            <asp:HiddenField ID="uxUrlHidden" runat="server" />
            <br />
            <br />
            <h1 class="GatewayPosting">
                Processing Your Order...
            </h1>
            <p class="GatewayPosting GatewayPostingWaringColor">
                Please do NOT hit 'BACK' or 'REFRESH' button
            </p>
            <p class="GatewayPosting">
                Please wait while we are transferring you to the payment information page.
            </p>
            <p class="GatewayPosting">
                <img src="Images/Loading.gif" />
            </p>
            <p class="GatewayPosting">
                If you are not redirected within 10 seconds,
                <asp:HyperLink ID="uxRefreshLink" runat="server"> please click here. </asp:HyperLink>
            </p>

            <script language="javascript" type="text/javascript">
                document.forms[0].action = document.getElementById('uxUrlHidden').value;
                document.forms[0].__VIEWSTATE.value = '';
                document.forms[0].__VIEWSTATE.name = 'NOVIEWSTATE';
                document.forms[0].submit();
            </script>

        </div>
    </form>
</body>
</html>

