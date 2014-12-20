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
using Vevo.Domain;
using Vevo.WebAppLib;

public partial class AffiliateRegisterFinish : Vevo.Deluxe.WebUI.Base.BaseDeluxeLanguagePage
{
    private void PopulateMessage()
    {
        string message = String.Empty;
        if (DataAccessContext.Configurations.GetBoolValue( "AffiliateAutoApprove" ))
        {
            message = "[$FinishWithAutoApprove]";
            uxAffilateLinkPanel.Visible = true;
        }
        else
        {
            message = "[$FinishWithManualApprove]";
            uxAffilateLinkPanel.Visible = false;
        }
        uxMessage.Text = message;
    }

    protected void Page_Load( object sender, EventArgs e )
    {

    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateMessage();
    }

}
