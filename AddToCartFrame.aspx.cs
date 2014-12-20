using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddToCartFrame : Page
{
    private string ProductID
    {
        get
        {
            if (String.IsNullOrEmpty( Request.QueryString["ProductID"] ))
                return "0";
            else
                return Request.QueryString["ProductID"];
        }
    }
    
    protected void Page_Load( object sender, EventArgs e )
    {

    }

    protected void uxAddToCartButton_Click( object sender, EventArgs e )
    {
        ClientScript.RegisterStartupScript( 
            this.GetType(), 
            "scriptid", 
            "window.parent.location.href='" + "AddtoCart.aspx?ProductID=" + ProductID + "'", 
            true );
    }

    public override string StyleSheetTheme
    {
        get
        {
            return String.Empty;
        }
    }
}
