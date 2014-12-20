<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MultiShippingZone.ascx.cs"
    Inherits="Admin_Components_MultiShippingZone" %>
<asp:HiddenField ID="uxZoneIDHidden" runat="server" Value="" />
<table class="MultiCatalogBackground">
    <tr>
        <td style="width: 230px">
            <div id="uxFromDiv" class="MultiCatalogListBox">
                <div>
                    <asp:ListBox ID="uxZoneFromList" runat="server" SelectionMode="Multiple" CssClass="MultiCatalogInnerListBox">
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
                    <asp:ListBox ID="uxZoneToList" runat="server" SelectionMode="Multiple" CssClass="MultiCatalogInnerListBox">
                    </asp:ListBox>
                </div>
            </div>
        </td>
    </tr>
</table>