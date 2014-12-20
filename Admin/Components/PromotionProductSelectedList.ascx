<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PromotionProductSelectedList.ascx.cs"
    Inherits="Admin_Components_PromotionProductSelectedList" %>
<%@ Register Src="../Components/Template/AdminTabContent.ascx" TagName="AdminTabContent"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Import Namespace="Vevo.Shared.Utilities" %>
<div class="CssAdminContentMessage">
    <uc3:Message ID="uxMessage" runat="server" />
</div>
<vevo:AdvanceButton ID="uxAddButton" runat="server" Text="Add Promotion Product"
    CssClassBegin="AdminButton" CssClassEnd="" CssClass="AdminButtonIconAdd CommonAdminButton fl"
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
        <asp:Label ID="uxConfirmLabel" runat="server" Text="<%$ Resources:PromotionSubGroupMessage, DeleteConfirmation %>"></asp:Label>
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
<asp:Panel ID="uxPromotionProductGridPanel" runat="server" CssClass="mgt5">
    <asp:GridView ID="uxPromotionProductGrid" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
        DataKeyNames="PromotionSubGroupID" OnRowDataBound="uxGrid_RowDataBound" ShowFooter="false">
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                    <input type="checkbox" name="SelectAllCheckBox" onclick="SelectAllCheckBoxes(this ,'uxPromotionProductGrid')" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="uxCheck" runat="server" />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" Width="35px" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:BoundField DataField="ProductID" HeaderText="<%$ Resources: PromotionProductFields, ProductID %>"
                SortExpression="ProductID">
                <ItemStyle HorizontalAlign="Center" Width="80px" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="<%$ Resources: PromotionProductFields, Name %>">
                <ItemTemplate>
                    <asp:Label ID="uxPromotionProductName" runat="server" Text='<%#GetProductName(Eval("ProductID")) %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" />
                <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources: PromotionProductFields, Options %>">
                <ItemTemplate>
                    <asp:Label ID="uxPromotionOptions" runat="server" Text='<%#GetOptionName(Eval("ProductID"),Eval("OptionItemID")) %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" />
                <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources: PromotionProductFields, Quantity %>">
                <ItemTemplate>
                    <asp:Label ID="uxPromotionQuantity" runat="server" Text='<%# Eval("Quantity") %>' />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="60px" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Panel>
<%--    </ContentTemplate>
</uc1:AdminTabContent>--%>
<asp:HiddenField ID="uxStatusHidden" runat="server" />
