<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductReviewList.ascx.cs"
    Inherits="AdminAdvanced_MainControls_ProductReviewList" %>
<%@ Register Src="../Components/SearchFilter.ascx" TagName="SearchFilter" TagPrefix="uc4" %>
<%@ Register Src="../Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc5" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/LanguageControl.ascx" TagName="LanguageControl" TagPrefix="uc2" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <MessageTemplate>
        <uc3:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <LanguageControlTemplate>
        <uc2:LanguageControl ID="uxLanguageControl" runat="server" ShowTitle="true" />
    </LanguageControlTemplate>
    <ButtonEventTemplate>
        <vevo:AdvancedLinkButton ID="uxProductDetailLink" runat="server" meta:resourcekey="lcProductDetail"
            OnClick="ChangePage_Click" StatusBarText="Product Details" CssClass="CommonAdminButtonIcon AdminButtonIconView fl" />
        <vevo:AdvancedLinkButton ID="uxImageListLink" runat="server" meta:resourcekey="lcProductImageList"
            OnClick="ChangePage_Click" StatusBarText="Product Details" CssClass="CommonAdminButtonIcon AdminButtonIconView fl" />
    </ButtonEventTemplate>
    <FilterTemplate>
        <uc4:SearchFilter ID="uxSearchFilter" runat="server" />
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
                <asp:Label ID="uxConfirmLabel" runat="server" Text="<%$ Resources:ProductReviewMessage, DeleteConfirmation %>"></asp:Label></div>
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
        <uc5:PagingControl ID="uxPagingControl" runat="server" />
    </PageNumberTemplate>
    <GridTemplate>
        <div id="uxProductNameDiv" runat="server" class="CommonTitle">
            <asp:Label ID="uxProductNameLabel" runat="server" /></div>
        <asp:GridView ID="uxGrid" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
            AllowSorting="true" OnSorting="uxGrid_Sorting" OnRowDataBound="uxGrid_RowDataBound"
            ShowFooter="false">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <input type="CheckBox" name="SelectAllCheckBox" onclick="SelectAllCheckBoxes(this , 'uxGrid')">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="uxCheck" runat="server" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="ReviewID" HeaderText="<%$ Resources:ProductReviewFields, ReviewID %>"
                    SortExpression="ReviewID">
                    <HeaderStyle />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="UserName" HeaderText="<%$ Resources:ProductReviewFields, UserName %>"
                    SortExpression="UserName">
                    <HeaderStyle />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Subject" HeaderText="<%$ Resources:ProductReviewFields, ReviewSubject %>"
                    SortExpression="Subject">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle />
                </asp:BoundField>
                <asp:BoundField DataField="ReviewRating" HeaderText="<%$ Resources:ProductReviewFields, ReviewRating %>"
                    SortExpression="ReviewRating">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle />
                </asp:BoundField>
                <asp:BoundField DataField="Enabled" HeaderText="<%$ Resources:ProductReviewFields, Enabled %>"
                    SortExpression="Enabled">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Edit">
                    <ItemTemplate>
                        <vevo:AdvancedLinkButton ID="uxReviewEditLink" runat="server" ToolTip="<%$ Resources:ProductReviewFields, EditCommand %>"
                            PageName="ProductReviewEdit.ascx" PageQueryString='<%# String.Format( "ReviewID={0}&ProductID={1}", Eval("ReviewID"),Eval("ProductID") ) %>'
                            OnClick="ChangePage_Click" StatusBarText='<%# String.Format( "Reviews {0}", Eval("Subject") ) %>'>
                            <asp:Image ID="uxEditImage" runat="server" SkinID="IConEditInGrid" />
                        </vevo:AdvancedLinkButton>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle Width="5%" HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="<%$ Resources:ProductReviewMessage, TableEmpty  %>"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>
    </GridTemplate>
</uc1:AdminContent>
