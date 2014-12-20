using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.DataAccessLib;
using Vevo.Domain;
using Vevo.Domain.Configurations;
using Vevo.WebAppLib;

public partial class AdminAdvanced_Components_SiteConfig_RatingReview : System.Web.UI.UserControl
{
    public void PopulateControls()
    {
        uxRatingStarAmountText.Text = DataAccessContext.Configurations.GetValue( "StarRatingAmount" ).ToString();
        uxMerchantRatingDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "MerchantRating" ).ToString();
        uxCustomerRatingDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "CustomerRating" ).ToString();
        RatingRequireLoginDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "RatingRequireLogin" ).ToString();
        uxCustomerReviewDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "CustomerReview" ).ToString();
        ReviewRequireLoginDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "ReviewRequireLogin" ).ToString();
        uxCustomerDispalyByDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "ReviewDisplayNameBy" ).ToString();
    }

    public void Update()
    {
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["StarRatingAmount"],
            uxRatingStarAmountText.Text.Trim() );
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["MerchantRating"],
            uxMerchantRatingDrop.SelectedValue.ToString() );
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["CustomerRating"],
            uxCustomerRatingDrop.SelectedValue.ToString() );
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["RatingRequireLogin"],
            RatingRequireLoginDrop.SelectedValue.ToString() );
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["CustomerReview"],
            uxCustomerReviewDrop.SelectedValue.ToString() );
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["ReviewRequireLogin"],
            ReviewRequireLoginDrop.SelectedValue.ToString() );
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["ReviewDisplayNameBy"],
            uxCustomerDispalyByDrop.SelectedValue.ToString() );
    }

    protected void Page_Load( object sender, EventArgs e )
    {

    }
}
