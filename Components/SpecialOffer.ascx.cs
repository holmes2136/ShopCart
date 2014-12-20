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
using Vevo.Domain;
using Vevo.WebAppLib;


public partial class Components_SpecialOffer : Vevo.WebUI.BaseControls.BaseUserControl
{
    private void PopulateControls()
    {
        uxImage.ImageUrl = "~/" + DataAccessContext.Configurations.GetValue( "SpecialOfferImage" );
        uxImage.MaximumWidth = new Unit( SystemConst.MaximumWidthLeftSidebarImage );
    }


    protected void Page_Load( object sender, EventArgs e )
    {
        if (!IsPostBack &&
            DataAccessContext.Configurations.GetBoolValue( "SpecialOfferModuleDisplay" ))
        {
            PopulateControls();
        }
        else
        {
            this.Visible = false;
        }
    }
}
