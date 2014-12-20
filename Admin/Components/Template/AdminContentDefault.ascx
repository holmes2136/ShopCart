<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminContentDefault.ascx.cs"
    Inherits="AdminAdvanced_Components_Template_AdminContentDefault" %>
<%@ Register Src="../BoxSet/Boxset.ascx" TagName="BoxSet" TagPrefix="uc2" %>
<div id="innerContentHolderArea" class="InnerContentHolderArea" onscroll="setscrollCordinate();"
    onclick="setscrollCordinate();" onkeypress="setscrollCordinate();">
    <div class="ContentArea">
        <asp:Panel ID="uxTopPagePanel" runat="server">
            <asp:Panel ID="uxMessagePanel" runat="server" CssClass="fl">
                <asp:PlaceHolder ID="uxMessagePlaceHolder" runat="server"></asp:PlaceHolder>
                <asp:Panel ID="uxValidationSummaryPanel" runat="server">
                    <asp:PlaceHolder ID="uxValidationSummaryPlaceHolder" runat="server"></asp:PlaceHolder>
                </asp:Panel>
                <asp:Panel ID="uxValidationDenotePanel" runat="server" CssClass="validator1 mgt25">
                    <asp:PlaceHolder ID="uxValidationDenotePlaceHolder" runat="server"></asp:PlaceHolder>
                </asp:Panel>
            </asp:Panel>
            <asp:Panel ID="uxLanguageControlPanel" runat="server" CssClass="fr ar">
                <asp:PlaceHolder ID="uxLanguageControlPlaceHolder" runat="server"></asp:PlaceHolder>
            </asp:Panel>
            <div class="Clear">
            </div>
        </asp:Panel>
        <asp:Panel ID="uxButtonEventPanel" runat="server" CssClass="mgb10">
            <asp:PlaceHolder ID="uxButtonEventPlaceHolder" runat="server"></asp:PlaceHolder>
            <div class="Clear">
            </div>
        </asp:Panel>
        <uc2:BoxSet ID="uxTopContentBoxSet" runat="server" CssClass="BoxSet1 b6 c4">
            <ContentTemplate>
                <div class="mgl10 mgr10">
                    <asp:Panel ID="uxFilterPanel" runat="server" CssClass="pdb10 CssAdminContentFilterPanel">
                        <asp:PlaceHolder ID="uxFilterPlaceHolder" runat="server"></asp:PlaceHolder>
                        <div class="Clear">
                        </div>
                    </asp:Panel>
                    <div style="overflow: hidden;" class="">
                        <asp:Panel ID="uxButtonCommandPanel" runat="server" CssClass="fl">
                            <asp:PlaceHolder ID="uxButtonCommandPlaceHolder" runat="server"></asp:PlaceHolder>
                            <div class="Clear">
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="uxPageNumberPanel" runat="server" CssClass="fr">
                            <asp:PlaceHolder ID="uxPageNumberPlaceHolder" runat="server"></asp:PlaceHolder>
                            <div class="Clear">
                            </div>
                        </asp:Panel>
                        <div class="Clear">
                        </div>
                    </div>
                    <asp:Panel ID="uxTopContentBoxPanel" runat="server">
                        <asp:PlaceHolder ID="uxTopContentBoxPlaceHolder" runat="server"></asp:PlaceHolder>
                        <div class="Clear">
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="uxGridPanel" runat="server" CssClass="BorderSet1 mgt5 pd1">
                        <asp:PlaceHolder ID="uxGridPlaceHolder" runat="server"></asp:PlaceHolder>
                        <div class="Clear">
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="uxBottomContentBoxPanel" runat="server">
                        <asp:PlaceHolder ID="uxBottomContentBoxPlaceHolder" runat="server"></asp:PlaceHolder>
                        <div class="Clear">
                        </div>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </uc2:BoxSet>
        <asp:Panel ID="uxContentPanel" runat="server">
            <asp:PlaceHolder ID="uxContentPlaceHolder" runat="server"></asp:PlaceHolder>
            <div class="Clear">
            </div>
        </asp:Panel>
        <div class="fl">
            <asp:PlaceHolder ID="uxFooterPlaceHolder" runat="server"></asp:PlaceHolder>
        </div>
        <div class="fr mgt10 mgb10 mgr0 ar w90">
            <asp:HyperLink ID="uxBackToTopHyperLink" runat="server" Visible="false" SkinID="BackToTop">Back to Top</asp:HyperLink></div>
        <div class="Clear">
        </div>
    </div>
</div>
