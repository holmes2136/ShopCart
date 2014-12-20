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
using Vevo.DataAccessLib;
using Vevo.Domain;
using Vevo.Domain.Configurations;
using Vevo.WebAppLib;

public partial class AdminAdvanced_Components_SiteConfig_UploadConfig : System.Web.UI.UserControl
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
        uxUploadExtension.Text = DataAccessContext.Configurations.GetValue( "UploadExtension" );
        uxUploadSize.Text = DataAccessContext.Configurations.GetValue( "UploadSize" );
    }

    public void Update()
    {
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["UploadExtension"],
            uxUploadExtension.Text );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["UploadSize"],
            uxUploadSize.Text );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        RequiredUploadExtension.ValidationGroup = ValidationGroup;
        RequiredUploadSize.ValidationGroup = ValidationGroup;
        uxUploadCompareValidator.ValidationGroup = ValidationGroup;
    }
}
