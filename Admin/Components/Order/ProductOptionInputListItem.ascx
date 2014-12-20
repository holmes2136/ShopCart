<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductOptionInputListItem.ascx.cs"
    Inherits="Admin_Components_Order_ProductOptionInputListItem" %>
<div class="OptionInputListItem">
    <div class="CommonOptionItemValidator">
        <asp:Label ID="uxInputListMessageLabel" runat="server" ForeColor="Red"></asp:Label>
    </div>
    <asp:DataList ID="uxInputDataList" runat="server" RepeatColumns="2" CssClass="OptionInputListItemDataList">
        <ItemTemplate>
            <div class="OptionInputListItemDataListItemDiv">
                <asp:TextBox ID="uxInputText" runat="server" Width="30px" ValidationGroup="ValidOption"></asp:TextBox><asp:CompareValidator
                    ID="uxInputListCompare" runat="server" ControlToValidate="uxInputText" ErrorMessage="Your input is invalid."
                    Operator="DataTypeCheck" Type="Integer" ValidationGroup="ValidOption">*</asp:CompareValidator>
                <asp:Label ID="uxInputLabel" runat="server" Text='<%# Eval( "PriceUp" ) %>'></asp:Label>
                <asp:HiddenField ID="uxNameHidden" runat="server" Value='<%# Eval( "Name" ) %>' />
                <asp:HiddenField ID="uxInputIDHidden" runat="server" Value='<%# Eval( "OptionItemID" ) %>' />
            </div>
        </ItemTemplate>
        <ItemStyle CssClass="OptionInputListItemDataListItemStyle" />
    </asp:DataList>
</div>