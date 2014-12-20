<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EmailTemplateList.ascx.cs"
    Inherits="AdminAdvanced_MainControls_EmailTemplateList" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <GridTemplate>
        <asp:GridView ID="uxGrid" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
            AllowSorting="false" OnRowDataBound="uxGrid_RowDataBound" ShowFooter="false">
            <Columns>
                <asp:BoundField DataField="EmailTemplateDetailName" HeaderText="Template">
                    <ItemStyle HorizontalAlign="left" CssClass="bulletEmailTemplate" />
                    <HeaderStyle HorizontalAlign="left" Width="200px" CssClass="pdl10" />
                </asp:BoundField>
                <asp:BoundField DataField="Description" HeaderText="Description">
                    <HeaderStyle HorizontalAlign="left" CssClass="pdl10" />
                    <ItemStyle HorizontalAlign="left" CssClass="pdl10" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Edit">
                    <ItemTemplate>
                        <vevo:AdvancedLinkButton ID="uxEmailTemplateEditLink" runat="server" PageName="EmailTemplateEdit.ascx"
                            PageQueryString='<%# String.Format( "Name={0}", Eval("EmailTemplateDetailName") ) %>' 
                            ToolTip="Edit" OnClick="ChangePage_Click" 
                            StatusBarText='<%# String.Format( "Edit {0}", Eval("EmailTemplateDetailName") ) %>'>
                            <asp:Image ID="uxEditImage" runat="server" SkinID="IConEditInGrid" />
                        </vevo:AdvancedLinkButton></ItemTemplate>
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="<%$ Resources:CommonMessages, TableEmpty  %>"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>
    </GridTemplate>
</uc1:AdminContent>
