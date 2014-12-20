<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ContentDetailsConfig.ascx.cs"
    Inherits="AdminAdvanced_Components_ContentDetailsConfig" %>
<%@ Register Src="Message.ascx" TagName="Message" TagPrefix="uc2" %>
<%@ Register Src="../Components/Template/AdminUserControlContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<uc1:AdminContent ID="uxAdminContent" runat="server">
    <MessageTemplate>
        <uc2:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <ValidationDenotesTemplate>
        <div class="topline">
            <div class="RequiredLabel c6">
                <asp:Label ID="uxSearchLabel" runat="server" meta:resourcekey="uxSearchLabel" /></div>
        </div>
    </ValidationDenotesTemplate>
    <ContentTemplate>
        <asp:Panel ID="uxGridPanel" runat="server">
            <asp:GridView ID="uxGridContent" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
                ShowHeader="true" ShowFooter="false" OnRowDataBound="uxGrid_RowDataBound">
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <input type="CheckBox" name="SelectAllCheckBox" onclick="SelectAllCheckBoxes(this , 'uxGridContent')" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="uxCheck" runat="server" Checked='<%# Eval("Display") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="Content" HeaderText="<%$ Resources:ProductFields, Name %>"
                        ReadOnly="True">
                        <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
                        <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </asp:Panel>
        <div class="mgt10">
            <vevo:AdvanceButton ID="uxButtonUpdateConfig" runat="server" meta:resourcekey="uxButtonUpdateConfig"
                CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                OnClick="uxButtonUpdateConfig_Click" OnClickGoTo="Top" />
            <div class="Clear" />
        </div>
    </ContentTemplate>
</uc1:AdminContent>
