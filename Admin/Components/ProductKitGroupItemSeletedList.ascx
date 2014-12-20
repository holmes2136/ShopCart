<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductKitGroupItemSeletedList.ascx.cs"
    Inherits="Admin_Components_ProductKitGroupItemSeletedList" %>
<%@ Register Src="../Components/Template/AdminTabContent.ascx" TagName="AdminTabContent"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Import Namespace="Vevo.Shared.Utilities" %>
<div class="CssAdminContentMessage">
    <uc3:Message ID="uxMessage" runat="server" />
</div>
<vevo:AdvanceButton ID="uxAddButton" runat="server" Text="Add Product Kit Group Item"
    CssClassBegin="AdminButton" CssClassEnd="" CssClass="AdminButtonIconAdd CommonAdminButton fl pdr5"
    ShowText="true" OnClick="uxAddButton_Click" />
<vevo:AdvanceButton ID="uxDeleteButton" runat="server" Text="Delete" CssClass="AdminButtonDelete CommonAdminButton"
    CssClassBegin="AdminButton" CssClassEnd="" ShowText="true" OnClick="uxDeleteButton_Click" />
<asp:Button ID="uxDummyButton" runat="server" Text="" CssClass="dn" />
<ajaxToolkit:ConfirmButtonExtender ID="uxDeleteConfirmButton" runat="server" TargetControlID="uxDeleteButton"
    ConfirmText="" DisplayModalPopupID="uxConfirmModalPopup">
</ajaxToolkit:ConfirmButtonExtender>
<ajaxToolkit:ModalPopupExtender ID="uxConfirmModalPopup" runat="server" TargetControlID="uxDeleteButton"
    CancelControlID="uxCancelButton" OkControlID="uxOkButton" PopupControlID="uxConfirmPanel"
    BackgroundCssClass="ConfirmBackground b7" DropShadow="true" RepositionMode="None">
</ajaxToolkit:ModalPopupExtender>
<asp:Panel ID="uxConfirmPanel" runat="server" CssClass="ConfirmPanel b6 ac pdt10"
    SkinID="ConfirmPanel">
    <div class="ConfirmTitle">
        <asp:Label ID="uxConfirmLabel" runat="server" Text="<%$ Resources:ProductKitGroupItemMessages, DeleteConfirmation %>"></asp:Label>
    </div>
    <div class="ConfirmButton mgt10">
        <vevo:AdvanceButton ID="uxOkButton" runat="server" Text="OK" 
            CssClassBegin="fl mgt10 mgb10 mgl10" CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter"
            OnClickGoTo="Top" />
        <vevo:AdvanceButton ID="uxCancelButton" runat="server" Text="Cancel" 
            CssClassBegin="fr mgt10 mgb10 mgr10" CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter"
            OnClickGoTo="None" />
    </div>
</asp:Panel>
<asp:Panel ID="uxProductKitGroupItemGridPanel" runat="server" CssClass="mgt5">
    <asp:GridView ID="uxProductKitGroupItemGrid" runat="server" CssClass="Gridview1"
        SkinID="DefaultGridView" DataKeyNames="ProductID" ShowFooter="false" AllowSorting="true"
        OnSorting="uxGrid_Sorting" OnRowDataBound="uxGrid_RowDataBound" OnRowUpdating="uxGrid_RowUpdating"
        OnRowCancelingEdit="uxGrid_CancelingEdit" OnRowEditing="uxGrid_RowEditing">
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                    <input type="checkbox" name="SelectAllCheckBox" onclick="SelectAllCheckBoxes(this ,'uxProductKitGroupItemGrid')" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="uxCheck" runat="server" />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" Width="35px" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources: ProductKitGroupItemFields, ProductID %>"
                SortExpression="ProductID">
                <EditItemTemplate>
                    <asp:Label ID="uxProductIDLabel" runat="server" Text='<%# Bind("ProductID") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="uxProductIDLabel" runat="server" Text='<%# Bind("ProductID") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="60px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources: ProductKitGroupItemFields, Name %>"
                SortExpression="Name">
                <EditItemTemplate>
                    <asp:Label ID="uxProductNameLabel" runat="server" Text='<%#GetProductName(Eval("ProductID")) %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="uxProductNameLabel" runat="server" Text='<%#GetProductName(Eval("ProductID")) %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" />
                <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources: ProductKitGroupItemFields, Quantity %>"
                SortExpression="Quantity">
                <EditItemTemplate>
                    <asp:TextBox ID="uxQuantityText" runat="server" Text='<%# Bind("Quantity") %>' Width="60px"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="uxQuantityLabel" runat="server" Text='<%# Bind("Quantity") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Right" Width="60px" CssClass="pdr15" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources: ProductKitGroupItemFields, IsUserDefinedQuantity %>"
                SortExpression="IsUserDefinedQuantity">
                <EditItemTemplate>
                    <asp:CheckBox ID="uxIsUserDefinedQuantityCheck" runat="server" Checked='<%#  Eval("IsUserDefinedQuantity") %>' />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="uxIsUserDefinedQuantityLable" runat="server" Text='<%# Bind("IsUserDefinedQuantity") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="150px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources: ProductKitGroupItemFields, IsDefault %>"
                SortExpression="IsDefault">
                <EditItemTemplate>
                    <asp:CheckBox ID="uxIsDefaultCheck" runat="server" Checked='<%#  Eval("IsDefault") %>' />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="uxIsDefaultLabel" runat="server" Text='<%# Bind("IsDefault") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="80px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Edit">
                <EditItemTemplate>
                    <asp:LinkButton ID="uxUpdateLinkButton" CausesValidation="True" Text="<%$ Resources:ContentMenuItemFields, CommandUpdate %>"
                        runat="server" CommandName="Update" CssClass="UnderlineDashed" />
                    <asp:LinkButton ID="uxCancelLinkButton" CausesValidation="False" Text="<%$ Resources:ContentMenuItemFields, CommandCancel %>"
                        runat="server" CommandName="Cancel" CssClass="UnderlineDashed" />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="uxEditLinkButton" CausesValidation="False" Text="<%$ Resources:ContentMenuItemFields, CommandEdit %>"
                        runat="server" CommandName="Edit" CssClass="UnderlineDashed" Visible='<%# IsAdminModifiable()%>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Panel>
<asp:HiddenField ID="uxStatusHidden" runat="server" />
