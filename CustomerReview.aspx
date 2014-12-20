<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" ValidateRequest="false"
    CodeFile="CustomerReview.aspx.cs" Inherits="CustomerReview" Title="Customer Review" %>

<%@ Register Src="Components/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="~/Components/CatalogImage.ascx" TagName="CatalogImage" TagPrefix="uc2" %>
<%@ Register Src="~/Components/RatingCustomer.ascx" TagName="RatingCustomer" TagPrefix="uc4" %>
<%@ Import Namespace="Vevo.Domain" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="CustomerReview">
        <div class="CommonPage">
            <div class="CommonPageTop">
                <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/DefaultBoxTopLeft.gif" runat="server"
                    CssClass="CommonPageTopImgLeft" />
                <asp:Label ID="uxDefaultTitle" runat="server" CssClass="CommonPageTopTitle">[$Customer Review]</asp:Label>
                <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/DefaultBoxTopRight.gif"
                    runat="server" CssClass="CommonPageTopImgRight" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonPageLeft">
                <div class="CommonPageRight">
                    <div class="CustomerReviewProduct">
                        <div class="CustomerReviewProductDetail">
                            <div class="ProductImage">
                                <uc2:CatalogImage ID="uxCatalogImage" runat="server" MaximumWidth="150px" />
                            </div>
                            <div class="ProductInfo">
                                <asp:HyperLink ID="uxNameLink" runat="server" CssClass="InfoName" />
                                <asp:Label ID="uxSkuLabel" runat="server" CssClass="InfoSku" Visible='<%# DataAccessContext.Configurations.GetBoolValue( "ShowSkuMode" ) %>' />
                                <div class="InfoRating">
                                    <asp:Label ID="uxRatingLabel" runat="server" CssClass="InfoTitleLabel" Text="[$Overall Rating]" />
                                    <uc4:RatingCustomer ID="uxRatingCustomer" runat="server" />
                                </div>
                                <div id="uxRetailPriceDiv" runat="server" class="InfoRetailPrice">
                                    <asp:Label ID="uxPriceLabel" runat="server" CssClass="InfoTitleLabel" Text="[$Price]" />
                                    <asp:Label ID="uxRetailPriceLabel" runat="server" CssClass="RetailPriceValue" />
                                </div>
                                <div id="uxPriceDiv" runat="server" class="InfoPrice">
                                    <asp:Label ID="uxPriceValueLabel" runat="server" CssClass="PriceValue" />
                                </div>
                                <asp:Label ID="uxShortDescriptionLabel" runat="server" CssClass="InfoShortDescription" />
                            </div>
                        </div>
                        <div class="CustomerReviewContent">
                            <div class="ReviewIntroHeader">
                                [$ReviewMessageP1]
                            </div>
                            <div class="ReviewIntroMessage">
                                [$ReviewMessageP2]
                            </div>
                            <div id="uxRatingDiv" runat="server" class="CustomerReviewStarRatingDiv">
                                <div class="ReviewTitle">
                                    [$Rating]
                                </div>
                                <div class="CustomerReviewStarList">
                                    <asp:RadioButtonList ID="uxProductRatingList" runat="server" CssClass="RatingRadio"
                                        RepeatDirection="Horizontal" RepeatLayout="FLOW">
                                    </asp:RadioButtonList>
                                </div>
                                <asp:RequiredFieldValidator ID="uxRatingListRequiredValidator" runat="server" ControlToValidate="uxProductRatingList"
                                    ValidationGroup="ValidRating" Display="Dynamic" CssClass="CommonValidatorText CustomerReviewStarRatingValidatorText">
                                    <div class="CommonValidateDiv CustomerReviewStarRatingValidateDiv">
                                    </div>
                                    <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> [$ErrorNoRatingSelected]
                                </asp:RequiredFieldValidator>
                            </div>
                            <asp:Panel ID="uxRatingLoginPanel" runat="server" CssClass="CustomerReviewRatingLoginPanel">
                                <asp:HyperLink ID="uxRatingLoginLink" runat="server" CssClass="CommonHyperLink">[$RatingLogin]</asp:HyperLink>
                            </asp:Panel>
                            <div id="uxReviewDiv" runat="server" class="CustomerReviewMessageDiv">
                                <div class="ReviewTitle">
                                    [$Review]
                                </div>
                                <div class="ReviewIntroMessage">
                                    [$WriteReviewGuide]</div>
                                <div class="CustomerReviewMessageDiv">
                                    <div class="ReviewSubjectRow">
                                        <div class="InfoTitleLabel">
                                            [$Subject]</div>
                                        <asp:TextBox ID="uxSubjectText" runat="server" ValidationGroup="ValidRating" MaxLength="255"
                                            CssClass="CommonTextBox CustomerReviewMessageTextBox">
                                        </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="uxRequiredSubjectValidator" runat="server" ControlToValidate="uxSubjectText"
                                            ValidationGroup="ValidRating" Display="Dynamic" CssClass="CommonValidatorText CustomerReviewValidatorText">
                                            <div class="CommonValidateDiv CustomerReviewValidateDiv"></div>
                                            <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> [$ErrorNoReviewSubject]
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="ReviewSubjectRow">
                                        <div class="InfoTitleLabel">
                                            [$WriteReview]</div>
                                        <asp:TextBox ID="uxBodyText" runat="server" CssClass="CommonTextBox CustomerReviewMessageTextBox"
                                            TextMode="MultiLine" Rows="8" ValidationGroup="ValidRating" />
                                        <asp:RequiredFieldValidator ID="uxRequiredBodyValidator" runat="server" ControlToValidate="uxBodyText"
                                            ValidationGroup="ValidRating" Display="Dynamic" CssClass="CommonValidatorText CustomerReviewValidatorText">
                                            <div class="CommonValidateDiv CustomerReviewValidateDiv"></div>
                                            <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> [$ErrorNoReviewBody]
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="CustomerReviewMessageLogingDiv">
                                <asp:HyperLink ID="uxReviewLogingLink" runat="server" CssClass="CommonHyperLink">[$ReviewLogin]</asp:HyperLink>
                            </div>
                            <div class="CustomerReviewButtonDiv">
                                <asp:LinkButton ID="uxSubmit" runat="server" OnClick="uxSubmit_Click" Text="[$BtnSubmit]"
                                    CssClass="CustomerReviewImageButton BtnStyle1" ValidationGroup="ValidRating" />
                            </div>
                            <div class="Clear">
                            </div>
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
