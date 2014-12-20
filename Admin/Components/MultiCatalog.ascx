<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MultiCatalog.ascx.cs"
    Inherits="AdminAdvanced_Components_MultiCatalog" %>
<asp:HiddenField ID="uxCategoryIDHidden" runat="server" Value="" />
<table class="MultiCatalogBackground">
    <tr>
        <td style="width: 230px">
            <div id="uxFromDiv" class="MultiCatalogListBox">
                <div>
                    <asp:ListBox ID="uxCategoryFromList" runat="server" SelectionMode="Multiple" CssClass="MultiCatalogInnerListBox">
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
            <div id="uxToDiv" class="MultiCatalogListBox">
                <div>
                    <asp:ListBox ID="uxCategoryToList" runat="server" SelectionMode="Multiple" CssClass="MultiCatalogInnerListBox">
                    </asp:ListBox>
                </div>
            </div>
        </td>
    </tr>
</table>
