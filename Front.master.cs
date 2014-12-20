using System;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.Security;
using Vevo.Shared.Utilities;
using Vevo.Shared.WebUI;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.Deluxe.WebUI.Marketing;

public partial class Front : Vevo.WebUI.BaseControls.BaseMasterPage
{
    protected void Page_Load( object sender, EventArgs e )
    {
        AffiliateHelper.SetAffiliateCookie( AffiliateCode );
    }

}
