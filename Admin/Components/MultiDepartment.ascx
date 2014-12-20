<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MultiDepartment.ascx.cs" Inherits="AdminAdvanced_Components_MultiDepartment" %>
<asp:HiddenField ID="uxDepartmentIDHidden" runat="server" Value="" />
<table class="MultiDepartmentBackground">
    <tr>
        <td style="width: 230px">
            <div id="uxFromDiv" class="MultiDepartmentListBox">
                <div>
                    <asp:ListBox ID="uxDepartmentFromList" runat="server" SelectionMode="Multiple" CssClass="MultiDepartmentInnerListBox">
                    </asp:ListBox>
                </div>
            </div>
        </td>
        <td style="text-align: center; width: 60px;">
            <div>
                <vevo:AdvanceButton ID="uxFromButton" runat="server" CssClassBegin="AdminButton"
                    CssClassEnd="" CssClass="AdminButtonFrom CommonAdminButton" ShowText="false"
                    OnLoad="uxFromButton_Load" Width="20px" /><br />
            </div>
            <div>
                <vevo:AdvanceButton ID="uxToButton" runat="server" CssClassBegin="AdminButton" CssClassEnd=""
                    CssClass="AdminButtonTo CommonAdminButton" ShowText="false" OnLoad="uxToButton_Load" Width="20px" />
            </div>
        </td>
        <td style="width: 230px">
            <div id="uxToDiv" class="MultiDepartmentListBox">
                <div>
                    <asp:ListBox ID="uxDepartmentToList" runat="server" SelectionMode="Multiple" CssClass="MultiDepartmentInnerListBox">
                    </asp:ListBox>
                </div>
            </div>
        </td>
    </tr>
</table>