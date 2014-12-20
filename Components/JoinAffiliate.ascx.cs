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
using Vevo.Domain;
using Vevo.WebAppLib;

public partial class Components_JoinAffiliate : Vevo.WebUI.International.BaseLanguageUserControl
{
    protected void Page_Load( object sender, EventArgs e )
    {
        this.Visible = DataAccessContext.Configurations.GetBoolValue( "AffiliateEnabled" );
    }

    protected void uxJoinAffiliateImageButton_Click( object sender, ImageClickEventArgs e )
    {
        Response.Redirect( "AffiliateDashboard.aspx" );
    }
}
