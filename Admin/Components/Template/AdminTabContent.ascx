<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminTabContent.ascx.cs"
    Inherits="Admin_Components_Template_AdminTabContent" %>
<%@ Register Src="../BoxSet/Boxset.ascx" TagName="BoxSet" TagPrefix="uc2" %>
<div id="innerContentHolderArea">
    <div class="TopContentArea" visible="false">
        <div class="TopContentArea-Left">
            <div class="TopContentArea-Right">
                <asp:Panel ID="uxLanguageControlPanel" runat="server" CssClass="CssAdminContentLanguage">
                    <asp:PlaceHolder ID="uxLanguageControlPlaceHolder" runat="server"></asp:PlaceHolder>
                    <div class="Clear">
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
    <div class="ContentArea">
        <asp:Panel ID="uxMessagePanel" runat="server" CssClass="CssAdminContentMessage">
            <asp:PlaceHolder ID="uxMessagePlaceHolder" runat="server"></asp:PlaceHolder>
        </asp:Panel>
        <asp:Panel ID="uxValidationSummaryPanel" runat="server" CssClass="CssAdminContentMessage">
            <asp:PlaceHolder ID="uxValidationSummaryPlaceHolder" runat="server"></asp:PlaceHolder>
            <div class="Clear">
            </div>
        </asp:Panel>
        <asp:Panel ID="uxButtonEventPanel" runat="server" CssClass="CssAdminContentButtonEvent">
            <asp:PlaceHolder ID="uxButtonEventPlaceHolder" runat="server"></asp:PlaceHolder>
            <div class="Clear">
            </div>
        </asp:Panel>
        <asp:Panel ID="uxTopPagePanel" runat="server" CssClass="CssAdminContentTop">
            <asp:Panel ID="uxSpecialFilterPanel" runat="server" CssClass="CssAdminContentSpecialFilter">
                <asp:PlaceHolder ID="uxSpecialFilterPlaceHolder" runat="server"></asp:PlaceHolder>
                <div class="Clear">
                </div>
            </asp:Panel>
            <asp:Panel ID="uxFilterPanel" runat="server" CssClass="CssAdminContentFilter">
                <asp:PlaceHolder ID="uxFilterPlaceHolder" runat="server"></asp:PlaceHolder>
                <div class="Clear">
                </div>
            </asp:Panel>
        </asp:Panel>
        <asp:Panel ID="uxHeaderMeassagePanel" runat="server" CssClass="CssAdminContentHeaderMessagePanel">
            <asp:PlaceHolder ID="uxHeaderMeassagePlaceHolder" runat="server"></asp:PlaceHolder>
            <div class="Clear">
            </div>
        </asp:Panel>
        <asp:Panel ID="uxValidationDenotePanel" runat="server" CssClass="CssAdminContentValidationDenote">
            <asp:PlaceHolder ID="uxValidationDenotePlaceHolder" runat="server"></asp:PlaceHolder>
            <div class="Clear">
            </div>
        </asp:Panel>
        <asp:Panel ID="uxPlainContentPanel" runat="server" CssClass="CssAdminPlainContentPanel">
            <asp:PlaceHolder ID="uxPlainContentPlaceHolder" runat="server"></asp:PlaceHolder>
            <div class="Clear">
            </div>
        </asp:Panel>
        <uc2:BoxSet ID="uxTopContentBoxSet" runat="server" CssClass="CssAdminContentBoxSet">
            <ContentTemplate>
                <div class="CssAdminContentPaging">
                    <asp:Panel ID="uxButtonEventInnerBoxPanel" runat="server" CssClass="ButtonEvent">
                        <asp:PlaceHolder ID="uxButtonEventInnerBoxPlaceHolder" runat="server"></asp:PlaceHolder>
                        <div class="Clear">
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="uxButtonCommandPanel" runat="server" CssClass="ButtonCommand">
                        <asp:PlaceHolder ID="uxButtonCommandPlaceHolder" runat="server"></asp:PlaceHolder>
                        <div class="Clear">
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="uxPageNumberPanel" runat="server" CssClass="PageNumber">
                        <asp:PlaceHolder ID="uxPageNumberPlaceHolder" runat="server"></asp:PlaceHolder>
                        <div class="Clear">
                        </div>
                    </asp:Panel>
                    <div class="Clear">
                    </div>
                </div>
                <asp:Panel ID="uxTopContentBoxPanel" runat="server" CssClass="TopContent">
                    <asp:PlaceHolder ID="uxTopContentBoxPlaceHolder" runat="server"></asp:PlaceHolder>
                    <div class="Clear">
                    </div>
                </asp:Panel>
                <asp:Panel ID="uxGridPanel" runat="server" CssClass="GridViewPanel">
                    <asp:PlaceHolder ID="uxGridPlaceHolder" runat="server"></asp:PlaceHolder>
                    <div class="Clear">
                    </div>
                </asp:Panel>
                <asp:Panel ID="uxBottomContentBoxPanel" runat="server" CssClass="BottomContent">
                    <asp:PlaceHolder ID="uxBottomContentBoxPlaceHolder" runat="server"></asp:PlaceHolder>
                    <div class="Clear">
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </uc2:BoxSet>
        <asp:Panel ID="uxContentPanel" runat="server" CssClass="CssAdminContentPanel">
            <asp:PlaceHolder ID="uxContentPlaceHolder" runat="server"></asp:PlaceHolder>
            <div class="Clear">
            </div>
        </asp:Panel>
        <div class="Clear">
        </div>
        <div class="AdminMenuBottomSpace">
            &nbsp;</div>
        <div class="Clear">
        </div>
    </div>
</div>
