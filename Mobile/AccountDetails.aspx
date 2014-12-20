<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile/Mobile.master" AutoEventWireup="true"
    CodeFile="AccountDetails.aspx.cs" Inherits="Mobile_AccountDetails" %>

<%@ Register Src="~/Components/VevoLinkButton.ascx" TagName="LinkButton" TagPrefix="ucLinkButton" %>
<%@ Register Src="~/Components/CountryAndStateList.ascx" TagName="CountryAndState"
    TagPrefix="uc1" %>
<%@ Register Src="Components/ShippingAddressItemDetails.ascx" TagName="ShippingAddressDetails"
    TagPrefix="uc2" %>
<%@ Register Src="Components/MobileMessage.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="MobileTitle">
        [$Head]
    </div>
    <div class="MobileCommonBox">
        <uc3:Message ID="uxMessage" runat="server" NumberOfNewLines="0" />
        <div class="MobileUserLoginControl">
            <asp:Panel ID="uxBillingPanel" runat="server" CssClass="MobileUserLoginControlPanel">
                <div class="MobileCommonPageInnerTitle">
                    [$Bill]</div>
                <div class="MobileUserLoginControl">
                    <div class="MobileCommonFormLabel">
                    </div>
                    <div class="MobileCommonFormData">
                        <asp:TextBox ID="uxUserName" runat="server" Visible="False" CssClass="MobileCommonTextBox MobileAccountDetailsTextBox">
                        </asp:TextBox>
                    </div>
                    <div class="MobileCommonFormData">
                        <asp:TextBox ID="uxPassword" runat="server" Visible="False" CssClass="MobileCommonTextBox MobileAccountDetailsTextBox">
                        </asp:TextBox>
                    </div>
                </div>
                <div class="MobileUserLoginControl">
                    <div class="MobileCommonFormLabel">
                        [$Fname]</div>
                    <div class="MobileCommonFormData">
                        <asp:TextBox ID="uxFirstName" runat="server" ValidationGroup="AccountDetail" CssClass="MobileCommonTextBox MobileAccountDetailsTextBox">
                        </asp:TextBox>
                        <span class="MobileCommonAsterisk">*</span>
                        <asp:RequiredFieldValidator ID="uxRequiredFirstNameValidator" runat="server" ControlToValidate="uxFirstName"
                            ValidationGroup="AccountDetail" Display="Dynamic" CssClass="MobileCommonValidatorText">
                        <div class="MobileCommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required First Name
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="MobileUserLoginControl">
                        <div class="MobileCommonFormLabel">
                            [$Lname]</div>
                        <div class="MobileCommonFormData">
                            <asp:TextBox ID="uxLastName" runat="server" ValidationGroup="AccountDetail" CssClass="MobileCommonTextBox MobileAccountDetailsTextBox">
                            </asp:TextBox>
                            <span class="MobileCommonAsterisk">*</span>
                            <asp:RequiredFieldValidator ID="uxRequiredLastNameValidator" runat="server" ControlToValidate="uxLastName"
                                ValidationGroup="AccountDetail" Display="Dynamic" CssClass="MobileCommonValidatorText">
                                <div class="MobileCommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Last Name
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="MobileUserLoginControl">
                            <div class="MobileCommonFormLabel">
                                [$Email]</div>
                            <div class="MobileCommonFormData">
                                <asp:TextBox ID="uxEmail" runat="server" CssClass="MobileCommonTextBox MobileAccountDetailsTextBox">
                                </asp:TextBox>
                                <span class="MobileCommonAsterisk">*</span>
                                <asp:RequiredFieldValidator ID="uxRequiredEmailValidator" runat="server" ControlToValidate="uxEmail"
                                    ValidationGroup="AccountDetail" Display="Dynamic" CssClass="MobileCommonValidatorText">
                                    <div class="MobileCommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Email Address
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="uxRegularEmailValidator" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                    ControlToValidate="uxEmail" ValidationGroup="AccountDetail" Display="Dynamic"
                                    CssClass="MobileCommonValidatorText">
                                    <div class="MobileCommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Wrong Email Format
                                </asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="MobileUserLoginControl">
                            <div class="MobileCommonFormLabel">
                                [$Company]</div>
                            <div class="MobileCommonFormData">
                                <asp:TextBox ID="uxCompany" runat="server" CssClass="MobileCommonTextBox MobileAccountDetailsTextBox">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="MobileUserLoginControl">
                            <div class="MobileCommonFormLabel">
                                [$Address]</div>
                            <div class="MobileCommonFormData">
                                <asp:TextBox ID="uxAddress1" runat="server" CssClass="MobileCommonTextBox MobileAccountDetailsLongTextBox">
                                </asp:TextBox>
                                <span class="MobileCommonAsterisk">*</span>
                                <asp:RequiredFieldValidator ID="uxRequiredAddressValidator" runat="server" ControlToValidate="uxAddress1"
                                    ValidationGroup="AccountDetail" Display="Dynamic" CssClass="MobileCommonValidatorText">
                                    <div class="MobileCommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Address
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="MobileUserLoginControl">
                            <div class="MobileCommonFormLabel">
                                &nbsp;</div>
                            <div class="MobileCommonFormData">
                                <asp:TextBox ID="uxAddress2" runat="server" CssClass="MobileCommonTextBox MobileAccountDetailsLongTextBox">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="MobileUserLoginControl">
                            <div class="MobileCommonFormLabel">
                                [$City]</div>
                            <div class="MobileCommonFormData">
                                <asp:TextBox ID="uxCity" runat="server" ValidationGroup="AccountDetail" CssClass="MobileCommonTextBox MobileAccountDetailsLongTextBox">
                                </asp:TextBox>
                                <span class="MobileCommonAsterisk">*</span>
                                <asp:RequiredFieldValidator ID="uxRequiredCityValidator" runat="server" ControlToValidate="uxCity"
                                    ValidationGroup="AccountDetail" Display="Dynamic" CssClass="MobileCommonValidatorText">
                                    <div class="MobileCommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required City
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="MobileUserLoginControl">
                            <div class="MobileCommonFormLabel">
                                [$Zipcode]</div>
                            <div class="MobileCommonFormData">
                                <asp:TextBox ID="uxZip" runat="server" ValidationGroup="AccountDetail" CssClass="MobileCommonTextBox MobileAccountDetailsTextBox">
                                </asp:TextBox>
                                <span class="MobileCommonAsterisk">*</span>
                                <asp:RequiredFieldValidator ID="uxRequiredZipValidator" runat="server" ControlToValidate="uxZip"
                                    ValidationGroup="AccountDetail" Display="Dynamic" CssClass="MobileCommonValidatorText">
                                    <div class="MobileCommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Zip
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="MobileUserLoginControl">
                            <uc1:CountryAndState ID="uxCountryAndState" runat="server" IsRequiredCountry="true"
                                IsRequiredState="true" IsCountryWithOther="true" IsStateWithOther="true" CssPanel="MobileAccountDetailsCountryStatePanel"
                                CssLabel="MobileCommonFormLabel" />
                            <div id="uxBillingCountryStateDiv" runat="server" class="MobileCommonValidatorText CustomerRegisterPanelCountryValidatorText"
                                visible="false">
                                <div class="MobileCommonValidateDiv CountryAndStateValidateDiv">
                                </div>
                                <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" />
                                <asp:Label ID="uxBillingCountryStateMessage" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="MobileUserLoginControl">
                            <div class="MobileCommonFormLabel">
                                [$Phone]</div>
                            <div class="MobileCommonFormData">
                                <asp:TextBox ID="uxPhone" runat="server" CssClass="MobileCommonTextBox MobileAccountDetailsTextBox">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="MobileUserLoginControl">
                            <div class="MobileCommonFormLabel">
                                [$Fax]</div>
                            <div class="MobileCommonFormData">
                                <asp:TextBox ID="uxFax" runat="server" CssClass="MobileCommonTextBox MobileAccountDetailsTextBox">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="MobileCustomerRegisterCheckBox MobileUserLoginControl">
                            <asp:CheckBox ID="uxSubscribeCheckBox" runat="server" Text="[$SubScribe]" CssClass="MobileCustomerRegisterCheckBox" />
                        </div>
                    </div>
            </asp:Panel>
            <div class="MobileUserLoginControlPanel">
                <asp:LinkButton ID="uxUpdateImageButton" runat="server" Text="[$UpdateAccount]" OnClick="uxUpdateButton_Click"
                    CssClass="MobileButton" ValidationGroup="AccountDetail" />
            </div>
                <div class="MobileUserLoginControlPanel">
                    <asp:LinkButton ID="uxAddNewShippingAddress" runat="server" Text="[$AddNewShippingAddress]"
                        CssClass="MobileButton MobileCouponButton" OnClick="uxAddNewShippingAddress_Click" />
                </div>
            <asp:Panel ID="uxShippingPanel" runat="server" CssClass="MobileUserLoginControlPanel">
                <div class="MobileCommonPageInnerTitle">
                    [$Shipping]</div>

                <div class="Clear">
                </div>
                <div class="MobileUserLoginControl">
                    <asp:DataList ID="uxList" CssClass="MobileUserLoginControl" runat="server" ShowFooter="false"
                        ShowHeader="false" CellSpacing="15">
                        <ItemTemplate>
                            <div Class="MobileShippingAddress" >
                                <uc2:ShippingAddressDetails ID="uxItem" runat="server" ShippingAddressID='<%# (Eval("ShippingAddressID").ToString()) %>' />
                            </div>
                        </ItemTemplate>
                        <ItemStyle />
                    </asp:DataList>
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
