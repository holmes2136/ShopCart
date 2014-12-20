<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GiftRegistryDetail.ascx.cs"
    Inherits="Components_GiftRegistryDetail" %>
<%@ Register Src="CalendarPopup.ascx" TagName="CalendarPopup" TagPrefix="uc3" %>
<%@ Register Src="CountryAndStateList.ascx" TagName="CountryState" TagPrefix="uc2" %>
<div class="GiftRegistryDetail">
    <div class="GiftRegistryDetailNote">
        [$Required Fields]</div>
    <div class="GiftRegistryDetailDiv">
        <div id="uxSummaryValidDIV" class="CommonValidateText">
            <asp:Literal ID="uxSummaryLiteral" runat="server"></asp:Literal>
        </div>
        <div class="Clear">
        </div>
        <div class="CommonFormLabel">
            [$EventName]</div>
        <div class="CommonFormData GiftRegistryForm">
            <asp:TextBox ID="uxEventName" runat="server" CssClass="CommonTextBox GiftRegistryDetailTextBox"
                ValidationGroup="ValidateGift"></asp:TextBox>
            <span class="CommonAsterisk">*</span>
            <asp:RequiredFieldValidator ID="uxRequiredEventNameValidator" runat="server" ControlToValidate="uxEventName"
                ValidationGroup="ValidateGift" Display="Dynamic" CssClass="CommonValidatorText">
                <div class="CommonValidateDiv GiftRegistryValidate"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Event Name
            </asp:RequiredFieldValidator>
        </div>
        <div class="CommonFormLabel">
            [$EventDate]
        </div>
        <div class="CommonFormData">
            <uc3:CalendarPopup ID="uxEventDateCalendarPopup" runat="server" TextBoxEnabled="false" />
            <span class="CommonAsterisk">*</span>
            <asp:RequiredFieldValidator ID="uxEventDateRequiredValidator" runat="server" ControlToValidate="uxEventDateCalendarPopup"
                ValidationGroup="ValidateGift" Display="Dynamic" CssClass="CommonValidatorText"
                EnableClientScript="false">
            <div class="CommonValidateDiv GiftRegistryValidate"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Event date is required
            </asp:RequiredFieldValidator>
        </div>
        <div class="Clear">
        </div>
        <div class="CommonPageInnerTitle">
            [$ShippingAddress]
        </div>
        <uc2:CountryState ID="uxCountryState" runat="server" IsRequiredCountry="true" IsRequiredState="true"
            IsCountryWithOther="true" IsStateWithOther="true" CssPanel="GiftRegistryDetailCityStatePanel"
            CssLabel="CommonFormLabel" />
        <div class="CommonFormLabel">
            [$Company]
        </div>
        <div class="CommonFormData">
            <asp:TextBox ID="uxCompany" runat="server" CssClass="CommonTextBox GiftRegistryDetailLongTextBox"></asp:TextBox>
        </div>
        <div class="CommonFormLabel">
            [$Address]
        </div>
        <div class="CommonFormData GiftRegistryForm">
            <asp:TextBox ID="uxAddress1" runat="server" CssClass="CommonTextBox  GiftRegistryDetailLongTextBox"
                ValidationGroup="ValidateGift"></asp:TextBox>
            <span class="CommonAsterisk">*</span>
            <asp:RequiredFieldValidator ID="uxRequiredAddressValidator" runat="server" ControlToValidate="uxAddress1"
                ValidationGroup="ValidateGift" Display="Dynamic" CssClass="CommonValidatorText">
                <div class="CommonValidateDiv GiftRegistryValidateLong"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Address
            </asp:RequiredFieldValidator>
        </div>
        <div class="CommonFormLabel">
            &nbsp;
        </div>
        <div class="CommonFormData">
            <asp:TextBox ID="uxAddress2" runat="server" CssClass="CommonTextBox  GiftRegistryDetailLongTextBox"></asp:TextBox>
        </div>
        <div class="CommonFormLabel">
            [$City]
        </div>
        <div class="CommonFormData GiftRegistryForm">
            <asp:TextBox ID="uxCity" runat="server" ValidationGroup="ValidateGift" CssClass="CommonTextBox GiftRegistryDetailTextBox"></asp:TextBox>
            <span class="CommonAsterisk">*</span>
            <asp:RequiredFieldValidator ID="uxRequiredCityValidator" runat="server" ControlToValidate="uxCity"
                ValidationGroup="ValidateGift" Display="Dynamic" CssClass="CommonValidatorText">
                <div class="CommonValidateDiv GiftRegistryValidate"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required City
            </asp:RequiredFieldValidator>
        </div>
        <div class="CommonFormLabel">
            [$Zipcode]
        </div>
        <div class="CommonFormData GiftRegistryForm">
            <asp:TextBox ID="uxZip" runat="server" ValidationGroup="ValidateGift" MaxLength="9"
                CssClass="CommonTextBox GiftRegistryDetailTextBox"></asp:TextBox>
            <span class="CommonAsterisk">*</span>
            <asp:RequiredFieldValidator ID="uxRequiredZipValidator" runat="server" ControlToValidate="uxZip"
                ValidationGroup="ValidateGift" Display="Dynamic" CssClass="CommonValidatorText">
                <div class="CommonValidateDiv GiftRegistryValidate"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Zip
            </asp:RequiredFieldValidator>
        </div>
        <div class="CommonFormLabel">
            [$Phone]
        </div>
        <div class="CommonFormData">
            <asp:TextBox ID="uxPhone" runat="server" CssClass="CommonTextBox GiftRegistryDetailTextBox"></asp:TextBox>
        </div>
        <div class="CommonFormLabel">
            [$Fax]
        </div>
        <div class="CommonFormData">
            <asp:TextBox ID="uxFax" runat="server" CssClass="CommonTextBox GiftRegistryDetailTextBox"></asp:TextBox>
        </div>
        <div id="uxResidentialLabelDiv" runat="server" class="CommonFormLabel">
            [$Residential]
        </div>
        <div id="uxResidentialDataDiv" runat="server" class="CommonFormData">
            <asp:DropDownList ID="uxResidentialDrop" runat="server">
                <asp:ListItem Value="True" Selected="True">[$Yes]</asp:ListItem>
                <asp:ListItem Value="False">[$No]</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="CommonFormLabel">
            &nbsp;
        </div>
        <div class="CommonFormData">
            <asp:CheckBox ID="uxHideAddressCheck" runat="server" CssClass="CommonCheckBox" Text="[$HideAddress]" />
        </div>
        <div class="CommonFormLabel">
            &nbsp;
        </div>
        <div class="CommonFormData">
            <asp:CheckBox ID="uxHideEventCheck" runat="server" CssClass="CommonCheckBox" Text="[$HideEvent]" />
        </div>
        <div class="CommonFormLabel">
            &nbsp;
        </div>
        <div class="CommonFormData">
            <asp:CheckBox ID="uxNotifyNewOrderCheck" runat="server" CssClass="CommonCheckBox"
                Text="[$NotifyNewOrder]" />
        </div>
        <div class="Clear">
        </div>
        <div class="GiftRegistryDetailButtonDiv">
            <asp:LinkButton ID="uxAddLinkButton" runat="server" OnClick="uxAddLinkButton_Click"
                ValidationGroup="ValidateGift" Text="[$BtnAddNewGiftRegistry]" CssClass="GiftRegistryDetailAddImage BtnStyle1" />
            <asp:LinkButton ID="uxEditLinkButton" runat="server" OnClick="uxEditLinkButton_Click"
                ValidationGroup="ValidateGift" Text="[$BtnEditGiftRegistry]" CssClass="GiftRegistryDetailEditImage BtnStyle2" />
            <asp:LinkButton ID="uxBackLink" runat="server" PostBackUrl="~/GiftRegistryList.aspx"
                Text="[$BtnCancel]" CssClass="GiftRegistryDetailBackLinkImage BtnStyle2" />
            <div class="Clear">
            </div>
        </div>
        <div class="Clear">
        </div>
    </div>
</div>
