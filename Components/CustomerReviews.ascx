<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustomerReviews.ascx.cs"
    Inherits="Components_CustomerReviews" %>
<%@ Import Namespace="Vevo.Domain" %>
<%@ Register Src="RatingControl.ascx" TagName="RatingControl" TagPrefix="uc3" %>
<div class="CustomerReviews">
    <div class="CustomerReviewsTop">
        <div class="CustomerReviewsTopLeft">
        </div>
        <div id="P1" class="CustomerReviewsTopTitle" runat="server">
            [$CustomerReview]
        </div>
        <div class="CustomerReviewsTopRight">
        </div>
        <div class="Clear">
        </div>
    </div>
    <div class="CustomerReviewsLeft">
        <div class="CustomerReviewsRight">
            <asp:DataList ID="uxCustomerReviewList" runat="server" RepeatColumns="1" RepeatDirection="Horizontal"
                CssClass="CustomerReviewsDataList">
                <ItemTemplate>
                    <table class="CustomerReviewsItemTable">
                        <tr>
                            <td class="CustomerReviewsItemDateColumn">
                                <div class="CustomerReviewsRating">
                                    <uc3:RatingControl ID="uxRatingControl" runat="server" CurrentRating='<%# GetCustomerRating( Eval( "ReviewID" ) ) %>'
                                        HideOnZero="True" Visible='<%# DataAccessContext.Configurations.GetBoolValue( "CustomerRating" ) %>' />
                                </div>
                                
                                <div id="uxCustomerReviewSubjectDIV" runat="server" class="CustomerReviewsSubject" 
                                    visible='<%# DataAccessContext.Configurations.GetBoolValue( "CustomerReview" ) %>'>
                                    <%# Eval("Subject") %>
                                </div>
                                <div class="Clear">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="CustomerReviewsItemBodyColumn">
                                <div id="uxCustomerReviewItemBodyDIV" runat="server" 
                                    visible='<%# DataAccessContext.Configurations.GetBoolValue( "CustomerReview" ) %>'>
                                    <%# Eval("Body") %>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="CustomerReviewsItemByColumn">
                                <div class="CustomerReviewsByLabel">
                                    [$By] :</div>
                                <div class="CustomerReviewsByValue">
                                    <%# Eval( DataAccessContext.Configurations.GetValue( "ReviewDisplayNameBy" ) )%>
                                </div>
                                <div class="CustomerReviewsDate">
                                    <%# Convert.ToDateTime( Eval( "ReviewDate" ) ).ToShortDateString() %>
                                </div>
                                <div class="Clear">
                                </div>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
                <ItemStyle CssClass="CustomerReviewsItemStyle" />
            </asp:DataList>
        </div>
    </div>
    <div class="CustomerReviewsBottom">
        <div class="CustomerReviewsBottomLeft">
        </div>
        <div class="CustomerReviewsBottomRight">
        </div>
        <div class="Clear">
        </div>
    </div>
</div>
