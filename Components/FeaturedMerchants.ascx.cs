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

public partial class Components_FeaturedMerchants : Vevo.WebUI.International.BaseLanguageUserControl
{
    private void PopulateControls()
    {
        int count = DataAccessContext.Configurations.GetIntValue( "FeaturedMerchantCount" );

        if (count >= 1)
        {
            uxHyperLink1.NavigateUrl = Page.ResolveUrl( DataAccessContext.Configurations.GetValue( "FeaturedMerchantUrl1" ) );
            uxImage1.ImageUrl = "~/" + DataAccessContext.Configurations.GetValue( "FeaturedMerchantImage1" );
            uxImage1.MaximumWidth = new Unit( SystemConst.MaximumWidthRightSidebarImage );
        }
        else
        {
            uxHyperLink1.Visible = false;
        }

        if (count >= 2)
        {
            uxHyperLink2.NavigateUrl = Page.ResolveUrl( DataAccessContext.Configurations.GetValue( "FeaturedMerchantUrl2" ) );
            uxImage2.ImageUrl = "~/" + DataAccessContext.Configurations.GetValue( "FeaturedMerchantImage2" );
            uxImage2.MaximumWidth = new Unit( SystemConst.MaximumWidthRightSidebarImage );
        }
        else
        {
            uxHyperLink2.Visible = false;
        }

        if (count >= 3)
        {
            uxHyperLink3.NavigateUrl = Page.ResolveUrl( DataAccessContext.Configurations.GetValue( "FeaturedMerchantUrl3" ) );
            uxImage3.ImageUrl = "~/" + DataAccessContext.Configurations.GetValue( "FeaturedMerchantImage3" );
            uxImage3.MaximumWidth = new Unit( SystemConst.MaximumWidthRightSidebarImage );
        }
        else
        {
            uxHyperLink3.Visible = false;
        }
    }


    protected void Page_Load( object sender, EventArgs e )
    {
        if (DataAccessContext.Configurations.GetBoolValue( "FeaturedMerchantModuleDisplay" ))
        {
            PopulateControls();
        }
        else
        {
            this.Visible = false;
        }
    }
}
