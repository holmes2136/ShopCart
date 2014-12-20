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

public partial class Components_FreeShippingAd : Vevo.WebUI.BaseControls.BaseUserControl
{
    private void PopulateControls()
    {
        uxImage.ImageUrl = "~/" + DataAccessContext.Configurations.GetValue( "FreeShippingImage" );
        uxImage.MaximumWidth = new Unit( SystemConst.MaximumWidthRightSidebarImage );
    }


    protected void Page_Load( object sender, EventArgs e )
    {
        if (DataAccessContext.Configurations.GetBoolValue( "FreeShippingModuleDisplay" ))
        {
            PopulateControls();
        }
        else
        {
            this.Visible = false;
        }
    }
}
