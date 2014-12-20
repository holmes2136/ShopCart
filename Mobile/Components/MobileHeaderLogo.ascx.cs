using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo.Domain;

public partial class Mobile_Components_MobileHeaderLogo : Vevo.WebUI.International.BaseLanguageUserControl
{
    private void SetUpImage()
    {
        if ( ImageUrl != null )
        {
            uxMobileLogoImage.ImageUrl = "~/" + ImageUrl;
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {

    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if ( !String.IsNullOrEmpty( ImageUrl ) &&
            ImageUrl != "~/" )
        {
            uxMobileLogoImage.Visible = true;
            SetUpImage();
        }
        else
        {
            uxMobileLogoImage.ImageUrl = "~/" + DataAccessContext.Configurations.GetValue( "DefaultImageUrl" );
            uxMobileLogoImage.Visible = false;
        }
    }

    public string ImageUrl
    {
        get
        {
            string imagePath = DataAccessContext.Configurations.GetValue( "LogoImage" );
            if ( imagePath != string.Empty )
                return imagePath;
            else
                return string.Empty;
        }
    }
}
