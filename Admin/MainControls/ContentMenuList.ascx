<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ContentMenuList.ascx.cs"
    Inherits="AdminAdvanced_MainControls_ContentMenu"  %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Src="../Components/StoreFilterDrop.ascx" TagName="StoreFilterDrop" TagPrefix="uc2" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <MessageTemplate>
        <uc3:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <SpecialFilterTemplate>
        <uc2:StoreFilterDrop ID="uxStoreFilterDrop" runat="server" />
    </SpecialFilterTemplate>
     <FilterTemplate>
     </FilterTemplate>
    <GridTemplate>
        <asp:GridView ID="uxTopContentMenuGrid" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
            DataKeyNames="ContentMenuID" AllowSorting="True" OnSorting="uxGrid_Sorting" ShowFooter="false"
            OnRowUpdating="uxTopContentMenuGrid_RowUpdating" OnRowCommand="uxTopContentMenuGrid_RowCommand"
            OnRowCancelingEdit="uxTopContentMenuGrid_CancelingEdit" OnRowEditing="uxTopContentMenuGrid_RowEditing">
            <Columns>
                <asp:TemplateField HeaderText="ID" SortExpression="ContentMenuID">
                    <EditItemTemplate>
                        <asp:Label ID="uxContentMenuIDLabel" runat="server" Text='<%# Bind("ContentMenuID") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxContentMenuIDLabel" runat="server" Text='<%# Bind("ContentMenuID") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle Width="60px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:ContentMenuItemFields, Position %>">
                    <ItemTemplate>
                        <asp:Label ID="uxPositionLabel" runat="server" Text='<%# GetName( Eval("ContentMenuID").ToString()) %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" Width="150px"  />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:ContentMenuItemFields, Style %>">
                    <EditItemTemplate>
                        <asp:DropDownList ID="uxContentMenuTypeDrop" runat="server" OnPreRender="uxContentMenuTypeDrop_PreRender">
                            <asp:ListItem Value="default" Text="Default" />
                            <asp:ListItem Value="cascade" Text="Cascade" />
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxTypeLabel" runat="server" Text='<%# GetType( Eval("ContentMenuID").ToString()) %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                     <HeaderStyle Width="150px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:ContentMenuItemFields, Enabled %>">
                    <EditItemTemplate>
                        <asp:CheckBox ID="uxContentEnabledCheck" runat="server" Checked='<%#  Eval("IsEnabled") %>' />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxContentMenuEnabledLabel" runat="server" Text='<%# ( Eval("IsEnabled").ToString()) %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle Width="140px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:ContentMenuItemFields, CommandOptionItems %>">
                    <ItemTemplate>
                        <vevo:AdvancedLinkButton ID="uxItemListLinkButton" runat="server" OnClick="ChangePage_Click"
                            ToolTip="<%$ Resources:ContentMenuItemFields, CommandOptionItems %>" PageName="ContentMenuItemList.ascx"
                            PageQueryString='<%# GetPageQueryString(Eval("ContentMenuID").ToString() )%>'>
                            <asp:Image ID="uxReviewImage" runat="server" SkinID="IconReviewInGrid" />
                        </vevo:AdvancedLinkButton>
                    </ItemTemplate>
                    <HeaderStyle />
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" Width="160px" />
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
                    <HeaderStyle HorizontalAlign="Center" Width="160px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </GridTemplate>
</uc1:AdminContent>
