<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MultiSubGroup.ascx.cs"
    Inherits="Admin_Components_MultiSubGroup" %>
<asp:HiddenField ID="uxSubGroupIDHidden" runat="server" Value="" />
<table class="MultiSubGroupBackground">
    <tr>
        <td style="width: 230px">
            <div id="uxFromDiv" class="MultiSubGroupListBox">
                <div>
                    <asp:ListBox ID="uxSubGroupFromList" runat="server" SelectionMode="Multiple" CssClass="MultiSubGroupInnerListBox">
                    </asp:ListBox>
                </div>
            </div>
        </td>
        <td style="text-align: center; width: 60px;">
            <div>
                <vevo:AdvanceButton ID="uxFromButton" runat="server" CssClassBegin="AdminButton"
                    CssClassEnd="" CssClass="AdminButtonFrom CommonAdminButton" ShowText="false"
                    OnLoad="uxFromButton_Load" Width="20px" />
                <br />
            </div>
            <div>
                <vevo:AdvanceButton ID="uxToButton" runat="server" CssClassBegin="AdminButton" CssClassEnd=""
                    CssClass="AdminButtonTo CommonAdminButton" ShowText="false" OnLoad="uxToButton_Load"
                    Width="20px" />
            </div>
        </td>
        <td style="width: 230px">
            <div id="uxToDiv" class="MultiSubGroupListBox">
                <div>
                    <asp:ListBox ID="uxSubGroupToList" runat="server" SelectionMode="Multiple" CssClass="MultiSubGroupInnerListBox">
                    </asp:ListBox>
                </div>
            </div>
        </td>
    </tr>
</table>
