<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PasswordDetails.ascx.cs"
    Inherits="Components_PasswordDetails" %>
<div class="PasswordDetails">
    <div class="PasswordDetailsDiv">
        <table id="T_Password" cellpadding="0" cellspacing="2" class="PasswordDetailsTable">
            <tr>
                <td class="PasswordDetailsLabelColumn">
                    [$UserName]
                </td>
                <td class="PasswordDetailsInputColumn">
                    <asp:TextBox ID="uxUserNameText" runat="server" Width="200px" Enabled="False" ValidationGroup="UserPass"
                        CssClass="PasswordDetailsTextBox"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="uxUsernameRequiredValidation" runat="server" Text="RequiredFieldValidator"
                        ControlToValidate="uxUserNameText" ValidationGroup="UserPass" Display="Dynamic" CssClass="CommonValidatorText">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="PasswordDetailsLabelColumn">
                    [$OldPass]
                </td>
                <td class="PasswordDetailsInputColumn">
                    <asp:TextBox ID="uxOldText" runat="server" TextMode="Password" Width="200px" ValidationGroup="UserPass"
                        CssClass="PasswordDetailsTextBox"></asp:TextBox><span class="CommonAsterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxOldPassRequiredValidation" runat="server" ControlToValidate="uxOldText"
                        ValidationGroup="UserPass" Display="Dynamic" CssClass="CommonValidatorText">
                        <div class="CommonValidateDiv PasswordValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Old Password
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="PasswordDetailsLabelColumn">
                    [$NewPass]
                </td>
                <td class="PasswordDetailsInputColumn">
                    <asp:TextBox ID="uxNewText" runat="server" TextMode="Password" Width="200px" ValidationGroup="UserPass"
                        CssClass="PasswordDetailsTextBox"></asp:TextBox><span class="CommonAsterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxNewPassRequiredValidation" runat="server" ControlToValidate="uxNewText"
                        ValidationGroup="UserPass" Display="Dynamic" CssClass="CommonValidatorText">
                        <div class="CommonValidateDiv PasswordValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required New Password
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="PasswordDetailsLabelColumn">
                    [$ConPass]
                </td>
                <td class="PasswordDetailsInputColumn">
                    <asp:TextBox ID="uxNewConfrimText" runat="server" TextMode="Password" Width="200px"
                        ValidationGroup="UserPass" CssClass="PasswordDetailsTextBox"></asp:TextBox><span
                            class="CommonAsterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxConfrimRequiredValidation" runat="server" ControlToValidate="uxNewConfrimText"
                        ValidationGroup="UserPass" Display="Dynamic" CssClass="CommonValidatorText">
                        <div class="CommonValidateDiv PasswordValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Confrim Password
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="uxPassCompareValidator" runat="server" ControlToCompare="uxNewText"
                        ControlToValidate="uxNewConfrimText" ValidationGroup="UserPass" Display="Dynamic" CssClass="CommonValidatorText">
                        <div class="CommonValidateDiv PasswordValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Please enter the same password on both password fields
                    </asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="PasswordDetailsMessageColumn">
                    <asp:Label ID="uxErrorMessageLabel" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div class="PasswordDetailsButtonDiv">
        <asp:LinkButton ID="uxSubmitImageButton" runat="server" ValidationGroup="UserPass"
            CssClass="PasswordDetailsSubmitLinkButton BtnStyle1" Text="[$BtnSubmit]" OnClick="uxSubmitImageButton_Click" />
    </div>
    <asp:HiddenField ID="uxStatusHidden" runat="server" />
</div>
