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
using Vevo.WebAppLib;
using Vevo.DataAccessLib;
using Vevo.Domain;


public partial class AdminAdvanced_Components_SiteSetup_DomainKey : System.Web.UI.UserControl
{
    public void PopulateControl()
    {
        try
        {
            uxDomainKeyRequestText.Text = DataAccessContext.Configurations.GetValue( "DomainRegistrationKey" );
        }
        catch (Exception ex)
        {
            SaveLogFile.SaveLog( ex );
            HttpRuntime.UnloadAppDomain();
        }
    }

    public void Update()
    {
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["DomainRegistrationKey"],
            uxDomainKeyRequestText.Text );
    }

    protected void Page_Load( object sender, EventArgs e )
    {

    }
}
