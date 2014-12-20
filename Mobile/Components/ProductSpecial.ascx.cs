using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using Vevo;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.Domain.Stores;

public partial class Mobile_Components_ProductSpecial : Vevo.WebUI.International.BaseLanguageUserControl
{
    private void PopulateControls()
    {
        IList<Product> products = DataAccessContext.ProductRepository.GetAllByTodaySpecial(
            StoreContext.Culture,
            DataAccessContext.Configurations.GetIntValue( "OutOfStockValue" ), 
            DataAccessContext.Configurations.GetBoolValue( "UseStockControl" ),
            new StoreRetriever().GetCurrentStoreID(),
            DataAccessContext.Configurations.GetValue( "RootCategory", new StoreRetriever().GetStore() ),false );


        uxRepeater.DataSource = products;
        uxRepeater.DataBind();
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
  
    public string GetImageUrl( object imgUrl )
    {
        return "~/" + imgUrl.ToString();
    }
    protected void Page_Load( object sender, EventArgs e )
    {
        RegisterScriptaculousJavaScript();
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
    protected string GetUrl( object productID, object urlName )
    {
        return Vevo.UrlManager.GetMobileProductUrl( productID, urlName );
    }
   
}