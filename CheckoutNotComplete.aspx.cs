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
using Vevo.WebAppLib;

public partial class CheckoutNotComplete : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    protected void Page_Load(object sender, EventArgs e)
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
