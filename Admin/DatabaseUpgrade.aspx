<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DatabaseUpgrade.aspx.cs"
    Inherits="AdminAdvanced_DatabaseUpgrade" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Upgrade Database</title>
</head>
<body style="background-color: #ffffff;">
    <form id="form1" runat="server">
        <div style="width: 650px;">
            <h3>
                Database Upgrade</h3>
            <p>
                Please enter file for database upgrade (e.g. SQL_UpgradeTo15.txt). You can locate
                these files in "App_Data" folder.
            </p>
            <p>
                Your Configuration will also updated to the lastest version.
            </p>
            <p style="color: Red;">
                <strong>Warning:</strong> Please verify that your application connects to the correct
                database (i.e. Access or SQL Server).
            </p>
            <p style="color: Red;">
                We recommend you back up your database first.
            </p>
            <p style="color: Red;">
                <strong>Warning:</strong> The source code should be upgraded first to the new version
                before upgrading database. Please refer to VevoCart Installation Guide for more
                information.
            </p>
            <asp:TextBox ID="uxPathFileText" runat="server" Width="270px"></asp:TextBox>&nbsp;
            <asp:Button ID="uxReadfileButton" runat="server" OnClick="uxReadfileButton_Click"
                Text="ReadFile" Width="144px" />
            <br />
            <asp:Label ID="uxMessageLabel" runat="server" ForeColor="Red"></asp:Label><br />
            <br />
            <asp:TextBox ID="uxExecuteText" runat="server" Enabled="False" Height="312px" TextMode="MultiLine"
                Width="555px"></asp:TextBox>
            <br />
            <asp:CheckBox ID="uxUpdateNewconfigCheckBox" runat="server" Text="Update New Config."
                Visible="false" />
            <br />
            <asp:Button ID="uxExecuteButton" runat="server" Text="Upgrade Database" OnClick="uxExecuteButton_Click" /></div>
        <asp:Button ID="uxUpdateConfigOnlyButton" runat="server" Text="Update Config Only"
            OnClick="uxUpdateConfigOnlyButton_Click" Visible="false" />
    </form>
</body>
</html>
