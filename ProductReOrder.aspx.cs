using System;


public partial class ProductReOrder : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    private string CurrentOrderID
    {
        get
        {
            if (!String.IsNullOrEmpty( Request.QueryString["OrderID"] ))
                return Request.QueryString["OrderID"];
            else
                return "0";
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (CurrentOrderID.Equals( "0" ))
            return;
    }
}
