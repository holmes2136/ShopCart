<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EBayListingProcess.ascx.cs"
    Inherits="AdminAdvanced_Components_EBayListingProcess" %>
<%@ Register Src="../LanguageControl.ascx" TagName="LanguageControl" TagPrefix="uc4" %>
<%@ Register Src="../Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Src="../Template/AdminContent.ascx" TagName="AdminContent" TagPrefix="uc2" %>
<%@ Register Src="EBayListingTemplate.ascx" TagName="eBayListingTemplate" TagPrefix="uc5" %>
<%@ Register Src="EBayCheckCondition.ascx" TagName="eBayCheckCondition" TagPrefix="uc6" %>
<%@ Register Src="EBayShowFee.ascx" TagName="eBayShowFee" TagPrefix="uc7" %>
<uc2:AdminContent ID="uxAdminContent" runat="server">
    <MessageTemplate>
        <uc3:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <LanguageControlTemplate>
        <uc4:LanguageControl ID="uxLanguageControl" runat="server" ShowLanguageDescription="true" />
    </LanguageControlTemplate>
    <ContentTemplate>
        <div class="Container-Row">
            <asp:Panel ID="uxListingTemplatePanel" runat="server">
                <div class="CommonAdminBorder">
                    <uc5:eBayListingTemplate ID="uxListingTemplate" runat="server" />
                    <div class="Clear">
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="uxCheckConditionPanel" runat="server" Visible="false">
                <div class="CommonAdminBorder">
                    <uc6:eBayCheckCondition ID="uxCheckCondition" runat="server" />
                    <div class="Clear">
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="uxShowFeePanel" runat="server" Visible="false">
                <div class="CommonAdminBorder">
                    <uc7:eBayShowFee ID="uxShowFee" runat="server" />
                    <div class="Clear">
                    </div>
                </div>
            </asp:Panel>
        </div>
        <div class="EBayListingProcessRow mgt10">
            <vevo:AdvanceButton ID="uxNext" runat="server" meta:resourcekey="uxNext" CssClassBegin="mgl10 fr"
                CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxNext_Click"></vevo:AdvanceButton>
            <vevo:AdvanceButton ID="uxListing" runat="server" meta:resourcekey="uxListing" CssClassBegin="mgl10 fr"
                CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxListing_Click">
            </vevo:AdvanceButton>
            <vevo:AdvanceButton ID="uxBack" runat="server" meta:resourcekey="uxBack" CssClassBegin="mgl10 fr"
                CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxBack_Click"></vevo:AdvanceButton>
            <div class="Clear">
            </div>
        </div>
    </ContentTemplate>
</uc2:AdminContent>
<asp:HiddenField ID="uxHiddenScheduleDateTime" runat="server" />
<asp:HiddenField ID="uxHiddenIsSchedule" runat="server" />
<asp:HiddenField ID="uxHiddenSelectedTemplateID" runat="server" />
