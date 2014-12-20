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
using Vevo.Shared.Utilities;

public partial class CouponAndGift : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    private bool IsSummaryPage
    {
        get
        {
            if (Request.QueryString["IsSummaryPage"] != null)
                return ConvertUtilities.ToBoolean( Request.QueryString["IsSummaryPage"] );
            else
                return false;
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {

    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        uxGiftCouponDetail.PopulateData();
    }

    protected void uxApplyImageButton_Click( object sender,EventArgs e )
    {
        if (uxGiftCouponDetail.ValidateAndSetUp())
        {
            if (IsSummaryPage)
                Response.Redirect( "OrderSummary.aspx" );
            else
                Response.Redirect( "CheckOut.aspx" );
        }
    }
}
