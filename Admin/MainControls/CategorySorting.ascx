<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategorySorting.ascx.cs"
    Inherits="AdminAdvanced_MainControls_CategorySorting" EnableViewState="true" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../Components/LanguageControl.ascx" TagName="LanguageControl" TagPrefix="uc2" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc3" %>
<%@ Register Src="../Components/BoxSet/BoxSet.ascx" TagName="BoxSet" TagPrefix="uc4" %>
<uc3:AdminContent ID="uxAdminContentFilter" runat="server">
    <MessageTemplate>
        <uc1:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <FilterTemplate>
        <div class="SearchFilterDefault">
            <asp:Panel ID="uxRootCategoryFilterPanel" runat="server">
                <asp:Label ID="uxRootCategoryBy" runat="server" meta:resourcekey="lcRootCategoryBy"
                    CssClass="Label" />
                <asp:DropDownList ID="uxRootCategoryDrop" runat="server" AutoPostBack="true" OnSelectedIndexChanged="uxRootCategoryDrop_SelectedIndexChanged"
                    CssClass="DropDown">
                </asp:DropDownList>
            </asp:Panel>
            <asp:Label ID="lcFilterBy" runat="server" meta:resourcekey="lcCategoryBy" CssClass="Label" />
            <asp:DropDownList ID="uxCategoryDrop" runat="server" AutoPostBack="true" OnSelectedIndexChanged="uxCategoryDrop_SelectedIndexChanged"
                CssClass="DropDown">
            </asp:DropDownList>
        </div>
    </FilterTemplate>
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
    <GridTemplate>
        <div class="CommonAdminBorder Container-Box1">
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
    </GridTemplate>
</uc3:AdminContent>
<asp:HiddenField ID="uxStatusHidden" runat="server" />
