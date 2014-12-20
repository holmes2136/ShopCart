<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PolicyAgreement.ascx.cs"
    Inherits="Admin_Components_SiteConfig_PolicyAgreement" %>
<%@ Register Src="../Common/HelpIcon.ascx" TagName="HelpIcon" TagPrefix="uc1" %>
<%@ Register Src="~/Components/TextEditor.ascx" TagName="TextEditor" TagPrefix="uc6" %>
<div id="uxPolicyAgreementTR" runat="server" class="ConfigRow">
    <uc1:HelpIcon ID="uxPolicyAgreementHelp" ConfigName="IsPolicyAgreementEnabled" runat="server" />
    <div class="Label">
        <asp:Label ID="lcPolicyAgreementEnabled" runat="server" meta:resourcekey="lcPolicyAgreementEnabled"
            CssClass="fl" />
    </div>
    <asp:DropDownList ID="uxPolicyAgreementEnabledDrop" runat="server" CssClass="fl DropDown mgr5"
        AutoPostBack="true" OnSelectedIndexChanged="uxPolicyAgreementEnabledDrop_SelectedIndexChanged">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
    <asp:Label ID="uxPolicyAgreementEnabledLabel" runat="server" Text="" />
    <vevo:LinkButton ID="uxPolicyAgreementEnabledLink" runat="server" Visible="false"
        meta:resourcekey="uxPolicyAgreementEnabledLink" CssClass="UnderlineDashed mgl5"
        OnClick="ChangePage_Click" />
    <ajaxToolkit:ModalPopupExtender ID="uxPolicyAgreementEnabledModalPopup" runat="server"
        TargetControlID="uxPolicyAgreementEnabledLabel" CancelControlID="uxCancelButton"
        PopupControlID="uxPolicyEditPanel" BackgroundCssClass="ConfirmBackground b7"
        DropShadow="true" RepositionMode="None">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="uxPolicyEditPanel" runat="server" CssClass="b6 pd10 ConfirmPanelBorder">
        <div class="CommonRowStyle">
            <uc6:TextEditor ID="uxPolicyArgreementText" runat="Server" PanelClass="PolicyPanelClass"
                TextClass="TextBox" />
        </div>
        <div class="CommonRowStyle">
            <vevo:AdvanceButton ID="uxCancelButton" runat="server" meta:resourcekey="uxCloseButton"
                CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                OnClickGoTo="Top" />
            <vevo:AdvanceButton ID="uxUpdateButton" runat="server" meta:resourcekey="uxUpdateButton"
                CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                OnClick="uxUpdatePolicyArgreement_Click" OnClickGoTo="Top" />
        </div>
        <div class="Clear">
        </div>
    </asp:Panel>
</div>
