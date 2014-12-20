<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductOptionTextItem.ascx.cs"
    Inherits="Admin_Components_Order_ProductOptionTextItem" %>
<div class="OptionTextItem">
    <table class="OptionTextItemTable">
        <tr>
            <td>
                <asp:Label ID="uxMessageLabel" runat="server" ForeColor="red" />
            </td>
        </tr>
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
                <asp:TextBox ID="uxOptionsText" runat="server" Height="80px" TextMode="MultiLine"
                    CssClass="OptionTextItemText"></asp:TextBox>
                <asp:Label ID="uxOptionTextLabel" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
</div>
