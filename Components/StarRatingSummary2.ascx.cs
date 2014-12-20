using System;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;

public partial class Components_StarRatingSummary2 : Vevo.WebUI.International.BaseLanguageUserControl
{
    private void PopulateControls()
    {
        if (!DataAccessContext.Configurations.GetBoolValue( "CustomerRating" ) &&
                !DataAccessContext.Configurations.GetBoolValue( "CustomerReview" ))
        {
            uxAddReviewButton.Visible = false;
        }

        if (!DataAccessContext.Configurations.GetBoolValue( "CustomerRating" ) &&
            !DataAccessContext.Configurations.GetBoolValue( "CustomerReview" ) &&
            !DataAccessContext.Configurations.GetBoolValue( "MerchantRating" ))
        {
            this.Visible = false;
        }
        else
        {
            this.Visible = true;
        }
        string numberOfRatingText = CustomerReviewAccess.GetNumberOfRating(ProductID);
        int numberOfReviews = Convert.ToInt32(numberOfRatingText);

        if (numberOfReviews > 0)
        {
     

            uxBaseOnAmount.Text = numberOfRatingText;
        }

    }

    protected void Page_Load( object sender, EventArgs e )
    {
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!IsPostBack)
        {
            PopulateControls();
        }
    }

    protected void uxAddReviewButton_Command( object sender, CommandEventArgs e )
    {
        if (Page.User.Identity.IsAuthenticated &&
            (!Roles.IsUserInRole( Page.User.Identity.Name, "Customers" )))
            FormsAuthentication.SignOut();

        string productID = e.CommandArgument.ToString();
        Response.Redirect( productID );
    }

    public string ProductID
    {
        get
        {
            return (string) ViewState["ProductID"];
        }
        set
        {
            ViewState["ProductID"] = value;
        }
    }

}
