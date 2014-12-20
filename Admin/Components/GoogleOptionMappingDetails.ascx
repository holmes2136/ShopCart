<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GoogleOptionMappingDetails.ascx.cs"
    Inherits="Admin_Components_GoogleOptionMappingDetails" %>
<%@ Register Src="LanguageControl.ascx" TagName="LanguageControl" TagPrefix="uc3" %>
<%@ Register Src="Message.ascx" TagName="Message" TagPrefix="uc2" %>
<%@ Register Src="Template/AdminContent.ascx" TagName="AdminContent" TagPrefix="uc1" %>
<%@ Register Src="../Components/LanguageLabelPlus.ascx" TagName="LanaguageLabelPlus"
    TagPrefix="uc5" %>
<uc1:AdminContent ID="uxAdminContent" runat="server">
    <MessageTemplate>
        <uc2:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <ValidationDenotesTemplate>
        <div class="topline">
        </div>
    </ValidationDenotesTemplate>
    <LanguageControlTemplate>
        <uc3:LanguageControl ID="uxLanguageControl" runat="server" />
    </LanguageControlTemplate>
    <ContentTemplate>
        <div class="Container-Box">
            <div class="CommonConfigTitle mgt0">
                <asp:Label ID="lcGoogleTagHead" runat="server" meta:resourcekey="lcGoogleTagHead"></asp:Label>
            </div>
            <div class="mgl5 CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="lcGoogleTag" runat="server" meta:resourcekey="lcGoogleTag"></asp:Label></div>
                <asp:DropDownList ID="uxGoogleTagDrop" runat="server" CssClass="fl DropDown" >
                </asp:DropDownList>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonConfigTitle">
                <asp:Label ID="lcOptionGroupHead" runat="server" meta:resourcekey="lcOptionGroupHead"></asp:Label>
            </div>
            <div class="mgl5">
                <div class="CommonRowStyle">
                    <div class="Label">
                        <asp:Label ID="lcOptionGroup" runat="server" meta:resourcekey="lcOptionGroup"></asp:Label>
                    </div>
                    <asp:DropDownList ID="uxOptionGroupDrop" runat="server" CssClass="fl DropDown" >
                    </asp:DropDownList>
                    <div class="Clear">
                    </div>
                </div>
            </div>
            <div class="Clear" />
            <div>
                <vevo:AdvanceButton ID="uxAddButton" runat="server" meta:resourcekey="uxAddButton" 
                    CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    OnClick="uxAddButton_Click" OnClickGoTo="Top" ValidationGroup="ValidShipping" />
                <vevo:AdvanceButton ID="uxUpdateButton" runat="server" meta:resourcekey="uxUpdateButton" 
                    CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    OnClick="uxUpdateButton_Click" OnClickGoTo="Top" />
            </div>
        </div>
    </ContentTemplate>
</uc1:AdminContent>
