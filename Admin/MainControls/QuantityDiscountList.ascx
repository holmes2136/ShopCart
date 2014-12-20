<%@ Control Language="C#" AutoEventWireup="true" CodeFile="QuantityDiscountList.ascx.cs"
    Inherits="AdminAdvanced_MainControls_QuantityDiscountList" %>
<%@ Register Src="../Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc3" %>
<%@ Register Src="../Components/SearchFilter/SearchFilter.ascx" TagName="SearchFilter"
    TagPrefix="uc2" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <MessageTemplate>
        <uc1:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <FilterTemplate>
        <uc2:SearchFilter ID="uxSearchFilter" runat="server" />
    </FilterTemplate>
    <ButtonEventInnerBoxTemplate>
        <vevo:AdvanceButton ID="uxAddButton" runat="server" meta:resourcekey="uxAddButton"
            CssClassBegin="AdminButton" CssClassEnd="" CssClass="AdminButtonIconAdd CommonAdminButton"
            ShowText="true" OnClick="uxAddButton_Click" OnClickGoTo="Top" />
    </ButtonEventInnerBoxTemplate>
    <ButtonCommandTemplate>
        <vevo:AdvanceButton ID="uxDeleteButton" runat="server" meta:resourcekey="uxDeleteButton"
            CssClass="AdminButtonDelete CommonAdminButton" CssClassBegin="AdminButton" CssClassEnd=""
            ShowText="true" OnClick="uxDeleteButton_Click" />
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
                <asp:Label ID="uxConfirmLabel" runat="server" Text="Are you sure to delete selected item(s)?"></asp:Label></div>
            <div class="ConfirmButton mgt10">
                <vevo:AdvanceButton ID="uxOkButton" runat="server" Text="OK" CssClassBegin="fl mgt10 mgb10 mgl10"
                    CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter" OnClickGoTo="Top">
                </vevo:AdvanceButton>
                <vevo:AdvanceButton ID="uxCancelButton" runat="server" Text="Cancel" CssClassBegin="fr mgt10 mgb10 mgr10"
                    CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter" OnClickGoTo="None">
                </vevo:AdvanceButton>
                <div class="Clear">
                </div>
            </div>
        </asp:Panel>
    </ButtonCommandTemplate>
    <PageNumberTemplate>
        <uc3:PagingControl ID="uxPagingControl" runat="server" />
    </PageNumberTemplate>
    <GridTemplate>
        <asp:GridView ID="uxDiscountGroupGrid" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
            DataKeyNames="DiscountGroupID" OnDataBound="uxDiscountGroupGrid_DataBound" OnRowCommand="uxDiscountGroupGrid_RowCommand"
            OnRowUpdating="uxDiscountGroupGrid_RowUpdating" AllowSorting="True" OnSorting="uxGrid_Sorting"
            OnRowDataBound="uxGrid_RowDataBound" ShowFooter="false" OnRowEditing="uxDiscountGroupGrid_RowEditing"
            OnRowCancelingEdit="uxDiscountGroupGrid_CancelingEdit">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <input type="CheckBox" name="SelectAllCheckBox" onclick="SelectAllCheckBoxes(this , 'uxDiscountGroupGrid')">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="uxCheck" runat="server" />
                    </ItemTemplate>
                    <EditItemTemplate>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle Width="35px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:DiscountGroupFields, DiscountGroupID %>"
                    SortExpression="DiscountGroupID">
                    <EditItemTemplate>
                        <asp:Label ID="uxDiscountGroupIDLable" runat="server" Text='<%# Eval("DiscountGroupID") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxDiscountGroupIDLable" runat="server" Text='<%# Eval("DiscountGroupID") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle Width="70px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:DiscountGroupFields, GroupName %>" SortExpression="GroupName">
                    <EditItemTemplate>
                        <asp:TextBox ID="uxGroupNameText" runat="server" Text='<%# Eval("GroupName") %>'
                            CssClass="TextBox"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxGroupNameLable" runat="server" Text='<%# Eval("GroupName") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="uxGroupNameText" runat="server" CssClass="TextBox"></asp:TextBox>
                    </FooterTemplate>
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" />
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
                    <FooterStyle HorizontalAlign="Left" CssClass="pdl15" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Discount Type" SortExpression="DiscountType">
                    <ItemTemplate>
                        <asp:Label ID="uxDiscountTypeLabel" runat="server" Text='<%# Eval("DiscountType") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="uxDiscountTypeDrop" runat="server" OnPreRender="uxDiscountTypeDrop_PreRender">
                            <asp:ListItem>Percentage</asp:ListItem>
                            <asp:ListItem>Price</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="uxDiscountTypeDrop" runat="server">
                            <asp:ListItem>Percentage</asp:ListItem>
                            <asp:ListItem>Price</asp:ListItem>
                        </asp:DropDownList>
                    </FooterTemplate>
                    <HeaderStyle HorizontalAlign="center" />
                    <ItemStyle HorizontalAlign="center" />
                    <FooterStyle HorizontalAlign="center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Apply discount to product option" SortExpression="ProductOptionDiscount">
                    <ItemTemplate>
                        <asp:Label ID="uxProductOptionDiscountLabel" runat="server" Text='<%# ( Eval("ProductOptionDiscount").ToString() == "True" ) ? "Yes" : "No" %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="uxProductOptionDiscount" runat="server" OnPreRender="uxProductOptionDiscountLabel_PreRender">
                            <asp:ListItem Value="False">No</asp:ListItem>
                            <asp:ListItem Value="True">Yes</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="uxProductOptionDiscount" runat="server">
                            <asp:ListItem Value="False">No</asp:ListItem>
                            <asp:ListItem Value="True">Yes</asp:ListItem>
                        </asp:DropDownList>
                    </FooterTemplate>
                    <HeaderStyle Width="250px" />
                    <ItemStyle HorizontalAlign="Center" />
                    <FooterStyle HorizontalAlign="center" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <EditItemTemplate>
                        <asp:LinkButton ID="uxUpdateLinkButton" runat="server" CausesValidation="True" CommandName="Update"
                            Text="<%$ Resources:DiscountGroupFields, CommandUpdate %>" CssClass="UnderlineDashed" />
                        <asp:LinkButton ID="uxCancelLinkButton" runat="server" CausesValidation="False" CommandName="Cancel"
                            Text="<%$ Resources:DiscountGroupFields, CommandCancel %>" CssClass="UnderlineDashed" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="uxEditLinkButton" runat="server" CausesValidation="False" CommandName="Edit"
                            ToolTip="<%$ Resources:DiscountGroupFields, CommandEdit %>" Text="<%$ Resources:DiscountGroupFields, CommandEdit %>"
                            OnPreRender="uxEditLinkButton_PreRender" CssClass="UnderlineDashed" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <vevo:AdvanceButton ID="uxAddButton" runat="server" Text="<%$ Resources:DiscountGroupFields, CommandAdd %>"
                            CssClassBegin="mgCenter" CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter" CommandName="Add" OnClickGoTo="Top"></vevo:AdvanceButton>
                    </FooterTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <FooterStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <vevo:AdvancedLinkButton ID="uxRuleListLinkButton" runat="server" PageName="QuantityDiscountRuleList.ascx"
                            PageQueryString='<%# String.Format( "DiscountGroupID={0}", Eval("DiscountGroupID") )%>'
                            ToolTip="<%$ Resources:DiscountGroupFields, ViewRule %>" Text="<%$ Resources:DiscountGroupFields, ViewRule %>"
                            OnClick="ChangePage_Click" StatusBarText='<%# String.Format( "Rule of {0} list.", Eval("GroupName") ) %>'
                            CssClass="UnderlineDashed" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="<%$ Resources:CommonMessages, TableEmpty  %>"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>
    </GridTemplate>
    <BottomContentBoxTemplate>
        <asp:HiddenField ID="uxStatusHidden" runat="server" />
    </BottomContentBoxTemplate>
</uc1:AdminContent>
