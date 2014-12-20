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
public partial class AdminAdvanced_MainControls_DataFeedList : AdminAdvancedBaseUserControl
{
    protected void Page_Load( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            uxDataFeedPriceGrabber.Visible = false;
            uxDataFeedShoppingDotCom.Visible = false;
            uxDataFeedYahooShopping.Visible = false;
            uxDataFeedAmazon.Visible = false;
            uxDataFeedShopzilla.Visible = false;
        }
    }

    protected void uxDataFeedDrop_OnSelectedIndexChanged( object sender, EventArgs e )
    {
        uxDataFeedGoogle.Visible = false;
        uxDataFeedPriceGrabber.Visible = false;
        uxDataFeedShoppingDotCom.Visible = false;
        uxDataFeedYahooShopping.Visible = false;
        uxDataFeedAmazon.Visible = false;
        uxDataFeedShopzilla.Visible = false;

        switch (uxDataFeedDrop.SelectedValue)
        {
            case "GoogleFeed":
                uxDataFeedGoogle.Visible = true;
                uxDataFeedGoogle.PopulateControl();
                break;

            case "PriceGrabber":
                uxDataFeedPriceGrabber.Visible = true;
                uxDataFeedPriceGrabber.PopulateControl();
                break;

            case "ShoppingDotCom":
                uxDataFeedShoppingDotCom.Visible = true;
                uxDataFeedShoppingDotCom.PopulateControl();
                break;

            case "YahooShopping":
                uxDataFeedYahooShopping.Visible = true;
                uxDataFeedYahooShopping.PopulateControl();
                break;

            case "Amazon":
                uxDataFeedAmazon.Visible = true;
                uxDataFeedAmazon.PopulateControl();
                break;

            case "Shopzilla":
                uxDataFeedShopzilla.Visible = true;
                uxDataFeedShopzilla.PopulateControl();
                break;

            default:
                uxDataFeedGoogle.Visible = true;
                uxDataFeedGoogle.PopulateControl();
                break;
        }
    }
}
