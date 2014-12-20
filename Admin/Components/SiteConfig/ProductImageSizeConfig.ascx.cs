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
using Vevo.DataAccessLib;
using Vevo.Domain;
using Vevo.Domain.Configurations;
using Vevo.WebAppLib;

public partial class AdminAdvanced_Components_SiteConfig_ProductImageSizeConfig : System.Web.UI.UserControl
{
    public string ValidationGroup
    {
        get
        {
            if (ViewState["ValidationGroup"] == null)
                return string.Empty;
            else
                return (string) ViewState["ValidationGroup"];
        }
        set
        {
            ViewState["ValidationGroup"] = value;
        }
    }

    public void PopulateControls()
    {
        uxRegularImageWidthText.Text = DataAccessContext.Configurations.GetValue( "RegularImageWidth" ).ToString();
        uxSecondaryImageWidthText.Text = DataAccessContext.Configurations.GetValue( "SecondaryImageWidth" ).ToString();
    }

    public void Update()
    {
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["RegularImageWidth"],
            uxRegularImageWidthText.Text );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["SecondaryImageWidth"],
            uxSecondaryImageWidthText.Text );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxRegularImageWidthCompare.ValidationGroup = ValidationGroup;
        uxSecondaryImageWidthCompare.ValidationGroup = ValidationGroup;
    }
}
