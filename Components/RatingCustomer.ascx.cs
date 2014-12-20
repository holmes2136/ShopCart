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
using Vevo.DataAccessLib.Cart;
using Vevo.DataAccessLib;
using Vevo.WebAppLib;
using System.Drawing;

public partial class Components_RatingCustomer : Vevo.WebUI.International.BaseLanguageUserControl
{
    private void PopulateControls()
    {
        string numberOfRatingText = CustomerReviewAccess.GetNumberOfRating( ProductID );
        int numberOfReviews = Convert.ToInt32( numberOfRatingText );

        if (numberOfReviews > 0)
        {
            uxReviewPlaceHolder.Visible = true;
            uxNoReviewPlaceHolder.Visible = false;

            uxRatingControl.CurrentRating = CustomerReviewAccess.GetAverageRating( ProductID );

            uxBaseOnAmount.Text = numberOfRatingText;
        }
        else
        {
            uxReviewPlaceHolder.Visible = false;
            uxNoReviewPlaceHolder.Visible = true;
        }
    }


    protected void Page_PreRender( object sender, EventArgs e )
    {
        // Do not check if (!IsPostback) since the parent's DataBing() function may be
        // the one who trigger the event.
        PopulateControls();
    }


    public string ProductID
    {
        get
        {
            if (ViewState["ProductID"] == null)
                ViewState["ProductID"] = "0";
            return ViewState["ProductID"].ToString();
        }
        set
        {
            ViewState["ProductID"] = value;
        }
    }

}

