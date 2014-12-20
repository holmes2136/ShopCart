<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DatabaseTester.aspx.cs" Inherits="DatabaseTesterPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DataBase Tester</title>
</head>
<body style="background-color: #fff">
    <div class="DatabaseTester">
        <form id="form1" runat="server">
            <div style="text-align: center;">
                <h4>
                    DataBase Tester</h4>
            </div>
            <div style="width: 100%; border: solid 1px #dcdcdc; margin: 10px auto 10px auto;
                background-color: #faebd7; padding: 5px 5px 5px 5px;">
                <asp:DropDownList ID="uxFileTestDrop" runat="server">
                    <asp:ListItem Value="WebConfig" Selected="True">Web.config File</asp:ListItem>
                    <asp:ListItem Value="Custom">Custom</asp:ListItem>
                </asp:DropDownList>
                <br />
                <br />
                <div id="uxConnectionStringPrefixDIV" runat="server">
                </div>
                <br />
                <br />
                <table cellpadding="3">
                    <tr>
                        <td valign="top">
                            Connection String:
                        </td>
                        <td>
                            <asp:TextBox ID="uxConnectionStringText" runat="server" Width="350px" Enabled="false"
                                Height="62px" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Provider:
                        </td>
                        <td>
                            <asp:DropDownList ID="uxProviderDrop" runat="server" Enabled="false">
                                <asp:ListItem Selected="True">System.Data.OleDb</asp:ListItem>
                                <asp:ListItem>System.Data.SqlClient</asp:ListItem>
                                <asp:ListItem>System.Data.Odbc</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                </table>
                <br />
                <asp:Button ID="uxTestConnectionStringButton" runat="server" Text="Test Connection"
                    OnClick="uxTestConnectionStringButton_Click" Width="160px" />&nbsp;
                <asp:Button ID="uxTestReadButton" runat="server" Text="Test Reading" OnClick="uxTestReadButton_Click"
                    Width="160px" />
                <asp:Button ID="uxTestWriteButton" runat="server" Text="Test Writing" OnClick="uxTestWriteButton_Click"
                    Width="160px" />&nbsp;
                <br />
                <br />
                <asp:Literal ID="uxResultLiteral" runat="server"></asp:Literal></div>
        </form>
    </div>
</body>
</html>
