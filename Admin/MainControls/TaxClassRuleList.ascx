<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TaxClassRuleList.ascx.cs"
    Inherits="AdminAdvanced_MainControls_TaxClassRuleList" %>
<%@ Register Src="../Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc7" %>
<%@ Register Src="../Components/SearchFilter/SearchFilter.ascx" TagName="SearchFilter"
    TagPrefix="uc6" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc4" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/CountryList.ascx" TagName="CountryList" TagPrefix="uc4" %>
<%@ Register Src="../Components/StateList.ascx" TagName="StateList" TagPrefix="uc5" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <MessageTemplate>
        <uc4:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <ValidationSummaryTemplate>
        <asp:ValidationSummary ID="uxValidationSummary" runat="server" meta:resourcekey="uxValidationSummary"
            ValidationGroup="ValidTax" CssClass="ValidationStyle" />
    </ValidationSummaryTemplate>
    <ButtonEventInnerBoxTemplate>
        <vevo:AdvanceButton ID="uxAddButton" runat="server" meta:resourcekey="uxAddLinkButton"
            CssClassBegin="AdminButton" CssClassEnd="" CssClass="AdminButtonIconAdd CommonAdminButton"
            ShowText="true" OnClick="uxAddButton_Click" OnClickGoTo="Top" />
    </ButtonEventInnerBoxTemplate>
    <FilterTemplate>
        <uc6:SearchFilter ID="uxSearchFilter" runat="server" />
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
        <asp:Panel ID="uxConfirmPanel" runat="server" CssClass="ConfirmPanel b6 ac pdt10"
            SkinID="ConfirmPanel">
            <div class="ConfirmTitle">
                <asp:Label ID="uxConfirmLabel" runat="server" Text="Delete Tax Class"></asp:Label></div>
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
        <uc7:PagingControl ID="uxPagingControl" runat="server" />
    </PageNumberTemplate>
    <GridTemplate>
        <asp:GridView ID="uxTaxClassRuleGrid" runat="Server" CssClass="Gridview1" SkinID="DefaultGridView"
            DataKeyNames="TaxClassRuleID" OnDataBound="uxTaxClassRuleGrid_DataBound" OnRowCommand="uxTaxClassRuleGrid_RowCommand"
            OnRowUpdating="uxTaxClassRuleGrid_RowUpdating" AllowSorting="true" OnRowEditing="uxTaxClassRuleGrid_RowEditing"
            OnRowCancelingEdit="uxTaxClassRuleGrid_CancelingEdit" OnSorting="uxTaxClassRuleGrid_Sorting"
            AutoGenerateColumns="False" ShowFooter="False">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <input type="CheckBox" name="SelectAllCheckBox" onclick="SelectAllCheckBoxes(this , 'uxTaxClassRuleGrid')">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="uxCheck" runat="server" Visible='<%# IsVisible( Eval("IsDefaultCountry"), Eval("IsDefaultState"), Eval("IsDefaultZip") ) %>' />
                    </ItemTemplate>
                    <HeaderStyle Width="35px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                    <FooterStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:TaxClassRuleID %>" SortExpression="TaxClassRuleID">
                    <EditItemTemplate>
                        <asp:Label ID="uxTaxClassRuleIDLabel" runat="server" Text='<%# Bind("TaxClassRuleID") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxTaxClassRuleIDLabel" runat="server" Text='<%# Bind("TaxClassRuleID") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                    <FooterStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:CountryCode %>" SortExpression="CountryCode">
                    <EditItemTemplate>
                        <asp:Label ID="uxCountryLabel" runat="server" Text='<%# GetCountryText( Eval("CountryCode"), Eval("IsDefaultCountry") ) %>' />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxCountryCodeLabel" runat="server" Text='<%# GetCountryText( Eval("CountryCode"), Eval("IsDefaultCountry") ) %>' />
                    </ItemTemplate>
                    <FooterTemplate>
                        <uc4:CountryList ID="uxCountryList" runat="server" IsCountryWithOther="false" OnBubbleEvent="uxStateFooter_RefreshHandler" />
                    </FooterTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:StateCode %>" SortExpression="StateCode">
                    <EditItemTemplate>
                        <asp:Label ID="uxStateLabel" runat="server" Text='<%# GetStateText(Eval("StateCode"), Eval("IsDefaultCountry"), Eval("IsDefaultState")) %>' />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxStateCodeLabel" runat="server" Text='<%# GetStateText(Eval("StateCode"), Eval("IsDefaultCountry"), Eval("IsDefaultState")) %>'>
                        </asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <uc5:StateList ID="uxStateList" runat="server" IsStateWithOther="false" />
                    </FooterTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:ZipCode %>" SortExpression="ZipCode">
                    <EditItemTemplate>
                        <asp:Label ID="uxZipLabel" runat="server" Text='<%# GetZipText(Eval("ZipCode"), Eval("IsDefaultCountry"), Eval("IsDefaultState"), Eval("IsDefaultZip")) %>' />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxZipCodeLabel" runat="server" Text='<%# GetZipText(Eval("ZipCode"), Eval("IsDefaultCountry"), Eval("IsDefaultState"), Eval("IsDefaultZip")) %>'>
                        </asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="uxZipCodeText" runat="server" Text='<%# Bind("ZipCode") %>' Width="70px"></asp:TextBox>
                    </FooterTemplate>
                    <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:TaxRate %>" SortExpression="TaxRate">
                    <EditItemTemplate>
                        <asp:TextBox ID="uxTaxRateText" runat="server" Text='<%# Bind("TaxRate") %>' Width="40px"></asp:TextBox>
                        <asp:CompareValidator ID="uxTaxRateCompare" runat="server" ErrorMessage="Tax rate must be Decimal."
                            Display="Dynamic" ControlToValidate="uxTaxRateText" Operator="DataTypeCheck"
                            Type="Currency" ValidationGroup="ValidTax">*</asp:CompareValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxTaxRateLabel" runat="server" Text='<%# Bind("TaxRate") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="uxTaxRateText" runat="server" Text='<%# Bind("TaxRate") %>' Width="40px"></asp:TextBox>
                        <asp:CompareValidator ID="uxTaxRateCompare" runat="server" ErrorMessage="Tax rate must be Decimal."
                            Display="Dynamic" ControlToValidate="uxTaxRateText" Operator="DataTypeCheck"
                            Type="Currency" ValidationGroup="ValidTax">*</asp:CompareValidator>
                    </FooterTemplate>
                    <HeaderStyle Width="100px" HorizontalAlign="center" />
                    <ItemStyle HorizontalAlign="Right" CssClass="pdr15" />
                    <FooterStyle HorizontalAlign="Right" CssClass="pdr15" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <EditItemTemplate>
                        <vevo:LinkButton ID="uxUpdateLinkButton" runat="server" CausesValidation="True" CommandName="Update"
                            Text="<%$ Resources:CommandUpdate %>" ValidationGroup="ValidTax" CssClass="UnderlineDashed" />
                        <vevo:LinkButton ID="uxCancelLinkButton" runat="server" CausesValidation="False"
                            CommandName="Cancel" Text="<%$ Resources:CommandCancel %>" CssClass="UnderlineDashed" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <vevo:LinkButton ID="uxEditLinkButton" runat="server" CausesValidation="False" CommandName="Edit"
                            Text="<%$ Resources:CommandEdit %>" Visible='<%# IsAdminModifiable() %>' CssClass="UnderlineDashed" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <vevo:AdvanceButton ID="uxAddLinkButton" runat="server" meta:resourcekey="uxAddLinkButton"
                            CssClassBegin="mgCenter" CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter"
                            CommandName="Add" OnClickGoTo="Top" ValidationGroup="ValidTax" />
                    </FooterTemplate>
                    <ItemStyle Width="100px" HorizontalAlign="center" />
                    <FooterStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="<%$ Resources:lcEmptyTaxClassRuleList  %>"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>
    </GridTemplate>
</uc1:AdminContent>
