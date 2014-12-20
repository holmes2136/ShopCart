<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddToCartFrame.aspx.cs" Inherits="AddToCartFrame" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add to Cart</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:LinkButton ID="uxAddToCartButton" runat="server" Text="[$BtnAddtoCart]" CssClass="BtnStyle1"
        OnClick="uxAddToCartButton_Click" />
    </div>
    </form>
</body>
</html>
