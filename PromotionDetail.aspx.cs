using System;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Marketing;
using Vevo.WebUI;
using Vevo.WebUI.International;
using Vevo.Deluxe.Domain;
using Vevo.Deluxe.Domain.BundlePromotion;

public partial class PromotionDetail : Vevo.Deluxe.WebUI.Base.BaseDeluxeLanguagePage
{
    #region Private

    private string CurrentPromotionGroupID
    {
        get
        {
            if ( ViewState[ "CurrentPromotionGroupID" ] == null )
                return "0";
            else
                return ( string ) ViewState[ "CurrentPromotionGroupID" ];
        }
        set
        {
            ViewState[ "CurrentPromotionGroupID" ] = value;
        }
    }

    private string PromotionGroupID
    {
        get
        {
            if ( !String.IsNullOrEmpty( Request.QueryString[ "PromoID" ] ) )
                return Request.QueryString[ "PromoID" ];
            else
                return DataAccessContextDeluxe.PromotionGroupRepository.GetPromotionGroupIDFromUrlName( PromotionGroupName );
        }
    }

    private string PromotionGroupName
    {
        get
        {
            if ( Request.QueryString[ "PromoName" ] == null )
                return String.Empty;
            else
                return Request.QueryString[ "PromoName" ];
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
        PromotionGroup promotionGroup = DataAccessContextDeluxe.PromotionGroupRepository.GetOne(
            StoreContext.Culture, StoreContext.CurrentStore.StoreID, CurrentPromotionGroupID, BoolFilter.ShowTrue );

        if ( !String.IsNullOrEmpty( promotionGroup.UrlName ) )
        {
            Response.Redirect( UrlManager.GetPromotionUrl( CurrentPromotionGroupID, promotionGroup.UrlName ) );
        }
        else
        {
            Response.Redirect( "~/Error404.aspx" );
        }
    }

    private void PromotionDetail_StoreCurrencyChanged( object sender, CurrencyEventArgs e )
    {
        uxItem.Reload();
    }

    private void IsPromotionAvailable()
    {
        PromotionGroup group = DataAccessContextDeluxe.PromotionGroupRepository.GetOne( 
            StoreContext.Culture, StoreContext.CurrentStore.StoreID, PromotionGroupID, BoolFilter.ShowTrue );
        
        if ( !group.IsEnabled || group.IsNull == true)
            Response.Redirect( "~/Error404.aspx" );
    }

    #endregion

    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
        RegisterStoreEvents();

        if ( !IsPostBack )
        {
            IsPromotionAvailable();
            uxItem.PromotionGroupID = PromotionGroupID;
            CurrentPromotionGroupID = PromotionGroupID;
        }

        DynamicPageElement element = new DynamicPageElement( this );
        element.SetUpTitleAndMetaTags( "Promotion - " + PromotionGroupName.Replace('_', ' '), NamedConfig.SiteDescription );
    }

    #endregion
}
