<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AffiliateDetails.ascx.cs"
    Inherits="Components_AffiliateDetails" %>
<%@ Register Src="~/Components/CountryAndStateList.ascx" TagName="CountryAndState"
    TagPrefix="uc1" %>
<div class="AffiliateDetails">
    <div class="CustomerRegisterDiv">
        <div id="uxUsernameValidDIV" class="CommonValidateText" runat="server">
            <asp:Literal ID="uxUsernameLiteral" runat="server"></asp:Literal>
        </div>
        <asp:Panel ID="uxRegisterPanel" runat="server" CssClass="CustomerRegisterPanel">
            <div class="CommonPageInnerTitle">
                [$AccountInfo]
            </div>
            <div class="CustomerRegisterLeft">
                <div class="CustomerRegisterLeftLabel">
                    [$UserName]</div>
                <div class="CustomerRegisterLeftData">
                    <asp:TextBox ID="uxUserName" runat="server" ValidationGroup="ValidateAffiliate" CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox>
                    <span class="CommonAsterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxRequiredUserNameValidator" runat="server" ControlToValidate="uxUserName"
                        ValidationGroup="ValidateAffiliate" Display="Dynamic" CssClass="CommonValidatorText">
                        <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required UserName
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="CustomerRegisterRight">
                <div class="CustomerRegisterRightLabel">
                    [$Email]</div>
                <div class="CustomerRegisterRightData">
                    <asp:TextBox ID="uxEmail" runat="server" CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox>
                    <span class="CommonAsterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxEmailRequiredFieldValidator" runat="server" ControlToValidate="uxEmail"
                        ValidationGroup="ValidateAffiliate" Display="Dynamic" ForeColor="Red" CssClass="CommonValidatorText">
                        <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Email
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="uxRegularEmailValidator" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        ControlToValidate="uxEmail" ValidationGroup="ValidateAffiliate" Display="Dynamic"
                        CssClass="CommonValidatorText">
                        <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Wrong Email Format
                    </asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="CustomerRegisterLeft" id="uxPasswordTR" runat="server">
                <div class="CustomerRegisterLeftLabel">
                    [$Password]</div>
                <div class="CustomerRegisterLeftData">
                    <asp:TextBox ID="uxPassword" runat="server" TextMode="Password" ValidationGroup="ValidateAffiliate"
                        CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox>
                    <span class="CommonAsterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxRequiredPasswordValidator" runat="server" ControlToValidate="uxPassword"
                        ValidationGroup="ValidateAffiliate" Display="Dynamic" CssClass="CommonValidatorText">
                        <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Password
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="CustomerRegisterRight" id="uxConfirmPasswordTR" runat="server">
                <div class="CustomerRegisterRightLabel">
                    [$Confirm]</div>
                <div class="CustomerRegisterRightData">
                    <asp:TextBox ID="uxTextBoxConfrim" runat="server" TextMode="Password" ValidationGroup="ValidateAffiliate"
                        CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox>
                    <span class="CommonAsterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxRequiredTexBoxConfirmValidator" runat="server"
                        ControlToValidate="uxTextBoxConfrim" ValidationGroup="ValidateAffiliate" Display="Dynamic"
                        CssClass="CommonValidatorText">
                        <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Confirm Password
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="uxCompareValidator" runat="server" ControlToCompare="uxPassword"
                        ControlToValidate="uxTextBoxConfrim" ValidationGroup="ValidateAffiliate" Display="Dynamic"
                        CssClass="CommonValidatorText">
                        <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Please enter the same password on both password fields
                    </asp:CompareValidator>
                </div>
            </div>
            <div class="CustomerRegisterLeft">
                <div class="CustomerRegisterLeftLabel">
                    [$FirstName]</div>
                <div class="CustomerRegisterLeftData">
                    <asp:TextBox ID="uxFirstName" runat="server" ValidationGroup="ValidateAffiliate"
                        CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox>
                    <span class="CommonAsterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxRequiredFirstNameValidator" runat="server" ControlToValidate="uxFirstName"
                        ValidationGroup="ValidateAffiliate" ForeColor="Red" Display="Dynamic" CssClass="CommonValidatorText">
                    <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required First Name
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="CustomerRegisterRight">
                <div class="CustomerRegisterRightLabel">
                    [$LastName]</div>
                <div class="CustomerRegisterRightData">
                    <asp:TextBox ID="uxLastName" runat="server" ValidationGroup="ValidateAffiliate" CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox>
                    <span class="CommonAsterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxRequiredLastNameValidator" runat="server" ControlToValidate="uxLastName"
                        ValidationGroup="ValidateAffiliate" ForeColor="Red" Display="Dynamic" CssClass="CommonValidatorText">
                    <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Last Name
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="CustomerRegisterLeft" id="uxCommissionRateTR" runat="server">
                <div class="CustomerRegisterLeftLabel">
                    [$CommissionRate]</div>
                <div class="CustomerRegisterLeftData CommissionRate">
                    <asp:Label ID="uxCommissionRateLabel" runat="server" />%
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="uxRegisAddressPanel" runat="server" CssClass="CustomerRegisterPanel">
            <div class="CommonPageInnerTitle">
                [$AddressInfo]</div>
            <div class="CustomerRegisterLeft">
                <div class="CustomerRegisterLeftLabel">
                    [$Website]</div>
                <div class="CustomerRegisterLeftData">
                    <asp:TextBox ID="uxWebSite" runat="server" CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox>
                    <span class="CommonAsterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxRequiredWebSiteValidator" runat="server" ControlToValidate="uxWebSite"
                        ValidationGroup="ValidateAffiliate" ForeColor="Red" Display="Dynamic" CssClass="CommonValidatorText">
                        <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Website
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="uxRegularWebSiteValidator" runat="server" ValidationExpression="(?:w{3}(\.\w+)+|\w+(\.\w+)+)"
                        ControlToValidate="uxWebSite" ValidationGroup="ValidateAffiliate" ForeColor="Red"
                        Display="Dynamic" CssClass="CommonValidatorText">
                        <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Wrong Website Format
                    </asp:RegularExpressionValidator>
                    <div class="CommonFormDataSample">
                        (e.g. www.abc.com)</div>
                </div>
            </div>
            <div class="CustomerRegisterRight">
                <div class="CustomerRegisterRightLabel">
                    [$Company]</div>
                <div class="CustomerRegisterRightData">
                    <asp:TextBox ID="uxCompany" runat="server" CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox></div>
            </div>
            <div class="CustomerRegisterLeft">
                <div class="CustomerRegisterLeftLabel">
                    [$Address]</div>
                <div class="CustomerRegisterLeftData">
                    <asp:TextBox ID="uxAddress1" runat="server" ValidationGroup="ValidateAffiliate" CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox>
                    <span class="CommonAsterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxRequiredAddressValidator" runat="server" ControlToValidate="uxAddress1"
                        ValidationGroup="ValidateAffiliate" Display="Dynamic" CssClass="CommonValidatorText">
                        <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Address
                    </asp:RequiredFieldValidator></div>
            </div>
            <div class="CustomerRegisterRight">
                <div class="CustomerRegisterRightLabel">
                    [$Address2]</div>
                <div class="CustomerRegisterRightData">
                    <asp:TextBox ID="uxAddress2" runat="server" CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox></div>
            </div>
            <div class="CustomerRegisterLeft">
                <div class="CustomerRegisterLeftLabel">
                    [$City]</div>
                <div class="CustomerRegisterLeftData">
                    <asp:TextBox ID="uxCity" runat="server" ValidationGroup="ValidateAffiliate" CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox>
                    <span class="CommonAsterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxRequiredCityValidator" runat="server" ControlToValidate="uxCity"
                        ValidationGroup="ValidateAffiliate" Display="Dynamic" CssClass="CommonValidatorText">
                        <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required City
                    </asp:RequiredFieldValidator></div>
            </div>
            <div class="CustomerRegisterRight">
                <div class="CustomerRegisterRightLabel">
                    [$Zipcode]</div>
                <div class="CustomerRegisterRightData">
                    <asp:TextBox ID="uxZip" runat="server" ValidationGroup="ValidateAffiliate" MaxLength="9"
                        CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox>
                    <span class="CommonAsterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxRequiredZipValidator" runat="server" ControlToValidate="uxZip"
                        ValidationGroup="ValidateAffiliate" Display="Dynamic" CssClass="CommonValidatorText">
                        <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Zip
                    </asp:RequiredFieldValidator></div>
            </div>
            <uc1:CountryAndState ID="uxCountryAndState" runat="server" IsRequiredCountry="true"
                IsRequiredState="true" IsCountryWithOther="true" IsStateWithOther="true" CssPanel="ClientCityStatePanel"
                CssLabel="CustomerRegisterLabel" />
            <div id="uxCountryStateDiv" runat="server" class="CommonValidatorText CustomerRegisterPanelCountryValidatorText"
                visible="false">
                <div class="CommonValidateDiv">
                </div>
                <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" />
                <asp:Label ID="uxCountryStateMessage" runat="server"></asp:Label>
            </div>
            <div class="CustomerRegisterLeft">
                <div class="CustomerRegisterLeftLabel">
                    [$Phone]</div>
                <div class="CustomerRegisterLeftData">
                    <asp:TextBox ID="uxPhone" runat="server" CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox></div>
            </div>
            <div class="CustomerRegisterRight">
                <div class="CustomerRegisterRightLabel">
                    [$Fax]</div>
                <div class="CustomerRegisterRightData">
                    <asp:TextBox ID="uxFax" runat="server" CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox></div>
            </div>
        </asp:Panel>
        <asp:Panel ID="uxAgreementPanel" runat="server" CssClass="AffiliateDetailsAgreementPanel">
            <div class="AffiliateDetailsDownloadDiv">
                <img src="Images/Design/Icon/icon_acrobat.gif" class="AffiliateDetailsDownloadLinkIcon" />
                <asp:HyperLink ID="uxAffiliateAgreementLink" NavigateUrl="~/Document/AffiliateAgreement.pdf"
                    runat="server" CssClass="CommonHyperLink" Target="_blank">Download Affiliate Agreement</asp:HyperLink>
            </div>
            <div id="uxAgreementDIV" class="AffiliateDetailsAgreementContent" runat="server">
            </div>
            <div id="uxPolicyAgreementValidatorDiv" runat="server" class="CommonValidatorText AffiliateDetailsValidatorText"
                visible="false">
                <div class="CommonValidateDiv AffiliateDetailsValidatorDiv">
                </div>
                <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" />
                <asp:Label ID="uxPolicyAgreementMessage" runat="server"></asp:Label>
            </div>
            <div class="AffiliateDetailsAgreementAcceptDiv">
                <asp:CheckBox ID="uxAcceptCheck" runat="server" Text="Accept" CssClass="AffiliateDetailsAgreementCheckBox" />
            </div>
        </asp:Panel>
        <div class="AffiliateDetailsButtonDiv">
            <asp:LinkButton ID="uxAddButton" runat="server" ValidationGroup="ValidateAffiliate"
                CssClass="AffiliateDetailsAddLinkButton BtnStyle1" Text="[$BtnRegister]" OnClick="uxAddButton_Click" />
            <asp:LinkButton ID="uxUpdateButton" runat="server" OnClick="uxUpdateButton_Click"
                ValidationGroup="ValidateAffiliate" CssClass="AffiliateDetailsUpdateLinkButton BtnStyle1"
                Text="[$BtnSubmit]" />
        </div>
    </div>
</div>
