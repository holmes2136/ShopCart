<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OptionTextItem.ascx.cs"
    Inherits="Components_OptionTextItem" %>
<div class="OptionTextItem">
    <table class="OptionTextItemTable">
        <tr>
            <td>
                <div class="OptionTextItemCheckDiv">
                    <asp:CheckBox ID="uxOptionCheck" runat="server" />
                    <asp:Label ID="uxOptionsLabel" runat="server" CssClass="OptionTextItemLabelCheckBox" />
                    <div class="Clear">
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div id="uxMessageDiv" class="TextOptionItemValidator" visible="false" runat="server">
                    <img src="Images/Design/Bullet/RequiredFillBullet_Down.gif" />
                    <asp:Label ID="uxMessageLabel" runat="server" ForeColor="Red"></asp:Label>
                    <div class="TextOptionValidateDiv">
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="uxOptionsText" runat="server" Height="80px" TextMode="MultiLine"
                    CssClass="OptionTextItemText"></asp:TextBox>
                <asp:Label ID="uxOptionTextLabel" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
</div>
