<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OptionItemsDetails.ascx.cs"
    Inherits="Components_OptionItemsDetails" %>
<%@ Register Src="PagingControl.ascx" TagName="PagingControl" TagPrefix="uc5" %>
<%@ Register Src="LanguageControl.ascx" TagName="LanguageControl" TagPrefix="uc4" %>
<%@ Register Src="Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Src="SearchFilter/SearchFilter.ascx" TagName="SearchFilter" TagPrefix="uc1" %>
<%@ Register Src="Template/AdminContent.ascx" TagName="AdminContent" TagPrefix="uc1" %>
<%@ Register Src="../Components/LanguageLabelPlus.ascx" TagName="LanaguageLabelPlus"
    TagPrefix="uc5" %>
<%@ Register Src="../Components/Common/Upload.ascx" TagName="Upload" TagPrefix="uc6" %>
<uc1:AdminContent ID="uxAdminContent" runat="server">
    <MessageTemplate>
        <uc3:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <LanguageControlTemplate>
        <uc4:LanguageControl ID="uxLanguageControl" runat="server" ShowLanguageDescription="true" />
    </LanguageControlTemplate>
    <ButtonEventInnerBoxTemplate>
        <vevo:AdvanceButton ID="uxAddOptionButton" runat="server" meta:resourcekey="lcAddOption"
            CssClassBegin="AdminButton" CssClassEnd="" CssClass="AdminButtonIconAdd CommonAdminButton"
            ShowText="true" OnClick="uxAddOptionButton_Click" OnClickGoTo="Top" />
    </ButtonEventInnerBoxTemplate>
    <FilterTemplate>
        <uc1:SearchFilter ID="uxSearchFilter" runat="server" />
    </FilterTemplate>
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
        <asp:Panel ID="uxConfirmPanel" runat="server" CssClass="ConfirmPanel1 b6 ac pdt10"
            SkinID="ConfirmPanel">
            <div class="ConfirmTitle">
                <asp:Label ID="uxConfirmLabel" runat="server" Text="<%$ Resources:ProductOptionMessages, DeleteConfirmation %>"></asp:Label></div>
            <div class="ConfirmButton mgt10">
                <vevo:AdvanceButton ID="uxOkButton" runat="server" Text="OK" CssClassBegin="fl mgt10 mgb10 mgl10"
                    CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter" OnClickGoTo="Top">
                </vevo:AdvanceButton>
                <vevo:AdvanceButton ID="uxCancelButton" runat="server" Text="Cancel" CssClassBegin="fr mgt10 mgb10 mgr10"
                    CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter"  OnClickGoTo="None">
                </vevo:AdvanceButton>
                <div class="Clear">
                </div>
            </div>
        </asp:Panel>
    </ButtonCommandTemplate>
    <PageNumberTemplate>
        <uc5:PagingControl ID="uxPagingControl" runat="server" />
    </PageNumberTemplate>
    <TopContentBoxTemplate>
        <asp:Label ID="lcOptionGroup" runat="server" meta:resourcekey="lcOptionGroup" />
        &nbsp
        <asp:Label ID="uxOptionGroupLabel" runat="server" />
    </TopContentBoxTemplate>
    <GridTemplate>
        <asp:GridView ID="uxOptionItemGrid" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
            OnRowUpdating="uxOptionItemGrid_RowUpdating" OnRowEditing="uxOptionItemGrid_RowEditing"
            OnRowCommand="uxOptionItemGrid_RowCommand" OnRowCancelingEdit="uxOptionItemGrid_CancelingEdit"
            DataKeyNames="OptionItemID" OnDataBound="uxOptionItemGrid_DataBound" AllowSorting="True"
            OnSorting="uxGrid_Sorting" OnRowDataBound="uxGrid_RowDataBound" ShowFooter="false">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <input type="CheckBox" name="SelectAllCheckBox" onclick="SelectAllCheckBoxes(this , 'uxOptionItemGrid')">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="uxCheck" runat="server" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:OptionItemFields, OptionItemID %>" SortExpression="OptionItemID">
                    <EditItemTemplate>
                        <asp:Label ID="uxOptionItemIDLabel" runat="server" Text='<%# Bind("OptionItemID") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxOptionItemIDLabel" runat="server" Text='<%# Bind("OptionItemID") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle Width="80px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:OptionItemFields, Name %>" SortExpression="Name">
                    <EditItemTemplate>
                        <asp:TextBox ID="uxNameText" runat="server" Text='<%# Bind("Name") %>' Width="100px"
                            CssClass="TextBox fl"></asp:TextBox>
                        <uc5:LanaguageLabelPlus ID="uxPlus1" runat="server" />
                        <div class="Clear">
                        </div>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxNameLabel" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" Width="125px" />
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:OptionItemFields, Addition %>" SortExpression="OptionItemType">
                    <EditItemTemplate>
                        <asp:DropDownList ID="uxPriceTypeDrop" runat="server" OnPreRender="uxPriceTypeDrop_PreRender">
                            <asp:ListItem Value="Price">Fixed Amount</asp:ListItem>
                            <asp:ListItem Value="Percentage">Percentage</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxTypeLabel" runat="server" Text='<%# ( Eval("OptionItemType").ToString() == "Percentage" ) ? "Percentage" : "Fixed Amount" %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle Width="120px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:OptionItemFields, Amount %>" SortExpression="PriceToAdd">
                    <EditItemTemplate>
                        <asp:TextBox ID="uxPriceTypeText" runat="server" Width="80px" Text='<%# ( Eval("OptionItemType").ToString() == "Percentage" ) ? Eval("PercentageChange") : Eval("PriceToAdd", "{0:f2}") %>'
                            CssClass="TextBox">
                        </asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxPriceTypeLabel" runat="server" Text='<%# ( Eval("OptionItemType").ToString() == "Percentage" ) ? Eval("PercentageChange").ToString() + "%" :  Eval( "PriceToAdd", "{0:n2}") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle Width="120px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:OptionItemFields, WeightToAdd %>" SortExpression="WeightToAdd">
                    <EditItemTemplate>
                        <asp:TextBox ID="uxWeightToAddText" runat="server" Text='<%# Bind("WeightToAdd", "{0:f2}") %>'
                            Width="80px" CssClass="TextBox"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxWeightToAddLabel" runat="server" Text='<%# Bind("WeightToAdd", "{0:n2}") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle Width="120px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:OptionItemFields, ImageFile %>" SortExpression="ImageFile">
                    <ItemTemplate>
                        <asp:Label ID="uxImageLabel" runat="server" Text='<%# Eval("ImageFile") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="uxImageFileText" runat="server" Text='<%# Eval("ImageFile") %>'
                            CssClass="TextBox" Width="250px"></asp:TextBox>
                        <uc6:Upload ID="uxUpload" runat="server" CheckType="Image" OnInit="uxUpload_Init"
                            PathDestination="Images/" LeftLabelClass="fl mgr10" ButtonWidth="100px" ButtonHeight="20px"
                            DestionationTextLabel="Destination" DestionationTextBoxWidth="180px" CssClass="OptionUploadUserControlPanel"
                            ButtonImage="Browse.png" ShowText="false" />
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Left" CssClass="pdt2 pdl15" />
                    <HeaderStyle CssClass="al pdl15" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Edit">
                    <EditItemTemplate>
                        <asp:LinkButton ID="uxUpdateLinkButton" CausesValidation="True" Text="<%$ Resources:OptionItemFields, CommandUpdate %>"
                            runat="server" CommandName="Update" CssClass="UnderlineDashed" />
                        <asp:LinkButton ID="uxCancelLinkButton" CausesValidation="False" Text="<%$ Resources:OptionItemFields, CommandCancel %>"
                            runat="server" CommandName="Cancel" CssClass="UnderlineDashed" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="uxEditLinkButton" CausesValidation="False" Text="<%$ Resources:OptionItemFields, CommandEdit %>"
                            runat="server" CommandName="Edit" CssClass="UnderlineDashed" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </GridTemplate>
    <BottomContentBoxTemplate>
        <asp:Panel ID="uxAddPanel" runat="server" CssClass="b15 GridBottomAddPanel">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;" class="GridInsertItemRowStyle">
                <tr>
                    <td style="width: 35px">
                        &nbsp;
                    </td>
                    <td style="width: 80px">
                        &nbsp;
                    </td>
                    <td style="width: 140px">
                        <div class="pdl15">
                            <asp:TextBox ID="uxNameText" runat="server" Width="100px" CssClass="TextBox fl"></asp:TextBox>
                            <uc5:LanaguageLabelPlus ID="uxPlus1" runat="server" />
                            <div class="Clear">
                            </div>
                        </div>
                    </td>
                    <td style="width: 120px;" class="ac">
                        <asp:DropDownList ID="uxPriceTypeDrop" runat="server">
                            <asp:ListItem Value="Price">Fixed Amount</asp:ListItem>
                            <asp:ListItem Value="Percentage">Percentage</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="width: 120px;" class="ac">
                        <asp:TextBox ID="uxPriceTypeText" runat="server" Width="80px" CssClass="TextBox"></asp:TextBox>
                    </td>
                    <td style="width: 120px;" class="ac">
                        <asp:TextBox ID="uxWeightToAddText" runat="server" Width="80px" CssClass="TextBox"></asp:TextBox>
                    </td>
                    <td class="pdt2 pdl15">
                        <asp:TextBox ID="uxImageFileText" runat="server" Text='<%# Eval("ImageFile") %>'
                            CssClass="TextBox" Width="250px"></asp:TextBox>
                        <uc6:Upload ID="uxUpload" runat="server" CheckType="Image" PathDestination="Images/"
                            LeftLabelClass="fl mgr10" ButtonWidth="72px" ButtonHeight="20px" DestionationTextLabel="Destination"
                            DestionationTextBoxWidth="180px" CssClass="OptionUploadUserControlPanel" ButtonImage="Browse.png"
                            ShowText="false" />
                    </td>
                    <td style="width: 80px; text-align: center;">
                        <vevo:AdvanceButton ID="uxAddOptionListButton" runat="server" Text="<%$ Resources:ShippingWeightRateFields, CommandAdd %>"
                            CssClassBegin="mgt10 mgl10 fl" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                            OnClick="AddOption_Click" OnClickGoTo="Top" ValidationGroup="VaildShipping">
                        </vevo:AdvanceButton>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </BottomContentBoxTemplate>
</uc1:AdminContent>
