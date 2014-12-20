using System;
using System.Web.UI.WebControls;
using Vevo.Domain;


public partial class Components_PromotionAds : Vevo.WebUI.BaseControls.BaseUserControl
{
    //private void PopulateControls()
    //{
    //    uxImage.ImageUrl = "~/" + DataAccessContext.Configurations.GetValue( "PromotionImageAds" );
    //    uxPromotionAdsLink.NavigateUrl = DataAccessContext.Configurations.GetValue("PromotionAdsNavigation");
    //}


    //protected void Page_Load( object sender, EventArgs e )
    //{
    //    if (!IsPostBack &&
    //        DataAccessContext.Configurations.GetBoolValue("PromotionAdsDisplay"))
    //    {
    //        PopulateControls();
    //    }
    //    else
    //    {
    //        this.Visible = false;
    //    }
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        // Fake banner - navigation
        uxImage.ImageUrl = "~/Images/Configuration/PromoitonAds.jpg";
        uxPromotionAdsLink.NavigateUrl = "~/PromotionGroupList.aspx";
    }
}
