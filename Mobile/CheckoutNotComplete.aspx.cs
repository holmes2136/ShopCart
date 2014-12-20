using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.WebAppLib;

public partial class Mobile_CheckoutNotComplete : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    protected void Page_Load( object sender, EventArgs e )
    {
        if (!IsPostBack)
        {
            uxHeadLiteral.Text = CheckoutNotCompletePage.TitlePage;
            uxDescriptionLiteral.Text = WebUtilities.ReplaceNewLine( CheckoutNotCompletePage.DescriptionPage );
            uxGotoPageLink.Text = CheckoutNotCompletePage.LinkName;
            uxGotoPageLink.NavigateUrl = CheckoutNotCompletePage.Link;
        }
    }
}
