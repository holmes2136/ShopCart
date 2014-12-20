using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo.WebUI.International;
using Vevo.Domain;
using Vevo.WebUI;
using Vevo.Domain.Marketing;
using Vevo;
using Vevo.Deluxe.Domain;
using Vevo.Deluxe.Domain.BundlePromotion;

public partial class Mobile_PromotionDetail : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    #region Private

    private string PromotionGroupID
    {
        get
        {
            if (!String.IsNullOrEmpty( Request.QueryString["PromoID"] ))
                return Request.QueryString["PromoID"];
            else
                return DataAccessContextDeluxe.PromotionGroupRepository.GetPromotionGroupIDFromUrlName( PromotionGroupName );
        }
    }

    private string PromotionGroupName
    {
        get
        {
            if (Request.QueryString["PromoName"] == null)
                return String.Empty;
            else
                return Request.QueryString["PromoName"];
        }
    }

    private void RegisterStoreEvents()
    {
        GetStorefrontEvents().StoreCultureChanged +=
            new StorefrontEvents.CultureEventHandler( PromotionDetail_StoreCultureChanged );
        GetStorefrontEvents().StoreCurrencyChanged +=
            new StorefrontEvents.CurrencyEventHandler( PromotionDetail_StoreCurrencyChanged );
    }

    private void PromotionDetail_StoreCultureChanged( object sender, CultureEventArgs e )
    {
        uxItem.Reload();
    }

    private void PromotionDetail_StoreCurrencyChanged( object sender, CurrencyEventArgs e )
    {
        uxItem.Reload();
    }

    private void IsPromotionAvailable()
    {
        PromotionGroup group = DataAccessContextDeluxe.PromotionGroupRepository.GetOne(
            StoreContext.Culture, StoreContext.CurrentStore.StoreID, PromotionGroupID, BoolFilter.ShowTrue );

        if (!group.IsEnabled)
            Response.Redirect( "~/Mobile/default.aspx" );
    }

    #endregion

    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
        RegisterStoreEvents();

        if (!IsPostBack)
        {
            IsPromotionAvailable();
            uxItem.PromotionGroupID = PromotionGroupID;
        }

        DynamicPageElement element = new DynamicPageElement( this );
        element.SetUpTitleAndMetaTags( "Promotion - " + PromotionGroupName.Replace( '_', ' ' ), NamedConfig.SiteDescription );
    }

    #endregion
}
