<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShippingList.ascx.cs"
    Inherits="AdminAdvanced_MainControls_ShippingList" %>
<%@ Register Src="../Components/Common/CountryList.ascx" TagName="CountryList" TagPrefix="uc4" %>
<%@ Register Src="../Components/LanguageControl.ascx" TagName="LanguageControl" TagPrefix="uc1" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc2" %>
<%@ Register Src="../Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc3" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/BoxSet/Boxset.ascx" TagName="BoxSet" TagPrefix="uc2" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <MessageTemplate>
        <uc2:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <LanguageControlTemplate>
        <uc1:LanguageControl ID="uxLanguageControl" runat="server" ShowTitle="true" />
    </LanguageControlTemplate>
    <ButtonEventTemplate>
        <vevo:Button ID="uxZoneGroupButton" runat="server" meta:resourcekey="uxZoneGroupButton"
            OnClick="uxZoneGroupButton_Click" CssClass="CommonAdminButtonIcon AdminButtonIconView"
            OnClickGoTo="None" />
    </ButtonEventTemplate>
    <ButtonEventInnerBoxTemplate>
        <vevo:AdvanceButton ID="uxAddButton" runat="server" meta:resourcekey="uxAddButton"
            CssClassBegin="AdminButton" CssClassEnd="" CssClass="AdminButtonIconAdd CommonAdminButton"
            ShowText="true" OnClick="uxAddButton_Click" OnClickGoTo="Top" />
    </ButtonEventInnerBoxTemplate>
    <ButtonCommandTemplate>
        <vevo:AdvanceButton ID="uxDeleteButton" runat="server" meta:resourcekey="uxDeleteButton"
            CssClass="AdminButtonDelete CommonAdminButton" CssClassBegin="AdminButton" CssClassEnd=""
            ShowText="true" OnClick="uxDeleteButton_Click"></vevo:AdvanceButton>
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
                <asp:Label ID="uxConfirmLabel" runat="server" Text="<%$ Resources:ShippingMessages, DeleteConfirmation %>"></asp:Label></div>
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
        <asp:GridView ID="uxGridShipping" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
            AllowSorting="true" OnSorting="uxGrid_Sorting" OnRowDataBound="uxGridShipping_RowDataBound">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <input type="CheckBox" name="SelectAllCheckBox" onclick="SelectAllCheckBoxes(this , 'uxGridShipping')" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="uxCheck" runat="server" Visible='<%# !Convert.ToBoolean( Eval("ShippingOptionType.IsRealTime") ) %>' />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="ShippingID" HeaderText="<%$ Resources:ShippingFields, ShippingID %>"
                    SortExpression="ShippingID">
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="ShippingName" HeaderText="<%$ Resources:ShippingFields, ShippingName %>"
                    SortExpression="ShippingName">
                    <ItemStyle HorizontalAlign="Center" Width="300px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="<%$ Resources:ShippingFields, IsRealTime %>" SortExpression="IsRealTimeShipping">
                    <ItemTemplate>
                        <asp:Image ID="uxIsRealTimeImage" runat="server" CssClass='<%# GetImage( Eval("ShippingOptionType.IsRealTime")) %>'
                            ImageUrl="~/Images/Design/space.gif" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:ShippingFields, IsEnabled %>" SortExpression="IsEnabled">
                    <ItemTemplate>
                        <asp:Image ID="uxIsEnabledImage" runat="server" CssClass='<%# GetImage( Eval("IsEnabled")) %>'
                            ImageUrl="~/Images/Design/space.gif" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <vevo:AdvancedLinkButton ID="uxWeightRate" runat="server" Text="<%$ Resources:ShippingFields, WeightRate %>"
                            PageName="ShippingWeightRate.ascx" Visible='<%# IsByWeightOptionType( Eval("ShippingOptionType.TypeName") ) %>'
                            PageQueryString='<%# String.Format( "ShippingID={0}", Eval( "ShippingID") ) %>'
                            OnClick="ChangePage_Click" OnClientClick="resetScrollCordinate();" StatusBarText='<%# String.Format( "Edit Weight Rate For {0}", Eval("ShippingName") ) %>'
                            CssClass="UnderlineDashed" />
                        <vevo:AdvancedLinkButton ID="uxOrderTotalRate" runat="server" Text="<%$ Resources:ShippingFields, OrderTotalRate %>"
                            PageName="ShippingOrderTotalRate.ascx" Visible='<%# IsByOrderTotalOptionType( Eval("ShippingOptionType.TypeName") ) %>'
                            PageQueryString='<%# String.Format( "ShippingID={0}", Eval( "ShippingID") ) %>'
                            OnClick="ChangePage_Click" OnClientClick="resetScrollCordinate();" StatusBarText='<%# String.Format( "Edit Order Total Rate For {0}", Eval("ShippingName") ) %>'
                            CssClass="UnderlineDashed" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:ShippingFields, CommandEdit %>">
                    <ItemTemplate>
                        <vevo:AdvancedLinkButton ID="uxShippingEdit" runat="server" ToolTip="<%$ Resources:ShippingFields, CommandEdit %>"
                            PageName='<%# String.Format( "{0}", Eval("ShippingOptionType.AdminMainControl") ) %>'
                            PageQueryString='<%# String.Format( "ShippingID={0}", Eval("ShippingID") ) %>'
                            StatusBarText='<%# String.Format( "Edit {0}", Eval("ShippingName") ) %>' OnClick="ChangePage_Click"
                            OnClientClick="resetScrollCordinate();">
                            <asp:Image ID="uxEditImage" runat="server" SkinID="IConEditInGrid" />
                        </vevo:AdvancedLinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="<%$ Resources:ShippingMessages, TableEmpty  %>"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>
    </GridTemplate>
</uc1:AdminContent>
