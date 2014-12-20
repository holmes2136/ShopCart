<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StarRatingSummary.ascx.cs"
    Inherits="Components_StarRatingSummary" %>
<%@ Register Src="RatingControl.ascx" TagName="RatingControl" TagPrefix="uc1" %>
<%@ Register Src="RatingCustomer.ascx" TagName="RatingCustomer" TagPrefix="uc2" %>
<%@ Import Namespace="Vevo.Domain" %>
<div class="StarRatingSummary">
    <table id="uxRatingReviewTB" runat="server" class="StarRatingSummaryTable">
        <tr>
            <td class="StarRatingSummaryDetailsColumn">
                <table class="StarRatingSummaryTableInner">
                    <tr id="uxMerchantRatingTR" runat="server" visible='<%# DataAccessContext.Configurations.GetBoolValue( "MerchantRating" ) %>'>
                        <td class="StarRatingSummaryTableInnerOurLabelColumn">
                            [$Our Rating]
                        </td>
                        <td class="StarRatingSummaryTableInnerOurStarColumn">
                            <uc1:RatingControl ID="uxMerchantRating" runat="server" CurrentRating='<%# Eval( "MerchantRating" ) %>' />
                        </td>
                    </tr>
                    <tr id="uxCustomerRatingTR" runat="server" visible='<%# DataAccessContext.Configurations.GetBoolValue( "CustomerRating" ) %>'>
                        <td class="StarRatingSummaryTableInnerCustomerLabelColumn">
                            [$Customer Rating]
                        </td>
                        <td class="StarRatingSummaryTableInnerCustomerStarColumn">
                            <uc2:RatingCustomer ID="uxRatingCustomer" runat="server" ProductID="<%# ProductID %>" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="StarRatingSummaryButtonColumn">
                <asp:LinkButton ID="uxAddReviewButton" runat="server" Text="[$BtnAddReview]" OnCommand="uxAddReviewButton_Command"
                    CommandArgument='<%#Vevo.UrlManager.GetCustomerReviewUrl( Eval( "ProductID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'
                    CssClass="StarRatingSummaryAddReviewImageButton BtnStyle1" />
            </td>
        </tr>
    </table>
</div>
