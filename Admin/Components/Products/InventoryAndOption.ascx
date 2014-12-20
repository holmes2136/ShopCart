<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InventoryAndOption.ascx.cs"
    Inherits="AdminAdvanced_Components_Products_InventoryAndOption" %>
<asp:Panel ID="uxUseInventoryTR" runat="server" CssClass="ProductDetailsRow">
    <asp:Label ID="lcUseInventory" runat="server" meta:resourcekey="lcUseInventory" CssClass="Label" />
    <asp:CheckBox ID="uxUseInventory" runat="server" Checked="true" AutoPostBack="true"
        OnCheckedChanged="uxUseInventory_CheckedChanged" CssClass="fl" />
    <div class="Clear">
    </div>
</asp:Panel>
<asp:Panel ID="uxProductOptionTR" runat="server" CssClass="ProductDetailsRow">
    <asp:Label ID="lcProductOptionLabel" runat="server" meta:resourcekey="lcProductOptionLabel"
        CssClass="Label" />
    <asp:Panel ID="uxOptionGroupTR" runat="server" CssClass="fl">
        <asp:GridView ID="uxOptionGroupGrid" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
            DataKeyNames="OptionGroupID" OnRowDataBound="uxGrid_RowDataBound" Width="360px">
            <FooterStyle BackColor="Tan" />
            <Columns>
                <asp:TemplateField HeaderText="Option">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:CheckBox ID="uxUseOptionCheck" runat="server" />
                    </ItemTemplate>
                    <HeaderStyle Width="100px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Option Stock">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:HiddenField ID="uxOptionGroupIDHidden" runat="server" Value='<%# Eval( "OptionGroupID" ) %>' />
                        <asp:CheckBox ID="uxUseStockCheck" runat="server" AutoPostBack="true" OnCheckedChanged="uxUseStockCheck_CheckedChanged"
                            Visible='<%# IsStockOptionVisible( Eval( "Type" ).ToString() ) %>' />
                    </ItemTemplate>
                    <HeaderStyle Width="100px" />
                </asp:TemplateField>
                <asp:BoundField DataField="Name" HeaderText="OptionGroup">
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl10" Width="150px" />
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl10" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <div class="Clear">
    </div>
</asp:Panel>
<asp:Panel ID="uxStockTR" runat="server" CssClass="ProductDetailsRow">
    <asp:Label ID="lcStock" runat="server" meta:resourcekey="lcStock" CssClass="Label" />
    <asp:TextBox ID="uxStockText" runat="server" Width="70px" CssClass="TextBox" />
    <div class="validator1 fl">
        <span class="Asterisk">*</span>
    </div>
    <asp:RequiredFieldValidator ID="uxStockRequiredValidator" runat="server" ControlToValidate="uxStockText"
        ValidationGroup="VaildProduct" Display="Dynamic" CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Stock is required.
        <div class="CommonValidateDiv CommonValidateDivProductPrice">
        </div>  
    </asp:RequiredFieldValidator>
    <asp:CompareValidator ID="uxStockCompare" runat="server" ControlToValidate="uxStockText"
        Operator="DataTypeCheck" Type="Integer" ValidationGroup="VaildProduct" Display="Dynamic"
        CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Stock is invalid.
        <div class="CommonValidateDiv CommonValidateDivProductPrice">
        </div>
    </asp:CompareValidator>
    <div class="Clear">
    </div>
</asp:Panel>
<asp:Panel ID="uxStockOptionTR" runat="server" CssClass="ProductDetailsRow">
    <asp:Label ID="lcStockOption" runat="server" meta:resourcekey="lcStock" CssClass="Label" />
    <div class="fl">
        <asp:GridView ID="uxStockOptionGrid" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
            ShowHeader="True" OnRowDataBound="uxGrid_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="Name">
                    <ItemTemplate>
                        <asp:Label ID="uxStockOptionLabel" runat="server" Text='<%# Eval( "CossGroup" ) %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="200px" />
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl10" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemStyle HorizontalAlign="center" />
                    <ItemTemplate>
                        <asp:CheckBox ID="uxStockEnabledCheck" runat="server" Checked="true" Visible="false" />
                    </ItemTemplate>
                    <HeaderStyle />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Stock">
                    <ItemTemplate>
                        <asp:HiddenField ID="uxOptionCombinationIDHidden" runat="server" />
                        <asp:TextBox ID="uxStockOptionText" runat="server" Width="80px" CssClass="TextBox"></asp:TextBox>
                        <div class="validator1">
                            <span class="Asterisk">*</span>
                        </div>
                        <asp:RequiredFieldValidator ID="uxStockOptionRequiredValidator" runat="server" ControlToValidate="uxStockOptionText"
                            ValidationGroup="VaildProduct" Display="Dynamic" CssClass="CommonValidatorText CommonValidatorTextStockOption">
                            <div class="CommonValidateDiv CommonValidateDivProductStockOption">
                            </div>
                            <img src="../Images/Design/Bullet/RequiredFillBullet_Up.gif" />
                            <asp:Label ID="uxStockOptionRequiredLabel" runat="server" Text='<%# CreateStockRequiredMessage(Eval( "CossGroup" ).ToString()) %>'></asp:Label>
                        </asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="uxStockOptionCompareValidator" runat="server" ControlToValidate="uxStockOptionText"
                            Operator="DataTypeCheck" Type="Integer" ValidationGroup="VaildProduct" Display="Dynamic"
                            CssClass="CommonValidatorText CommonValidatorTextStockOption">
                            <div class="CommonValidateDiv CommonValidateDivProductStockOption">
                            </div>
                            <img src="../Images/Design/Bullet/RequiredFillBullet_Up.gif" />
                            <asp:Label ID="uxStockOptionCompareLabel" runat="server" Text='<%# CreateStockCompareMessage(Eval( "CossGroup" ).ToString()) %>'></asp:Label>
                        </asp:CompareValidator>
                    </ItemTemplate>
                    <HeaderStyle Width="100px" />
                    <ItemStyle CssClass="pdl10" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div class="Clear">
    </div>
</asp:Panel>
