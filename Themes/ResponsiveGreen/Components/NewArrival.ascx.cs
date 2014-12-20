using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.WebUI;
using Vevo.WebUI.Products;

public partial class Themes_ResponsiveGreen_Components_NewArrival : BaseProductUserControl
{
    private void RegisterHeader()
    {
        HtmlLink cssLink = new HtmlLink();
        cssLink.Href = "~/ClientScripts/jCarousel/skins/tango/skin.css";
        cssLink.Attributes.Add( "rel", "stylesheet" );
        cssLink.Attributes.Add( "type", "text/css" );
        Page.Header.Controls.Add( cssLink );

        ScriptManager.RegisterClientScriptInclude(
            this, this.GetType(), "jCarousel", ResolveClientUrl( "~/ClientScripts/jCarousel/lib/jquery.jcarousel.min.js" ) );
    }
    private void RegisterEvents()
    {
        GetStorefrontEvents().StoreCultureChanged +=
            new StorefrontEvents.CultureEventHandler( NewArrivalProduct_StoreCultureChanged );
        GetStorefrontEvents().StoreCurrencyChanged +=
            new StorefrontEvents.CurrencyEventHandler( NewArrivalProduct_StoreCurrencyChanged );
    }

    private void RegisterScripts()
    {
        StringBuilder sb = new StringBuilder();

        sb.Append( "function mycarousel_initCallback(carousel) {" );
        sb.Append( "    jQuery('#" + uxNextPageButton.ClientID + "').bind('click', function() {" );
        sb.Append( "    	carousel.next();" );
        sb.Append( "    	return false;" );
        sb.Append( "    });" );
        sb.Append( "    jQuery('#" + uxPreviousPageButton.ClientID + "').bind('click', function() {" );
        sb.Append( "    	carousel.prev();" );
        sb.Append( "    	return false;" );
        sb.Append( "    });" );
        sb.Append( "};" );

        sb.Append( "jQuery(document).ready(function() {" );
        sb.Append( "    var item = 1;" );
        sb.Append( "    jQuery('#" + uxNewArrialCarousel.ClientID + "').jcarousel({" );        
        sb.Append( "         scroll:     item ," );
        sb.Append( "    	wrap: 'circular'," );
        sb.Append( "    	initCallback: mycarousel_initCallback," );
        sb.Append( "    	buttonNextHTML: null," );
        sb.Append( "    	buttonPrevHTML: null" );
        sb.Append( "    });" );
        sb.Append( "});" );
        sb.AppendLine();
        
        ScriptManager.RegisterClientScriptBlock( this, this.GetType(), "NewArrivalCarousel", sb.ToString(), true );
    }

    private void PopulateControls()
    {
        int productDisplayLimit = DataAccessContext.Configurations.GetIntValue( "MaximumDisplayProductNewArrival", StoreContext.CurrentStore );
        int howManyItems;
        string rootID = DataAccessContext.Configurations.GetValueNoThrow("RootCategory", StoreContext.CurrentStore);

        IList<Product> productList = DataAccessContext.ProductRepository.GetProductNewArrival( 
            StoreContext.Culture, 
            StoreContext.CurrentStore.StoreID,
            productDisplayLimit, 
            "CreateDate", 
            BoolFilter.ShowTrue, 
            0,
            productDisplayLimit - 1,
            rootID,
            out howManyItems );

        if ( productList.Count == 0 )
        {
            uxNewArrivalPanel.Visible = false;
            return;
        }

        uxNewArrialRepeater.DataSource = productList;
        uxNewArrialRepeater.DataBind();
    }

    private void NewArrivalProduct_StoreCultureChanged( object sender, CultureEventArgs e )
    {
        PopulateControls();
    }

    private void NewArrivalProduct_StoreCurrencyChanged( object sender, CurrencyEventArgs e )
    {
        PopulateControls();
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if ( !DataAccessContext.Configurations.GetBoolValue( "EnableNewArrivalProduct", StoreContext.CurrentStore ) )
        {
            uxNewArrivalPanel.Visible = false;
            return;
        }

        RegisterHeader();
        RegisterEvents();
        RegisterScripts();
        
        if ( !IsPostBack )
        {
            PopulateControls();
        }
    }
}