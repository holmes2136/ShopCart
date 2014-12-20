<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CheckoutPaymentMethods.ascx.cs"
    Inherits="Components_CheckoutPaymentMethods" %>
<%@ Register Src="Message.ascx" TagName="Message" TagPrefix="uc1" %>
<div class="PaymentDiv">
    <div class="SidebarTop">
        <asp:Label ID="uxHeaderCheckoutLabel" runat="server" Text="[$Title]" CssClass="CheckoutAddressTitle"></asp:Label>
    </div>
    <div class="CheckoutInnerTitle" runat="server" id="uxCheckoutInnerTitle">
        [$Command]
    </div>
    <div id="uxPaymentValidatorDiv" runat="server" class="CommonValidatorText PaymentValidatorText"
        visible="false">
        <img src="Images/Design/Bullet/RequiredFillBullet_Down.gif" />
        <asp:Label ID="uxMessage" runat="server"></asp:Label>
        <div class="CommonValidateDiv PaymentValidateDiv">
        </div>
    </div>
    <asp:DataList ID="uxPaymentList" CssClass="PaymentDataList" runat="server" DataKeyField="Name"
        OnItemDataBound="uxPaymentList_ItemDataBound">
        <ItemTemplate>
            <div class="PaymentItemDiv" style='<%# (IsRecurring( Eval("Name") )? "display:block": "display:none") %>'>
                <div class="PaymentItemNameDiv">
                    <div class="PaymentItemOptionsDiv">
                        <asp:RadioButton ID="uxRadio" runat="server" Text='<%# Eval( "DisplayName" ) %>'
                            GroupName="PaymentGroup" OnDataBinding="uxRadio_DataBinding" Enabled='<%# IsRecurring( Eval("Name") ) %>'
                            CssClass="PaymentItemRadioButton" OnCheckedChanged="uxRadio_CheckedChanged" />
                        <asp:DropDownList ID="uxDrop" runat="server" Visible="false" CssClass="CommonDropDownList" />
                        <asp:HiddenField ID="uxPaymentNameHidden" runat="server" Value='<%# Eval("Name") %>' />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="PaymentItemDescriptionDiv">
                        <%# Eval( "Description" ) %>
                    </div>
                    <div class="Clear">
                    </div>
                </div>
                <div class="PaymentItemImageDiv">
                    <asp:Image ID="uxImage" runat="server" ImageUrl='<%# Eval( "PaymentImage" ) %>' Visible='<%# IsStringEmpty( Eval( "PaymentImage" ) ) %>'
                        CssClass="PaymentItemImage" />
                </div>
                <div class="Clear">
                </div>
            </div>
        </ItemTemplate>
        <ItemStyle CssClass="PaymentDataListItemStyle" />
    </asp:DataList>
    <asp:Panel ID="uxPOpanel" runat="server" CssClass="PaymentPOPanel">
        <div class="CheckoutInnerTitle">
            [$InputPurchaseOrderNumber]</div>
        <div class="PaymentItemTextboxDiv">
            <asp:Label ID="uxPONumberLabel" runat="server" Text="PO Number : " CssClass="CommonFormLabel"></asp:Label>
            <div class="CommonFormData">
                <asp:TextBox ID="uxPONumberText" runat="server" MaxLength="255" CssClass="CommonTextBox"></asp:TextBox>
                <asp:Label ID="uxPORequire" runat="server" Text=" *" ForeColor="Red"></asp:Label>
                <div id="uxPOValidatorDiv" runat="server" class="CommonValidatorText" visible="false">
                    <div class="CommonValidateDiv">
                    </div>
                    <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" />
                    <asp:Label ID="uxPOMessage" runat="server"></asp:Label>
                </div>
            </div>
        </div>
    </asp:Panel>
    <div id="uxPolicyAgreementDiv" runat="server" visible="false">
        <div class="PaymentAgreeDiv">
            <div class="PaymentAgreeBox" id="uxLicenseDiv" runat="server">
            </div>
            <div id="uxPolicyAgreementValidatorDiv" runat="server" class="CommonValidatorText PaymentValidatorText1"
                visible="false">
                <div class="CommonValidateDiv PaymentValidateDiv1">
                </div>
                <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" />
                <asp:Label ID="uxPolicyAgreementMessage" runat="server"></asp:Label>
            </div>
            <div class="PaymentAgreeCheckBox">
                <asp:CheckBox ID="uxAgreeChecked" runat="server" Text="[$IAgree]" />
            </div>
        </div>
    </div>
    <div class="Clear">
    </div>
</div>
