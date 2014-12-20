<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RatingCustomer.ascx.cs"
    Inherits="Components_RatingCustomer" %>
<%@ Register Src="RatingControl.ascx" TagName="RatingControl" TagPrefix="uc1" %>
<div id="uxReviewPlaceHolder" runat="server" class="RatingCustomerDiv">
    <div class="RatingCustomerRatingControlDiv">
        <uc1:RatingControl ID="uxRatingControl" runat="server" />
    </div>
    <div class="RatingCustomerMessageDiv">
        [$base on]
        <asp:Label ID="uxBaseOnAmount" runat="server" CssClass="RatingCustomerBaseOnAmountLabel"></asp:Label>
        [$Review]</div>
    <div class="Clear">
    </div>
</div>
<asp:PlaceHolder ID="uxNoReviewPlaceHolder" runat="server">[$NoReview] </asp:PlaceHolder>
