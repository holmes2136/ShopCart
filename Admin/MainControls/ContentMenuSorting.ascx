<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ContentMenuSorting.ascx.cs"
    Inherits="AdminAdvanced_MainControls_ContentMenuSorting" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../Components/LanguageControl.ascx" TagName="LanguageControl" TagPrefix="uc2" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <MessageTemplate>
        <uc1:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <LanguageControlTemplate>
        <uc2:LanguageControl ID="uxLanguageControl" runat="server" ShowTitle="true" />
    </LanguageControlTemplate>
    <ButtonEventInnerBoxTemplate>
        <vevo:AdvanceButton ID="uxSortByNameButton" runat="server" Text="Sort By Name" CssClassBegin="AdminButton"
            CssClassEnd="" CssClass="AdminButtonSorting CommonAdminButton" ShowText="true"
            OnClick="uxSortByNameButton_Click" OnClickGoTo="Top" />
        <vevo:AdvanceButton ID="uxSortByIDButton" runat="server" Text="Sort By ID" CssClassBegin="AdminButton"
            CssClassEnd="" CssClass="AdminButtonSorting CommonAdminButton" ShowText="true"
            OnClick="uxSortByIDButton_Click" OnClickGoTo="Top" />
    </ButtonEventInnerBoxTemplate>
    <FilterTemplate>
        <div class="SearchFilterDefault">
            <asp:Label ID="lcFilterBy" runat="server" meta:resourcekey="lcCategoryBy" CssClass="Label" />
            <asp:DropDownList ID="uxContentMenuDrop" runat="server" AutoPostBack="true" OnSelectedIndexChanged="uxContentMenuDrop_SelectedIndexChanged"
                CssClass="DropDown">
            </asp:DropDownList></div>
    </FilterTemplate>
    <GridTemplate>
        <div class="CommonAdminBorder Container-Box1" id="uxContentData" runat="server">
            <div class="CategorySortingLabel">
                <asp:Label ID="uxCategoryDescriptionLabel" runat="server" meta:resourcekey="lcCategoryDescription"></asp:Label>
            </div>
            <div class="CategorySorting">
                <asp:Label ID="uxListLabel" runat="server" Text=""></asp:Label>
                <div class="Clear">
                </div>
            </div>
            <div class="mgb10 mgr10">
                <vevo:AdvanceButton ID="uxCancelButton" runat="server" meta:resourcekey="uxCancelButton" CssClassBegin="fr mgl10"
                    CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxCancelButton_Click"
                    OnClickGoTo="Top"></vevo:AdvanceButton>
                <vevo:AdvanceButton ID="uxSaveButton" runat="server" meta:resourcekey="uxSaveButton" CssClassBegin="fr"
                    CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxSaveButton_Click"
                    OnClickGoTo="Top"></vevo:AdvanceButton>
                <div class="Clear">
                </div>
            </div>
        </div>
        <div class="DefaultGridEmptyDataRowStyle pdl10 pdr10" id="uxNoData" runat="server"
            visible="false">
            There is no item to display.
        </div>
    </GridTemplate>
</uc1:AdminContent>
<asp:HiddenField ID="uxStatusHidden" runat="server" />
