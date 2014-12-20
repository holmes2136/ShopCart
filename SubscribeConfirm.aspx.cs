using System;
using System.Web;
using System.Web.UI;
using Vevo.WebUI.International;

public partial class SubscribeConfirm : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    private string Email
    {
        get
        {
            return Request.QueryString["Email"];
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void uxSubmitButton_Click( object sender, EventArgs e )
    {
        try
        {
            Response.Redirect( "Newsletter.aspx?Email=" + HttpUtility.UrlEncode( Email ) + "&Act=confirm" );
        }
        catch (VevoException ex)
        {
            uxMessage.DisplayError("Error: " + ex.Message);
        }
    }
}
