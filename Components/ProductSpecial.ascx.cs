using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.WebUI;
using Vevo.WebUI.International;

public partial class Components_ProductSpecial : BaseLanguageUserControl
{
    private void PopulateControls()
    {
        if (DataAccessContext.Configurations.GetBoolValue( "TodaySpecialModuleDisplay" ))
        {
            IList<Product> products = DataAccessContext.ProductRepository.GetAllByTodaySpecial(
                StoreContext.Culture,
                DataAccessContext.Configurations.GetIntValue( "OutOfStockValue" ),
                DataAccessContext.Configurations.GetBoolValue( "UseStockControl" ),
                new StoreRetriever().GetCurrentStoreID(),
                DataAccessContext.Configurations.GetValue( "RootCategory", new StoreRetriever().GetStore() ) );

            uxRepeater.DataSource = products;
            uxRepeater.DataBind();
        }
        else
            this.Visible = false;
    }

    public string GetUrl( object id, object url )
    {
        return Page.ResolveUrl( UrlManager.GetProductUrl( id.ToString(), url.ToString() ) );
    }

    public string GetImageUrl( object productID )
    {
        string imageUrl = "";
        Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, ( string ) productID, new StoreRetriever().GetCurrentStoreID() );
        ProductImage image = product.GetPrimaryProductImage();

        if ( String.IsNullOrEmpty( image.RegularImage ) || image.RegularImage == "~/" )
        {
            imageUrl = "~/" + DataAccessContext.Configurations.GetValue( "DefaultImageUrl" );
        }
        else
        {
            imageUrl = "~/" + image.RegularImage;
        }

        return imageUrl;
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        RegisterScriptaculousJavaScript();

        GetStorefrontEvents().StoreCultureChanged +=
            new StorefrontEvents.CultureEventHandler( ProductSpecial_StoreCultureChanged );
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (DataAccessContext.Configurations.GetBoolValue( "TodaySpecialModuleDisplay" ))
        {
            PopulateControls();
        }
        else
        {
            this.Visible = false;
        }
    }

    private void RegisterScriptaculousJavaScript()
    {
        String csname = "jqeuryCycleBlock";
        ClientScriptManager cs = Page.ClientScript;

        StringBuilder sb = new StringBuilder();
        sb.AppendLine( "$(document).ready(function() {" );
        sb.AppendLine( "$('.ProductSpecialImage').cycle({" );
        sb.AppendLine( String.Format( "fx:     '{0}',", DataAccessContext.Configurations.GetValue( "ProductSpecialEffectMode" ) ) );
        sb.AppendLine( String.Format( "speed:   {0},", DataAccessContext.Configurations.GetIntValue( "ProductSpecialTransitionSpeed" ) ) );
        sb.AppendLine( String.Format( "timeout: {0},", (DataAccessContext.Configurations.GetDoubleValue( "ProductSpecialEffectWaitTime" ) * 1000) ) );
        sb.AppendLine( String.Format( "pause: {0} ", DataAccessContext.Configurations.GetIntValue( "ProductSpecialEffectTimeout" ) ) );
        sb.AppendLine( "});" );
        sb.AppendLine( "});" );

        if (!cs.IsClientScriptBlockRegistered( this.GetType(), csname ))
        {
            cs.RegisterClientScriptBlock( this.GetType(), csname, sb.ToString(), true );
        }
    }

    private void ProductSpecial_StoreCultureChanged( object sender, CultureEventArgs e )
    {
        PopulateControls();
    }
}
