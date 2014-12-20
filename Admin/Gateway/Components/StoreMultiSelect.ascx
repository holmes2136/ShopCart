<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StoreMultiSelect.ascx.cs"
    Inherits="Admin_Gateway_Components_StoreMultiSelect" %>
<div class="CommonRowStyle" id="uxStoreListDiv" runat="server">
    <asp:Label ID="uxStoreListLabel" runat="server" Text="Enable for stores" CssClass="Label" />
    <asp:GridView ID="uxStoreGrid" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
        DataKeyNames="StoreID" Width="360px">
        <FooterStyle BackColor="Tan" />
        <Columns>
            <asp:TemplateField HeaderText="Enable">
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:CheckBox ID="uxEnableStoreCheck" runat="server" Checked="true" />
                </ItemTemplate>
                <HeaderStyle Width="100px" />
            </asp:TemplateField>
            <asp:BoundField DataField="StoreName" HeaderText="Store">
                <HeaderStyle HorizontalAlign="Left" CssClass="pdl10" Width="150px" />
                <ItemStyle HorizontalAlign="Left" CssClass="pdl10" />
            </asp:BoundField>
        </Columns>
    </asp:GridView>
</div>
