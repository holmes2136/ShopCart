<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="AccountDetails.aspx.cs"
    Inherits="AccountDetails" Title="[$Title]" %>

<%@ Register Src="Components/CountryAndStateList.ascx" TagName="CountryAndState"
    TagPrefix="uc1" %>
<%@ Register Src="Components/ShippingAddressItemDetails.ascx" TagName="ShippingAddressDetails"
    TagPrefix="uc2" %>
<%@ Register Src="Components/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="AccountDetails">
        <uc3:Message ID="uxMessage" runat="server" NumberOfNewLines="1" />
        <div class="CommonPage">
            <div class="CommonPageTop">
                <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/DefaultBoxTopLeft.gif" runat="server"
                    CssClass="CommonPageTopImgLeft" />
                <asp:Label ID="uxAccountDetailsTitle" runat="server" Text="[$Head]" CssClass="CommonPageTopTitle">
                </asp:Label>
                <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/DefaultBoxTopRight.gif"
                    runat="server" CssClass="CommonPageTopImgRight" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonPageLeft">
                <div class="CommonPageRight">
                    <div class="AccountDetailsDiv">
                        <div class="CommonValidateText">
                            <asp:Literal ID="uxSummaryLiteral" runat="server"></asp:Literal>
                        </div>
                        <asp:Panel ID="uxAccountPanel" runat="server" CssClass="AccountDetailsPanel">
                            <div class="MyAccountTitle">
                                [$AccountInfo]
                            </div>
                            <div class="CommonFormData">
                                <asp:TextBox ID="uxPassword" runat="server" Visible="False" CssClass="CommonTextBox AccountDetailsTextBox">
                                </asp:TextBox>
                            </div>
                            <div class="CustomerRegisterLeft">
                                <div class="CustomerRegisterLeftLabel">
                                    [$Username]</div>
                                <div class="CustomerRegisterLeftData">
                                    <asp:TextBox ID="uxUserName" runat="server" Enabled="false" CssClass="CommonTextBox CustomerRegisterTextBox">
                                    </asp:TextBox>
                                </div>
                            </div>
                            <div class="CustomerRegisterRight">
                                <div class="CustomerRegisterRightLabel">
                                    [$Email]</div>
                                <div class="CustomerRegisterRightData">
                                    <asp:TextBox ID="uxEmail" runat="server" ValidationGroup="AccountDetail" CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox>
                                    <span class="CommonAsterisk">*</span>
                                    <asp:RequiredFieldValidator ID="uxRequiredEmailValidator" runat="server" ControlToValidate="uxEmail"
                                        ValidationGroup="AccountDetail" Display="Dynamic" CssClass="CommonValidatorText">
                                        <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Email
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="uxRegularEmailValidator" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ControlToValidate="uxEmail" ValidationGroup="AccountDetail" Display="Dynamic"
                                        CssClass="CommonValidatorText">
                                        <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Wrong Email Format
                                    </asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="CustomerRegisterLeft">
                                <div class="CustomerRegisterLeftLabel">
                                    [$FName]</div>
                                <div class="CustomerRegisterLeftData">
                                    <asp:TextBox ID="uxFirstName" runat="server" ValidationGroup="AccountDetail" CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox>
                                    <span class="CommonAsterisk">*</span>
                                    <asp:RequiredFieldValidator ID="uxRequiredFirstNameValidator" runat="server" ControlToValidate="uxFirstName"
                                        ValidationGroup="AccountDetail" Display="Dynamic" CssClass="CommonValidatorText">
                                        <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required First Name
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="CustomerRegisterRight">
                                <div class="CustomerRegisterRightLabel">
                                    [$LName]
                                </div>
                                <div class="CustomerRegisterRightData">
                                    <asp:TextBox ID="uxLastName" runat="server" ValidationGroup="AccountDetail" CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox>
                                    <span class="CommonAsterisk">*</span>
                                    <asp:RequiredFieldValidator ID="uxRequiredLastNameValidator" runat="server" ControlToValidate="uxLastName"
                                        ValidationGroup="AccountDetail" Display="Dynamic" CssClass="CommonValidatorText">
                                        <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Last Name
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="uxBillingPanel" runat="server" CssClass="AccountDetailsPanel">
                            <div class="MyAccountTitle">
                                [$Bill]</div>
                            <div class="CustomerRegisterLeft">
                                <div class="CustomerRegisterLeftLabel">
                                    [$Company]</div>
                                <div class="CustomerRegisterLeftData">
                                    <asp:TextBox ID="uxCompany" runat="server" CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox></div>
                            </div>
                            <div class="CustomerRegisterRight">
                                &nbsp;
                            </div>
                            <div class="CustomerRegisterLeft">
                                <div class="CustomerRegisterLeftLabel">
                                    [$Address]</div>
                                <div class="CustomerRegisterLeftData">
                                    <asp:TextBox ID="uxAddress1" runat="server" ValidationGroup="AccountDetail" CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox>
                                    <span class="CommonAsterisk">*</span>
                                    <asp:RequiredFieldValidator ID="uxRequiredAddressValidator" runat="server" ControlToValidate="uxAddress1"
                                        ValidationGroup="AccountDetail" Display="Dynamic" CssClass="CommonValidatorText">
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
                                    <asp:TextBox ID="uxCity" runat="server" ValidationGroup="AccountDetail" CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox>
                                    <span class="CommonAsterisk">*</span>
                                    <asp:RequiredFieldValidator ID="uxRequiredCityValidator" runat="server" ControlToValidate="uxCity"
                                        ValidationGroup="AccountDetail" Display="Dynamic" CssClass="CommonValidatorText">
                                        <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required City
                                    </asp:RequiredFieldValidator></div>
                            </div>
                            <div class="CustomerRegisterRight">
                                <div class="CustomerRegisterRightLabel">
                                    [$Zipcode]</div>
                                <div class="CustomerRegisterRightData">
                                    <asp:TextBox ID="uxZip" runat="server" ValidationGroup="AccountDetail" MaxLength="9"
                                        CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox>
                                    <span class="CommonAsterisk">*</span>
                                    <asp:RequiredFieldValidator ID="uxRequiredZipValidator" runat="server" ControlToValidate="uxZip"
                                        Text="Required Zip" ValidationGroup="AccountDetail" Display="Dynamic" CssClass="CommonValidatorText">
                                        <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Zip
                                    </asp:RequiredFieldValidator></div>
                            </div>
                            <uc1:CountryAndState ID="uxCountryAndState" runat="server" IsRequiredCountry="true"
                                IsRequiredState="true" IsCountryWithOther="true" IsStateWithOther="true" CssPanel="ClientCityStatePanel"
                                CssLabel="CustomerRegisterLabel" />
                            <div id="uxBillingCountryStateDiv" runat="server" class="CommonValidatorText CustomerRegisterPanelCountryValidatorText"
                                visible="false">
                                <div class="CommonValidateDiv">
                                </div>
                                <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" />
                                <asp:Label ID="uxBillingCountryStateMessage" runat="server"></asp:Label>
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
                            <div class="CustomerRegisterLeftLabel2">
                                &nbsp;</div>
                            <asp:CheckBox ID="uxSubscribeCheckBox" runat="server" ValidationGroup="AccountDetail"
                                Text=" [$SubScribe]" Checked="true" CssClass="CustomerRegisterCheckBox" />
                        </asp:Panel>
                        <asp:Panel ID="uxCustomerTaxExemptPanel" runat="server">
                            <div class="CommonFormLabel">
                                [$TaxExemptID]</div>
                            <div class="CommonFormData">
                                <asp:Label ID="uxTaxExemptID" runat="server" CssClass="CheckoutTextBox">
                                </asp:Label>
                            </div>
                            <div class="CommonFormLabel">
                                [$TaxExemptCountry]</div>
                            <div class="CommonFormData">
                                <asp:Label ID="uxTaxExemptCountry" runat="server" CssClass="CheckoutTextBox">
                                </asp:Label>
                            </div>
                            <div class="CommonFormLabel">
                                [$TaxExemptState]</div>
                            <div class="CommonFormData">
                                <asp:Label ID="uxTaxExemptState" runat="server" CssClass="CheckoutTextBox">
                                </asp:Label>
                            </div>
                            <div class="Clear">
                            </div>
                        </asp:Panel>
                        <div class="AccountDetailsButtonDiv">
                            <asp:LinkButton ID="uxUpdateImageButton" runat="server" OnClick="uxUpdateImageButton_Click"
                                Text="[$BtnSubmit]" ValidationGroup="AccountDetail" CssClass="BtnStyle1" />
                        </div>
                        <div class="Clear">
                        </div>
                    </div>
                </div>
            </div>
            <div class="CommonPageBottom">
                <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/DefaultBoxBottomLeft.gif"
                    runat="server" CssClass="CommonPageBottomImgLeft" />
                <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/DefaultBoxBottomRight.gif"
                    runat="server" CssClass="CommonPageBottomImgRight" />
                <div class="Clear">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
