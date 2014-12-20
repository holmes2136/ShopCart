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
using Vevo.WebUI.International;
using Vevo.Domain;
using Vevo.WebUI;

public partial class Register : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    private bool IsCheckout
    {
        get
        {
            return Request.QueryString["Checkout"] != null;
        }
    }

    protected void Page_PreInit( object sender, EventArgs e )
    {
        string theme = DataAccessContext.Configurations.GetValue( "StoreTheme", StoreContext.CurrentStore );

        if (IsCheckout)
        {
            this.Page.MasterPageFile = "~/Themes/" + theme + "/Checkout.master";
        }
        else
        {
            this.Page.MasterPageFile = "~/Themes/" + theme + "/1Column.Master";
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (IsCheckout)
        {
            uxCheckoutIndicator.StepID = 1;
            uxRegisterDiv.Attributes.Add("class", "Checkout");
            uxRegisterTitle.Attributes.Add("class", "SidebarTopTitle");
        }
        else
        {
            uxRegisterDiv.Attributes.Add("class", "Register");
            uxRegisterTitle.Attributes.Add("class", "CommonPageTopTitle");
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!String.IsNullOrEmpty( uxCustomerRegister.GetMessage() ))
            uxMessage.DisplayError( uxCustomerRegister.GetMessage() );
    }
}
