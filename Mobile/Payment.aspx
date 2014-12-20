<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile/Mobile.master" AutoEventWireup="true"
    CodeFile="Payment.aspx.cs" Inherits="Mobile_Payment" %>

<%@ Register Src="Components/MobileMessage.ascx" TagName="Message" TagPrefix="uc1" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="MobilePayment">
        <div class="MobileCommonPage">
            <div class="MobileCommonPageTop">
                <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/DefaultBoxTopLeft.gif" runat="server"
                    Cssclass="MobileCommonPageTopImgLeft" />
                <asp:Label ID="uxDefaultTitle" runat="server" Cssclass="MobileCommonPageTopTitle">[$Head]</asp:Label>
                <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/DefaultBoxTopRight.gif"
                    runat="server" Cssclass="MobileCommonPageTopImgRight" />
                <div class="Clear">
                </div>
            </div>
            <div class="MobileCommonPageLeft">
                <div class="MobileCommonPageRight">
                    <div class="MobilePaymentDiv">
                        <uc1:Message ID="uxMessage" runat="server" NumberOfNewLines="2" FontSize="1em" />
                        <uc1:Message ID="uxValidateMessage" runat="server" NumberOfNewLines="2" FontSize="1em" />
                        <div class="MobileCommonPageInnerTitle">
                            [$Command]</div>
                        <%--<asp:DataList ID="uxPaymentList" Cssclass="MobilePaymentDataList" runat="server" DataKeyField="Name"
                            OnItemDataBound="uxPaymentList_ItemDataBound">
                            <ItemTemplate>
                                <div class="MobilePaymentItemDiv" style='<%# (IsRecurring( Eval("Name") )? "display:block": "display:none") %>'>
                                    <div class="MobilePaymentItemNameDiv">
                                        <div class="MobilePaymentItemOptionsDiv">
                                            <asp:RadioButton ID="uxRadio" runat="server" Text='<%# Eval( "DisplayName" ) %>'
                                                GroupName="PaymentGroup" OnDataBinding="uxRadio_DataBinding" Enabled='<%# IsRecurring( Eval("Name") ) %>'
                                                Cssclass="MobilePaymentItemRadioButton" />
                                            <asp:DropDownList ID="uxDrop" runat="server" Visible="false" Cssclass="MobileCommonDropDownList" />
                                            <asp:HiddenField ID="uxPaymentNameHidden" runat="server" Value='<%# Eval("Name") %>' />
                                            <div class="Clear">
                                            </div>
                                        </div>
                                        <div class="MobilePaymentItemDescriptionDiv">
                                            <%# Eval( "Description" ) %>
                                        </div>
                                        <div class="Clear">
                                        </div>
                                    </div>
                                    <div class="MobilePaymentItemImageDiv">
                                        <asp:Image ID="uxImage" runat="server" ImageUrl='<%# Eval( "PaymentImage" ) %>' Visible='<%# IsStringEmpty( Eval( "PaymentImage" ) ) %>'
                                            Cssclass="MobilePaymentItemImage" />
                                    </div>
                                    <div class="Clear">
                                    </div>
                                </div>
                            </ItemTemplate>
                            <ItemStyle Cssclass="MobilePaymentDataListItemStyle" />
                        </asp:DataList>--%>
                        <div id="uxPolicyAgreementDiv" runat="server" visible="false">
                            <div class="MobilePaymentAgreeDiv">
                                <div class="MobilePaymentAgreeBox" id="uxLicenseDiv" runat="server">
                                </div>
                                <div class="MobilePaymentAgreeCheckBox">
                                    <asp:CheckBox ID="uxAgreeChecked" runat="server" Text="[$IAgree]" />
                                </div>
                            </div>
                        </div>
                        <div class="MobilePaymentButtonDiv">
                            <vevo:ImageButton ID="uxPaymentImageButton" runat="server" OnClick="uxPaymentImageButton_Click"
                                ThemeImageUrl="[$NextButton]" Cssclass="MobilePaymentImageButton" />
                        </div>
                        <div class="Clear">
                        </div>
                    </div>
                </div>
            </div>
            <div class="MobileCommonPageBottom">
                <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/DefaultBoxBottomLeft.gif"
                    runat="server" Cssclass="MobileCommonPageBottomImgLeft" />
                <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/DefaultBoxBottomRight.gif"
                    runat="server" Cssclass="MobileCommonPageBottomImgRight" />
                <div class="Clear">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
